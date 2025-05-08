using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.EmailVerification;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IOtpService
    {
		Task<ApiMessageModelV2<bool>> SendOtpCodeAsync(SendOtpDto email);
        Task<ApiMessageModelV2<object>> VerifyCodeAsync(string email, string code, string emailPurposeVerifyRegistrationForm,
            int memberRegistrationSessionId);

	}
}
