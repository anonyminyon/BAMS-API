using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class UserFace
{
    public int UserFaceId { get; set; }

    public string UserId { get; set; } = null!;

    public string RegisteredFaceId { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public DateTime RegisteredAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
