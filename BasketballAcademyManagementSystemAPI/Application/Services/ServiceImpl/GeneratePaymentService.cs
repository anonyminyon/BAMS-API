using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class GeneratePaymentService : IGeneratePaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly Random _random = new();

        public GeneratePaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<string> GenerateUniquePaymentIdAsync()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string paymentId;

            do
            {
                var code = new string(Enumerable.Range(0, 5)
                    .Select(_ => chars[_random.Next(chars.Length)]).ToArray());

                paymentId = $"YHBT{code}";
            }
            while (await _paymentRepository.AnyPaymentById(paymentId));

            return paymentId;
        }
    }
}
