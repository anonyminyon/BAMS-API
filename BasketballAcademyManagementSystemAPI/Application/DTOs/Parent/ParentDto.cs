using BasketballAcademyManagementSystemAPI.Application.DTOs.Team;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Parent
{
	public class ParentDto
	{
		public string UserId { get; set; }
		public string Fullname { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string PlayerOfParent { get; set; }
		public string CitizenId { get; set; }
		public string Address { get; set; }
		public DateOnly? DateOfBirth { get; set; }
	
	}
}
