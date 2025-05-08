using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class UserRefreshTokenRepository : IUserRefreshTokenRepository
    {
        private readonly BamsDbContext _dbContext;

        public UserRefreshTokenRepository(BamsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserRefreshToken?> GetByIdAsync(int id)
        {
            return await _dbContext.UserRefreshTokens.FindAsync(id);
        }

        public async Task<UserRefreshToken?> GetByTokenAsync(string refreshToken)
        {
            return await _dbContext.UserRefreshTokens.FirstOrDefaultAsync(t => t.RefreshToken == refreshToken);
        }

        public async Task<IEnumerable<UserRefreshToken>> GetAllAsync()
        {
            return await _dbContext.UserRefreshTokens.ToListAsync();
        }

        public async Task AddAsync(UserRefreshToken token)
        {
            await _dbContext.UserRefreshTokens.AddAsync(token);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserRefreshToken token)
        {
            _dbContext.UserRefreshTokens.Update(token);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var token = await GetByIdAsync(id);
            if (token != null)
            {
                _dbContext.UserRefreshTokens.Remove(token);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.UserRefreshTokens.AnyAsync(t => t.UserRefreshTokenId == id);
        }
    }
}
