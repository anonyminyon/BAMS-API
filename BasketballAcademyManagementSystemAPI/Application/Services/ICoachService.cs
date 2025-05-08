using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.CoachManagement;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface ICoachService
    {
        Task<ApiMessageModelV2<UserAccountDto<CoachDetailDto>>> CreateCoachAsync(CreateCoachDto coachDto);
        Task<ApiMessageModelV2<PagedResponseDto<UserAccountDto<CoachAccountDto>>>> GetCoachListAsync(CoachFilterDto filter);
        Task<ApiMessageModelV2<UserAccountDto<CoachDetailDto>>> GetCoachDetailAsync(string userId);
        Task<ApiMessageModelV2<UserAccountDto<CoachAccountDto>>> ChangeCoachAccountStatusAsync(string userId);
        Task<ApiMessageModelV2<CoachAccountDto>> AssignCoachToTeamAsync(string userId, string teamId);
        Task<ApiMessageModelV2<UserAccountDto<CoachDetailDto>>> UpdateCoachAsync(UpdateCoachDto coachDto);
    }
}
