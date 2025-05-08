using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class MailTemplateRepository : IMailTemplateRepository
    {
        private readonly BamsDbContext _dbContext;
        public MailTemplateRepository(BamsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<MailTemplate>> GetAllAsync()
        {
            return await _dbContext.MailTemplates.ToListAsync();
        }

        public async Task<MailTemplate?> GetByIdAsync(string id)
        {
            return await _dbContext.MailTemplates.FindAsync(id);
        }

        public async Task<MailTemplate?> GetByTitleAsync(string title)
        {
            return await _dbContext.MailTemplates.Where(m => m.TemplateTitle.ToUpper().Equals(title.ToUpper())).FirstOrDefaultAsync();
        }

        public async Task AddAsync(MailTemplate mailTemplate)
        {
            mailTemplate.CreatedDate = DateTime.UtcNow;
            mailTemplate.UpdatedAt = DateTime.UtcNow;
            _dbContext.MailTemplates.Add(mailTemplate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(MailTemplate mailTemplate)
        {
            var existingTemplate = await _dbContext.MailTemplates.FindAsync(mailTemplate.MailTemplateId);
            if (existingTemplate == null) return;

            existingTemplate.TemplateTitle = mailTemplate.TemplateTitle;
            existingTemplate.Content = mailTemplate.Content;
            existingTemplate.UpdatedAt = DateTime.UtcNow;

            _dbContext.MailTemplates.Update(existingTemplate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var mailTemplate = await _dbContext.MailTemplates.FindAsync(id);
            if (mailTemplate == null) return;

            _dbContext.MailTemplates.Remove(mailTemplate);
            await _dbContext.SaveChangesAsync();
        }
    }
}
