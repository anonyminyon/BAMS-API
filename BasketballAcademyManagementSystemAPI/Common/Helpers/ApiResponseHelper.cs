using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademyManagementSystemAPI.Common.Helpers
{
	public static class ApiResponseHelper
	{
		public static IActionResult HandleApiResponse<T>(ApiMessageModelV2<T> response)
		{
			return response.Status == ApiResponseStatusConstant.SuccessStatus
				? new OkObjectResult(response)
				: new BadRequestObjectResult(response);
		}
	}
}
