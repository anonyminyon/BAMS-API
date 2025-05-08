using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class UserForgotPasswordToken
{
    public int ForgotPasswordTokenId { get; set; }

    public string UserId { get; set; } = null!;

    public string ForgotPasswordToken { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public bool IsRevoked { get; set; }

    public DateTime? RevokedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
