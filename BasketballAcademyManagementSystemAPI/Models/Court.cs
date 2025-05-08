using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class Court
{
    public string CourtId { get; set; } = null!;

    public string CourtName { get; set; } = null!;

    public decimal RentPricePerHour { get; set; }

    public string Address { get; set; } = null!;

    public string? Contact { get; set; }

    public int Status { get; set; }

    public string? Type { get; set; }

    public string? Kind { get; set; }

    public string? ImageUrl { get; set; }

    public int UsagePurpose { get; set; }

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

    public virtual ICollection<TrainingSessionStatusChangeRequest> TrainingSessionStatusChangeRequests { get; set; } = new List<TrainingSessionStatusChangeRequest>();

    public virtual ICollection<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();
}
