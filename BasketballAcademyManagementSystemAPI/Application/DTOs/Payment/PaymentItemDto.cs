namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Payment
{
    public class PaymentItemDto
    {
        public int PaymentItemId { get; set; }

        public string PaymentId { get; set; } = null!;

        public string PaidItemName { get; set; } = null!;

        public decimal Amount { get; set; }

        public string Note { get; set; } = null!;
    }
}
