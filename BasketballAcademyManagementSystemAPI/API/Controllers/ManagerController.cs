using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.ManagerManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
    [Route("api/manager")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpPut("assign-manager-to-team")]
        [Authorize(Roles = RoleCodeConstant.PresidentCode)]
        public async Task<IActionResult> AssignManagerToTeamAsync([FromBody] ManagerDto managerDto)
        {
            try
            {
                var response = await _managerService.AssignManagerToTeamAsync(managerDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("manager-detail/{userId}")]
        [Authorize(Roles = RoleCodeConstant.PresidentCode)]
        public async Task<IActionResult> ViewDetailAManagerAsync(string userId)
        {
            var managerDetail = await _managerService.GetManagerDetailAsync(userId);
            return ApiResponseHelper.HandleApiResponse(managerDetail);
        }

        [HttpPut("disable-manager/{userId}")]
        [Authorize(Roles = RoleCodeConstant.PresidentCode)]
        public async Task<IActionResult> DisableManagerAsync(string userId)
        {
            var response = await _managerService.DisableManagerAsync(userId);
            return ApiResponseHelper.HandleApiResponse(response);
        }

        [HttpPut("update-manager")]
        [Authorize(Roles = $"{RoleCodeConstant.PresidentCode},{RoleCodeConstant.ManagerCode}")]
        public async Task<IActionResult> UpdateManager([FromBody] UserAccountDto<ManagerDto> managerDto)
        {
            var result = await _managerService.UpdateManagerAsync(managerDto);
            return ApiResponseHelper.HandleApiResponse(result);
        }

        [HttpGet("list-manager-filter-and-paging")]
        [Authorize(Roles = RoleCodeConstant.PresidentCode)]
        public async Task<IActionResult> GetManagersAsync([FromQuery] ManagerFilterDto filter)
        {
            try
            {
                var managers = await _managerService.GetFilteredPagedManagersAsync(filter);
                return ApiResponseHelper.HandleApiResponse(managers);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

    }
}
