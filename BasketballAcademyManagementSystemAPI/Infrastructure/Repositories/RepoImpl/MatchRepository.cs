using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class MatchRepository : IMatchRepository
    {
        private readonly BamsDbContext _dbContext;

        public MatchRepository(BamsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Match>> GetMatchesByPlayerAsync(string userId, DateTime startDate, DateTime endDate)
        {
            var player = await _dbContext.Players.FindAsync(userId);
            if (player == null)
            {
                return new List<Match>();
            }
            else if (player.TeamId == null)
            {
                return new List<Match>();
            }

            return await _dbContext.Matches
                .Where(m => 
                    m.Status != MatchConstant.Status.CANCELED 
                    && m.MatchDate >= startDate && m.MatchDate <= endDate
                    && (m.HomeTeamId == player.TeamId || m.AwayTeamId == player.TeamId))
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Court)
                .ToListAsync();
        }

        public async Task<List<Match>> GetMatchesByManagerAsync(string managerId, DateTime startDate, DateTime endDate)
        {
            var manager = await _dbContext.Managers.FindAsync(managerId);
            if (manager == null)
            {
                return new List<Match>();
            }
            else if (manager.TeamId == null)
            {
                return new List<Match>();
            }

            return await _dbContext.Matches
                .Where(m =>
                    m.Status != MatchConstant.Status.CANCELED
                    && (m.MatchDate >= startDate && m.MatchDate <= endDate)
                    && (m.HomeTeamId == manager.TeamId || m.AwayTeamId == manager.TeamId))
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Court)
                .ToListAsync();
        }

        public async Task<List<Match>> GetMatchesByParentAsync(string parentId, DateTime startDate, DateTime endDate)
        {
            return await _dbContext.Matches
                .Where(m => m.MatchDate >= startDate && m.MatchDate <= endDate 
                    && m.Status != MatchConstant.Status.CANCELED
                    && m.MatchLineups.Any(ml => ml.Player.ParentId == parentId))
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Court)
                .ToListAsync();
        }

        public async Task<List<Match>> GetMatchesByCoachAsync(string coachId, DateTime startDate, DateTime endDate, string? teamId, string? courtId)
        {
            var coach = await _dbContext.Coaches.FindAsync(coachId);
            if (coach == null)
            {
                return new List<Match>();
            }
            else if (coach.TeamId == null)
            {
                return new List<Match>();
            }

            var query = _dbContext.Matches.AsQueryable();

            if (teamId != null && teamId != "all")
            {
                query = query.Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId);
            }
            else if (teamId == null)
            {
                if (coach != null)
                {
                    query = query.Where(m => m.HomeTeamId == coach.TeamId || m.AwayTeamId == coach.TeamId);
                }
            }

            if (courtId != null && courtId != "all")
            {
                query = query.Where(ts => ts.CourtId == courtId);
            }

            return await query
                .Where(m => 
                    m.Status != MatchConstant.Status.CANCELED
                    && (m.MatchDate >= startDate && m.MatchDate <= endDate))
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Court)
                .ToListAsync();
        }

        public async Task<List<Match>> GetAllMatchesAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbContext.Matches
                .Where(m => m.Status != MatchConstant.Status.CANCELED
                    && (m.MatchDate >= startDate && m.MatchDate <= endDate))
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Court)
                .ToListAsync();
        }

        public async Task<string> GetUserRoleAsync(string userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            return user?.RoleCode ?? string.Empty;
        }

        public async Task<Match?> GetMatchByIdAsync(int matchId)
        {
            return await _dbContext.Matches
                .Where(x => x.MatchId == matchId)
                .Include(x => x.HomeTeam)
                .Include(x => x.AwayTeam)
                .Include(x => x.Court)
                .Include(x => x.MatchArticles)
                .Include(x => x.MatchLineups)
                .FirstOrDefaultAsync();
        }

        public async Task AddMatchAsync(Match match)
        {
            await _dbContext.Matches.AddAsync(match);
            await _dbContext.SaveChangesAsync();
        }

        // Check court availability for create match
        public async Task<bool> IsCourtAvailableAsync(string courtId, DateTime startTime, DateTime endTime)
        {
            // Kiểm tra trạng thái court
            var court = await _dbContext.Courts
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CourtId == courtId);

            // Court không tồn tại hoặc status là đã xoá hoặc sân chỉ dành cho tập luyện -> unavailable
            if (court == null || court.Status == CourtConstant.Status.DELETED || court.UsagePurpose == CourtConstant.UsagePurpose.TRAINING)
            {
                return false;
            }

            // Kiểm tra xem có training session nào đang diễn ra tại sân này không
            var isTrainingSessionConflict = await _dbContext.TrainingSessions
                .AnyAsync(ts => ts.CourtId == courtId
                                && ts.ScheduledDate == DateOnly.FromDateTime(startTime)
                                && (ts.StartTime < TimeOnly.FromDateTime(endTime) && ts.EndTime > TimeOnly.FromDateTime(startTime))
                                && ts.Status != TrainingSessionConstant.Status.CANCELED);

            // Kiểm tra xem có trận đấu nào đang diễn ra tại sân này không
            var isMatchConflict = await _dbContext.Matches
                .AnyAsync(m => m.CourtId == courtId
                                && m.Status != MatchConstant.Status.CANCELED
                                && DateOnly.FromDateTime(m.MatchDate) == DateOnly.FromDateTime(startTime)
                                && ((m.MatchDate <= startTime && m.MatchDate.AddHours(1) > startTime) 
                                || (m.MatchDate < endTime && m.MatchDate.AddHours(1) >= endTime)));

            return !isMatchConflict && !isTrainingSessionConflict;
        }


        // Check court availability for update match
        public async Task<bool> IsCourtAvailableAsync(Match match, string courtId, DateTime startTime, DateTime endTime)
        {
            // Kiểm tra trạng thái court
            var court = await _dbContext.Courts
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CourtId == courtId);

            // Court không tồn tại hoặc status là đã xoá hoặc sân chỉ dành cho tập luyện -> unavailable
            if (court == null || court.Status == CourtConstant.Status.DELETED || court.UsagePurpose == CourtConstant.UsagePurpose.TRAINING)
            {
                return false;
            }

            // Kiểm tra xem có training session nào đang diễn ra tại sân này không
            var isTrainingSessionConflict = await _dbContext.TrainingSessions
                .AnyAsync(ts => ts.CourtId == courtId
                                && ts.ScheduledDate == DateOnly.FromDateTime(startTime)
                                && (ts.StartTime < TimeOnly.FromDateTime(endTime) && ts.EndTime > TimeOnly.FromDateTime(startTime))
                                && ts.Status != TrainingSessionConstant.Status.CANCELED);

            // Kiểm tra xem có trận đấu nào đang diễn ra tại sân này không
            var isMatchConflict = await _dbContext.Matches
                .AnyAsync(m => m.CourtId == courtId
                                && m.Status != MatchConstant.Status.CANCELED
                                && m.MatchId != match.MatchId
                                && DateOnly.FromDateTime(m.MatchDate) == DateOnly.FromDateTime(startTime)
                                && ((m.MatchDate <= startTime && m.MatchDate.AddHours(1) > startTime)
                                || (m.MatchDate < endTime && m.MatchDate.AddHours(1) >= endTime)));

            return !isMatchConflict && !isTrainingSessionConflict;
        }

        // Check team availability for create match
        public async Task<bool> IsTeamAvailableAsync(string teamId, DateTime startTime, DateTime endTime)
        {
            // Kiểm tra xem có trận đấu hoặc training session nào đang diễn ra tại team này không
            var isMatchConflict = await _dbContext.Matches.AnyAsync(m => (m.HomeTeamId == teamId || m.AwayTeamId == teamId)
                    && DateOnly.FromDateTime(m.MatchDate) == DateOnly.FromDateTime(startTime)
                    && m.Status != MatchConstant.Status.CANCELED
                    && ((m.MatchDate <= startTime && m.MatchDate.AddHours(1) > startTime)
                    || (m.MatchDate < endTime && m.MatchDate.AddHours(1) >= endTime)));

            // Kiểm tra xem có training session nào đang diễn ra tại team này không
            var isTrainingSessionConflict = await _dbContext.TrainingSessions.AnyAsync(ts => ts.TeamId == teamId
                    && ts.ScheduledDate == DateOnly.FromDateTime(startTime)
                    && ts.Status != TrainingSessionConstant.Status.CANCELED
                    && ((ts.StartTime <= TimeOnly.FromDateTime(startTime) && ts.EndTime > TimeOnly.FromDateTime(startTime))
                    || (ts.StartTime < TimeOnly.FromDateTime(endTime) && ts.EndTime >= TimeOnly.FromDateTime(endTime))));

            return !isMatchConflict && !isTrainingSessionConflict;
        }

        // Check team availability for update match
        public async Task<bool> IsTeamAvailableAsync(Match match, string teamId, DateTime startTime, DateTime endTime)
        {
            // Kiểm tra xem có trận đấu hoặc training session nào đang diễn ra tại team này không
            var isMatchConflict = await _dbContext.Matches.AnyAsync(m => (m.HomeTeamId == teamId || m.AwayTeamId == teamId)
                    && DateOnly.FromDateTime(m.MatchDate) == DateOnly.FromDateTime(startTime)
                    && m.Status != MatchConstant.Status.CANCELED
                    && m.MatchId != match.MatchId
                    && ((m.MatchDate <= startTime && m.MatchDate.AddHours(1) > startTime)
                    || (m.MatchDate < endTime && m.MatchDate.AddHours(1) >= endTime)));

            // Kiểm tra xem có training session nào đang diễn ra tại team này không
            var isTrainingSessionConflict = await _dbContext.TrainingSessions.AnyAsync(ts => ts.TeamId == teamId
                    && ts.ScheduledDate == DateOnly.FromDateTime(startTime)
                    && ts.Status != TrainingSessionConstant.Status.CANCELED
                    && ((ts.StartTime <= TimeOnly.FromDateTime(startTime) && ts.EndTime > TimeOnly.FromDateTime(startTime))
                    || (ts.StartTime < TimeOnly.FromDateTime(endTime) && ts.EndTime >= TimeOnly.FromDateTime(endTime))));

            return !isMatchConflict && !isTrainingSessionConflict;
        }

        public async Task<bool> UpdateMatchAsync(Match match)
        {
            var entry = _dbContext.Matches.Update(match);
            if (entry.State == EntityState.Modified)
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Court>> GetAvailableCourtsForMatchInARangeOfTimeAsync(DateTime matchDate, DateTime endTime)
        {
            var availableCourts = await _dbContext.Courts
                .Where(c => c.UsagePurpose != CourtConstant.UsagePurpose.TRAINING
                            && c.Status != CourtConstant.Status.DELETED
                            && !_dbContext.TrainingSessions.Any(ts => ts.CourtId == c.CourtId
                                && ts.ScheduledDate == DateOnly.FromDateTime(matchDate)
                                && (ts.StartTime < TimeOnly.FromDateTime(endTime) && ts.EndTime > TimeOnly.FromDateTime(matchDate))
                                && ts.Status != TrainingSessionConstant.Status.CANCELED)
                            && !_dbContext.Matches.Any(m => m.CourtId == c.CourtId
                                && m.Status != MatchConstant.Status.CANCELED
                                && DateOnly.FromDateTime(m.MatchDate) == DateOnly.FromDateTime(matchDate)
                                && ((m.MatchDate <= matchDate && m.MatchDate.AddHours(1) > matchDate)
                                || (m.MatchDate < endTime && m.MatchDate.AddHours(1) >= endTime))))
                .ToListAsync();

            return availableCourts;
        }

        public async Task<IEnumerable<Match>> GetNonCanceledMatchesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Matches
                .Where(m => m.Status != MatchConstant.Status.CANCELED)
                .ToListAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
