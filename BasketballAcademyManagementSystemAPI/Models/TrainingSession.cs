using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class TrainingSession
{
    public string TrainingSessionId { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public string CourtId { get; set; } = null!;

    public int Status { get; set; }

    public DateOnly ScheduledDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string CreatedByUserId { get; set; } = null!;

    public string? CreatedDecisionByManagerId { get; set; }

    public DateTime? CreatedDecisionAt { get; set; }

    public string? CreateRejectedReason { get; set; }

    public decimal? CourtPrice { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Court Court { get; set; } = null!;

    public virtual Coach CreatedByUser { get; set; } = null!;

    public virtual Manager? CreatedDecisionByManager { get; set; }

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<TrainingSessionStatusChangeRequest> TrainingSessionStatusChangeRequests { get; set; } = new List<TrainingSessionStatusChangeRequest>();
}
