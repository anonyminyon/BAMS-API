namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Team
{
	public class TeamDto
	{

		public string? TeamId { get; set; } = null!;

		public string? TeamName { get; set; } = null!;

		public int? Status { get; set; }
		public DateTime? CreateAt { get; set; }
		

	}
}
