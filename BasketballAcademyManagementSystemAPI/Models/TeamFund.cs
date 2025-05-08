using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class TeamFund
{
    public string TeamFundId { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int Status { get; set; }

    public string? Description { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public virtual ICollection<Expenditure> Expenditures { get; set; } = new List<Expenditure>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Team Team { get; set; } = null!;
}
