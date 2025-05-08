using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
    [Route("api/my-account")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAccountService _accountService;

        public AccountController(IAuthService authService, IAccountService accountService)
        {
            _authService = authService;
            _accountService = accountService;
        }

        [HttpGet("update-profile")]
        public async Task<IActionResult> GetInformationToUpdateProfile()
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

        [HttpPost("update-profile")]
        public async Task<IActionResult> UpdateProfile(object userUpdateData)
        {
            try
            {
                var accountResponse = await _accountService.UpdateProfileAsync(userUpdateData);
                return Ok(accountResponse);
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

        [HttpPost("reset-password/{userId}")]
        public async Task<IActionResult> ResetPassword(string userId)
        {
            var result = await _accountService.ResetPasswordAsync(userId);
            if (result.Errors!.Any())
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }
    }
}
