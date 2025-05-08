using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.CourtManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Match;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IMatchService
    {
        Task<ApiMessageModelV2<CreateMatchRequest>> CreateMatchAsync(CreateMatchRequest matchCreateDto);
        Task<ApiMessageModelV2<MatchDetailDto>> UpdateMatchAsync(int matchId, UpdateMatchRequest request);
        Task<ApiResponseModel<MatchDetailDto>> GetMatchDetailAsync(int matchId);
        Task<ApiResponseModel<bool>> CancelMatchAsync(int matchId);
        Task<ApiResponseModel<List<CourtDto>>> GetAvailableCourtsAsync(DateTime matchDate);
        Task<ApiResponseModel<List<MatchDetailDto>>> GetMatchesInAWeekAsync(DateTime startDate, DateTime endDate, string? teamId, string? courtId);
    }
}
