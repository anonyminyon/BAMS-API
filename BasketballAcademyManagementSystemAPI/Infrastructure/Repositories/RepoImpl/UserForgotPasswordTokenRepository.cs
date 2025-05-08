using System;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class UserForgotPasswordTokenRepository : IUserForgotPasswordTokenRepository
    {
        private readonly BamsDbContext _dbContext;

        public UserForgotPasswordTokenRepository(BamsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserForgotPasswordToken?> GetByIdAsync(int id)
        {
            return await _dbContext.UserForgotPasswordTokens.FindAsync(id);
        }

        public async Task<UserForgotPasswordToken?> GetByTokenAsync(string token)
        {
            return await _dbContext.UserForgotPasswordTokens.FirstOrDefaultAsync(t => t.ForgotPasswordToken == token);
        }

        public async Task AddAsync(UserForgotPasswordToken token)
        {
            await _dbContext.UserForgotPasswordTokens.AddAsync(token);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserForgotPasswordToken token)
        {
            _dbContext.UserForgotPasswordTokens.Update(token);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var token = await _dbContext.UserForgotPasswordTokens.FindAsync(id);
            if (token != null)
            {
                _dbContext.UserForgotPasswordTokens.Remove(token);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string token)
        {
            return await _dbContext.UserForgotPasswordTokens.AnyAsync(t => t.ForgotPasswordToken == token);
        }

        public async Task RevokeTokenAsync(string token)
        {
            var entity = await _dbContext.UserForgotPasswordTokens.FirstOrDefaultAsync(t => t.ForgotPasswordToken == token);
            if (entity != null && !entity.IsRevoked)
            {
                entity.IsRevoked = true;
                entity.RevokedAt = DateTime.Now;
                _dbContext.UserForgotPasswordTokens.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<UserForgotPasswordToken?> GetCurrentWorkingTokenOfUserAsync(string userId)
        {
            return await _dbContext.UserForgotPasswordTokens
                .FirstOrDefaultAsync(t => t.UserId.Equals(userId) && t.ExpiresAt > DateTime.Now && !t.IsRevoked);
        }

        public async Task RevokeTokensOfUserAsync(string userId)
        {
            var tokens = await _dbContext.UserForgotPasswordTokens.Where(x => x.UserId.Equals(userId) && !x.IsRevoked).ToListAsync();
            foreach (var token in tokens)
            {
                token.IsRevoked = true;
                token.RevokedAt = DateTime.Now;

                await UpdateAsync(token);
            }
        }
    }
}
