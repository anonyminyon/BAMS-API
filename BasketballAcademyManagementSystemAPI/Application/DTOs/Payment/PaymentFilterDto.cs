namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Payment
{
    public class PaymentFilterDto
    {
        public string? UserId { get; set; }
        public string? PaymentId { get; set; }
        public string? TeamFundId { get; set; }
        public int? Status { get; set; }
        public int? PaymentMethod { get; set; }
        public DateTime? PaidDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Note { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
