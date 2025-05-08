using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using static BasketballAcademyManagementSystemAPI.Common.Messages.AuthenticationMessage;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class UserRepository : IUserRepository
    {
        private readonly BamsDbContext _dbContext;
        public UserRepository(BamsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByUsernameOrEmailAsync(string usernameOrEmail)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == usernameOrEmail || u.Username == usernameOrEmail);
            return user;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<User> GetUserWithRoleByIdAsync(string userId)
        {
            return await _dbContext.Users
                .Include(x => x.President)
                .Include(x => x.Manager).ThenInclude(mn => mn.Team)
                .Include(x => x.Player).ThenInclude(pl => pl.Team)
                .Include(x => x.Coach).ThenInclude(c => c.Team)
                .Include(x => x.Coach).ThenInclude(c => c.CreatedByPresident).ThenInclude(p => p.User)
                .Include(x => x.Parent)
                .Where(x => x.UserId == userId).FirstAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> UserExistsAsync(string userId)
        {
            return await _dbContext.Users.AnyAsync(u => u.UserId == userId);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(string roleCode)
        {
            return await _dbContext.Users.Where(u => u.RoleCode == roleCode).ToListAsync();
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(string keyword)
        {
            return await _dbContext.Users
                .Where(u => u.Username.Contains(keyword) || u.Fullname.Contains(keyword) || u.Email.Contains(keyword))
                .ToListAsync();
        }

        public async Task ToggleUserStatusAsync(string userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                user.IsEnable = !user.IsEnable;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }
    }
}
