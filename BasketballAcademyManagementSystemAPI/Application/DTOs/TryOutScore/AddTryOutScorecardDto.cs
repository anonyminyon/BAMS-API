namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore
{
    public class AddTryOutScorecardDto
    {
        public string SkillCode { get; set; } = null!;
        public string Score { get; set; } = null!;
        public string? Note { get; set; }
    }
}
