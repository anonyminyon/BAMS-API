namespace BasketballAcademyManagementSystemAPI.Application.DTOs.ClubContact
{
    public class UpdateClubContactDto
    {
        public int ContactMethodId { get; set; }

        public string ContactMethodName { get; set; } = null!;

        public string MethodValue { get; set; } = null!;

        public string IconUrl { get; set; } = null!;
    }
}
