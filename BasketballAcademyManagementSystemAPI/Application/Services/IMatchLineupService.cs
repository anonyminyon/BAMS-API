using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Match;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Team;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IMatchLineupService
    {
        Task<ApiResponseModel<bool>> CallPlayerForMatchAsync(CallPlayerForMatchRequest request);
        Task<ApiResponseModel<bool>> RemovePlayerFromMatchLineupAsync(int matchId, string playerId);
        Task<ApiResponseModel<List<AvailablePlayerForAMatchDto>>> GetAvailablePlayersAsync(int matchId);
    }
}
