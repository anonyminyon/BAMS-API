using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class Coach
{
    public string UserId { get; set; } = null!;

    public string? TeamId { get; set; }

    public string CreatedByPresidentId { get; set; } = null!;

    public string? Bio { get; set; }

    public DateOnly ContractStartDate { get; set; }

    public DateOnly ContractEndDate { get; set; }

    public virtual President CreatedByPresident { get; set; } = null!;

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

    public virtual Team? Team { get; set; }

    public virtual ICollection<TrainingSessionStatusChangeRequest> TrainingSessionStatusChangeRequests { get; set; } = new List<TrainingSessionStatusChangeRequest>();

    public virtual ICollection<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();

    public virtual User User { get; set; } = null!;
}
