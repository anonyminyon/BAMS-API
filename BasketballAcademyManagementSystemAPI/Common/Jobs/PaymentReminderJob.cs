using BasketballAcademyManagementSystemAPI.Application.Services;
using Quartz;

namespace BasketballAcademyManagementSystemAPI.Common.Jobs
{
    public class PaymentReminderJob : IJob
    {
        private readonly IPaymentService _paymentService;

        public PaymentReminderJob(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"[PaymentReminderJob] bắt đầu chạy: {DateTime.Now}");
            await _paymentService.SendDueDateReminderEmailsAsync();
            Console.WriteLine($"[PaymentReminderJob] đã chạy xong: {DateTime.Now}");
        }
    }
}


