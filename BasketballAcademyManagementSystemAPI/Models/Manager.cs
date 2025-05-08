using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class Manager
{
    public string UserId { get; set; } = null!;

    public string? TeamId { get; set; }

    public string? BankName { get; set; }

    public string? BankAccountNumber { get; set; }

    public int? PaymentMethod { get; set; }

    public string? BankBinId { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<Expenditure> Expenditures { get; set; } = new List<Expenditure>();

    public virtual ICollection<Parent> Parents { get; set; } = new List<Parent>();

    public virtual Team? Team { get; set; }

    public virtual ICollection<TrainingSessionStatusChangeRequest> TrainingSessionStatusChangeRequests { get; set; } = new List<TrainingSessionStatusChangeRequest>();

    public virtual ICollection<TrainingSession> TrainingSessions { get; set; } = new List<TrainingSession>();

    public virtual User User { get; set; } = null!;
}
