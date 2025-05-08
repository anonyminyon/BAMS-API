using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class CachedQuestionRepository : ICachedQuestionRepository
    {
        private readonly BamsDbContext _dbContext;
        public CachedQuestionRepository(BamsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddQuestionAndAnswerToCacheAsync(CachedQuestion question)
        { 
            await _dbContext.CachedQuestions.AddAsync(question);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAllAsync(string useFor)
        {
            _dbContext.CachedQuestions.RemoveRange(_dbContext.CachedQuestions.Where(cq => cq.IsForGuest == (useFor == ChatbotConstant.UseForGuest)));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CachedQuestion> GetRelevantQuestionAsync(string question, string useFor)
        {
            var allQuestions = await _dbContext.CachedQuestions.Where(cq => cq.IsForGuest == (useFor == ChatbotConstant.UseForGuest)).ToListAsync();

            var bestMatch = allQuestions
                .Select(q => new { Question = q, Score = FuzzySharp.Fuzz.Ratio(q.Question, question) })
                .Where(x => x.Score >= 85)
                .OrderByDescending(x => x.Score)
                .FirstOrDefault();

            return bestMatch?.Question;
        }
    }
}
