using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class Player
{
    public string UserId { get; set; } = null!;

    public string? ParentId { get; set; }

    public string? TeamId { get; set; }

    public string? RelationshipWithParent { get; set; }

    public decimal? Weight { get; set; }

    public decimal? Height { get; set; }

    public string? Position { get; set; }

    public int? ShirtNumber { get; set; }

    public DateOnly ClubJoinDate { get; set; }

    public virtual ICollection<MatchLineup> MatchLineups { get; set; } = new List<MatchLineup>();

    public virtual Parent? Parent { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Team? Team { get; set; }

    public virtual User User { get; set; } = null!;
}
