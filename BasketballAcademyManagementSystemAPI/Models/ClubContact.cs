using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class ClubContact
{
    public int ContactMethodId { get; set; }

    public string ContactMethodName { get; set; } = null!;

    public string MethodValue { get; set; } = null!;

    public string IconUrl { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
