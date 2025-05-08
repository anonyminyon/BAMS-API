using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Parent;
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
	[Route("api/parent")]
	[ApiController]
	public class ParentController : ControllerBase
	{
		private readonly IParentService _parentService;

		public ParentController(IParentService parentServices)
		{
			_parentService = parentServices;
		}

		[HttpPost("assign-parent/{playerId}")]
		[Authorize(Roles = RoleCodeConstant.ManagerCode)]
		public async Task<IActionResult> AddParentForPlayerAsync(string playerId, [FromBody] string parentId)
		{
			return ApiResponseHelper.HandleApiResponse(await _parentService.AddParentForPlayerAsync(playerId, parentId));
		}


		[HttpGet("parent-details/{parentId}")]
        [Authorize(Roles =
            $"{RoleCodeConstant.ManagerCode}," +
            $"{RoleCodeConstant.CoachCode}," +
            $"{RoleCodeConstant.PresidentCode}")]
        public async Task<IActionResult> GetParentDetails(string parentId)
		{
			return ApiResponseHelper.HandleApiResponse(await _parentService.GetParentDetailsAsync(parentId));
		}

		[HttpGet("get-players-of-parent/{parentId}")]
		[Authorize]
		public async Task<IActionResult> GetPlayersByParentId(string parentId)
		{
			var result = await _parentService.GetPlayersByParentIdAsync(parentId);
			return Ok(result);
		}
		[HttpPost("create-and-assign-parent-to-player")]
		[Authorize(Roles = RoleCodeConstant.ManagerCode)]
		public async Task<IActionResult> CreateParentAccount([FromBody] CreateParentRequestDto dto)
		{
			var result = await _parentService.CreateParentAccountAsync(dto);
			return Ok(result);
		}

		[HttpGet("filter-parents")]
        [Authorize(Roles =
            $"{RoleCodeConstant.ManagerCode}," +
            $"{RoleCodeConstant.CoachCode}," +
            $"{RoleCodeConstant.PresidentCode}")]
        public async Task<IActionResult> FilterParents([FromQuery] ParentFilterDto filter)
		{
			return ApiResponseHelper.HandleApiResponse(await _parentService.FilterParentsAsync(filter));
		}

	}
}
