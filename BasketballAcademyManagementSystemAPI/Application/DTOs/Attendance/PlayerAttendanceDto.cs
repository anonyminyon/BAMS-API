namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Attendance
{
	public class PlayerAttendanceDto
	{
		public string UserId { get; set; } = null!;
		public string Username { get; set; } = null!;

		public string FullName { get; set; } = default!;

		public int? Status { get; set; }

		public string? Note { get; set; }
	}
}
