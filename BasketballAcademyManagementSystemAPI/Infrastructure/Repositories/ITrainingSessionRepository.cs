using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface ITrainingSessionRepository
    {
        Task<bool> IsCourtAvailableAsync(string courtId, DateOnly scheduledDate, TimeOnly startTime, TimeOnly endTime);
        Task<bool> IsCourtAvailableAsync(string trainingSessionId, string courtId, DateOnly scheduledDate, TimeOnly startTime, TimeOnly endTime);
        Task<TrainingSession?> GetConflictingTrainingSessionByCourtAndTimeAsync(string courtId, DateOnly scheduledDate, TimeOnly startTime, TimeOnly endTime);
        Task<TrainingSession?> GetConflictingTrainingSessionOfTeamInDateTime(string teamId, DateOnly scheduledDate, TimeOnly startTime, TimeOnly endTime);
        Task<bool> IsUserCoachOfTeamAsync(string userId, string teamId);
        Task<bool> IsUserManagerOfTeamAsync(string userId, string teamId);
        Task<bool> IsTeamAvailableAsync(string teamId, DateOnly scheduledDate, TimeOnly startTime, TimeOnly endTime);
        Task<bool> IsTeamAvailableAsync(string trainingSessionId, string teamId, DateOnly scheduledDate, TimeOnly startTime, TimeOnly endTime);
        Task<bool> IsValidTeamAsync(string teamId);
        Task<bool> IsValidCourtAsync(string courtId);
        Task<bool> AddTrainingSessionAsync(TrainingSession trainingSession);
        Task<List<TrainingSession>> GetTrainingSessionsByPlayerAsync(string userId, DateTime startDate, DateTime endDate);
        Task<List<Player>> GetPlayersOfParentAsync(string parentId);
        Task<List<TrainingSession>> GetTrainingSessionsByManagerAsync(string managerId, DateTime startDate, DateTime endDate);
        Task<List<TrainingSession>> GetTrainingSessionsByParentAsync(string parentId, DateTime startDate, DateTime endDate);
        Task<List<TrainingSession>> GetTrainingSessionsByCoachAsync(string coachId, DateTime startDate, DateTime endDate, string? teamId, string? courtId);
        Task<List<TrainingSession>> GetAllTrainingSessionsAsync(DateTime startDate, DateTime endDate);
        Task<Attendance?> GetAttendanceByUserIdAndSessionIdAsync(string userId, string sessionId);
        Task<bool> AnyAttendanceBySessionIdAsync(string sessionId);
        Task<string> GetUserRoleAsync(string userId);
        Task<TrainingSession?> GetTrainingSessionWithExcerciseBySessionIdAsync(string sessionId);
        Task<TrainingSession?> GetTrainingSessionDetailAsync(string trainingSessionId);
        Task<TrainingSession?> GetTrainingSessionByIdAsync(string trainingSessionId);
        Task<bool> UpdateTrainingSessionAsync(TrainingSession trainingSession);
        Task<List<Court>> GetAvailableCourtsAsync(List<DateOnly> dates, TimeOnly startTime, TimeOnly endTime);
        Task AddAsync(TrainingSession trainingSession);
        Task<List<string>> GetTeamEmailsAsync(string teamId);
        Task<List<string>> GetTeamManagerEmailsAsync(string teamId);
        Task<List<string>> GetTeamPlayerAndCoachUserIdAsync(string teamId);
        Task<bool> CreateRequestToChangeTrainingSessionStatus(TrainingSessionStatusChangeRequest request);
        Task<bool> IsCancelTrainingSessionRequestExistsAsync(string trainingSessionId);
        Task<bool> IsTrainingSessionHaveRequestExistsAsync(string trainingSessionId);
        Task<TrainingSessionStatusChangeRequest?> GetSessionStatusChangeRequestByTrainingSessionIdAsync(string trainingSessionId);
        Task<bool> UpdateTrainingSessionStatusChangeRequestAsync(TrainingSessionStatusChangeRequest request);
        Task<List<TrainingSession>> GetPendingTrainingSessionOfATeamAsync(string teamId);
        Task<List<TrainingSession>> GetPendingTrainingSessionAsync();
        Task<List<TrainingSessionStatusChangeRequest>> GetTrainingSessionPendingChangeRequestOfATeamAsync(string teamId, int requestType);
        Task<List<TrainingSessionStatusChangeRequest>> GetTrainingSessionPendingChangeRequestAsync();
        Task<TrainingSessionInforToParentEmailDto> GetTrainingSessionForParentEmailAsync(string trainingSessionId);

	}
}
