using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class TryOutMeasurementScale
{
    public string MeasurementScaleCode { get; set; } = null!;

    public string MeasurementName { get; set; } = null!;

    public string? Content { get; set; }

    public string? Duration { get; set; }

    public string? Location { get; set; }

    public string? Description { get; set; }

    public string? Equipment { get; set; }

    public string? MeasurementScale { get; set; }

    public string? ParentMeasurementScaleCode { get; set; }

    public int SortOrder { get; set; }

    public virtual ICollection<TryOutMeasurementScale> InverseParentMeasurementScaleCodeNavigation { get; set; } = new List<TryOutMeasurementScale>();

    public virtual TryOutMeasurementScale? ParentMeasurementScaleCodeNavigation { get; set; }

    public virtual ICollection<TryOutScoreCriterion> TryOutScoreCriteria { get; set; } = new List<TryOutScoreCriterion>();

    public virtual ICollection<TryOutScorecard> TryOutScorecards { get; set; } = new List<TryOutScorecard>();
}
