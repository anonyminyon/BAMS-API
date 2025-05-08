using System.Text.Json.Serialization;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Attendance
{
    public class TakeAttendanceDTO
    {

		public string UserId { get; set; } = null!;
		[JsonIgnore]
		public string? ManagerId { get; set; } = null!;

        public string TrainingSessionId { get; set; } = null!;

        public int Status { get; set; }

        public string? Note { get; set; }

    }
}
