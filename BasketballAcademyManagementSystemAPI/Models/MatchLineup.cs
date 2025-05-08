using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class MatchLineup
{
    public int LineupId { get; set; }

    public int MatchId { get; set; }

    public bool IsStarting { get; set; }

    public string PlayerId { get; set; } = null!;

    public virtual Match Match { get; set; } = null!;

    public virtual Player Player { get; set; } = null!;
}
