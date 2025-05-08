using BasketballAcademyManagementSystemAPI.Application.DTOs.Attendance;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AttendanceController : ControllerBase
	{
		private readonly IAttendanceService _attendanceService;

		public AttendanceController(IAttendanceService service)
		{
			_attendanceService = service;
		}


		// API điểm danh cho một danh sách học viên
		[HttpPost("take-attendance")]
		[Authorize(Roles = RoleCodeConstant.ManagerCode)]

		public async Task<IActionResult> TakeAttendance([FromBody] List<TakeAttendanceDTO> attendances)
		{
			var response = await _attendanceService.TakeAttendanceAsync(attendances);
			return ApiResponseHelper.HandleApiResponse(response);
		}

		//[HttpPut("edit/{attendanceId}")]
		//[Authorize(Roles = RoleCodeConstant.ManagerCode)]

		//public async Task<IActionResult> EditAttendance( [FromBody] EditAttendanceDto attendanceDto)
		//{
		//	return ApiResponseHelper.HandleApiResponse(await _attendanceService.EditAttendanceAsync(attendanceDto));
		//}

		//[HttpGet("team/view-attendances-training-session")]
		//[Authorize(Roles = RoleCodeConstant.ManagerCode)]

		//public async Task<IActionResult> GetAttendance(string trainingSessionId)
		//{
		//	return ApiResponseHelper.HandleApiResponse(await _attendanceService.GetAttendancesByTrainingSessionAsync(trainingSessionId));
		//}

		//api lấy thông tin điểm danh của 1 người (userId) theo buổi học ( training session) 
		[HttpGet("view-attendance")]
		[Authorize(Roles =
			$"{RoleCodeConstant.ManagerCode}," +
			$"{RoleCodeConstant.PresidentCode}," +
			$"{RoleCodeConstant.CoachCode}," +
			$"{RoleCodeConstant.PlayerCode}")]
		public async Task<IActionResult> ViewUserAttendance(string trainingSessionId, string userId)
		{
			return ApiResponseHelper.HandleApiResponse(await _attendanceService.GetAttendanceByTrainingSessionOrUser(trainingSessionId, userId));
		}

		[HttpGet("players-for-attendance")]
		[Authorize(Roles =
			$"{RoleCodeConstant.ManagerCode}," +
			$"{RoleCodeConstant.CoachCode}")]
		public async Task<IActionResult> GetPlayersForAttendance([FromQuery] string trainingSessionId)
		{
			var result = await _attendanceService.GetPlayersForAttendanceAsync(trainingSessionId);
			return Ok(result);
		}

		[HttpGet("coaches-for-attendance")]
        [Authorize(Roles =
            $"{RoleCodeConstant.ManagerCode}," +
            $"{RoleCodeConstant.CoachCode}")]

        public async Task<IActionResult> GetCoachesForAttendance([FromQuery] string trainingSessionId)
		{
			var result = await _attendanceService.GetCoachesForAttendanceAsync(trainingSessionId);
			return Ok(result);
		}


		[HttpGet("get-user-summary-attendances")]
		[Authorize]
		public async Task<IActionResult> GetSummaryAttendances([FromQuery] string userId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
		{
			return ApiResponseHelper.HandleApiResponse(await _attendanceService.GetUserAttendanceSumary(userId, startDate,endDate));
		}

	}
}
