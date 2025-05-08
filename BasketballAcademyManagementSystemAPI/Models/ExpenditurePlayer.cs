using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class ExpenditurePlayer
{
    public string? PlayerId { get; set; }

    public string? ExpenditureId { get; set; }

    public int ExpenditurePlayerId { get; set; }
}
