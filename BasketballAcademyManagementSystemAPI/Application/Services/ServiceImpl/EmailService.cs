using BasketballAcademyManagementSystemAPI.Application.DTOs.Common;
using System.Net.Mail;
using System.Net;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private GmailSMTPSetting _gmailSMTPSettings;
        private static readonly string SmtpServer = "smtp.gmail.com";
        private static readonly int SmtpPort = 587;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            GetGmailSMTPSettings();
        }

        private void GetGmailSMTPSettings()
        {
            var gmailSMTPSettingsSection = _configuration.GetSection("GmailSMTPSettings");
            _gmailSMTPSettings = gmailSMTPSettingsSection.Get<GmailSMTPSetting>();
        }

        public void SendEmailMultiThread(string email, string subject, string body)
        {
            SendEmailAsync(email, subject, body).Wait();
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient(SmtpServer, SmtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(GmailSMTPSetting.SenderEmail, GmailSMTPSetting.SenderPassword)
            };

            var senderAddress = new MailAddress(GmailSMTPSetting.SenderEmail, GmailSMTPSetting.SenderName);
            var mailMessage = new MailMessage
            {
                From = senderAddress,
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            return client.SendMailAsync(mailMessage);
        }

        public void SendEmailMultiThread(string subject, string body, List<string> ccEmails)
        {
            SendEmailAsync(subject, body, ccEmails).Wait();
        }

        public Task SendEmailAsync(string subject, string body, List<string> ccEmails)
        {
            var client = new SmtpClient(SmtpServer, SmtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(GmailSMTPSetting.SenderEmail, GmailSMTPSetting.SenderPassword)
            };

            var senderAddress = new MailAddress(GmailSMTPSetting.SenderEmail, GmailSMTPSetting.SenderName);
            var mailMessage = new MailMessage
            {
                From = senderAddress,
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            if (ccEmails != null)
            {
                foreach (var ccEmail in ccEmails)
                {
                    if (!string.IsNullOrWhiteSpace(ccEmail))
                    {
                        mailMessage.CC.Add(ccEmail);
                    }
                }
            }

            return client.SendMailAsync(mailMessage);
        }

        public async Task<bool> SendEmailBoolAsync(string email, string subject, string message)
        {
            try
            {
                var client = new SmtpClient(SmtpServer, SmtpPort)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(GmailSMTPSetting.SenderEmail, GmailSMTPSetting.SenderPassword)
                };

                var senderAddress = new MailAddress(GmailSMTPSetting.SenderEmail, GmailSMTPSetting.SenderName);
                var mailMessage = new MailMessage
                {
                    From = senderAddress,
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);
                await client.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }

        public async Task SendEmailMultiThreadAsync(string email, string subject, string body)
        {
            await Task.Run(() => SendEmailBoolAsync(email, subject, body));
        }
    }
}
