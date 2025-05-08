using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Payment;
using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.Spreadsheet;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IPaymentService
    {
        Task<ApiMessageModelV2<PagedResponseDto<PaymentDto>>> GetPayments(PaymentFilterDto filter);
        Task<ApiMessageModelV2<PagedResponseDto<PaymentDto>>> GetPaymentOfMyChild(PaymentFilterDto filter);
        Task<ApiMessageModelV2<PaymentDetailDto>> GetPaymentDetail(MyDetailPaymentRequestDto request);
        Task<ApiMessageModelV2<PagedResponseDto<PaymentDto>>> GetListPlayerNotPaymentYet(PaymentFilterDto filter);
        //logic hàm này giống hệt GetPayments nhưng truyền vào status not paid và lấy tất bất kể userId là gì
        Task<ApiMessageModelV2<PagedResponseDto<PaymentDto>>> GetPaymentsOfATeam(PaymentFilterDto filter, string teamId);
        Task SendDueDateReminderEmailsAsync();
    }
}
