using BasketballAcademyManagementSystemAPI.Application.DTOs.PlayerManagement;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Repositories
{
    public interface IUserTeamHistoryRepository
    {
        Task<UserTeamHistory> GetMostRecentUserTeamHistoryAsync(string userId);
        Task UpdateUserTeamHistoryAsync(UserTeamHistory userTeamHistory);
        Task AddUserTeamHistoryAsync(UserTeamHistory userTeamHistory);
		Task<List<PlayerExpenditureResponseDto>> GetUserIdsByTeamAndDateAsync(string teamId, DateTime targetDate);

	}
}
