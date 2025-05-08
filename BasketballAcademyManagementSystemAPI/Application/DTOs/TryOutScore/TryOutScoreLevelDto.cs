namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore
{
    public class TryOutScoreLevelDto
    {
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public string ScoreLevel { get; set; } = null!;
        public string? FiveScaleScore { get; set; }
    }
}
