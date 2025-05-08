using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class TryOutScoreCriterion
{
    public int ScoreCriteriaId { get; set; }

    public string MeasurementScaleCode { get; set; } = null!;

    public string CriteriaName { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public bool Gender { get; set; }

    public virtual TryOutMeasurementScale MeasurementScaleCodeNavigation { get; set; } = null!;

    public virtual ICollection<TryOutScoreLevel> TryOutScoreLevels { get; set; } = new List<TryOutScoreLevel>();
}
