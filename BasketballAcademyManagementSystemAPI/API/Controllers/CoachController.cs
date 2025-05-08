using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.CoachManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl;
using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
    [Route("api/coach")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private readonly ICoachService _coachService;

        public CoachController(ICoachService coachService)
        {
            _coachService = coachService;
        }
        //Luồng hoạt động
        //1.	CoachController.CreateCoach:
        //•	Nhận CreateCoachDto từ client.
        //•	Gọi CoachService.CreateCoachAsync.
        //2.	CoachService.CreateCoachAsync:
        //•	Chuyển đổi CreateCoachDto thành User và Coach model.
        //•	Gọi CoachRepository.AddCoachAsync để lưu User và Coach vào cơ sở dữ liệu.
        //•	Gọi SendMailService.SendMailAssignToTeamAsync để gửi email thông báo.
        //•	Gọi UserTeamHistoryService.UserAssignToNewTeamHistory để ghi lại lịch sử đội.
        //3.	CoachRepository.AddCoachAsync:
        //•	Lưu User vào bảng Users thông qua UserRepository.AddUserAsync.
        //•	Lưu Coach vào bảng Coaches.
        //4.	DTOs và Models:
        //•	CreateCoachDto: Dữ liệu đầu vào từ client.
        //•	User: Thông tin người dùng được lưu vào bảng Users.
        //•	Coach: Thông tin huấn luyện viên được lưu vào bảng Coaches.
        //•	ApiMessageModelV2<T>: Định dạng phản hồi API.
        //•	UserAccountDto<CoachDetailDto>: Kết hợp thông tin User và chi tiết Coach.
        [HttpPost("create")]
        [Authorize(Roles = RoleCodeConstant.PresidentCode)]
        public async Task<IActionResult> CreateCoach([FromBody] CreateCoachDto coachDto)
        {
            var result = await _coachService.CreateCoachAsync(coachDto);
            return ApiResponseHelper.HandleApiResponse(result);
        }

        [HttpGet("list")]
        [Authorize(Roles =
            $"{RoleCodeConstant.PresidentCode}," +
            $"{RoleCodeConstant.CoachCode}")]
        public async Task<IActionResult> GetCoachList([FromQuery] CoachFilterDto filter)
        {
            try
            {
                var result = await _coachService.GetCoachListAsync(filter);
                return ApiResponseHelper.HandleApiResponse(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("detail/{userId}")]
        [Authorize(Roles =
            $"{RoleCodeConstant.PresidentCode}," +
            $"{RoleCodeConstant.CoachCode}")]
        public async Task<IActionResult> GetCoachDetail(string userId)
        {
            //still not validate case if login is coach that coach can only view detail of their not other coach
            var result = await _coachService.GetCoachDetailAsync(userId);
            return ApiResponseHelper.HandleApiResponse(result);
        }

        [HttpPatch("change-status/{userId}")]
        [Authorize(Roles = RoleCodeConstant.PresidentCode)]
        public async Task<IActionResult> DisableCoach(string userId)
        {
            var result = await _coachService.ChangeCoachAccountStatusAsync(userId);
            return ApiResponseHelper.HandleApiResponse(result);
        }

        [HttpPatch("assign-to-team")]
        [Authorize(Roles = RoleCodeConstant.PresidentCode)]
        public async Task<IActionResult> AssignCoachToTeam([FromBody] AssignCoachToTeamDto dto)
        {
            var result = await _coachService.AssignCoachToTeamAsync(dto.UserId, dto.TeamId);
            return ApiResponseHelper.HandleApiResponse(result);
        }


        [HttpPut("update")]
        [Authorize(Roles = RoleCodeConstant.PresidentCode)]
        public async Task<IActionResult> UpdateCoach([FromBody] UpdateCoachDto coachDto)
        {
            var result = await _coachService.UpdateCoachAsync(coachDto);
            return ApiResponseHelper.HandleApiResponse(result);
        }
    }
}
