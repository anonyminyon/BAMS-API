using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class MemberRegistrationSession
{
    public int MemberRegistrationSessionId { get; set; }

    public string RegistrationName { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsAllowPlayerRecruit { get; set; }

    public bool IsAllowManagerRecruit { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsEnable { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ManagerRegistration> ManagerRegistrations { get; set; } = new List<ManagerRegistration>();

    public virtual ICollection<PlayerRegistration> PlayerRegistrations { get; set; } = new List<PlayerRegistration>();
}
