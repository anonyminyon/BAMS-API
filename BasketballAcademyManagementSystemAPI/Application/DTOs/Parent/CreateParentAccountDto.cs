namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Parent
{
	public class CreateParentAccountDto
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Fullname { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public DateOnly? DateOfBirth { get; set; }
		public string CitizenId { get; set; }
		public string CreatedByManagerId { get; set; }
	}
}
