namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession
{
    public class TrainingSessionDto
    {
        public string TrainingSessionId { get; set; }
        public string TeamId { get; set; }
        public string TeamName { get; set; }
        public string? PlayerId { get; set; }
        public string? PlayerName { get; set; }
        public string CourtId { get; set; }
        public string CourtName { get; set; }
        public string CourtAddress { get; set; }
        public string CourtContact { get; set; }
        public decimal CourtPrice { get; set; }
        public DateOnly ScheduledDate { get; set; }
        public TimeOnly ScheduledStartTime { get; set; }
        public TimeOnly ScheduledEndTime { get; set; }
        public DateTime? CreatedAt { get; set; } = null;
        public int RequestRemainingTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Note { get; set; }
    }
}
