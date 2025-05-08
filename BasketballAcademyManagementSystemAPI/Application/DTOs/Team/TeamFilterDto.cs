namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Team
{
	public class TeamFilterDto
	{
		public string? TeamName { get; set; }
		public int? Status { get; set; }
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;
	}
}
