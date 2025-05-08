using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.PlayerManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Team;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface ITeamService
    {
		Task<ApiMessageModelV2<TeamDto>> CreateTeamAsync(string teamName, int? status);
		Task<ApiMessageModelV2<TeamDetailsDto>> GetTeamDetailsAsync(string teamId);
		//Update team
		Task<ApiMessageModelV2<TeamDto>> UpdateTeamInfoAsync(string teamId, string teamName, int status);
		Task<ApiMessageModelV2<List<PlayerRemoveResultDto>>> RemovePlayersAsync(string teamId, List<string> playerIds, DateTime leftDate);
		Task<ApiMessageModelV2<bool>> RemoveCoachesAsync(string teamId, List<string> coachIds, DateTime leftDate);
		Task<ApiMessageModelV2<bool>> RemoveManagersAsync(string teamId, List<string> managerIds, DateTime leftDate);
		Task<ApiMessageModelV2<object>> UpdateFundManagerAsync(string teamId, string managerUserId);

		//Disband team
		Task<ApiMessageModelV2<object>> DisbandTeamAsync(string teamId, string note);

		//TeamList
		Task<ApiMessageModelV2<PagedResponseDto<TeamDto>>> GetTeamsAsync(TeamFilterDto filter);
	
	}
}
