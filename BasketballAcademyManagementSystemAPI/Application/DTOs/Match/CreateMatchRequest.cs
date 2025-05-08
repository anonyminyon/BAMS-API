namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Match
{
    public class CreateMatchRequest
    {
        public string? MatchName { get; set; }
        public DateTime MatchDate { get; set; }
        public string? HomeTeamId { get; set; }
        public string? AwayTeamId { get; set; }
        public string? OpponentTeamName { get; set; }
        public string? CourtId { get; set; }
    }
}
