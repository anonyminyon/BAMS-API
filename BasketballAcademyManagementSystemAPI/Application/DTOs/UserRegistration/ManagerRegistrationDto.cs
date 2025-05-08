namespace BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration
{
    public class ManagerRegistrationDto
    {
        public int? ManagerRegistrationId { get; set; }

        public int MemberRegistrationSessionId { get; set; } 

        public string FullName { get; set; } = null!;

        public string GenerationAndSchoolName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string FacebookProfileUrl { get; set; } = null!;

        public string KnowledgeAboutAcademy { get; set; } = null!;

        public string ReasonToChooseUs { get; set; } = null!;

        public string KnowledgeAboutAmanager { get; set; } = null!;

        public string ExperienceAsAmanager { get; set; } = null!;

        public string Strength { get; set; } = null!;

        public string WeaknessAndItSolution { get; set; } = null!;

        public string? Status { get; set; } = null!;

        public string? SubmitedDate { get; set; } = null!;
    }
}
