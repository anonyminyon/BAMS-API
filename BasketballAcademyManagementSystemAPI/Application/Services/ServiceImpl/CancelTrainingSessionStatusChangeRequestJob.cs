using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using Quartz;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class CancelTrainingSessionStatusChangeRequestJob : IJob
    {
        private readonly ITrainingSessionRepository _trainingSessionRepository;
        public CancelTrainingSessionStatusChangeRequestJob(ITrainingSessionRepository trainingSessionRepository)
        {
            _trainingSessionRepository = trainingSessionRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var trainingSessionStatusChangeRequestId = context.JobDetail.JobDataMap.GetString("TrainingSessionStatusChangeRequestId");
            if (!string.IsNullOrEmpty(trainingSessionStatusChangeRequestId))
            {
                var tsscr = await _trainingSessionRepository.GetSessionStatusChangeRequestByTrainingSessionIdAsync(trainingSessionStatusChangeRequestId);
                if (tsscr != null && tsscr.Status == TrainingSessionConstant.StatusChangeRequestStatus.PENDING)
                {
                    tsscr.Status = TrainingSessionConstant.Status.CANCELED;
                    tsscr.RejectedReason = "Yêu cầu cập nhật buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.";
                    tsscr.DecisionAt = DateTime.Now;

                    await _trainingSessionRepository.UpdateTrainingSessionStatusChangeRequestAsync(tsscr);
                    Console.WriteLine($"Cancelled training session status change request with id {trainingSessionStatusChangeRequestId} at {DateTime.Now}");
                }
                else
                {
                    Console.WriteLine("Can not cancel training session status change request with id " + trainingSessionStatusChangeRequestId);
                }
            }
            Console.WriteLine("Not found any training session status change request with id " + trainingSessionStatusChangeRequestId);
        }
    }
}
