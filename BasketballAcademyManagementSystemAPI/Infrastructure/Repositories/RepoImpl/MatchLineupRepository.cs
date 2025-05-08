using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class MatchLineupRepository : IMatchLineupRepository
    {
        private readonly BamsDbContext _dbContext;

        public MatchLineupRepository(BamsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddPlayerToMatchLineupAsync(MatchLineup matchLineup)
        {
            await _dbContext.MatchLineups.AddAsync(matchLineup);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<int> CountStartingPlayersAsync(int matchId, string teamId)
        {
            return await _dbContext.MatchLineups
                .CountAsync(ml => ml.MatchId == matchId && ml.Player.TeamId == teamId && ml.IsStarting);
        }

        public async Task<MatchLineup?> GetMatchLineupByMatchIdAndPlayerIdAsync(int matchId, string playerId)
        {
            return await _dbContext.MatchLineups
                .Where(ml => ml.MatchId == matchId && ml.PlayerId == playerId)
                .Include(ml => ml.Player)
                .FirstOrDefaultAsync();
        }

        // Get players by match and team
        public async Task<List<Player>> GetPlayersByMatchAndTeamAsync(int matchId, string teamId)
        {
            return await _dbContext.MatchLineups
                .Where(ml => ml.MatchId == matchId && ml.Player.TeamId == teamId)
                .Include(ml => ml.Player)
                .ThenInclude(p => p.User)
                .Select(ml => ml.Player)
                .ToListAsync();
        }


        // Remove player from match lineup
        public async Task<bool> RemovePlayerFromMatchLineupAsync(int matchId, string playerId)
        {
            var matchLineup = await _dbContext.MatchLineups
                .FirstOrDefaultAsync(ml => ml.MatchId == matchId && ml.PlayerId == playerId);

            if (matchLineup == null)
            {
                return false;
            }

            _dbContext.MatchLineups.Remove(matchLineup);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemovePlayerFromMatchLineupAsync(MatchLineup matchLineup)
        {
            _dbContext.MatchLineups.Remove(matchLineup);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Player?> GetPlayerByIdAsync(string playerId)
        {
            return await _dbContext.Players.FindAsync(playerId);
        }

        public async Task<List<Player>> GetAvailablePlayersOfATeamInAMatchAsync(int matchId, string teamId)
        {
            var playersInLineup = await _dbContext.MatchLineups
                .Where(ml => ml.MatchId == matchId)
                .Select(ml => ml.PlayerId)
                .ToListAsync();

            return await _dbContext.Players
                .Where(p => p.TeamId == teamId && p.User.IsEnable && !playersInLineup.Contains(p.UserId))
                .Include(p => p.User)
                .ToListAsync();
        }
    }
}
