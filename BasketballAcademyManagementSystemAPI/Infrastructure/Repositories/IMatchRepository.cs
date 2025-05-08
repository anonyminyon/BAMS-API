using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface IMatchRepository
    {
        Task<Match?> GetMatchByIdAsync(int matchId);
        Task AddMatchAsync(Match match);
        Task<bool> UpdateMatchAsync(Match match);
        Task<bool> IsCourtAvailableAsync(string courtId, DateTime startTime, DateTime endTime);
        Task<bool> IsCourtAvailableAsync(Match match, string courtId, DateTime startTime, DateTime endTime);
        Task<bool> IsTeamAvailableAsync(string teamId, DateTime startTime, DateTime endTime);
        Task<bool> IsTeamAvailableAsync(Match match, string teamId, DateTime startTime, DateTime endTime);
        Task<List<Court>> GetAvailableCourtsForMatchInARangeOfTimeAsync(DateTime matchDate, DateTime endTime);
        Task<List<Match>> GetMatchesByPlayerAsync(string userId, DateTime startDate, DateTime endDate);
        Task<List<Match>> GetMatchesByManagerAsync(string managerId, DateTime startDate, DateTime endDate);
        Task<List<Match>> GetMatchesByParentAsync(string parentId, DateTime startDate, DateTime endDate);
        Task<List<Match>> GetMatchesByCoachAsync(string coachId, DateTime startDate, DateTime endDate, string? teamId, string? courtId);
        Task<List<Match>> GetAllMatchesAsync(DateTime startDate, DateTime endDate);
        Task<string> GetUserRoleAsync(string userId);
        Task<IEnumerable<Match>> GetNonCanceledMatchesAsync(CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
