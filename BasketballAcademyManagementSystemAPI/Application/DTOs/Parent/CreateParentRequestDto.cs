namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Parent
{
	public class CreateParentRequestDto
	{
		public string Fullname { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public DateOnly? DateOfBirth { get; set; }
		public string CitizenId { get; set; }
		public string? ProfileImage { get; set; }
		public string PlayerId { get; set; }
	}
}
