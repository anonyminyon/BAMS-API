namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession
{
    public class ApproveTrainingSessionRequest
    {
        public string TrainingSessionId { get; set; } = string.Empty;
        public decimal CourtPrice { get; set; } = 0;
    }
}
