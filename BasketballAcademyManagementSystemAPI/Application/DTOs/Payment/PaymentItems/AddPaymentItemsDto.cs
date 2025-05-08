namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Payment.PaymentItems
{
	public class AddPaymentItemsDto
	{
		public string PaymentId { get; set; }
		public string PaidItemName { get; set; }
		public decimal Amount { get; set; }
		public string Note { get; set; }
	}
}
