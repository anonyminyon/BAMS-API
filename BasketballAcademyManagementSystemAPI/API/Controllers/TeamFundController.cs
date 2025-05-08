using BasketballAcademyManagementSystemAPI.Application.DTOs.TeamFund;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Payment.PaymentItems;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Payment;
using BasketballAcademyManagementSystemAPI.Models;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
    [Route("api/team-fund")]
    [ApiController]
    public class TeamFundController : ControllerBase
    {
        private readonly ITeamFundService _teamFundService;
        private readonly IVietQRService _vietQRService;

        public TeamFundController(ITeamFundService teamFundService, IVietQRService vietQRService)
        {
            _teamFundService = teamFundService;
            _vietQRService = vietQRService;
        }

        
        [Authorize(Roles = $"{RoleCodeConstant.ManagerCode}")]
        [HttpPost("add-expenditure-has-user-id")]
        public async Task<IActionResult> AddExpenditures([FromBody] IEnumerable<CreateExpenditureHasListUserIdDto> expenditures, [FromQuery] string teamFundId)
        {
            var response = await _teamFundService.AddExpendituresAsync(expenditures, teamFundId);
            return ApiResponseHelper.HandleApiResponse(response);
        }

        [Authorize(Roles = $"{RoleCodeConstant.ManagerCode}")]
        [HttpPut("edit-expenditure-has-user-id")]
        public async Task<IActionResult> UpdateExpenditures([FromBody] IEnumerable<UpdateExpenditureHasListUserIdDto> expenditures, [FromQuery] string teamFundId)
        {
            var response = await _teamFundService.UpdateExpendituresAsync(expenditures, teamFundId);
            return ApiResponseHelper.HandleApiResponse(response);
        }

        [Authorize(Roles = $"{RoleCodeConstant.ManagerCode},{RoleCodeConstant.PresidentCode}")]
        [HttpGet("list-expenditure")]
        public async Task<IActionResult> GetExpenditures([FromQuery] string? teamFundId, [FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var response = await _teamFundService.GetExpendituresAsync(teamFundId, pageNumber ?? 1, pageSize ?? 10);
            return ApiResponseHelper.HandleApiResponse(response);
        }
        
        [Authorize(Roles = $"{RoleCodeConstant.ManagerCode}")]
        [HttpDelete("delete-expenditure")]
        public async Task<IActionResult> DeleteAExpenditure([FromQuery] string? expenditureId)
        {
            var response = await _teamFundService.DeleteExpendituresAsync(expenditureId);
            return ApiResponseHelper.HandleApiResponse(response);
        }

        [Authorize(Roles = $"{RoleCodeConstant.ManagerCode}")]
        [HttpPost("delete-player-expenditure")]
        public async Task<IActionResult> DeletePlayerExpenditure([FromBody] DeletePlayerExpenditureDto dto)
        {
            var response = await _teamFundService.DeletePlayerInExpendituresAsync(dto.ExpenditureId, dto.UserId);
            return ApiResponseHelper.HandleApiResponse(response);
        }
        
        [Authorize(Roles = $"{RoleCodeConstant.ManagerCode}")]
        [HttpPost("add-player-expenditure")]
        public async Task<IActionResult> AddPlayerExpenditure([FromBody] AddPlayerExpenditureDto dto)
        {
            var response = await _teamFundService.AddPlayersToExpenditureAsync(dto.ExpenditureId, dto.UserIds);
            return ApiResponseHelper.HandleApiResponse(response);
        }

        [HttpPost("test-auto-create")]
        public async Task<IActionResult> TestAutoCreate()
        {
            await _teamFundService.AutoCreateTeamFundsAsync(true);
            return Ok(new
            {
                Success = true,
                Message = "Đã gọi hàm tự động tạo TeamFund (bỏ qua ngày)."
            });
        }

		[Authorize(Roles = $"{RoleCodeConstant.PresidentCode}")]
		[HttpPost("approve-team-fund")]
        public async Task<IActionResult> ApprovedTeamFund([FromBody] TeamFundRequest request)
        {
            return ApiResponseHelper.HandleApiResponse(await _teamFundService.ApproveTeamFundAsync(request.TeamFundId));
        }

        [Authorize]
		[HttpGet("team-fund-list")]
        public async Task<IActionResult> GetTeamFunds([FromQuery] TeamFundFilterDto filter)
		{
			return ApiResponseHelper.HandleApiResponse(await _teamFundService.GetTeamFundsAsync(filter));
		}

		[Authorize]
		[HttpPost("generate-qr-by-paymentId")]
        public async Task<IActionResult> GenerateQr([FromBody] GenerateQrRequest request)
        {
            return ApiResponseHelper.HandleApiResponse(await _vietQRService.GenerateQrAsync(request.PaymentId));
        }

		[Authorize]
		[HttpGet("get-manager-payment-type-by-paymentid")]
		public async Task<IActionResult> GetPaymentManagerBankInfor([FromQuery] string payment)
		{
			return ApiResponseHelper.HandleApiResponse(await _teamFundService.GetManagerBankInfor(payment));
		}

		[HttpPut("update-payment-status")]
		[Authorize]
		public async Task<IActionResult> UpdatePaymentStatus([FromBody] UpdatePaymentStatusRequest payment)
		{
			return ApiResponseHelper.HandleApiResponse(await _teamFundService.UpdateStatusPayment(payment));
		}

		[HttpGet("players-by-date/{teamId}")]
        [Authorize]
		public async Task<IActionResult> GetPlayersByTeamAndDate(string teamId, [FromQuery] DateTime targetDate)
		{
			return ApiResponseHelper.HandleApiResponse(await _teamFundService.GetPlayerUserIdsByTeamAndDateAsync(teamId, targetDate));
		}

		[HttpPost("reject-teamfund")]
		[Authorize(Roles = $"{RoleCodeConstant.PresidentCode}")]
		public async Task<IActionResult> RejectTeamFund([FromBody] RejectTeamFundDto request)
		{
			return ApiResponseHelper.HandleApiResponse(await _teamFundService.RejectTeamFund(request.TeamFundId, request.ReasonReject));
		}
	}
}
