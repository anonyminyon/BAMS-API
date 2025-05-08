using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.CourtManagement;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
    [Route("api/court")]
    [ApiController]
    public class CourtController : ControllerBase
    {
        private readonly ICourtService _courtService;
        private readonly IFileUploadService _fileUploadService;

        public CourtController(ICourtService courtService, IFileUploadService fileUploadService)
        {
            _courtService = courtService;
            _fileUploadService = fileUploadService;
        }

        [HttpPost("add-new-court")]
        [Authorize(Roles =
            $"{RoleCodeConstant.ManagerCode}," +
            $"{RoleCodeConstant.CoachCode}")]
        public async Task<IActionResult> CreateCourt([FromBody] CourtDto courtDto)
        {
            var result = await _courtService.CreateCourt(courtDto);
            return ApiResponseHelper.HandleApiResponse(result);
        }

        [HttpGet("list-court-arranged-by-status")]
        [Authorize(Roles =
            $"{RoleCodeConstant.ManagerCode}," +
            $"{RoleCodeConstant.PresidentCode}," +
            $"{RoleCodeConstant.CoachCode}")]
        public async Task<IActionResult> GetAllCourts([FromQuery] CourtFilterDto filter)
        {
            try
            {
                var result = await _courtService.GetAllCourts(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseModel<PagedResponseDto<CourtDto>>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ex.Message,
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        [HttpGet("{courtId}")]
        [Authorize(Roles =
            $"{RoleCodeConstant.ManagerCode}," +
            $"{RoleCodeConstant.PresidentCode}," +
            $"{RoleCodeConstant.CoachCode}")]
        public async Task<IActionResult> GetCourtById(string courtId)
        {
            var result = await _courtService.GetCourtById(courtId);
            return ApiResponseHelper.HandleApiResponse(result);
        }

        [HttpPut("update")]
        [Authorize(Roles =
            $"{RoleCodeConstant.ManagerCode}," +
            $"{RoleCodeConstant.CoachCode}")]
        public async Task<IActionResult> UpdateCourt([FromBody] CourtDto courtDto)
        {
            var result = await _courtService.UpdateCourt(courtDto);
            return ApiResponseHelper.HandleApiResponse(result);
        }

        [HttpPut("disable/{courtId}")]
        [Authorize(Roles =
            $"{RoleCodeConstant.ManagerCode}," +
            $"{RoleCodeConstant.CoachCode}")]
        public async Task<IActionResult> DisableCourt(string courtId)
        {
            var result = await _courtService.DisableCourt(courtId);
            return ApiResponseHelper.HandleApiResponse(result);
        }

        [HttpPost("upload-image-and-get-url")]
        [Authorize(Roles =
            $"{RoleCodeConstant.ManagerCode}," +
            $"{RoleCodeConstant.CoachCode}")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                string url = "court/images/";
                string fileUrl = await _fileUploadService.UploadFileAsync(file, url);
                return Ok(fileUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = ApiResponseStatusConstant.FailedStatus, message = ex.Message });
            }
        }

        [HttpPost("validate-court-name")]
        [Authorize(Roles =
            $"{RoleCodeConstant.ManagerCode}," +
            $"{RoleCodeConstant.CoachCode}")]
        public async Task<IActionResult> ValidateThirdSecond([FromQuery] CourtDto court)
        {
            var result = await _courtService.CheckDuplicateCourt(court);
            return ApiResponseHelper.HandleApiResponse(result);
        }
    }
}