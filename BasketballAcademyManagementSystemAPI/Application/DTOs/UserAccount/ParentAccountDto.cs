namespace BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount
{
    public class ParentAccountDto
    {
        public string UserId { get; set; } = null!;

        public string CitizenId { get; set; } = null!;

        public string CreatedByManagerId { get; set; } = null!;
    }
}
