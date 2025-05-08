using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.SePay;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
	[Route("api/webhooks/sepay")]
	[ApiController]
	public class WebhookController : ControllerBase
	{
		private readonly ITeamFundService _teamFundService;

		public WebhookController(ITeamFundService teamFundService)
		{
			_teamFundService = teamFundService;
		}

		[HttpPost]
		public async Task<IActionResult> ReceiveAsync([FromBody] SePayWebhookDto data)
		{
			if (data == null)
			{
				var response = new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Message = "No data received."
				};
				return ApiResponseHelper.HandleApiResponse(response);
			}
			return ApiResponseHelper.HandleApiResponse(await _teamFundService.UpdateStatusPaymentAutoSepay(data));
		}

		[HttpPost("webhook-manual-test")]
		public async Task<IActionResult> ManualTest([FromBody] SePayWebhookDto data)
		{
			if (data == null)
			{
				var response = new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Message = "No data received."
				};
				return ApiResponseHelper.HandleApiResponse(response);
			}

			return ApiResponseHelper.HandleApiResponse(await _teamFundService.UpdateStatusPaymentAutoSepay(data));
		}
	}
}
