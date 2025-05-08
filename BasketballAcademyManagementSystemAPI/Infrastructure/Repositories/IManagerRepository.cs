using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.ManagerManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface IManagerRepository
    {
        Task<User?> GetUserWithManagerDetailAsync(string userId);
        Task<ManagerDto?> UpdateManagerByUserIdAsync(ManagerDto managerDto);
        Task<bool> ChangeStatusManagerAsync(string userId);
        Task<Manager> GetManagerByUserIdAsync(string userId);
        Task<User> GetUserByIdAsync(string userId);
        Task<bool> UpdateManagerAsync(Manager manager);
        Task<bool> UpdateUserAsync(User user);
        Task<PagedResponseDto<Manager>> GetFilteredPagedManagersAsync(ManagerFilterDto filter);
        Task<bool> IsEmailFullNameExists(UserAccountDto<ManagerDto> user);
        Task AddManagerAsync(User user, Manager manager);
    }
}
