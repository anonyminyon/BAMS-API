namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Team
{
	public class CoachDto
	{
		public string UserId { get; set; }
		public string? TeamId { get; set; }
		public string CoachName { get; set; }
		public string CoachEmail { get; set; }
		public string CoachPhone {  get; set; }
		public string? CreatedByPresidentId { get; set; } = null!;
		public string Bio { get; set; }
		public DateOnly ContractStartDate { get; set; }
		public DateOnly ContractEndDate { get; set; }
	}
}
