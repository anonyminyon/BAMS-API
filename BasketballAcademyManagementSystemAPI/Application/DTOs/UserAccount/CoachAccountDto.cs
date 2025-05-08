using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount
{
    public class CoachAccountDto
    {
        public string UserId { get; set; } = null!;

        public string? TeamId { get; set; }

        public string? TeamName { get; set; }

        public string CreatedByPresidentId { get; set; } = null!;

        public string Bio { get; set; } = null!;

        public string ContractStartDate { get; set; } = null!;

        public string ContractEndDate { get; set; } = null!;

        public string CreatedByPresident { get; set; } = null!;
    }
}
