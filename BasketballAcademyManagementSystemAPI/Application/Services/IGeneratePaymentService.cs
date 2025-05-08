namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IGeneratePaymentService
    {
        Task<string> GenerateUniquePaymentIdAsync();
    }
}
