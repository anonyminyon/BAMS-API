namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Payment
{
    public class PaymentDetailDto
    {
        public string PaymentId { get; set; } = null!;
        public string? TeamFundId { get; set; }
        public string? TeamFundDescription { get; set; }
        public string UserId { get; set; } = null!;
        public int Status { get; set; }
        public int? PaymentMethod { get; set; }
        public DateTime? PaidDate { get; set; }
        public string? Note { get; set; } = null!;
        public string TeamName { get; set; } = null!;
        public string? Fullname { get; set; } = null!;
        public DateTime? DueDate { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public IEnumerable<PaymentItemDto> PaymentItems { get; set; } = new List<PaymentItemDto>();
        public decimal TotalAmount { get; set; } 
    }
}