namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore
{
    public class RegistrationSessionScoresDto
    {
        public int PlayerRegistrationId { get; set; }
        public int? CandidateNumber { get; set; }
        public string FullName { get; set; } = null!;
        public bool Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public List<TryOutScorecardDto> Scores { get; set; } = new();
        public decimal BasketballSkillAverage { get; set; }
        public decimal PhysicalSkillAverage { get; set; }
        public decimal OverallAverage { get; set; }
    }
}
