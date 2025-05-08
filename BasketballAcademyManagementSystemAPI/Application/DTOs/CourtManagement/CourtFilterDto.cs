using BasketballAcademyManagementSystemAPI.Common.Enums;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs.CourtManagement
{
    public class CourtFilterDto
    {
        public string? CourtName { get; set; }
        public string? Address { get; set; }
        public string? Contact { get; set; }
        public int? Status { get; set; }
        public string? Type { get; set; }
        public string? Kind { get; set; }
        // Sử dụng enum, ở đây cho dù đưa cho nó 1,2,3 hay
        // UsagePurpose=Compete or UsagePurpose=Training or UsagePurpose=CompeteAndTraining đều có thể lọc được
        public CourtUsagePurpose? UsagePurpose { get; set; } 
        public decimal? MinRentPricePerHour { get; set; }
        public decimal? MaxRentPricePerHour { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}