using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.FaceRecognition;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IFaceRecognitionService
    {
        Task<ApiResponseModel<RegisterFaceResponse>> RegisterFacesForAPlayerAsync(string userId, IFormFile image);
        Task<ApiResponseModel<DetectFaceResponse>> DetectFacesInGroupAsync(IFormFile image);
        Task<ApiResponseModel<object>> DeleteRegisteredFaceAsync(int userFaceId);
        Task<ApiResponseModel<UserRegisteredFacesDto>> GetRegisteredFacesForPlayerAsync(string userId);
        Task<ApiResponseModel<List<UserRegisteredFacesDto>>> GetRegisteredFacesForTeamAsync(string teamId);
    }
}
