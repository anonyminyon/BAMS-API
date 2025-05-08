using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.ManagerManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IManagerRegistrationService
    {
        Task<ApiMessageModelV2<ManagerRegistrationDto>> RegisterManagerAsync(ManagerRegistrationDto managerRegistrationDto);
        Task<PagedResponseDto<ManagerRegistrationDto>> GetAllRegisterManager(ManagerRegistrationFilterDto filter);
        Task<ApiMessageModelV2<UserAccountDto<ManagerDto>>> ApproveRegistrationAsync(int id);
        Task<ApiMessageModelV2<ManagerRegistrationDto>> RejectRegistrationAsync(int id);
        Task<ApiMessageModelV2<object>> ValidateInfoRegistrationAndSendOtpAsync(string email, int memberRegistrationSessionId);
        Task<ApiMessageModelV2<ManagerRegistrationDto>> UpdateRegisterManagerAsync(ManagerRegistrationDto managerRegistrationDto);
    }
}
