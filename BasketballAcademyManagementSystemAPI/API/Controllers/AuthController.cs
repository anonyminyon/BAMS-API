using System.Security.Claims;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Authentication;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BasketballAcademyManagementSystemAPI.Common.Messages.AuthenticationMessage;
using static BasketballAcademyManagementSystemAPI.Common.Messages.GeneralServerMessage;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            try
            {
                var response = await _authService.AuthenticateAsync(loginDto);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { ex.Message });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = AuthenticationErrorMessage.UserNotFound });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshAccessToken()
        {
            try
            {
                var response = await _authService.RefreshTokensAsync();
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { ex.Message });
            }
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            var response = await _authService.LogoutAsync();
            return Ok(response);
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new { Message = AuthenticationErrorMessage.UserNotFound });
            }

            try
            {
                await _authService.ChangePasswordAsync(userId.ToString(), request);
                return Ok(new { Message = AuthenticationSuccessMessage.ChangePasswordSuccess });
            }
            catch (ArgumentException argumentEx)
            {
                return BadRequest(new { Message = argumentEx.Message });
            }
            catch (KeyNotFoundException keynotfoundEx)
            {
                return Unauthorized(new { Message = keynotfoundEx.Message });
            }
            catch (UnauthorizedAccessException unauthorizedAccessEx)
            {
                return Unauthorized(new { Message = unauthorizedAccessEx.Message });
            } 
            
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto forgotPasswordRequest)
        {
            if (!RegexHelper.IsMatchRegex(forgotPasswordRequest.Email, RegexConstant.EmailRegex))
            {
                return BadRequest( new { Message = AuthenticationErrorMessage.InvalidEmail });
            }

            try
            {
                var authResponse = await _authService.ForgotPasswordAsync(forgotPasswordRequest.Email);
                return Ok(authResponse);
            }
            catch (KeyNotFoundException keyNotFoundEx)
            {
                return NotFound(new { Message = keyNotFoundEx.Message });
            }
            catch (UnauthorizedAccessException unauthorizedAccessEx)
            {
                return Unauthorized(new { Message = unauthorizedAccessEx.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = GeneralServerErrorMessage.SomeThingWentWrong});
            }
        }

        [HttpPost("validate-forgot-password-token")]
        public async Task<IActionResult> ValidateForgotPasswordToken([FromBody] ValidateForgotPasswordTokenRequestDto validateForgotPasswordTokenRequest)
        {
            try
            {
                var authResponse = await _authService.IsThisForgotPasswordTokenValid(validateForgotPasswordTokenRequest.Token, false);
                return Ok(authResponse);
            }
            catch (KeyNotFoundException keyNotFoundEx)
            {
                return NotFound(new { Message = keyNotFoundEx.Message });
            }
            catch (UnauthorizedAccessException unauthorizedAccessEx)
            {
                return Unauthorized(new { Message = unauthorizedAccessEx.Message });
            }
            catch (ArgumentException argumentEx)
            {
                return BadRequest(new { Message = argumentEx.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = GeneralServerErrorMessage.SomeThingWentWrong });
            }
        }

        [HttpPost("set-new-password")]
        public async Task<IActionResult> SetNewPassword([FromBody] SetNewPasswordRequestDto setNewPasswordRequest)
        {
            try
            {
                var authResponse = await _authService.SetNewPassword(setNewPasswordRequest);
                return Ok(authResponse);
            }
            catch (KeyNotFoundException keyNotFoundEx)
            {
                return NotFound(new { Message = keyNotFoundEx.Message });
            }
            catch (UnauthorizedAccessException unauthorizedAccessEx)
            {
                return Unauthorized(new { Message = unauthorizedAccessEx.Message });
            }
            catch (ArgumentException argumentEx)
            {
                return BadRequest(new { Message = argumentEx.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = GeneralServerErrorMessage.SomeThingWentWrong });
            }
        }

        [HttpGet("my-information")]
        [Authorize]
        public async Task<IActionResult> GetCurrentLoggedInUserInformation()
        {
            try
            {
                var user = await _authService.GetCurrentLoggedInUserInformationAsync();
                return Ok(user);
            }
            catch (UnauthorizedAccessException unauthorizedAccessEx)
            {
                return Unauthorized(new { Message = unauthorizedAccessEx.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

    }
}
