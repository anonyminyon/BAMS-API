namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore
{
    public class PlayerTryOutScoreCardDto
    {
        public int PlayerRegistrationId { get; set; }
        public int? CandidateNumber { get; set; }
        public string FullName { get; set; } = null!;
        public bool Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public List<TryOutScorecardDto> Scores { get; set; } = new();
    }
}
