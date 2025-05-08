using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.EmailVerification;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class OtpService : IOtpService
    {
        private readonly IEmailVerificationRepository _emailVerificationRepository;
		private readonly ISendMailService _sendMailService;
		private readonly IManagerRegistrationRepository _managerRegistrationRepository;
		private readonly IPlayerRegistrationRepository _playerRegistrationRepository;

        public OtpService(IManagerRegistrationRepository managerRegistrationRepository, IPlayerRegistrationRepository playerRegistrationRepository,
        IEmailVerificationRepository emailVerificationRepository, ISendMailService sendMailService)
        {
            _emailVerificationRepository = emailVerificationRepository;
			_sendMailService = sendMailService;
            _managerRegistrationRepository = managerRegistrationRepository;
            _playerRegistrationRepository = playerRegistrationRepository;
        }
		public async Task<ApiMessageModelV2<bool>> SendOtpCodeAsync(SendOtpDto request)
		{
			var errors = new Dictionary<string, string>();

			try
			{
				//Tạo code 6 số ngẫu nhiên
				var random = new Random();
				string verificationCode = (random.Next(100000, 999999)).ToString();

				// Lưu mã OTP vào database
				await _emailVerificationRepository.SaveVerificationCodeWithOtpAsync(request.Email, verificationCode, request.Purpose);

				// Gửi email otp
				await _sendMailService.SendMailByMailTemplateIdAsync(
					 MailTemplateConstant.VerifyEmailRegistration,
					 request.Email, 
					 new { Otp = verificationCode });

				return new ApiMessageModelV2<bool>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = EmailVerificationMessage.Success.CodeSent,
					Data = true
				};
			}
			catch (Exception ex)
			{
				errors.Add("exception", ex.Message);
				return new ApiMessageModelV2<bool>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors,
					Data = false
				};
			}
		}

		public async Task<ApiMessageModelV2<object>> VerifyCodeAsync(string email, string code, string emailPurposeVerifyRegistrationForm,
            int memberRegistrationSessionId)
		{
			var errors = new Dictionary<string, string>();

			try
			{
				// Kiểm tra email tồn tại trong hệ thống trước khi xác minh
				//var emailCheckResults = await GetEmailResultsAsync(email);
				// Kiểm tra email đã verify code trước đó chưa
				bool isEmailRegistered = await _emailVerificationRepository.IsEmailVerified(email);
				if (isEmailRegistered)
				{
					errors.Add(EmailVerificationMessage.Key.EmailRegistered, EmailVerificationMessage.Error.EmailRegisteredFormError);
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors,
						Data = false
					};
				}

				bool isVerified = await _emailVerificationRepository.VerifyCodeAsync(email, code, emailPurposeVerifyRegistrationForm);

				if (!isVerified)
				{
					errors.Add(EmailVerificationMessage.Key.InvalidCode, EmailVerificationMessage.Error.InvalidOrExpiredCode);
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors,
						Data = false
					};
				}
				
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = EmailVerificationMessage.Success.EmailVerified,
					Data = await GetRegistrationFormForUpdate(email,memberRegistrationSessionId,emailPurposeVerifyRegistrationForm)
                };
			}
			catch (Exception ex)
			{
				errors.Add("exception", ex.Message);
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors,
					Data = false
				};
			}
		}

        //logic lấy ra thằng form đăng ký đã tồn tại ở 1 session của player hoặc manager để update lại thông tin thì chỉ cần truyền cặp email và MemberRegistrationSessionId
        //vì 1 mùa đăng ký thì mail đấy chỉ được đăng ký 1 lần duy nhất nếu bị rejected thì sẽ có 1 session mới và email đấy sẽ được đăng ký lại
        private async Task<object?> GetRegistrationFormForUpdate(string email, int memberRegistrationSessionId, string emailPurposeVerifyRegistrationForm)
        {
            if (emailPurposeVerifyRegistrationForm.Equals(EmailConstant.ManagerRegistrationForm))
            {
				ManagerRegistrationFilterDto mrfd = new ManagerRegistrationFilterDto();
                mrfd.Email = email;
				mrfd.MemberRegistrationSessionId = memberRegistrationSessionId;
                return await _managerRegistrationRepository.GetAllManagerRegistrations(mrfd);
            }
            else if (emailPurposeVerifyRegistrationForm.Equals(EmailConstant.PlayerRegistrationForm))
            {
                return await _playerRegistrationRepository.GetPlayersOldForm(memberRegistrationSessionId,email);
            }
			return null;
        }
    }
}
