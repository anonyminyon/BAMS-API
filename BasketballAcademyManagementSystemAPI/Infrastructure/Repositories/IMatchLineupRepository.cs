using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface IMatchLineupRepository
    {
        Task<MatchLineup?> GetMatchLineupByMatchIdAndPlayerIdAsync(int matchId, string playerId);
        Task<List<Player>> GetPlayersByMatchAndTeamAsync(int matchId, string teamId);
        Task<bool> RemovePlayerFromMatchLineupAsync(int matchId, string playerId);
        Task<bool> RemovePlayerFromMatchLineupAsync(MatchLineup matchLineup);
        Task<bool> AddPlayerToMatchLineupAsync(MatchLineup matchLineup);
        Task<int> CountStartingPlayersAsync(int matchId, string teamId);
        Task<Player?> GetPlayerByIdAsync(string playerId);
        Task<List<Player>> GetAvailablePlayersOfATeamInAMatchAsync(int matchId, string teamId);
    }
}
