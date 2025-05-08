namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession
{
    public class CreateExerciseRequest
    {
        public string TrainingSessionId { get; set; } = null!;
        public string ExerciseName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? Duration { get; set; }
        public string? CoachId { get; set; }
    }
}
