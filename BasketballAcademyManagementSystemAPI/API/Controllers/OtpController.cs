using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.EmailVerification;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
    [Route("api/otp")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        public readonly IOtpService _otpService;

        public OtpController(IOtpService otpService)
        {
            _otpService = otpService;
        }

		//[HttpPost("send")]
		//public async Task<IActionResult> SendOtp([FromBody] SendOtpDto request)
		//{
		//	var response = await _otpService.SendOtpCodeAsync(request);
		//	return ApiResponseHelper.HandleApiResponse(response);
		//}


		[HttpPost("verify")]
		public async Task<IActionResult> VerifyCode([FromBody] VerifyEmailDto request)
		{
			return ApiResponseHelper.HandleApiResponse(await _otpService.VerifyCodeAsync(
					request.Email,
					request.Code,
					request.Purpose,
					request.MemberRegistrationSessionId));
		}
	}
}
