namespace BasketballAcademyManagementSystemAPI.Application.DTOs
{
	public class VerifyEmailDto
	{

		public string Email { get; set; }
		public string Code { get; set; }
		public string Purpose { get; set; }
		public int MemberRegistrationSessionId { get; set; }
	}
}
