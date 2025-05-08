namespace BasketballAcademyManagementSystemAPI.Application.DTOs.PlayerManagement
{
	public class PlayerResponse
	{
		public string UserId { get; set; }
		public string Fullname { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public DateOnly? DateOfBirth { get; set; }
		public string RoleCode { get; set; }
		public bool IsEnable { get; set; }
		public bool? Gender { get; set; }
		public string? ParentId { get; set; }
		public string? TeamId { get; set; }
		public string? TeamName { get; set; }  // ✅ Thêm dòng này
		public string? RelationshipWithParent { get; set; }
		public decimal? Weight { get; set; }
		public decimal? Height { get; set; }
		public string? Position { get; set; }
		public int? ShirtNumber { get; set; }
		public DateOnly ClubJoinDate { get; set; }
	}
}
