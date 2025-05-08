using BasketballAcademyManagementSystemAPI.Application.DTOs.CourtManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Models;

public interface ICourtRepository
{
    Task<PagedResponseDto<Court>> GetAllCourts(CourtFilterDto filter);
    Task<Court?> GetCourtById(string courtId);
    Task<Court> CreateCourt(Court court);
    Task<Court?> UpdateCourt(Court court);
    Task<bool> DisableCourt(string courtId);
    Task<Court> GetCourtByNameExceptCourtID(string courtName, string? courtId);
    Task<bool> CourtExistsAsync(string courtId);
}
