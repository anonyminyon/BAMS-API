namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession
{
    public class UpdateTrainingSessionRequest
    {
        public string TrainingSessionId { get; set; }
        public string UpdateReason { get; set; }
        public string CourtId { get; set; }
        public DateOnly ScheduledDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
