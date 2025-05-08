namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore
{
    public class TryOutScorecardDto
    {
        public int TryOutScorecardId { get; set; }
        public int PlayerRegistrationId { get; set; }
        public string MeasurementScaleCode { get; set; } = null!;
        public string MeasurementName { get; set; } = null!;
        public string Score { get; set; } = null!;
        public string? Note { get; set; }
        public string ScoredBy { get; set; } = null!;
        public DateTime ScoredAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
