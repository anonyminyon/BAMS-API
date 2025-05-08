using BasketballAcademyManagementSystemAPI.Application.DTOs.EmailVerification;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutNote;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using static BasketballAcademyManagementSystemAPI.Common.Messages.AuthenticationMessage;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using BasketballAcademyManagementSystemAPI.Application.DTOs.CallToTryOut;
using DocumentFormat.OpenXml;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
	[Route("api/player-registration")]
	[ApiController]
	public class PlayerRegistrationController : ControllerBase
	{
		private readonly IPlayerRegistrationService _playerRegistrationService;
		private readonly IOtpService _otpService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public PlayerRegistrationController(IPlayerRegistrationService playerRegistrationService, IOtpService otpService, IHttpContextAccessor httpContextAccessor)
		{
			_playerRegistrationService = playerRegistrationService;
			_otpService = otpService;
			_httpContextAccessor = httpContextAccessor;
		}

		//User input form
		[HttpPost("player-register")]
		public async Task<IActionResult> RegisterPlayerForm([FromBody] PlayerRegistrationDto playerRegiterDto)
		{
			return ApiResponseHelper.HandleApiResponse(await _playerRegistrationService.RegisterPlayerAsync(playerRegiterDto));
		}

		//User input form
		[HttpPut("update-player-register")]
		public async Task<IActionResult> UpdateRegisterPlayerForm([FromBody] PlayerRegistrationDto playerUpdateRegisterDto)
		{
			return ApiResponseHelper.HandleApiResponse(await _playerRegistrationService.UpdatePlayerRegisterFormAsync(playerUpdateRegisterDto));
		}


		[HttpGet("registration-list")]
		[Authorize(Roles =
			$"{RoleCodeConstant.ManagerCode}," +
			$"{RoleCodeConstant.PresidentCode}," +
            $"{RoleCodeConstant.CoachCode}")]
		public async Task<IActionResult> GetPlayerRegistrarionForms(
	
		[FromQuery] int? memberRegistrationSessionId = null,
		[FromQuery] string? email = null,
		[FromQuery] DateTime? startDate = null,
		[FromQuery] DateTime? endDate = null,
		[FromQuery] int? minAge = null,
		[FromQuery] int? maxAge = null,
		[FromQuery] bool? gender = null,
		[FromQuery] string? status = null)
		{
			return ApiResponseHelper.HandleApiResponse(await _playerRegistrationService.GetPlayers(
				memberRegistrationSessionId,email, startDate, endDate, minAge, maxAge, gender,status));
		}

		[HttpPut("{id}/tryout-note")]
		[Authorize(Roles = RoleCodeConstant.ManagerCode)]

		public async Task<IActionResult> UpdateTryOutNote(string id, [FromBody] TryOutNoteDto request)
		{
			var success = await _playerRegistrationService.AddTryOutNote(id, request.TryOutNote);
			if (!success)
				return NotFound(new { Message = "Player registration not found." });

			return Ok(new { Message = "TryOutNote updated successfully." });
		}

		[HttpPost("approve/{id}")]
		[Authorize(Roles = RoleCodeConstant.ManagerCode)]

		public async Task<IActionResult> Approve(int id)
		{
			return ApiResponseHelper.HandleApiResponse(await _playerRegistrationService.ApproveRegistrationAsync(id));
		}


		[HttpPost("call-try-out")]
		[Authorize(Roles = RoleCodeConstant.ManagerCode)]
		public async Task<IActionResult> CallToTryOutList([FromBody] CallToTryOutListRequest request)
		{
			try
			{
				var result = await _playerRegistrationService.CallToTryoutList(
					request.PlayerRegistIds,
					request.Location,
					request.TryOutDateTime
				);

				return Ok(result); // Trả về list trực tiếp
			}
			catch (Exception ex)
			{
				// Ghi log nếu có hệ thống log
				// _logger.LogError(ex, "Lỗi khi xử lý call tryout list");

				return StatusCode(StatusCodes.Status500InternalServerError, new
				{
					Message = "Đã xảy ra lỗi khi xử lý lời mời tham gia buổi đầu vào tới học viên.",
					Details = ex.Message
				});
			}
		}


		[HttpPost("reject-registration-form")]
		[Authorize(Roles = RoleCodeConstant.ManagerCode)]
		public async Task<IActionResult> Reject(int id)
		{
			return ApiResponseHelper.HandleApiResponse(await _playerRegistrationService.RejectRegistrationAsync(id));
		}

		[HttpPost("update-status-form-by-id")]
		[Authorize(Roles = RoleCodeConstant.ManagerCode)]
		public async Task<IActionResult> UpdateStatusForm(int id, string status)
		{
			return ApiResponseHelper.HandleApiResponse(await _playerRegistrationService.UpdateStatusForm(id, status));
		}

		[HttpPost("validate-and-send-otp-player-registration")]
		public async Task<IActionResult> CheckMailAndSendOtpRegistration([FromBody] EmailRegisterDto emailRegisterDto)
		{
			return ApiResponseHelper.HandleApiResponse(await _playerRegistrationService.ValidateEmailRegistrationAsync(emailRegisterDto.Email, emailRegisterDto.MemberSessionId));
		}

	}


}

