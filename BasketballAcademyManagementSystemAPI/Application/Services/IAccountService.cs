using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IAccountService
    {
        Task<AccountResponseDto> UpdateProfileAsync(object userUpdateData);
        Task<ApiResponseModel<string>> ResetPasswordAsync(string userId);
    }
}
