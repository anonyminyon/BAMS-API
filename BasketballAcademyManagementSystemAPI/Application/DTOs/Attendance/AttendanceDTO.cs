namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Attendance
{
	public class AttendanceDTO
	{
		public string AttendanceId { get; set; }
		public string UserId { get; set; }
		public string UserFullName { get; set; }  // Nếu bạn muốn lấy tên người dùng từ bảng User
		public string ManagerId { get; set; }
		public string TeamName { get; set; }
		public string ManagerFullName { get; set; }  // Nếu bạn muốn lấy tên quản lý từ bảng Manager
		public string TrainingSessionId { get; set; }
		public string TrainingSessionName { get; set; }  // Tên buổi tập (nếu có trong bảng TrainingSession)
		public string TeamId { get; set; }  // ID của đội trong bảng TrainingSession
		public DateTime ScheduledDate { get; set; }  // Ngày lịch tập
		public TimeSpan StartTime { get; set; }  // Thời gian bắt đầu
		public TimeSpan EndTime { get; set; }  // Thời gian kết thúc
		public int Status { get; set; }  // Trạng thái của Attendance
		public string Note { get; set; }  // Ghi chú (nếu có)
		public string CreateByUserId { get; set; }  // Nếu bạn muốn lấy tên người dùng từ bảng User
		public string CreateByUserName { get; set; }  // Nếu bạn muốn lấy tên người dùng từ bảng User
	}
}
