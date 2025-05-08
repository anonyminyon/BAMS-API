namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Attendance
{
	public class EditAttendanceDto
	{
		public string? AttendanceId { get; set; } = null!;

		public string UserId { get; set; } = null!;

		public string ManagerId { get; set; } = null!;

		public string TrainingSessionId { get; set; } = null!;

		public int Status { get; set; }

		public string? Note { get; set; }

	}
}
