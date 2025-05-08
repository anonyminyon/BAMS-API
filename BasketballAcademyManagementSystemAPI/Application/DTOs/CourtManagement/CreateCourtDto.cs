namespace BasketballAcademyManagementSystemAPI.Application.DTOs.CourtManagement
{
    public class CreateCourtDto
    {
        public string? CourtName { get; set; }
        public decimal? RentPricePerHour { get; set; }
        public string? Address { get; set; }
        public string? Contact { get; set; }
        public string? Type { get; set; }
        public string? Kind { get; set; }
        public string? ImageUrl { get; set; }
        public int? UsagePurpose { get; set; }
    }
}
