using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface IUserForgotPasswordTokenRepository
    {
        Task<UserForgotPasswordToken?> GetByIdAsync(int id);
        Task<UserForgotPasswordToken?> GetByTokenAsync(string token);
        Task<UserForgotPasswordToken?> GetCurrentWorkingTokenOfUserAsync(string userId);
        Task AddAsync(UserForgotPasswordToken token);
        Task UpdateAsync(UserForgotPasswordToken token);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(string token);
        Task RevokeTokenAsync(string token);
        Task RevokeTokensOfUserAsync(string userId);
    }
}
