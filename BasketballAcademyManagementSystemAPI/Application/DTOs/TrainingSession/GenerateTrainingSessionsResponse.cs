namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession
{
    public class GenerateTrainingSessionsResponse
    {
        public List<TrainingSessionInADayResponseDto> SuccessfulSessions { get; set; }
        public List<TrainingSessionInADayResponseDto> FailedSessions { get; set; }
    }

    public class TrainingSessionInADayResponseDto
    {
        public DateOnly ScheduledDate { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string SelectedCourtId { get; set; }
    }
}
