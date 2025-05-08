using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration
{
	public class PlayerRegistrationDto
	{
		public int PlayerRegistrationId { get; set; }
		public int MemberRegistrationSessionId { get; set; }
		public string FullName { get; set; }
		public string GenerationAndSchoolName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public bool Gender { get; set; }
		public DateOnly DateOfBirth { get; set; }
		public decimal Height { get; set; }
		public decimal Weight { get; set; }
		public string FacebookProfileURL { get; set; }
		public string KnowledgeAboutAcademy { get; set; }
		public string ReasonToChooseUs { get; set; }
		public string Position { get; set; }
		public string Experience { get; set; }
		public string Achievement { get; set; }
		public string? ParentName { get; set; }
		public string? ParentPhoneNumber { get; set; }
		public string? ParentEmail { get; set; }
		public string? RelationshipWithParent { get; set; }
		public string? ParentCitizenId { get; set; }
		public string? TryOutNote { get; set; }
	
	}
}
