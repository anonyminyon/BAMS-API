namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession
{
    public class RequestUpdateTrainingSessionDto
    {
        public int StatusChangeRequestId { get; set; }
        public string TrainingSessionId { get; set; } = null!;
        public string RequestedByCoachId { get; set; } = null!;
        public string RequestedByCoachUsername { get; set; } = null!;
        public string RequestReason { get; set; } = null!;
        public string OldCourtId { get; set; }
        public string OldCourtName { get; set; }
        public string OldCourtAddress { get; set; }
        public string OldCourtContact { get; set; }
        public decimal OldCourtRentPrice { get; set; }
        public string OldScheduledDate { get; set; }
        public string OldStartTime { get; set; }
        public string OldEndTime { get; set; }
        public decimal OldCourtPrice { get; set; }
        public string? NewCourtId { get; set; }
        public string? NewCourtName { get; set; }
        public string? NewCourtAddress { get; set; }
        public string? NewCourtContact { get; set; }
        public decimal? NewCourtRentPrice { get; set; }
        public string? NewScheduledDate { get; set; }
        public string? NewStartTime { get; set; }
        public string? NewEndTime { get; set; }
        public string RequestedAt { get; set; }
        public int RequestRemainingTime { get; set; }
        public string Status { get; set; }
        public string? RejectedReason { get; set; }
        public string? DecisionByManagerId { get; set; }
        public string? DecisionByManagerUsername { get; set; }
        public string? DecisionAt { get; set; }
    }
}
