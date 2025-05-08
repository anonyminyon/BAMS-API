using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface ITryOutScorecardService
    {
        Task<ApiResponseModel<string>> AddOrUpdateScoresAsync(BulkPlayerScoreInputDto input);
        Task<ApiResponseModel<string>> UpdateScoresAsync(BulkPlayerScoreInputDto input);
        Task<ApiResponseModel<PlayerTryOutScoreCardDto>> GetScoresByPlayerRegistrationIdAsync(int playerRegistrationId);
        Task<ApiResponseModel<PlayerSkillScoreReportDto>> GetReportByPlayerRegistrationIdAsync(int playerRegistrationId);
        Task<ApiResponseModel<List<RegistrationSessionScoresDto>>> GetScoresByRegistrationSessionIdAsync(int sessionId, PlayerRegistrationScoresFilterDto filter);
        Task<ApiResponseModel<List<PlayerSkillScoreReportDto>>> GetReportByRegistrationSessionIdAsync(int sessionId, PlayerRegistrationScoresFilterDto filter);
        Task<ApiResponseModel<ReportFileResponse>> GetPlayerRegistrationSessionScoreReportAsync(int playerRegistrationId, bool? gender);
    }
}
