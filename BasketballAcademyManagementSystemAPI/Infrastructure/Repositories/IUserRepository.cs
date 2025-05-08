using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
        Task<User> GetUserByIdAsync(string userId);
        Task<User> GetUserWithRoleByIdAsync(string userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string userId);
        Task<bool> UserExistsAsync(string userId);
        Task<IEnumerable<User>> GetUsersByRoleAsync(string roleCode);
        Task<IEnumerable<User>> SearchUsersAsync(string keyword);
        Task ToggleUserStatusAsync(string userId);
        Task<bool> EmailExistsAsync(string email);
    }
}
