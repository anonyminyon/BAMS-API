using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.PlayerManagement;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
	public interface IPlayerService
	{
		Task<ApiMessageModelV2<object>> AssignPlayersToTeamAsync(List<string> playerIds, string teamId);
		Task<bool> RemoveParentFromPlayerAsync(string playerId);
		Task<bool> DisablePlayer(string playerId);
		Task<PagedResponseDto<PlayerResponse>> GetFilteredPlayersAsync(PlayerFilterDto filter);
		Task<ApiMessageModelV2<object>> GetPlayersWithoutTeamByGenderAsync(string teamId);
	}
}
