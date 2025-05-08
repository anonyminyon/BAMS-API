namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TeamFund
{
	public class TeamFundFilterDto
	{
		public string? TeamFundId { get; set; }   // Lọc theo TeamFundId
		public string? TeamId { get; set; }       // Lọc theo TeamId
		public int? Status { get; set; }          // Lọc theo Status
		public DateOnly? StartDate { get; set; }  // Lọc theo StartDate (>=)
		public DateOnly? EndDate { get; set; }    // Lọc theo EndDate (<=)
		public string? Description { get; set; }  // Lọc theo Description (nội dung)
	}
}
