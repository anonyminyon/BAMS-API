using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class President
{
    public string UserId { get; set; } = null!;

    public int? Generation { get; set; }

    public virtual ICollection<Coach> Coaches { get; set; } = new List<Coach>();

    public virtual User User { get; set; } = null!;
}
