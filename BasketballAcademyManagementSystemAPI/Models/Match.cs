using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class Match
{
    public int MatchId { get; set; }

    public string MatchName { get; set; } = null!;

    public DateTime MatchDate { get; set; }

    public string? HomeTeamId { get; set; }

    public string? AwayTeamId { get; set; }

    public string? OpponentTeamName { get; set; }

    public string CourtId { get; set; } = null!;

    public int ScoreHome { get; set; }

    public int ScoreAway { get; set; }

    public int Status { get; set; }

    public string CreatedByCoachId { get; set; } = null!;

    public virtual Team? AwayTeam { get; set; }

    public virtual Court Court { get; set; } = null!;

    public virtual Coach CreatedByCoach { get; set; } = null!;

    public virtual Team? HomeTeam { get; set; }

    public virtual ICollection<MatchArticle> MatchArticles { get; set; } = new List<MatchArticle>();

    public virtual ICollection<MatchLineup> MatchLineups { get; set; } = new List<MatchLineup>();
}
