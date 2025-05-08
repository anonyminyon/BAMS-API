using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface ITryOutMeasurementScaleService
    {
        Task<ApiResponseModel<List<TryOutMeasurementScaleDto>>> GetSkillTreeAsync();
        Task<ApiResponseModel<TryOutMeasurementScaleDto>> GetSkillByCodeAsync(string measurementScaleCode);
        Task<ApiResponseModel<List<TryOutMeasurementScaleDto>>> GetLeafSkillsAsync();
    }
}
