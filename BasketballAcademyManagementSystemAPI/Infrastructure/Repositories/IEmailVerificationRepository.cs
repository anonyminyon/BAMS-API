using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
	public interface IEmailVerificationRepository
	{
		Task SaveVerificationCodeWithOtpAsync(string email, string otp, string purpose);
		//xác minh code
		Task<bool> VerifyCodeAsync(string email, string code, string purpose);

		// kiểm tra email đã đăng kí hay chưa
		Task<bool> IsEmailVerifiedOtp(string email);

		//Kiểm tra email đã verify hay chưa
		Task<bool> IsEmailVerified(string email);

		
	}
}
