using BasketballAcademyManagementSystemAPI.Application.DTOs.CourtManagement;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession
{
    public class TrainingSessionDetailDto
    {
        public string ScheduledDate { get; set; }
        public string Time { get; set; }
        public string TeamId { get; set; }
        public string TeamName { get; set; }
        public string Status { get; set; }
        public string CourtPrice { get; set; }
        public CourtDto Court { get; set; }
        public List<ExerciseDto> Exercises { get; set; }
    }
}
