using BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using static BasketballAcademyManagementSystemAPI.Common.Messages.ApiResponseMessage;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IExerciseService
    {
        Task<ApiMessageModelV2<CreateExerciseRequest>> AddExerciseForTrainingSessionAsync(CreateExerciseRequest request);
        Task<ApiMessageModelV2<UpdateExerciseRequest>> UpdateExerciseAsync(UpdateExerciseRequest request);
        Task<ApiResponseModel<bool>> RemoveExerciseAsync(string exerciseId);
    }
}
