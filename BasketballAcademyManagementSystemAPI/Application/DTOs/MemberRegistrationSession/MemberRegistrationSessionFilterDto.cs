namespace BasketballAcademyManagementSystemAPI.Application.DTOs.MemberRegistrationSession
{
    public class MemberRegistrationSessionFilterDto
    {
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsEnable { get; set; }
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; } = false;
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
