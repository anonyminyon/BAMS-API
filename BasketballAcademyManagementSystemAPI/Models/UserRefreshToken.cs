using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class UserRefreshToken
{
    public int UserRefreshTokenId { get; set; }

    public string UserId { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public virtual User User { get; set; } = null!;
}
