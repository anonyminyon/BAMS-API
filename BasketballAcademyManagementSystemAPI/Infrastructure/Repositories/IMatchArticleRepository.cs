using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface IMatchArticleRepository
    {
        Task<MatchArticle?> GetMatchArticleByIdAsync(int matchId, int articleId);
        Task<bool> AddMatchArticleAsync(MatchArticle matchArticle);
        Task<bool> RemoveMatchArticleAsync(int matchId, int articleId);
        Task<bool> RemoveMatchArticleAsync(MatchArticle matchArticle);
    }
}
