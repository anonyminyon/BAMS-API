namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore
{
    public class PlayerScoreInputDto
    {
        public int PlayerRegistrationId { get; set; }
        public List<AddTryOutScorecardDto> Scores { get; set; } = new();
    }
}
