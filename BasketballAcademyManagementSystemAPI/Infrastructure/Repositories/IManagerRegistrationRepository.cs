using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface IManagerRegistrationRepository
    {
        //add new line data to manager registration in db
        Task AddNewManagerRegistrationAsync(ManagerRegistration managerEntity);
        //change status Approve, Reject, Pending
        Task<bool> ChangeStatusByManagerRegistrationID(int managerRegistrationId, string status);
        //find manager registration by id 
        Task<ManagerRegistration> GetManagerRegistrationByID(int id);
        Task<PagedResponseDto<ManagerRegistrationDto>> GetAllManagerRegistrations(ManagerRegistrationFilterDto filter);
        Task DeleteAsync(ManagerRegistration managerRegistration);
        Task DeleteRangeAsync(ICollection<ManagerRegistration> managerRegistrations);
        //update info manager registration
        Task UpdateManagerRegistrationAsync(ManagerRegistration managerEntity);
        Task<bool> IsEmailExistsAndPendingAndNotInMemberRegistrationSessionAsync(string? email, int memberRegistrationSessionId);
        Task<bool> IsEmailExistsAndRejectAndInMemberRegistrationSessionAsync( string email, int memberRegistrationSessionId);
        Task<ManagerRegistration> IsEmailExistsAndPendingAndInMemberRegistrationSessionAsync(string? email, int memberRegistrationSessionId);
    }
}
