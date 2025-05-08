using System;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using Quartz;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class CancelCreateTrainingSessionRequestJob : IJob
    {
        private readonly ITrainingSessionRepository _trainingSessionRepository;
        public CancelCreateTrainingSessionRequestJob(ITrainingSessionRepository trainingSessionRepository)
        {
            _trainingSessionRepository = trainingSessionRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var trainingSessionId = context.JobDetail.JobDataMap.GetString("TrainingSessionId");
            if (!string.IsNullOrEmpty(trainingSessionId))
            {
                var ts = await _trainingSessionRepository.GetTrainingSessionByIdAsync(trainingSessionId);
                if (ts != null && ts.Status == TrainingSessionConstant.Status.PENDING)
                {
                    ts.Status = TrainingSessionConstant.Status.CANCELED;
                    ts.CreateRejectedReason = "Yêu cầu tạo buổi tập đã quá hạn mà chưa được duyệt bởi quản lý đội.";
                    ts.CreatedDecisionAt = DateTime.Now;

                    await _trainingSessionRepository.UpdateTrainingSessionAsync(ts);
                    Console.WriteLine($"Cancelled training session with id {trainingSessionId} at {DateTime.Now}");
                } else
                {
                    Console.WriteLine("Can not cancel training session with id " + trainingSessionId);
                }
            }
            Console.WriteLine("Not found any training session");
        }
    }
}
