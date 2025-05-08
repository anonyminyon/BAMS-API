using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Team;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
	public interface ITeamRepository
	{
		//Add team
		Task<Team> AddTeamAsync(Team team);
		Task<bool> IsTeamNameExistsAsync(string teamName, string? excludeTeamId = null);
		Task<TeamDetailsDto?> GetTeamDtoByIdAsync(string teamId);

		//Update team 
		Task<Team> GetTeamByIdAsync(string teamId);
		Task UpdateTeamAsync(Team team);
		Task<List<Player>> GetPlayersByTeamAsync(string teamId, List<string> playerIds);
		Task<List<Coach>> GetCoachesByTeamAsync(string teamId, List<string> coachIds);
		Task<List<Manager>> GetManagersByTeamAsync(string teamId, List<string> managerIds);
		Task<bool> UpdateFundManagerIdAsync(string teamId, string fundManagerId);
		//Disband team
		Task AddUserTeamHistoryAsync(UserTeamHistory userTeamHistory);
		Task<List<Player>> GetAllPlayersByTeamAsync(string teamId);
		Task<List<Coach>> GetAllCoachesByTeamAsync(string teamId);
		Task<List<Manager>> GetAllManagersByTeamAsync(string teamId);

		//Get Team List + filter + phân trang
		Task<PagedResponseDto<TeamDto>> GetTeamsAsync(TeamFilterDto filter);

		//Update db
		Task SaveChangesAsync();

		//Check team exisst
		Task<bool> TeamExistsAsync(string teamId);
    }
}
