namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession
{
    public class CheckTrainingSessionConflictRequest
    {
        public string TeamId { get; set; }
        public DateOnly ScheduledDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
