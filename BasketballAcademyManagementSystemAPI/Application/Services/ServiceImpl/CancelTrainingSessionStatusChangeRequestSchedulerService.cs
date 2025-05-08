using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Quartz;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class CancelTrainingSessionStatusChangeRequestSchedulerService : ICancelTrainingSessionStatusChangeRequestSchedulerService
    {
        private readonly IScheduler _scheduler;
        private readonly ITrainingSessionRepository _trainingSessionRepository;

        public CancelTrainingSessionStatusChangeRequestSchedulerService(ISchedulerFactory schedulerFactory, ITrainingSessionRepository trainingSessionRepository)
        {
            _scheduler = schedulerFactory.GetScheduler().Result;
            _trainingSessionRepository = trainingSessionRepository;

        }
        public async Task ScheduleCancelIfNotExistsAsync(TrainingSessionStatusChangeRequest tsStatusChangeRequest)
        {
            var jobKey = new JobKey($"cancel_{tsStatusChangeRequest.TrainingSessionId}");

            if (await _scheduler.CheckExists(jobKey))
            {
                Console.WriteLine($"[i] Job already exists for training session {tsStatusChangeRequest.TrainingSessionId}");
                return;
            }

            var cancelTime = tsStatusChangeRequest.RequestedAt.AddHours(8);
            var now = DateTime.Now;

            if (cancelTime <= now)
            {
                await CancelRequestImmediatelyAsync(tsStatusChangeRequest.TrainingSessionId);
                return;
            }

            var job = JobBuilder.Create<CancelTrainingSessionStatusChangeRequestJob>()
                .WithIdentity(jobKey)
                .UsingJobData("TrainingSessionStatusChangeRequestId", tsStatusChangeRequest.TrainingSessionId)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"trigger_{tsStatusChangeRequest.TrainingSessionId}")
                .StartAt(cancelTime)
            .Build();

            await _scheduler.ScheduleJob(job, trigger);
            Console.WriteLine($"[+] Scheduled cancel training session status change request for session {tsStatusChangeRequest.TrainingSessionId} at {cancelTime}");
        }

        private async Task CancelRequestImmediatelyAsync(string trainingSessionId)
        {
            if (!string.IsNullOrEmpty(trainingSessionId))
            {
                var tsscrq = await _trainingSessionRepository.GetSessionStatusChangeRequestByTrainingSessionIdAsync(trainingSessionId);
                if (tsscrq != null)
                {
                    tsscrq.Status = TrainingSessionConstant.Status.CANCELED;
                    tsscrq.RejectedReason = "Yêu cầu cập nhật buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.";
                    tsscrq.DecisionAt = DateTime.Now;

                    await _trainingSessionRepository.UpdateTrainingSessionStatusChangeRequestAsync(tsscrq);
                    Console.WriteLine($"Cancelled training session status change request with id {trainingSessionId} at {DateTime.Now}");
                }
                else
                {
                    Console.WriteLine("Not found training session status change request with id " + trainingSessionId);
                }
            }
        }

        public async Task SyncPendingRequestsFromDatabaseAsync()
        {
            var pendingRequests = await _trainingSessionRepository.GetTrainingSessionPendingChangeRequestAsync();
            foreach (var request in pendingRequests)
            {
                await ScheduleCancelIfNotExistsAsync(request);
            }
        }
    }
}
