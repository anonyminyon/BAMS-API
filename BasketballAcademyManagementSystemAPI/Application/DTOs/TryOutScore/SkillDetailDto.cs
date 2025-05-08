namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore
{
    public class SkillDetailDto
    {
        public string MeasurementScaleCode { get; set; }
        public string MeasurementName { get; set; }
        public int SortOrder { get; set; }
        public decimal Score { get; set; }
        public decimal AverageScore { get; set; }
    }
}
