namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Parent
{
	public class ParentFilterDto
	{
		public string? Fullname { get; set; }
		public string? Email { get; set; }
		public string? CitizenId { get; set; }
		public bool OnlyUnassigned { get; set; } = false;
	}
}
