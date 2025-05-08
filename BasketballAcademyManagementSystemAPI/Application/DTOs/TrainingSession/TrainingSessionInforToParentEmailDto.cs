namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession
{
	public class TrainingSessionInforToParentEmailDto
	{
		public string TrainingSessionId { get; set; }
		public DateOnly TrainingDate { get; set; }
		public TimeOnly StartTime { get; set; }
		public TimeOnly EndTime { get; set; }
		public string CourtName { get; set; }
		public string CourtAddress { get; set; }
	}
}
