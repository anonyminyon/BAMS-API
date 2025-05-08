using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface ICancelTrainingSessionStatusChangeRequestSchedulerService
    {
        Task SyncPendingRequestsFromDatabaseAsync();
        Task ScheduleCancelIfNotExistsAsync(TrainingSessionStatusChangeRequest tsStatusChangeRequest);
    }
}
