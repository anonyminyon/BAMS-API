using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface ITryOutScorecardRepository
    {
        Task<TryOutScorecard?> GetByPlayerAndMeasurementAsync(int playerRegistrationId, string measurementScaleCode);
        Task AddOrUpdateAsync(TryOutScorecard scorecard);
        Task<TryOutScorecard?> GetAsync(int tryOutScorecardId);
        Task<List<TryOutScorecard>> GetScoresByPlayerIdAsync(int playerRegistrationId);
        Task<List<PlayerRegistration>> GetFilteredPlayersAsync(int sessionId, PlayerRegistrationScoresFilterDto filter); 
        Task DeleteAsync(TryOutScorecard scorecard);
        Task DeleteRangeAsync(ICollection<TryOutScorecard> scorecards);
    }
}
