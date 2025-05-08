namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IEmailService
    {
        void SendEmailMultiThread(string email, string subject, string body);
        Task SendEmailAsync(string email, string subject, string message);
        void SendEmailMultiThread(string subject, string body, List<string> ccEmails);
        Task SendEmailAsync(string subject, string body, List<string> ccEmails);
        Task<bool> SendEmailBoolAsync(string email, string subject, string message);
        Task SendEmailMultiThreadAsync(string email, string subject, string body);
    }
}
