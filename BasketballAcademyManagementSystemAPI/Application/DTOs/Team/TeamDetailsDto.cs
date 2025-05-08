namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Team
{
	public class TeamDetailsDto
	{
		public string TeamId { get; set; }
		public string TeamName { get; set; }
		public int Status { get; set; }
		public DateTime CreateAt { get; set; }
		public string? FundManagerId { get; set; }
		public string? FundManagerName { get; set; }

		public List<CoachDto>? Coaches { get; set; }
		public List<ManagerDto>? Managers { get; set; }
		public List<PlayerDto>? Players { get; set; }

	}
}
