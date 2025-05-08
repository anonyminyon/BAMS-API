namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore
{
    public class PlayerSkillScoreReportDto
    {
        public int PlayerRegistrationId { get; set; }
        public int CandidateNumber { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public decimal AverageBasketballSkill { get; set; }
        public decimal AveragePhysicalFitness { get; set; }
        public decimal OverallAverage { get; set; }
        public List<SkillDetailDto> ScoreList { get; set; }
    }
}
