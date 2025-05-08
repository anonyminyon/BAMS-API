using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.CourtManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface ITrainingSessionService
    {
        Task<ApiMessageModelV2<TrainingSession>> CreateAddtitionalTrainingSessionAsync(CreateTrainingSessionRequest request);
        Task<ApiResponseModel<List<TrainingSessionDto>>> GetPendingTrainingSession();
        Task<ApiResponseModel<TrainingSessionDto>> ApproveTrainingSessionAsync(ApproveTrainingSessionRequest request);
        Task<ApiResponseModel<TrainingSessionDto>> RejectTrainingSessionAsync(CancelTrainingSessionRequest request);
        Task<ApiResponseModel<TrainingSessionDto>> CheckTrainingSessionConflictAsync(CheckTrainingSessionConflictRequest request);
        Task<ApiMessageModelV2<List<TrainingSessionDto>>> GetTrainingSessionsAsync(DateTime startDate, DateTime endDate, string? teamId, string? courtId);
        Task<ApiResponseModel<TrainingSessionDetailDto>> GetTrainingSessionByIdAsync(string trainingSessionId);
        Task<ApiResponseModel<bool>> RequestToCancelTrainingSessionAsync(CancelTrainingSessionRequest request);
        Task<ApiResponseModel<string>> CancelTrainingSessionAsync(string trainingSessionId);
        Task<ApiResponseModel<List<RequestCancelTrainingSessionDto>>> GetRequestCancelTrainingSession();
        Task<ApiResponseModel<string>> RejectCancelTrainingSessionRequestAsync(CancelTrainingSessionChangeStatusRequest request);
        Task<ApiMessageModelV2<TrainingSession>> RequestToUpdateTrainingSessionAsync(UpdateTrainingSessionRequest request);
        Task<ApiResponseModel<List<RequestUpdateTrainingSessionDto>>> GetPendingRequestUpdateTrainingSession();
        Task<ApiResponseModel<TrainingSession>> UpdateTrainingSessionAsync(ApproveTrainingSessionRequest request);
        Task<ApiResponseModel<TrainingSessionDto>> RejectUpdateTrainingSessionRequestAsync(CancelTrainingSessionChangeStatusRequest request);
        Task<ApiResponseModel<List<CourtDto>>> GetAvailableCourtsAsync(GetAvailableCourtsRequest request);
        Task<ApiMessageModelV2<GenerateTrainingSessionsResponse>> GenerateTrainingSessionsAsync(GenerateTrainingSessionsRequest request);
        Task<ApiMessageModelV2<GenerateTrainingSessionsResponse>> BulkCreateTrainingSessionsAsync(List<CreateTrainingSessionRequest> requests);
    }
}
