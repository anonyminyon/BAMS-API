using System.Diagnostics.Eventing.Reader;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Authentication;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> AuthenticateAsync(LoginRequestDto loginDto);
        Task<AuthResponseDto> RefreshTokensAsync();
        Task<AuthTokensResponseDto> AutoRefreshTokensAsync();
        Task<AuthResponseDto> LogoutAsync();
        Task<AuthResponseDto> ChangePasswordAsync(string userId, ChangePasswordRequestDto changePasswordRequest);
        Task<AuthResponseDto> ForgotPasswordAsync(string email);
        Task<AuthResponseDto> IsThisForgotPasswordTokenValid(string forgotPasswordToken, bool isReturnUserInf);
        Task<AuthResponseDto> SetNewPassword(SetNewPasswordRequestDto setNewPasswordRequest);
        Task<object> GetCurrentLoggedInUserInformationAsync();
        Task<bool> IsUserValidAsync(string userId);
    }
}
