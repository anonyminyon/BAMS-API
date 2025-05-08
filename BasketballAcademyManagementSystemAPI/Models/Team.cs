using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class Team
{
    public string TeamId { get; set; } = null!;

    public string TeamName { get; set; } = null!;

    public int Status { get; set; }

    public DateTime CreateAt { get; set; }

    public string? FundManagerId { get; set; }

    public virtual ICollection<Coach> Coaches { get; set; } = new List<Coach>();

    public virtual User? FundManager { get; set; }

    public virtual ICollection<Manager> Managers { get; set; } = new List<Manager>();

    public virtual ICollection<Match> MatchAwayTeams { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchHomeTeams { get; set; } = new List<Match>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual ICollection<TeamFund> TeamFunds { get; set; } = new List<TeamFund>();

    public virtual ICollection<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();

    public virtual ICollection<UserTeamHistory> UserTeamHistories { get; set; } = new List<UserTeamHistory>();
}
