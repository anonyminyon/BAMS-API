using BasketballAcademyManagementSystemAPI.Application.DTOs.Attendance;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
	public interface IAttendanceRepository
	{
		Task<bool> TakeAttendanceAsync(List<Attendance> attendances);
		Task UpdateAttendanceAsync(Attendance attendance);
		Task<Attendance> GetAttendanceBySessionAndUserAsync(string sessionId, string userId);
		Task<List<Attendance>> GetAttendancesByTrainingSessionAsync(string trainingSessionId);
		Task<List<PlayerAttendanceDto>> GetPlayersByTrainingSessionIdAsync(string trainingSessionId);
		Task<List<CoachAttendanceDto>> GetCoachesByTrainingSessionIdAsync(string trainingSessionId);
		Task<Attendance?> GetAttendanceByUserAndSessionAsync(string userId, string sessionId);
		Task<bool> UpdateAttendanceListAsync(List<Attendance> attendances);
		Task<bool> AddAttendanceListAsync(List<Attendance> attendances);
		Task<User> GetUserById(string userId);
		Task<List<UserTeamHistory>> GetUserTeamHistoriesByUserIdAsync(string userId, DateTime startDate, DateTime endDate);
		Task<List<TrainingSession>> GetTrainingSessionByTeamIdInMonth(DateTime startDate, DateTime endDate);
		Task<string> GetParentEmailForUnderagePlayer(string playerId);

	}

}
