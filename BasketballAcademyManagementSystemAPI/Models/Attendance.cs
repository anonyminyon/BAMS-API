using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class Attendance
{
    public string AttendanceId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? ManagerId { get; set; }

    public string TrainingSessionId { get; set; } = null!;

    public int Status { get; set; }

    public string? Note { get; set; }

    public virtual Manager? Manager { get; set; }

    public virtual TrainingSession TrainingSession { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
