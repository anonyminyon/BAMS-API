using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class UserTeamHistory
{
    public string UserId { get; set; } = null!;

    public int UserTeamHistoryId { get; set; }

    public string TeamId { get; set; } = null!;

    public DateTime JoinDate { get; set; }

    public DateTime? LeftDate { get; set; }

    public string? Note { get; set; }

    public string? RemovedByUserId { get; set; }

    public virtual User? RemovedByUser { get; set; }

    public virtual Team Team { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
