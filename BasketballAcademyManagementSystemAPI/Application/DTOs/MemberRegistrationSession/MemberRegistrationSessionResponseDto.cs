namespace BasketballAcademyManagementSystemAPI.Application.DTOs.MemberRegistrationSession
{
    public class MemberRegistrationSessionResponseDto
    {
        public int Id { get; set; }
        public string RegistrationName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAllowPlayerRecruit { get; set; }
        public bool IsAllowManagerRecruit { get; set; }
        public string CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public bool IsEnable { get; set; }
    }
}
