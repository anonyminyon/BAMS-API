using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.CourtManagement;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface ICourtService
    {
        Task<ApiMessageModelV2<CourtDto>> CreateCourt(CourtDto courtDto);
        Task<PagedResponseDto<CourtDto>> GetAllCourts(CourtFilterDto filter);
        Task<ApiMessageModelV2<CourtDto>> GetCourtById(string courtId);
        Task<ApiMessageModelV2<CourtDto>> UpdateCourt(CourtDto courtDto);
        Task<ApiMessageModelV2<bool>> DisableCourt(string courtId);
        Task<ApiMessageModelV2<bool>> CheckDuplicateCourt(CourtDto courtDto);
    }
}

