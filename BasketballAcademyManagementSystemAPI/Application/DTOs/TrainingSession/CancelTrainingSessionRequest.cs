namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession
{
    public class CancelTrainingSessionRequest
    {
        public string TrainingSessionId { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
    }
}
