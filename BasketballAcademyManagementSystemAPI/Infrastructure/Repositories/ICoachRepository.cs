using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.CoachManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface ICoachRepository
    {
        Task AddCoachAsync(User user, Coach coach);
        Task<bool> ChangeStatusCoachAsync(string userId);
        Task<User> GetCoachByUserIdAsync(string userId);
        Task<bool> UpdateCoachAsync(Coach coach);
        Task<PagedResponseDto<Coach>> GetFilteredPagedCoachesAsync(CoachFilterDto filter);
        Task<Dictionary<string, string>> CheckDuplicateCoachAsync(string phone, string email, string? username, string? userId);
        Task<Dictionary<string, string>> CheckDuplicateCoachAsync(string phone, string email);
        Task<Coach?> GetACoachByUserIdAsync(string userId);
    }
}
