using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class TryOutScoreLevel
{
    public int ScoreLevelId { get; set; }

    public int ScoreCriteriaId { get; set; }

    public decimal? MinValue { get; set; }

    public decimal? MaxValue { get; set; }

    public string ScoreLevel { get; set; } = null!;

    public decimal FivePointScaleScore { get; set; }

    public virtual TryOutScoreCriterion ScoreCriteria { get; set; } = null!;
}
