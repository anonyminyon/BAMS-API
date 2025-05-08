using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class MatchArticleRepository : IMatchArticleRepository
    {
        private readonly BamsDbContext _dbContext;

        public MatchArticleRepository(BamsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddMatchArticleAsync(MatchArticle matchArticle)
        {
            await _dbContext.MatchArticles.AddAsync(matchArticle);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public Task<MatchArticle?> GetMatchArticleByIdAsync(int matchId, int articleId)
        {
            return _dbContext.MatchArticles
                .Where(ma => ma.MatchId == matchId && ma.ArticleId == articleId)
                .Include(ma => ma.Match)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> RemoveMatchArticleAsync(int matchId, int articleId)
        {
            var matchArticle = await _dbContext.MatchArticles
                .FirstOrDefaultAsync(ma => ma.MatchId == matchId && ma.ArticleId == articleId);

            if (matchArticle == null)
            {
                return false;
            }

            _dbContext.MatchArticles.Remove(matchArticle);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveMatchArticleAsync(MatchArticle matchArticle)
        {
            _dbContext.MatchArticles.Remove(matchArticle);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
