using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class MatchArticle
{
    public int ArticleId { get; set; }

    public int MatchId { get; set; }

    public string Title { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string ArticleType { get; set; } = null!;

    public virtual Match Match { get; set; } = null!;
}
