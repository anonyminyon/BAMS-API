using Azure.Core;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class TrainingSessionRepository : ITrainingSessionRepository
    {
        private readonly BamsDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public TrainingSessionRepository(BamsDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<bool> IsCourtAvailableAsync(
            string courtId,
            DateOnly scheduledDate,
            TimeOnly startTime,
            TimeOnly endTime)
        {
            // Kiểm tra trạng thái court
            var court = await _dbContext.Courts
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CourtId == courtId);

            // Court không tồn tại hoặc status là đã xoá hoặc sân dành cho thi đấu -> unavailable
            if (court == null || court.Status == CourtConstant.Status.DELETED || court.UsagePurpose == CourtConstant.UsagePurpose.COMPETE)
            {
                return false;
            }

            // Kiểm tra xung đột lịch với training session khác
            bool isTrainingSessionConflict = await _dbContext.TrainingSessions
                .AnyAsync(ts => ts.CourtId == courtId
                                && ts.ScheduledDate == scheduledDate
                                && (ts.StartTime < endTime && ts.EndTime > startTime)
                                && ts.Status != TrainingSessionConstant.Status.CANCELED);

            // Kiểm tra xem có trận đấu nào đang diễn ra tại sân này không
            var matchSettingsSection = _configuration.GetSection(MatchConstant.Setting.MatchSettingsSection);
            var minimumHourDurationOfAMatch = Double.Parse(matchSettingsSection[MatchConstant.Setting.MinimumHourDurationOfAMatch] ?? "1");
            var scheduledStartTime = scheduledDate.ToDateTime(startTime);
            var scheduledEndTime = scheduledDate.ToDateTime(endTime);
            var isMatchConflict = await _dbContext.Matches
                .AnyAsync(m => m.CourtId == courtId
                                && m.Status != MatchConstant.Status.CANCELED
                                && DateOnly.FromDateTime(m.MatchDate) == scheduledDate
                                && ((m.MatchDate <= scheduledStartTime && m.MatchDate.AddHours(minimumHourDurationOfAMatch) > scheduledStartTime)
                                || (m.MatchDate < scheduledEndTime && m.MatchDate.AddHours(minimumHourDurationOfAMatch) >= scheduledEndTime)));

            return !isTrainingSessionConflict && !isMatchConflict;
        }

        public async Task<bool> IsCourtAvailableAsync(
            string trainingSessionId,
            string courtId,
            DateOnly scheduledDate,
            TimeOnly startTime,
            TimeOnly endTime)
        {
            // Kiểm tra trạng thái court
            var court = await _dbContext.Courts
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CourtId == courtId);

            // Court không tồn tại hoặc status là đã xoá hoặc sân dành cho thi đấu -> unavailable
            if (court == null || court.Status == CourtConstant.Status.DELETED || court.UsagePurpose == CourtConstant.UsagePurpose.COMPETE)
            {
                return false;
            }

            // Kiểm tra xung đột lịch
            bool isTrainingSessionConflict = await _dbContext.TrainingSessions
                .AnyAsync(ts => ts.CourtId == courtId
                                && ts.ScheduledDate == scheduledDate
                                && ts.TrainingSessionId != trainingSessionId
                                && (ts.StartTime < endTime && ts.EndTime > startTime)
                                && ts.Status != TrainingSessionConstant.Status.CANCELED);

            // Kiểm tra xem có trận đấu nào đang diễn ra tại sân này không
            var matchSettingsSection = _configuration.GetSection(MatchConstant.Setting.MatchSettingsSection);
            var minimumHourDurationOfAMatch = Double.Parse(matchSettingsSection[MatchConstant.Setting.MinimumHourDurationOfAMatch] ?? "1");
            var scheduledStartTime = scheduledDate.ToDateTime(startTime);
            var scheduledEndTime = scheduledDate.ToDateTime(endTime);
            var isMatchConflict = await _dbContext.Matches
                .AnyAsync(m => m.CourtId == courtId
                                && m.Status != MatchConstant.Status.CANCELED
                                && DateOnly.FromDateTime(m.MatchDate) == scheduledDate
                                && ((m.MatchDate <= scheduledStartTime && m.MatchDate.AddHours(minimumHourDurationOfAMatch) > scheduledStartTime)
                                || (m.MatchDate < scheduledEndTime && m.MatchDate.AddHours(minimumHourDurationOfAMatch) >= scheduledEndTime)));

            return !isTrainingSessionConflict && !isMatchConflict;
        }

        public async Task<TrainingSession?> GetConflictingTrainingSessionByCourtAndTimeAsync(string courtId
            , DateOnly scheduledDate
            , TimeOnly startTime
            , TimeOnly endTime)
        {
            return await _dbContext.TrainingSessions
                .Where(ts => ts.CourtId == courtId 
                    && ts.ScheduledDate == scheduledDate 
                    && ts.StartTime < endTime 
                    && ts.EndTime > startTime 
                    && ts.Status != TrainingSessionConstant.Status.CANCELED)
                .Include(ts => ts.Team)
                .FirstOrDefaultAsync();
        }

        public async Task<TrainingSession?> GetConflictingTrainingSessionOfTeamInDateTime(string teamId
            , DateOnly scheduledDate
            , TimeOnly startTime
            , TimeOnly endTime)
        {
            return await _dbContext.TrainingSessions
                .Where(ts => ts.TeamId == teamId 
                    && ts.ScheduledDate == scheduledDate 
                    && ((ts.StartTime < endTime && ts.EndTime > startTime))
                    && ts.Status != TrainingSessionConstant.Status.CANCELED)
                .Include(ts => ts.Team)
                .Include(ts => ts.Court)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> AddTrainingSessionAsync(TrainingSession trainingSession)
        {
            await _dbContext.TrainingSessions.AddAsync(trainingSession);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsUserCoachOfTeamAsync(string userId, string teamId)
        {
            return await _dbContext.Coaches.AnyAsync(c => c.UserId == userId && c.TeamId == teamId);
        }

        public async Task<bool> IsUserManagerOfTeamAsync(string userId, string teamId)
        {
            return await _dbContext.Managers.AnyAsync(c => c.UserId == userId && c.TeamId == teamId);
        }

        public async Task<bool> IsTeamAvailableAsync(string teamId, DateOnly scheduledDate, TimeOnly startTime, TimeOnly endTime)
        {
            // Kiểm tra xem có trận đấu hoặc training session nào đang diễn ra tại team này không
            var scheduledStartTime = scheduledDate.ToDateTime(startTime);
            var scheduledEndTime = scheduledDate.ToDateTime(endTime);
            var isMatchConflict = await _dbContext.Matches.AnyAsync(m => (m.HomeTeamId == teamId || m.AwayTeamId == teamId)
                    && DateOnly.FromDateTime(m.MatchDate) == scheduledDate
                    && m.Status != MatchConstant.Status.CANCELED
                    && ((m.MatchDate <= scheduledStartTime && m.MatchDate.AddHours(1) > scheduledStartTime)
                    || (m.MatchDate < scheduledEndTime && m.MatchDate.AddHours(1) >= scheduledEndTime)));

            var isTrainingSessionConflict = await _dbContext.TrainingSessions
                .AnyAsync(ts => ts.TeamId == teamId 
                    && ts.ScheduledDate == scheduledDate 
                    && ts.Status != TrainingSessionConstant.Status.CANCELED
                    && ((ts.StartTime < endTime && ts.EndTime > startTime)));

            return !isMatchConflict && !isTrainingSessionConflict;
        }

        public async Task<bool> IsTeamAvailableAsync(string trainingSessionId
            , string teamId
            , DateOnly scheduledDate
            , TimeOnly startTime
            , TimeOnly endTime)
        {
            return !await _dbContext.TrainingSessions
                .AnyAsync(ts => ts.TeamId == teamId 
                && ts.ScheduledDate == scheduledDate
                && ts.TrainingSessionId != trainingSessionId
                && ((ts.StartTime < endTime && ts.EndTime > startTime)));
        }

        public async Task<bool> IsValidTeamAsync(string teamId)
        {
            return await _dbContext.Teams.AnyAsync(t => t.TeamId == teamId);
        }

        public async Task<bool> IsValidCourtAsync(string courtId)
        {
            return await _dbContext.Courts.AnyAsync(c => c.CourtId == courtId);
        }

        public async Task<List<TrainingSession>> GetTrainingSessionsByManagerAsync(string managerId, DateTime startDate, DateTime endDate)
        {
            var manager = await _dbContext.Managers.FirstOrDefaultAsync(p => p.UserId == managerId);
            if (manager == null || manager.TeamId == null)
            {
                return new List<TrainingSession>();
            }

            return await _dbContext.TrainingSessions
                .Where(ts => ts.TeamId == manager.TeamId 
                    && ts.ScheduledDate >= DateOnly.FromDateTime(startDate) 
                    && ts.ScheduledDate <= DateOnly.FromDateTime(endDate)
                    && ts.Status == TrainingSessionConstant.Status.ACTIVE)
                .Include(ts => ts.Team)
                .Include(ts => ts.Court)
                .ToListAsync();
        }

        public async Task<List<TrainingSession>> GetTrainingSessionsByPlayerAsync(string playerId, DateTime startDate, DateTime endDate)
        {
            var player = await _dbContext.Players.FirstOrDefaultAsync(p => p.UserId == playerId);
            if (player == null || player.TeamId == null)
            {
                return new List<TrainingSession>();
            }

            return await _dbContext.TrainingSessions
                .Where(ts => ts.TeamId == player.TeamId
                    && ts.ScheduledDate >= DateOnly.FromDateTime(startDate)
                    && ts.ScheduledDate <= DateOnly.FromDateTime(endDate)
                    && ts.Status == TrainingSessionConstant.Status.ACTIVE)
                .Include(ts => ts.Team)
                .Include(ts => ts.Court)
                .ToListAsync();
        }

        public async Task<List<TrainingSession>> GetTrainingSessionsByParentAsync(string parentId, DateTime startDate, DateTime endDate)
        {
            var playerIds = await _dbContext.Players
                .Where(p => p.ParentId == parentId)
                .Select(p => p.TeamId)
                .ToListAsync();

            return await _dbContext.TrainingSessions
                .Where(ts => playerIds.Contains(ts.TeamId) 
                    && ts.ScheduledDate >= DateOnly.FromDateTime(startDate)
                    && ts.ScheduledDate <= DateOnly.FromDateTime(endDate)
                    && ts.Status == TrainingSessionConstant.Status.ACTIVE)
                .Include(ts => ts.Team).ThenInclude(t => t.Players).ThenInclude(p => p.User)
                .Include(ts => ts.Court)
                .Include(ts => ts.Attendances)
                .ToListAsync();
        }

        public async Task<List<TrainingSession>> GetTrainingSessionsByCoachAsync(string coachId
            , DateTime startDate
            , DateTime endDate
            , string? teamId
            , string? courtId)
        {
            var query = _dbContext.TrainingSessions.AsQueryable();

            if (teamId != null && teamId != "all")
            {
                query = query.Where(ts => ts.TeamId == teamId);
            }
            else if (teamId == null)
            {
                var coach = await _dbContext.Coaches.FirstOrDefaultAsync(c => c.UserId == coachId);
                if (coach != null)
                {
                    query = query.Where(ts => ts.TeamId == coach.TeamId);
                }
            }

            if (courtId != null && courtId != "all")
            {
                query = query.Where(ts => ts.CourtId == courtId);
            }

            return await query
                .Where(ts => ts.ScheduledDate >= DateOnly.FromDateTime(startDate) 
                    && ts.ScheduledDate <= DateOnly.FromDateTime(endDate)
                    && ts.Status == TrainingSessionConstant.Status.ACTIVE)
                .Include(ts => ts.Team)
                .Include(ts => ts.Court)
                .ToListAsync();
        }

        public async Task<List<TrainingSession>> GetAllTrainingSessionsAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbContext.TrainingSessions
                .Where(ts => ts.ScheduledDate >= DateOnly.FromDateTime(startDate) 
                    && ts.ScheduledDate <= DateOnly.FromDateTime(endDate)
                    && ts.Status == TrainingSessionConstant.Status.ACTIVE)
                .Include(ts => ts.Team)
                .Include(ts => ts.Court)
                .ToListAsync();
        }

        public async Task<string> GetUserRoleAsync(string userId)
        {
            var user = await _dbContext.Users.Include(u => u.Parent).FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                return string.Empty;
            }

            if (await _dbContext.Players.AnyAsync(p => p.UserId == userId))
            {
                return RoleCodeConstant.PlayerCode;
            }

            if (await _dbContext.Managers.AnyAsync(m => m.UserId == userId))
            {
                return RoleCodeConstant.ManagerCode;
            }

            if (user.Parent != null)
            {
                return RoleCodeConstant.ParentCode;
            }

            if (await _dbContext.Coaches.AnyAsync(c => c.UserId == userId))
            {
                return RoleCodeConstant.CoachCode;
            }

            if (await _dbContext.Presidents.AnyAsync(p => p.UserId == userId))
            {
                return RoleCodeConstant.PresidentCode;
            }

            return string.Empty;
        }

        public async Task<Attendance?> GetAttendanceByUserIdAndSessionIdAsync(string userId, string sessionId)
        {
            return await _dbContext.Attendances
                .FirstOrDefaultAsync(a => a.UserId == userId && a.TrainingSessionId == sessionId);
        }

        public async Task<bool> AnyAttendanceBySessionIdAsync(string sessionId)
        {
            return await _dbContext.Attendances
                .AnyAsync(a => a.TrainingSessionId == sessionId);
        }

        public async Task<TrainingSession?> GetTrainingSessionWithExcerciseBySessionIdAsync(string sessionId)
        {
            return await _dbContext.TrainingSessions
                .Include(ts => ts.Exercises)
                .FirstOrDefaultAsync(ts => ts.TrainingSessionId == sessionId);
        }

        public async Task<TrainingSession?> GetTrainingSessionDetailAsync(string trainingSessionId)
        {
            return await _dbContext.TrainingSessions
                .Include(ts => ts.Team)
                .Include(ts => ts.Court)
                .Include(ts => ts.Exercises)
                .ThenInclude(e => e.Coach).ThenInclude(c => c.User)
                .FirstOrDefaultAsync(ts => ts.TrainingSessionId == trainingSessionId);
        }

        public Task<TrainingSession?> GetTrainingSessionByIdAsync(string trainingSessionId)
        {
            return _dbContext.TrainingSessions
                .FirstOrDefaultAsync(ts => ts.TrainingSessionId == trainingSessionId);
        }

        public async Task<bool> UpdateTrainingSessionAsync(TrainingSession trainingSession)
        {
            _dbContext.TrainingSessions.Update(trainingSession);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Court>> GetAvailableCourtsAsync(List<DateOnly> scheduledDates, TimeOnly startTime, TimeOnly endTime)
        {
            var availableCourts = new List<Court>();

            var matchSettingsSection = _configuration.GetSection(MatchConstant.Setting.MatchSettingsSection);
            var minimumHourDurationOfAMatch = Double.Parse(matchSettingsSection[MatchConstant.Setting.MinimumHourDurationOfAMatch] ?? "1");

            foreach (var date in scheduledDates)
            {
                // Lấy danh sách TrainingSessions trùng thời gian
                var sessions = await _dbContext.TrainingSessions
                    .Where(ts => ts.ScheduledDate == date
                        && ts.StartTime < endTime
                        && ts.EndTime > startTime
                        && ts.Status != TrainingSessionConstant.Status.CANCELED)
                    .ToListAsync();

                // Lấy danh sách Matches trùng thời gian
                var matches = await _dbContext.Matches
                    .Where(m => m.Status != MatchConstant.Status.CANCELED
                        && (
                            (m.MatchDate <= date.ToDateTime(startTime) && m.MatchDate.AddHours(minimumHourDurationOfAMatch) > date.ToDateTime(startTime))
                            ||
                            (m.MatchDate < date.ToDateTime(endTime) && m.MatchDate.AddHours(minimumHourDurationOfAMatch) >= date.ToDateTime(endTime))
                        ))
                    .ToListAsync();

                // Lấy danh sách CourtId bị chiếm
                var occupiedCourtIds = sessions
                    .Select(ts => ts.CourtId)
                    .Concat(matches.Select(m => m.CourtId))
                    .Distinct()
                    .ToList();

                // Lọc danh sách sân khả dụng
                var courts = await _dbContext.Courts
                    .Where(c => !occupiedCourtIds.Contains(c.CourtId)
                        && c.Status == CourtConstant.Status.ACTIVE
                        && c.UsagePurpose != CourtConstant.UsagePurpose.COMPETE)
                    .ToListAsync();

                availableCourts.AddRange(courts);
            }

            return availableCourts.Distinct().ToList();
        }

        public async Task AddAsync(TrainingSession trainingSession)
        {
            await _dbContext.TrainingSessions.AddAsync(trainingSession);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<string>> GetTeamEmailsAsync(string teamId)
        {
            var coachEmails = await _dbContext.Coaches
                .Where(c => c.TeamId == teamId)
                .Select(c => c.User.Email)
                .ToListAsync();

            var playerEmails = await _dbContext.Players
                .Where(p => p.TeamId == teamId)
                .Select(p => p.User.Email)
                .ToListAsync();

            var parentEmails = await _dbContext.Players
                .Where(p => p.TeamId == teamId && p.ParentId != null)
                .Select(p => p.Parent.User.Email)
                .Distinct()
                .ToListAsync();

            var managerEmails = await _dbContext.Managers
                .Where(m => m.TeamId == teamId)
                .Select(m => m.User.Email)
                .ToListAsync();

            var allEmails = coachEmails
                .Union(playerEmails)
                .Union(parentEmails)
                .Union(managerEmails)
                .Distinct()
                .ToList();

            return allEmails;
        }

        public async Task<List<string>> GetTeamManagerEmailsAsync(string teamId)
        {
            return await _dbContext.Managers
                .Where(m => m.TeamId == teamId)
                .Select(m => m.User.Email)
                .ToListAsync();
        }

        public async Task<List<string>> GetTeamPlayerAndCoachUserIdAsync(string teamId)
        {
            var coachUserIds = await _dbContext.Coaches
                .Where(c => c.TeamId == teamId)
                .Select(c => c.User.UserId)
                .ToListAsync();

            var playerUserIds = await _dbContext.Players
                .Where(p => p.TeamId == teamId)
                .Select(p => p.User.UserId)
                .ToListAsync();

            var allTeamUserIds = coachUserIds
                .Union(playerUserIds)
                .Distinct()
                .ToList();

            return allTeamUserIds;
        }

        public async Task<bool> CreateRequestToChangeTrainingSessionStatus(TrainingSessionStatusChangeRequest request)
        {
            await _dbContext.TrainingSessionStatusChangeRequests.AddAsync(request);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsCancelTrainingSessionRequestExistsAsync(string trainingSessionId)
        {
            return await _dbContext.TrainingSessionStatusChangeRequests
                .AnyAsync(r => r.TrainingSessionId == trainingSessionId 
                && r.RequestType == TrainingSessionConstant.StatusChangeRequestType.CANCEL
                && r.Status == TrainingSessionConstant.StatusChangeRequestStatus.PENDING);
        }

        public async Task<bool> IsTrainingSessionHaveRequestExistsAsync(string trainingSessionId)
        {
            return await _dbContext.TrainingSessionStatusChangeRequests
                .AnyAsync(r => r.TrainingSessionId == trainingSessionId 
                    && r.Status == TrainingSessionConstant.StatusChangeRequestStatus.PENDING);
        }

        public async Task<TrainingSessionStatusChangeRequest?> GetSessionStatusChangeRequestByTrainingSessionIdAsync(string trainingSessionId)
        {
            return await _dbContext.TrainingSessionStatusChangeRequests
                .FirstOrDefaultAsync(r => r.TrainingSessionId == trainingSessionId
                    && r.Status == TrainingSessionConstant.StatusChangeRequestStatus.PENDING);
        }

        public async Task<bool> UpdateTrainingSessionStatusChangeRequestAsync(TrainingSessionStatusChangeRequest request)
        {
            _dbContext.TrainingSessionStatusChangeRequests.Update(request);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<TrainingSession>> GetPendingTrainingSessionOfATeamAsync(string teamId)
        {
            return await _dbContext.TrainingSessions
                .Where(ts => ts.TeamId == teamId && ts.Status == TrainingSessionConstant.Status.PENDING)
                .Include(ts => ts.Court)
                .ToListAsync();
        }

        public async Task<List<TrainingSession>> GetPendingTrainingSessionAsync()
        {
            return await _dbContext.TrainingSessions
                .Where(ts => ts.Status == TrainingSessionConstant.Status.PENDING)
                .Include(ts => ts.Court)
                .ToListAsync();
        }

        public Task<List<TrainingSessionStatusChangeRequest>> GetTrainingSessionPendingChangeRequestOfATeamAsync(string teamId, int requestType)
        {
            return _dbContext.TrainingSessionStatusChangeRequests
                .Where(r => r.TrainingSession.TeamId == teamId
                    && r.RequestType == requestType
                    && r.Status == TrainingSessionConstant.StatusChangeRequestStatus.PENDING)
                .Include(r => r.RequestedByCoach).ThenInclude(rqbc => rqbc.User)
                .Include(r => r.TrainingSession).ThenInclude(ts => ts.Court)
                .ToListAsync();
        }

        public Task<List<TrainingSessionStatusChangeRequest>> GetTrainingSessionPendingChangeRequestAsync()
        {
            return _dbContext.TrainingSessionStatusChangeRequests
                .Where(r => r.Status == TrainingSessionConstant.StatusChangeRequestStatus.PENDING)
                .Include(r => r.RequestedByCoach).ThenInclude(rqbc => rqbc.User)
                .Include(r => r.TrainingSession).ThenInclude(ts => ts.Court)
                .ToListAsync();
        }

        public async Task<List<Player>> GetPlayersOfParentAsync(string parentId)
        {
            return await _dbContext.Players
                .Where(p => p.ParentId == parentId)
                .Include(p => p.User)
                .ToListAsync();
        }

		//============ Training session của tuấn anh===========
		public async Task<TrainingSessionInforToParentEmailDto> GetTrainingSessionForParentEmailAsync(string trainingSessionId)
		{
			var trainingSession = await _dbContext.TrainingSessions
				.Where(ts => ts.TrainingSessionId == trainingSessionId)
				.Include(ts => ts.Court) // Bao gồm thông tin sân tập
				.FirstOrDefaultAsync();

			if (trainingSession == null)
			{
				return null; // Nếu không tìm thấy buổi tập
			}

			// Tạo DTO để trả về thông tin
			var sessionDetails = new TrainingSessionInforToParentEmailDto
			{
				TrainingSessionId = trainingSession.TrainingSessionId,
				TrainingDate = trainingSession.ScheduledDate,
				StartTime = trainingSession.StartTime,
				EndTime = trainingSession.EndTime,
				CourtName = trainingSession.Court.CourtName,
				CourtAddress = trainingSession.Court.Address
			};

			return sessionDetails;
		}

	}
}
