namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession
{
    public class ExerciseDto
    {
        public string ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public decimal? Duration { get; set; }
        public string? Description { get; set; }
        public string CoachId { get; set; }
        public string CoachUsername { get; set; }
    }
}
