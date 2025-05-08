namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Payment
{
    public class PaymentDto
    {
        public string PaymentId { get; set; } = null!;
        public string? TeamFundId { get; set; }
        public string? TeamFundDescription { get; set; }
        public string UserId { get; set; } = null!;
        public int Status { get; set; }
        public DateTime? PaidDate { get; set; } = null!;
        public string Note { get; set; } = null!;
        public DateTime? DueDate { get; set; }
        public int? PaymentMethod { get; set; }
        //this is attribute from Team table, join from TeamFund then Team
        public string? TeamName { get; set; } = null!;
        //this is attribute from table user
        public string Fullname { get; set; } = null!;
        //this is attribute to show total money from their payment item
        public decimal TotalAmount { get; set; } = 0!;
        public DateTime? ApprovedAt { get; set; }
    }
}
