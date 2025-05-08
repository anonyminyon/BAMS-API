using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface ICachedQuestionRepository
    {
        Task<CachedQuestion> GetRelevantQuestionAsync(string question, string useFor);
        Task AddQuestionAndAnswerToCacheAsync(CachedQuestion question);
        Task DeleteAllAsync(string useFor);
    }
}
