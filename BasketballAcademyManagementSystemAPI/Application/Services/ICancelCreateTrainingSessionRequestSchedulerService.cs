using Azure.Core;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface ICancelCreateTrainingSessionRequestSchedulerService
    {
        Task SyncPendingRequestsFromDatabaseAsync();
        Task ScheduleCancelIfNotExistsAsync(TrainingSession trainingSession);
    }
}
