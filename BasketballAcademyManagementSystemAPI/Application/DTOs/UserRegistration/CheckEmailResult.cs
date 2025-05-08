namespace BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration
{
	public class CheckEmailResult
	{
		public bool Exists { get; set; }
		public string Message { get; set; }
		public string Type { get; set; } // User hoặc Registration
	}
}
