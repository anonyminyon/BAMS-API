namespace BasketballAcademyManagementSystemAPI.Application.DTOs.MemberRegistrationSession
{
    public class UpdateMemberRegistrationSessionRequestDto
    {
        public string RegistrationName { get; set; } = string.Empty!;
        public string? Description { get; set; }
        public string StartDate { get; set; } = null!;
        public string EndDate { get; set; } = null!;
        public bool IsAllowPlayerRecruit { get; set; }
        public bool IsAllowManagerRecruit { get; set; }
        public bool IsEnable { get; set; }
    }
}
