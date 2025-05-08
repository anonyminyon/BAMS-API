using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Team;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
	[Route("api/team")]
	[ApiController]
	public class TeamController : ControllerBase
	{
		private readonly ITeamService _teamService;

		public TeamController(ITeamService teamService)
		{
			_teamService = teamService;
		}

		// Create Team
		[HttpPost("create")]
		[Authorize(Roles = RoleCodeConstant.PresidentCode)]
		public async Task<IActionResult> CreateTeam([FromBody] TeamRequestModel request)
		{
			var team = await _teamService.CreateTeamAsync(request.TeamName, request.Status);
			return HandleApiResponse(team);

		}

		// Get Team Details
		[HttpGet("{teamId}")]
        [Authorize]
        public async Task<IActionResult> GetTeamDetails(string teamId)
		{

			var teamDetails = await _teamService.GetTeamDetailsAsync(teamId);
			return HandleApiResponse(teamDetails);

		}

		// Update Team
		[HttpPut("{teamId}")]
		[Authorize(Roles = RoleCodeConstant.PresidentCode)]
		public async Task<IActionResult> UpdateTeam(string teamId, [FromBody] TeamDto request)
		{
			var teamUpdate = await _teamService.UpdateTeamInfoAsync(teamId, request.TeamName, (int)request.Status);
			return HandleApiResponse(teamUpdate);
		}

		// Remove Players
		[HttpPost("remove/{teamId}/players")]
		[Authorize(Roles =
			$"{RoleCodeConstant.ManagerCode}," +
			$"{RoleCodeConstant.PresidentCode}")]
		public async Task<IActionResult> RemovePlayers(string teamId, [FromBody] List<string> playerIds, DateTime leftDate)
		{
			return HandleApiResponse(await _teamService.RemovePlayersAsync(teamId, playerIds, leftDate));
		}

		// Remove Coaches
		[HttpPost("remove/{teamId}/coaches")]
		[Authorize(Roles = RoleCodeConstant.PresidentCode)]
		public async Task<IActionResult> RemoveCoaches(string teamId, [FromBody] List<string> coachIds, DateTime leftDate)
		{
			return HandleApiResponse(await _teamService.RemoveCoachesAsync(teamId, coachIds, leftDate));
		}

		// Remove Managers
		[HttpPost("remove/{teamId}/managers")]
		[Authorize(Roles = RoleCodeConstant.PresidentCode)]
		public async Task<IActionResult> RemoveManagers(string teamId, [FromBody] List<string> managerIds, DateTime leftDate)
		{
			return HandleApiResponse(await _teamService.RemoveManagersAsync(teamId, managerIds, leftDate));
		}

		// Disband Team
		[HttpDelete("disband/{teamId}")]
		[Authorize(Roles = RoleCodeConstant.PresidentCode)]
		public async Task<IActionResult> DisbandTeam(string teamId, string note)
		{
			return HandleApiResponse(await _teamService.DisbandTeamAsync(teamId, note));
		}

		// Get Teams with Pagination
		[HttpGet]
		[Authorize(Roles =
			$"{RoleCodeConstant.ManagerCode}," +
			$"{RoleCodeConstant.PresidentCode}," +
			$"{RoleCodeConstant.PlayerCode}," +
		    $"{RoleCodeConstant.ParentCode}," +
			$"{RoleCodeConstant.CoachCode}")]
		public async Task<IActionResult> GetTeams([FromQuery] TeamFilterDto filter)
		{
			return HandleApiResponse(await _teamService.GetTeamsAsync(filter));
		}

		// Hàm hỗ trợ xử lý ApiResponseModel và trả về IActionResult phù hợp
		private IActionResult HandleApiResponse<T>(ApiMessageModelV2<T> response)
		{
			if (response.Status == ApiResponseStatusConstant.SuccessStatus)
			{
				return Ok(response);
			}
			return BadRequest(response);
		}

		[HttpPut("{teamId}/update-fund-manager/{fundManagerId}")]
		[Authorize(Roles = RoleCodeConstant.PresidentCode)]
		public async Task<IActionResult> UpdateFundManager(string teamId, string fundManagerId)
		{
			var result = await _teamService.UpdateFundManagerAsync(teamId, fundManagerId);
			return Ok(result);
		}

	}
}
