namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Match
{
    public class CallPlayerForMatchRequest
    {
        public int MatchId { get; set; }
        public string PlayerId { get; set; } = null!;
        public bool IsStarting { get; set; }
    }

}
