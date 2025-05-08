using BasketballAcademyManagementSystemAPI.Application.DTOs.PlayerManagement;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
	[Route("api/player")]
	[ApiController]
	public class PlayerController : ControllerBase
	{
		private readonly IPlayerService _playerService;
		public PlayerController(IPlayerService playerService)
		{
			_playerService = playerService;
		}

		[HttpPost("assign-team/{teamId}")]
		[Authorize(Roles = RoleCodeConstant.ManagerCode)]
		public async Task<IActionResult> AssignPlayersToTeam([FromBody] List<string> playerIds, string teamId)
		{
			return ApiResponseHelper.HandleApiResponse(await _playerService.AssignPlayersToTeamAsync(playerIds, teamId));
		}


		[HttpPut("{playerId}/remove-parent")]
		[Authorize(Roles = RoleCodeConstant.ManagerCode)]

		public async Task<IActionResult> RemoveParentFromPlayer(string playerId)
		{
			var result = await _playerService.RemoveParentFromPlayerAsync(playerId);
			if (!result)
			{
				return NotFound(new { message = "Không tìm thấy học viên" });
			}

			return Ok(new { message = "Đã gỡ thành công phụ huynh" });
		}


		[HttpPut("disable/{playerId}")]
		[Authorize(Roles = RoleCodeConstant.ManagerCode)]

		public async Task<IActionResult> DisablePlayer(string playerId)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				return BadRequest("⚠️ ID cầu thủ không được để trống.");
			}

			try
			{
				bool isDisabled = await _playerService.DisablePlayer(playerId);
				if (isDisabled)
				{
					return Ok(new { message = "✅ Cầu thủ đã bị vô hiệu hóa thành công." });
				}
				return NotFound(new { message = "Không tìm thấy cầu thủ hoặc lỗi khi cập nhật." });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "Lỗi hệ thống khi vô hiệu hóa cầu thủ.", error = ex.Message });
			}
		}


		[HttpGet("player-list")]
		[Authorize(Roles =
			$"{RoleCodeConstant.ManagerCode}," +
            $"{RoleCodeConstant.CoachCode},"+
            $"{RoleCodeConstant.PresidentCode}")]
		public async Task<IActionResult> GetPlayers([FromQuery] PlayerFilterDto filter)
		{
			try
			{
				var result = await _playerService.GetFilteredPlayersAsync(filter);
				return Ok(new
				{
					Status = "Success",
					Message = "Lấy danh sách player thành công",
					Data = result
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new
				{
					Status = "Failed",
					Message = "Có lỗi xảy ra khi lấy danh sách player",
					Error = ex.Message
				});
			}
		}

		[HttpGet("players-to-assign-to-team")]
		[Authorize(Roles =
			$"{RoleCodeConstant.ManagerCode}," +
			$"{RoleCodeConstant.PresidentCode}")]
		public async Task<IActionResult> GetPlayersToAssignTeam([FromQuery] string teamId)
		{
			return ApiResponseHelper.HandleApiResponse(await _playerService.GetPlayersWithoutTeamByGenderAsync(teamId));
		}


	}
}
