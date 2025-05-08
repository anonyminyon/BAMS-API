using System;
using System.Collections.Generic;

namespace BasketballAcademyManagementSystemAPI.Models;

public partial class Exercise
{
    public string ExerciseId { get; set; } = null!;

    public string TrainingSessionId { get; set; } = null!;

    public string? CoachId { get; set; }

    public string ExerciseName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? Duration { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Coach? Coach { get; set; }

    public virtual TrainingSession TrainingSession { get; set; } = null!;
}
