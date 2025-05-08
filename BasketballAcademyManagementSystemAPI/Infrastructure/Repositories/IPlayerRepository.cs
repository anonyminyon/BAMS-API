using BasketballAcademyManagementSystemAPI.Application.DTOs.PlayerManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
	public interface IPlayerRepository
	{
		Task<User?> GetPlayerByIdAsync(string playerId);
        Task<List<User>> GetPlayersByIdsAsync(IEnumerable<string> userIds);
        Task<bool> DisablePlayer(string userId); 
		Task<bool> RemoveParentFromPlayerAsync(string playerId);
		Task<Player?> GetPlayerByParentIdAsync(string parentId);
		Task<PagedResponseDto<PlayerResponse>> GetFilteredPlayersAsync(PlayerFilterDto request);
		Task SaveChangesAsync();
		Task<List<string>> GetPlayerUserIdsByParentIdAsync(string parentId);//đang sử dụng để cho parent trong phần payment
		Task<List<PlayerResponse>> GetPlayersWithoutTeamByGenderAsync(string teamId);

	}

}
