namespace BasketballAcademyManagementSystemAPI.Application.DTOs.VietQr
{
	public class VietQrRequestDto
	{
		public string AddInfo { get; set; }
		public decimal Amount { get; set; }
		public string? AccountNo { get; set; }
		public string? AccountName { get; set; }
		public string? AcqId { get; set; }
		public string? Template { get; set; }
	}
}
