using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.ManagerManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IManagerService
    {
        Task<ApiMessageModelV2<ManagerDto>> AssignManagerToTeamAsync(ManagerDto managerDto);
        Task<ApiMessageModelV2<PagedResponseDto<UserAccountDto<ManagerDto>>>> GetFilteredPagedManagersAsync(ManagerFilterDto filter);
        Task<ApiMessageModelV2<UserAccountDto<ManagerDto>>> GetManagerDetailAsync(string userId);
        Task<ApiMessageModelV2<UserAccountDto<ManagerDto>>> UpdateManagerAsync(UserAccountDto<ManagerDto> manager);
        Task<ApiMessageModelV2<bool>> DisableManagerAsync(string userId);

    }
}
