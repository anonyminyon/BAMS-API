using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Payment;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface IPaymentRepository
    {
        Task<bool> HasAccessToPaymentDetailAsync(string userId, string paymentId, string roleCode);
        Task<PagedResponseDto<PaymentDto>> GetPaymentAsync(PaymentFilterDto filter);
        Task<PaymentDetailDto> GetPaymentDetailAsync(MyDetailPaymentRequestDto request);
        Task<PagedResponseDto<PaymentDto>> GetPaymentByTeamIdAsync(PaymentFilterDto filter, string teamId);
        Task<PagedResponseDto<PaymentDto>> GetPaymentByListUserIdAsync(PaymentFilterDto filter, List<string> playerId);
        Task<bool> AnyPaymentById(string paymentId);
        Task<List<Payment>> GetPaymentsDueOnDateAsync(DateTime dueDate);
        //===========Phần này của tuấn anh==========
        Task UpdatePaymentAsync(Payment payment);
        Task<Payment?> GetPaymentByIdAsync(string paymentId);
	}
}
