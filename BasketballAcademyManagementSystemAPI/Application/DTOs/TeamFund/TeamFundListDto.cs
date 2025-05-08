namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TeamFund
{
	public class TeamFundListDto
	{
		public string TeamFundId { get; set; }
		public string TeamId { get; set; }
		public string? TeamName { get; set; } // nếu cần
		public DateOnly StartDate { get; set; }
		public string? ManagerId { set; get; }
		public string? ManagerName { get; set; }
		public DateOnly EndDate { get; set; }
		public int? Status { get; set; }
		public string? Description { get; set; }
		public string? TotalExpenditure { get; set; }
		public DateTime? ApproveAt { get; set; }
	}
}
