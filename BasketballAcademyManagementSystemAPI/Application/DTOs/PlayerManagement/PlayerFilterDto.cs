namespace BasketballAcademyManagementSystemAPI.Application.DTOs.PlayerManagement
{
	public class PlayerFilterDto
	{
		public string? Fullname { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public string? TeamId { get; set; }
		public bool? Gender { get; set; }
		public bool? OnlyNoTeam { get; set; }
		public bool? IsEnable { get; set; }

		public int PageIndex { get; set; } = 1;
		public int PageSize { get; set; } = 10;
	}
}
