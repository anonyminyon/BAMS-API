namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Attendance
{
	public class CoachAttendanceDto
	{
		public string UserId { get; set; }
		public string FullName { get; set; }
		public string Username { get; set; }
		public int? Status { get; set; } // 0: Absent, 1: Present, null: chưa điểm danh
		public string? Note { get; set; }
	}
}
