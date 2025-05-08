using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? ProfileImage { get; set; }

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string RoleCode { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsEnable { get; set; }

    public bool? Gender { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Coach? Coach { get; set; }

    public virtual Manager? Manager { get; set; }

    public virtual Parent? Parent { get; set; }

    public virtual Player? Player { get; set; }

    public virtual ICollection<PlayerRegistration> PlayerRegistrations { get; set; } = new List<PlayerRegistration>();

    public virtual President? President { get; set; }

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public virtual ICollection<TryOutScorecard> TryOutScorecards { get; set; } = new List<TryOutScorecard>();

    public virtual ICollection<UserFace> UserFaces { get; set; } = new List<UserFace>();

    public virtual ICollection<UserForgotPasswordToken> UserForgotPasswordTokens { get; set; } = new List<UserForgotPasswordToken>();

    public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; } = new List<UserRefreshToken>();

    public virtual ICollection<UserTeamHistory> UserTeamHistoryRemovedByUsers { get; set; } = new List<UserTeamHistory>();

    public virtual ICollection<UserTeamHistory> UserTeamHistoryUsers { get; set; } = new List<UserTeamHistory>();
}
