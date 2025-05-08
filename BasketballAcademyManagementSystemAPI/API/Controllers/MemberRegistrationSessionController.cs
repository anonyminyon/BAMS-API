
using BasketballAcademyManagementSystemAPI.Application.DTOs.MemberRegistrationSession;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BasketballAcademyManagementSystemAPI.Common.Messages.GeneralServerMessage;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
    [Route("api/member-registraion-session")]
    [ApiController]
    public class MemberRegistrationSessionController : ControllerBase
    {
        private readonly IMemberRegistrationSessionService _memberRegistrationSessionService;
        private readonly ILog _log = LogManager.GetLogger(LogConstant.LogName.MemberRegistrationSessionFeature);

        public MemberRegistrationSessionController(IMemberRegistrationSessionService memberRegistrationSessionService)
        {
            _memberRegistrationSessionService = memberRegistrationSessionService;
        }

        [HttpPost]
        [Authorize(Roles = RoleCodeConstant.PresidentCode)]
        public async Task<IActionResult> Create([FromBody] CreateMemberRegistrationSessionRequestDto dto)
        {
            try
            {
                var apiResponse = await _memberRegistrationSessionService.CreateAsync(dto);
                if (apiResponse.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(apiResponse);
                }
                return Ok(apiResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            try
            {
                var session = await _memberRegistrationSessionService.GetDetailsAsync(id);
                if (session == null)
                {
                    return NotFound();
                }
                return Ok(session);
            } catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = RoleCodeConstant.PresidentCode)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMemberRegistrationSessionRequestDto dto)
        {
            try
            {
                var apiResponse = await _memberRegistrationSessionService.UpdateAsync(id, dto);
                if (apiResponse.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(apiResponse);
                }
                return Ok(apiResponse);
            }
            catch (KeyNotFoundException ex)
            {
                _log.Error(ex.Message, ex);
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                _log.Error(ex.Message, ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("toggle-status/{id}")]
        [Authorize(Roles = RoleCodeConstant.PresidentCode)]
        public async Task<IActionResult> ToggleMemberRegistrionSessionStatus(int id)
        {
            try
            {
                var apiResponse = await _memberRegistrationSessionService.ToggleMemberRegistratrionSessionStatusAsync(id);
                if (apiResponse.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(apiResponse);
                }
                return Ok(apiResponse);
            }
            catch (KeyNotFoundException ex)
            {
                _log.Error(ex.Message, ex);
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                _log.Error(ex.Message, ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFilteredAndPagedRegistrationSession([FromQuery] MemberRegistrationSessionFilterDto filterDto)
        {
            try
            {
                var response = await _memberRegistrationSessionService.GetFilteredPagedAsync(filterDto);
                if (response.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleCodeConstant.PresidentCode)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var apiResponse = await _memberRegistrationSessionService.DeleteAsync(id);
                if (!apiResponse.Data)
                {
                    return NotFound(apiResponse);
                }

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }
    }
}
