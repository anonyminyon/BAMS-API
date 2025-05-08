using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class CachedQuestion
{
    public int CachedQuestionId { get; set; }

    public string Question { get; set; } = null!;

    public string Answer { get; set; } = null!;

    public bool IsForGuest { get; set; }
}
