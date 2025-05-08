namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Team
{
	public class PlayerDto
	{
		public string UserId { get; set; }
		public string Fullname { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string Phone { get; set; } = null!;
		public string? TeamId { get; set; }
		public string? TeamName { get; set; }
		public string? ProfileImage { get; set; }
		public string Address { get; set; } = null!;

		public DateOnly? DateOfBirth { get; set; }
		public decimal? Weight { get; set; }
		public decimal? Height { get; set; }
		public string Position { get; set; }
		public int? ShirtNumber { get; set; }
		public string? RelationshipWithParent { get; set; }
		public DateOnly? ClubJoinDate { get; set; }
	}
}
