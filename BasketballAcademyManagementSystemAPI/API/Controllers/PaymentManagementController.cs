using BasketballAcademyManagementSystemAPI.Application.DTOs.Payment;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
    [Route("api/payment-management")]
    [ApiController]
    public class PaymentManagementController : ControllerBase
    {
        private IPaymentService _paymentService;

        public PaymentManagementController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("team")]
        [Authorize(Roles = $"{RoleCodeConstant.ManagerCode},{RoleCodeConstant.CoachCode},{RoleCodeConstant.PresidentCode}")]
        // Lấy ra payment thuộc team thằng coach hoặc manager đang quản lý, nên phải truyền vào teamId
        public async Task<IActionResult> GetTeamPayments([FromQuery] PaymentFilterDto filter, [FromQuery] string teamId)
        {
            var response = await _paymentService.GetPaymentsOfATeam(filter, teamId);
            return ApiResponseHelper.HandleApiResponse(response);
        }

        [HttpGet("my-payment")]
        public async Task<IActionResult> GetMyPayment([FromQuery] PaymentFilterDto filter)
        {
            var response = await _paymentService.GetPayments(filter);
            return ApiResponseHelper.HandleApiResponse(response);
        }

        [HttpGet("payment-detail")]
        [Authorize]
        public async Task<IActionResult> GetMyPaymentDetail([FromQuery] MyDetailPaymentRequestDto request)
        {
            var response = await _paymentService.GetPaymentDetail(request);
            return ApiResponseHelper.HandleApiResponse(response);
        }

        [HttpGet("require-payments")]
        [Authorize(Roles = $"{RoleCodeConstant.ManagerCode},{RoleCodeConstant.CoachCode},{RoleCodeConstant.PresidentCode}")]
        public async Task<IActionResult> GetPendingPayments([FromQuery] PaymentFilterDto filter)
        {
            var response = await _paymentService.GetListPlayerNotPaymentYet(filter);
            return ApiResponseHelper.HandleApiResponse(response);
        }

        [HttpGet("my-child-payment")]
        // Xem luôn require-payment và history payment của các con thông qua api này và lọc bằng filter trong filter này có status trả tiền hay chưa trả
        [Authorize(Roles = $"{RoleCodeConstant.ParentCode}")] // Thêm role parent
        public async Task<IActionResult> GetMyChildPaymentHistory([FromQuery] PaymentFilterDto filter)
        {
            var response = await _paymentService.GetPaymentOfMyChild(filter);
            return ApiResponseHelper.HandleApiResponse(response);
        }
    }
}
