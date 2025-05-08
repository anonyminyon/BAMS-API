using BasketballAcademyManagementSystemAPI.Application.DTOs.Parent;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
	public interface IParentRepository
	{
		Task<bool> AddParentForPlayerAsync(string playerId, string parentId);
		Task<Player> GetPlayerByIdAsync(string playerId);
		Task<Parent> GetParentByIdAsync(string userId);
		Task<List<Player>> GetPlayersOfParentAsync(string parentId);
		Task<User> GetUserParentByIdAsync(string userId);

		Task<bool> IsUsernameExistsAsync(string username);
		Task<bool> IsEmailExistsAsync(string email);
		Task<bool> IsCitizenIdExistsAsync(string citizenId);
		Task AddUserAndParentAsync(User user, Parent parent);
		Task SaveChangesAsync();
		Task<List<Parent>> FilterParentsAsync(ParentFilterDto filter);
	}
}
