using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class TryOutScorecard
{
    public int TryOutScorecardId { get; set; }

    public int PlayerRegistrationId { get; set; }

    public string MeasurementScaleCode { get; set; } = null!;

    public string Score { get; set; } = null!;

    public string? Note { get; set; }

    public string ScoredBy { get; set; } = null!;

    public DateTime ScoredAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual TryOutMeasurementScale MeasurementScaleCodeNavigation { get; set; } = null!;

    public virtual PlayerRegistration PlayerRegistration { get; set; } = null!;

    public virtual User ScoredByNavigation { get; set; } = null!;
}
