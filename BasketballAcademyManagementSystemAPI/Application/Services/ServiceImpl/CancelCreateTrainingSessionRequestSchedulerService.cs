using Azure.Core;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Quartz;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class CancelCreateTrainingSessionRequestSchedulerService : ICancelCreateTrainingSessionRequestSchedulerService
    {
        private readonly IScheduler _scheduler;
        private readonly ITrainingSessionRepository _trainingSessionRepository;

        public CancelCreateTrainingSessionRequestSchedulerService(ISchedulerFactory schedulerFactory, ITrainingSessionRepository trainingSessionRepository)
        {
            _scheduler = schedulerFactory.GetScheduler().Result;
            _trainingSessionRepository = trainingSessionRepository;
        }

        public async Task ScheduleCancelIfNotExistsAsync(TrainingSession trainingSession)
        {
            var jobKey = new JobKey($"cancel_{trainingSession.TrainingSessionId}");

            if (await _scheduler.CheckExists(jobKey))
            {
                Console.WriteLine($"[i] Job already exists for training session {trainingSession.TrainingSessionId}");
                return;
            }

            var cancelTime = trainingSession.CreatedAt.AddHours(12);
            var now = DateTime.Now;

            if (cancelTime <= now)
            {
                await CancelRequestImmediatelyAsync(trainingSession.TrainingSessionId);
                return;
            }

            var job = JobBuilder.Create<CancelCreateTrainingSessionRequestJob>()
                .WithIdentity(jobKey)
                .UsingJobData("TrainingSessionId", trainingSession.TrainingSessionId)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"trigger_{trainingSession.TrainingSessionId}")
                .StartAt(cancelTime)
            .Build();

            await _scheduler.ScheduleJob(job, trigger);
            Console.WriteLine($"[+] Scheduled cancel training session {trainingSession.TrainingSessionId} at {cancelTime}");
        }

        public async Task SyncPendingRequestsFromDatabaseAsync()
        {
            var pendingRequests = await _trainingSessionRepository.GetPendingTrainingSessionAsync();
            foreach (var request in pendingRequests)
            {
                await ScheduleCancelIfNotExistsAsync(request);
            }
        }

        private async Task CancelRequestImmediatelyAsync(string trainingSessionId)
        {
            if (!string.IsNullOrEmpty(trainingSessionId))
            {
                var ts = await _trainingSessionRepository.GetTrainingSessionByIdAsync(trainingSessionId);
                if (ts != null)
                {
                    ts.Status = TrainingSessionConstant.Status.CANCELED;
                    ts.CreateRejectedReason = "Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.";
                    ts.CreatedDecisionAt = DateTime.Now;

                    await _trainingSessionRepository.UpdateTrainingSessionAsync(ts);
                    Console.WriteLine($"Cancelled training session with id {trainingSessionId} at {DateTime.Now}");
                }
                else
                {
                    Console.WriteLine("Not found training session with id " + trainingSessionId);
                }
            }
        }
    }
}
