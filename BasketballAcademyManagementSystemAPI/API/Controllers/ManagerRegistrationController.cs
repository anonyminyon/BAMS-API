using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
    [Route("api/manager-registration")]
    [ApiController]
    public class ManagerRegistrationController : ControllerBase
    {
        private readonly IManagerRegistrationService _managerRegistrationService;

        public ManagerRegistrationController(IManagerRegistrationService managerRegistrationService)
        {
            _managerRegistrationService = managerRegistrationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> SaveManagerRegistrationFormAsync([FromBody] ManagerRegistrationDto managerRegistrationDto)
        {
            var response = await _managerRegistrationService.RegisterManagerAsync(managerRegistrationDto);
            return ApiResponseHelper.HandleApiResponse(response);
        }

        [HttpGet("list")]
        [Authorize(Roles = RoleCodeConstant.PresidentCode)]
        public async Task<IActionResult> GetListManagerRegistrationAsync([FromQuery] ManagerRegistrationFilterDto filter)
        {
            try
            {
                var result = await _managerRegistrationService.GetAllRegisterManager(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiMessageModelV2<List<ManagerRegistrationDto>>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ex.Message,
                    Errors = new Dictionary<string, string>() { { CommonMessage.Key.ErrorGeneral, ex.Message } }
                });
            }
        }

        [HttpPost("approve/{id}")]
        [Authorize(Roles = RoleCodeConstant.PresidentCode)]
        public async Task<IActionResult> Approve(int id)
        {
            var response = await _managerRegistrationService.ApproveRegistrationAsync(id);
            return ApiResponseHelper.HandleApiResponse(response);
        }

        [HttpPost("reject/{id}")]
        [Authorize(Roles = RoleCodeConstant.PresidentCode)]
        public async Task<IActionResult> Reject(int id)
        {
            var response = await _managerRegistrationService.RejectRegistrationAsync(id);
            return ApiResponseHelper.HandleApiResponse(response);
        }

        [HttpPost("update-registration-form")]
        public async Task<IActionResult> UpdateRegistrationFormAsync([FromBody] ManagerRegistrationDto managerRegistrationDto)
        {
            var response = await _managerRegistrationService.UpdateRegisterManagerAsync(managerRegistrationDto);
            return ApiResponseHelper.HandleApiResponse(response);
        }

        [HttpPost("validate-email-and-send-otp/{memberRegistrationSessionId}")]
        public async Task<IActionResult> CheckMailRegistrationAndSendOtpAsync(
            [FromBody] string email,
            int memberRegistrationSessionId)
        {
            var response = await _managerRegistrationService.ValidateInfoRegistrationAndSendOtpAsync(
                email,
                memberRegistrationSessionId);
            return ApiResponseHelper.HandleApiResponse(response);
        }
    }
}