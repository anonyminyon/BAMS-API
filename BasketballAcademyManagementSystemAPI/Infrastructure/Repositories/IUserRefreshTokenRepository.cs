using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface IUserRefreshTokenRepository
    {
        Task<UserRefreshToken?> GetByIdAsync(int id);
        Task<UserRefreshToken?> GetByTokenAsync(string refreshToken);
        Task<IEnumerable<UserRefreshToken>> GetAllAsync();
        Task AddAsync(UserRefreshToken token);
        Task UpdateAsync(UserRefreshToken token);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
