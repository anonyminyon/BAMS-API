using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs.CoachManagement
{
    public class UpdateCoachDto
    {
        public string UserId { get; set; } = null!;

        public string? Username { get; set; } = null!;

        public string? Fullname { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public string? ProfileImage { get; set; }

        public string? Phone { get; set; } = null!;

        public string? Address { get; set; } = null!;

        public string? DateOfBirth { get; set; }

        //attribute from Coach table

        public string? Bio { get; set; }

        public string? ContractStartDate { get; set; }

        public string? ContractEndDate { get; set; }

    }
}
