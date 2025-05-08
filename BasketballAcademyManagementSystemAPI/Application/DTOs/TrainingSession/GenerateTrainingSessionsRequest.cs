namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession
{
    public class GenerateTrainingSessionsRequest
    {
        public string TeamId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public List<TrainingSessionInADayOfWeekDetailDto> TrainingSessionInADayOfWeekDetails { get; set; }
    }

    public class TrainingSessionInADayOfWeekDetailDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string CourtId { get; set; }
    }
}
