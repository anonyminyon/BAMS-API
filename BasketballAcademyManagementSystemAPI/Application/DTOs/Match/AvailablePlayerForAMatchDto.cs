namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Match
{
    public class AvailablePlayerForAMatchDto
    {
        public string UserId { get; set; }
        public string? TeamId { get; set; }
        public string PlayerName { get; set; } = null!;
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public string Position { get; set; }
        public int? ShirtNumber { get; set; }
        public DateOnly? ClubJoinDate { get; set; }
    }
}
