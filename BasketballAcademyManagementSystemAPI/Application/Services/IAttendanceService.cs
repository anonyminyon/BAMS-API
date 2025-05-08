using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Attendance;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
	public interface IAttendanceService
	{
		Task<ApiMessageModelV2<object>> TakeAttendanceAsync(List<TakeAttendanceDTO> attendances);
		Task<ApiMessageModelV2<object>> EditAttendanceAsync(EditAttendanceDto attendanceDto);
		Task<ApiMessageModelV2<List<AttendanceDTO>>> GetAttendancesByTrainingSessionAsync(string trainingSessionId);
		Task<ApiMessageModelV2<object>> GetAttendanceByTrainingSessionOrUser(string trainingSessionId, string userId);
		Task<ApiMessageModelV2<object>> GetPlayersForAttendanceAsync(string trainingSessionId);
		Task<ApiMessageModelV2<object>> GetCoachesForAttendanceAsync(string trainingSessionId);
		Task<ApiMessageModelV2<object>> GetUserAttendanceSumary(string userId, DateTime startDate, DateTime endDate);
	}
}
