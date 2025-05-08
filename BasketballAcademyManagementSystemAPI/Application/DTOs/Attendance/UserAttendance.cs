namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Attendance
{
	public class UserAttendance
	{
		public string AttendanceId { get; set; }
		public string UserId { get; set; }
		public string ManagerId { get; set; }
		public string TrainingSessionId { get; set; }
		public int Status { get; set; }
		public string? Note { get; set; }
	}
}
