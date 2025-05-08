namespace BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration
{
	public class PlayerSubmittedFormDto
	{
		public int PlayerRegistrationId { get; set; }
		public string FullName { get; set; } = null!;

		public string GenerationAndSchoolName { get; set; } = null!;

		public string PhoneNumber { get; set; } = null!;

		public string Email { get; set; } = null!;

		public bool Gender { get; set; }

		public DateOnly DateOfBirth { get; set; }

		public decimal Height { get; set; }

		public decimal Weight { get; set; }

		public string FacebookProfileUrl { get; set; } = null!;

		public string KnowledgeAboutAcademy { get; set; } = null!;

		public string ReasonToChooseUs { get; set; } = null!;

		public string Position { get; set; } = null!;

		public string Experience { get; set; } = null!;

		public string Achievement { get; set; } = null!;

		public string? ParentName { get; set; }

		public string? ParentPhoneNumber { get; set; }

		public string? ParentEmail { get; set; }

		public string? RelationshipWithParent { get; set; }

		public string? ParentCitizenId { get; set; }

		public int? CandidateNumber { get; set; }

		public string? TryOutNote { get; set; }
		public string Status { get; set; } = null!;

		public DateTime SubmitedDate { get; set; }
		public DateTime? TryOutDate { get; set; }
		public string? TryOutLocation { get; set; }

		//Trường dữ liệu mùa tuyển quân
		public int MemberRegistrationSessionId { get; set; }
		public string RegistrationName { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}
