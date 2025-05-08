namespace BasketballAcademyManagementSystemAPI.Application.DTOs.PlayerManagement
{
	public class PlayerAssignResultDto
	{
		public string PlayerId { get; set; } = string.Empty;
		public bool Success { get; set; }
		public string Message { get; set; } = string.Empty;
	}
}
