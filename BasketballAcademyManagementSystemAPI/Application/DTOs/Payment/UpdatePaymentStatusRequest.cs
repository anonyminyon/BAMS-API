namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Payment
{
	public class UpdatePaymentStatusRequest
	{
		public string PaymentId { get; set; } = null!;
		public int Status { get; set; }
		public DateTime? PaidDate { get; set; }
		public int? PaymentMethod { get; set; }
	}
}
