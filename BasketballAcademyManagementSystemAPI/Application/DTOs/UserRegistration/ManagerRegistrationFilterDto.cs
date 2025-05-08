using System;
using System.ComponentModel.DataAnnotations;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration
{
    public class ManagerRegistrationFilterDto
    {
        public int? ManagerRegistrationId { get; set; }
        public int? MemberRegistrationSessionId { get; set; }
        public string? FullName { get; set; }
        public string? GenerationAndSchoolName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? FacebookProfileUrl { get; set; }
        public string? KnowledgeAboutAcademy { get; set; }
        public string? ReasonToChooseUs { get; set; }
        public string? KnowledgeAboutAmanager { get; set; }
        public string? ExperienceAsAmanager { get; set; }
        public string? Strength { get; set; }
        public string? WeaknessAndItSolution { get; set; }
        public string? Status { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? SubmitedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
