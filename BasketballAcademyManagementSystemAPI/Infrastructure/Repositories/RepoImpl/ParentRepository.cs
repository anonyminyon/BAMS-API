using BasketballAcademyManagementSystemAPI.Application.DTOs.Parent;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class ParentRepository : IParentRepository
	{
		private readonly BamsDbContext _context;

		public ParentRepository(BamsDbContext context)
		{
			_context = context;
		}

		public async Task<Parent> GetParentByIdAsync(string userId)
		{
			return await _context.Parents
				.Include(p => p.User) // nếu bạn có navigation property tới User
				.FirstOrDefaultAsync(p => p.UserId == userId);
		}

		public async Task<User> GetUserParentByIdAsync(string userId)
		{
			return await _context.Users
				.Include(p => p.Parent) // nếu bạn có navigation property tới User
				.FirstOrDefaultAsync(p => p.UserId == userId);
		}

		public async Task<bool> AddParentForPlayerAsync(string playerId, string parentId)
		{
			var player = await _context.Players.FindAsync(playerId);
			if (player == null)
			{
				return false;
			}

			player.ParentId = parentId;
			_context.Players.Update(player);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<Player> GetPlayerByIdAsync(string playerId)
		{
			return await _context.Players.FindAsync(playerId);
		}

		public async Task<List<Player>> GetPlayersOfParentAsync(string parentId)
		{
			return await _context.Players
				.Include(p => p.Team) // nếu bạn có navigation tới Team
				.Include(p => p.User) // nếu bạn có navigation tới User
				.Where(p => p.ParentId == parentId)
				.ToListAsync();
		}

		public async Task<bool> IsUsernameExistsAsync(string username)
		{
			return await _context.Users.AnyAsync(u => u.Username == username);
		}
		
		public async Task AddUserAndParentAsync(User user, Parent parent)
		{
			await _context.Users.AddAsync(user);
			await _context.Parents.AddAsync(parent);
		}

		public async Task<bool> IsEmailExistsAsync(string email)
		{
			return await _context.Users.AnyAsync(u => u.Email == email);
		}

		public async Task<bool> IsCitizenIdExistsAsync(string citizenId)
		{
			return await _context.Parents.AnyAsync(p => p.CitizenId == citizenId);
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}

		public async Task<List<Parent>> FilterParentsAsync(ParentFilterDto filter)
		{
			var query = _context.Parents
	.Include(p => p.User)
	.AsQueryable();

			if (!string.IsNullOrWhiteSpace(filter.Fullname))
			{
				query = query.Where(p => p.User.Fullname.Contains(filter.Fullname));
			}

			if (!string.IsNullOrWhiteSpace(filter.Email))
			{
				query = query.Where(p => p.User.Email.Contains(filter.Email));
			}

			if (!string.IsNullOrWhiteSpace(filter.CitizenId))
			{
				query = query.Where(p => p.CitizenId.Contains(filter.CitizenId));
			}

			if (filter.OnlyUnassigned)
			{
				query = query.Where(p => !_context.Players.Any(player => player.ParentId == p.UserId));
			}

			return await query.ToListAsync();
		}

	}
}
