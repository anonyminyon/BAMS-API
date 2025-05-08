namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession
{
    public class UpdateExerciseRequest
    {
        public string ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public string? Description { get; set; }
        public decimal? Duration { get; set; }
        public string? CoachId { get; set; }
    }
}
