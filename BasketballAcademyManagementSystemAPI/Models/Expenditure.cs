using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class Expenditure
{
    public string ExpenditureId { get; set; } = null!;

    public string TeamFundId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateOnly Date { get; set; }

    public string ByManagerId { get; set; } = null!;

    public DateTime? PayoutDate { get; set; }

    public string? UsedByUserId { get; set; }

    public virtual Manager ByManager { get; set; } = null!;

    public virtual TeamFund TeamFund { get; set; } = null!;
}
