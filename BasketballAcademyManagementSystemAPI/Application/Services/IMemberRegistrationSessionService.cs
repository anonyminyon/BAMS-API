using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.MemberRegistrationSession;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IMemberRegistrationSessionService
    {
        Task<ApiResponseModel<MemberRegistrationSessionResponseDto>> CreateAsync(CreateMemberRegistrationSessionRequestDto dto);
        Task<MemberRegistrationSessionResponseDto?> GetDetailsAsync(int id);
        Task<ApiResponseModel<MemberRegistrationSessionResponseDto>> UpdateAsync(int id, UpdateMemberRegistrationSessionRequestDto dto);
        Task<ApiResponseModel<MemberRegistrationSessionResponseDto>> ToggleMemberRegistratrionSessionStatusAsync(int id);
        Task<ApiResponseModel<PagedResponseDto<MemberRegistrationSessionResponseDto>>> GetFilteredPagedAsync(MemberRegistrationSessionFilterDto filter);
        Task<ApiResponseModel<bool>> DeleteAsync(int id);
    }
}
