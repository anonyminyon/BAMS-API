using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IUserTeamHistoryService
    {
        Task<UserTeamHistory> UserAssignToNewTeamHistory(string userId, string teamId);
		Task<UserTeamHistory> UpdateLeftDateByUserIdAndTeamId(string userId, string teamId, DateTime leftDate);
    }
}
