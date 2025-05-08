using BasketballAcademyManagementSystemAPI.Common.Constants;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Match
{
    public class MatchDetailDto
    {
        public int MatchId { get; set; }
        public string MatchName { get; set; }
        public DateOnly ScheduledDate { get; set; }
        public TimeOnly ScheduledStartTime { get; set; }
        public TimeOnly ScheduledEndTime { get; set; }
        public string? HomeTeamId { get; set; }
        public string? HomeTeamName { get; set; }
        public int ScoreHome { get; set; }
        public string? AwayTeamId { get; set; }
        public string? AwayTeamName { get; set; }
        public int ScoreAway { get; set; }
        public string Status { get; set; } = MatchConstant.Status.GetStatusName(-1);
        public string CourtId { get; set; }
        public string CourtName { get; set; }
        public string CourtAddress { get; set; }
        public string CreatedByCoachId { get; set; }
        public List<PlayerInLineUpDto> HomeTeamPlayers { get; set; } = new();
        public List<PlayerInLineUpDto> AwayTeamPlayers { get; set; } = new();
        public List<MatchArticleDto> MatchArticles { get; set; } = new();
    }

    public class PlayerInLineUpDto
    {
        public string UserId { get; set; }
        public string? TeamId { get; set; }
        public string PlayerName { get; set; }
        public string? Position { get; set; }
        public int? ShirtNumber { get; set; }
        public bool? IsStarting { get; set; }
    }

    public class MatchArticleDto
    {
        public int ArticleId { get; set; }
        public string Url { get; set; } = null!;
        public string ArticleType { get; set; } = null!;
        public string Title { get; set; } = null!;

    }
}
