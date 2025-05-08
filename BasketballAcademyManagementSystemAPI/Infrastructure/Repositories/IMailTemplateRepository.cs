using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface IMailTemplateRepository
    {
        Task<List<MailTemplate>> GetAllAsync();
        Task<MailTemplate?> GetByIdAsync(string id);
        Task<MailTemplate?> GetByTitleAsync(string title);
        Task AddAsync(MailTemplate mailTemplate);
        Task UpdateAsync(MailTemplate mailTemplate);
        Task DeleteAsync(string id);
    }
}
