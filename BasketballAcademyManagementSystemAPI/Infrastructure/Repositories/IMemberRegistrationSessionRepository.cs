using BasketballAcademyManagementSystemAPI.Application.DTOs.MemberRegistrationSession;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface IMemberRegistrationSessionRepository
    {
        Task<IEnumerable<MemberRegistrationSession>> GetAllAsync();
        Task<MemberRegistrationSession?> GetByIdAsync(int id);
        Task<MemberRegistrationSession?> GetToDeleteByIdAsync(int id);
        Task AddAsync(MemberRegistrationSession session);
        Task UpdateAsync(MemberRegistrationSession session);
        Task DeleteByIdAsync(int id);
        Task<int> DeleteAsync(MemberRegistrationSession session);
        Task<(IEnumerable<MemberRegistrationSession>, int)> GetPagedAsync(int pageNumber, int pageSize);
        Task<IEnumerable<MemberRegistrationSession>> FilterAsync(string? name, DateTime? startDate, DateTime? endDate);
        Task<PagedResponseDto<MemberRegistrationSession>> GetFilteredPagedAsync(MemberRegistrationSessionFilterDto filter);
    }
}
