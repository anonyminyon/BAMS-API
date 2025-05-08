using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class TrainingSessionStatusChangeRequest
{
    public int StatusChangeRequestId { get; set; }

    public string TrainingSessionId { get; set; } = null!;

    public string RequestedByCoachId { get; set; } = null!;

    public int RequestType { get; set; }

    public string RequestReason { get; set; } = null!;

    public string? NewCourtId { get; set; }

    public DateOnly? NewScheduledDate { get; set; }

    public TimeOnly? NewStartTime { get; set; }

    public TimeOnly? NewEndTime { get; set; }

    public DateTime RequestedAt { get; set; }

    public int Status { get; set; }

    public string? RejectedReason { get; set; }

    public string? DecisionByManagerId { get; set; }

    public DateTime? DecisionAt { get; set; }

    public virtual Manager? DecisionByManager { get; set; }

    public virtual Court? NewCourt { get; set; }

    public virtual Coach RequestedByCoach { get; set; } = null!;

    public virtual TrainingSession TrainingSession { get; set; } = null!;
}
