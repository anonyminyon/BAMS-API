namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore
{
    public class TryOutMeasurementScaleDto
    {
        public string MeasurementScaleCode { get; set; } = null!;
        public string MeasurementName { get; set; } = null!;
        public string? Content { get; set; }
        public string? Duration { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string? Equipment { get; set; }
        public string? MeasurementScale { get; set; }
        public int SortOrder { get; set; }

        public List<TryOutMeasurementScaleDto> SubScales { get; set; } = new List<TryOutMeasurementScaleDto>();
        public List<TryOutScoreCriterionDto>? ScoreCriteria { get; set; }
    }
}
