using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class Parent
{
    public string UserId { get; set; } = null!;

    public string? CitizenId { get; set; }

    public string CreatedByManagerId { get; set; } = null!;

    public virtual Manager CreatedByManager { get; set; } = null!;

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    public virtual User User { get; set; } = null!;
}
