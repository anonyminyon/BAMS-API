using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class UserFaceRepository : IUserFaceRepository
    {
        private readonly BamsDbContext _dbContext;

        public UserFaceRepository(BamsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> DeleteRegisteredFaceAsync(UserFace oldFace)
        {
            _dbContext.UserFaces.Remove(oldFace);
            return _dbContext.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }

        public async Task<List<UserFace>> GetRegisteredFacesByTeamIdIdAsync(string teamId)
        {
            return await _dbContext.UserFaces
                .Where(uf => _dbContext.Coaches.Any(m => m.UserId == uf.UserId && m.TeamId == teamId) ||
                             _dbContext.Players.Any(p => p.UserId == uf.UserId && p.TeamId == teamId))
                .Include(uf => uf.User)
                .ToListAsync();
        }

        public async Task<List<UserFace>> GetRegisteredFacesByUserIdAsync(string userId)
        {
            return await _dbContext.UserFaces
                .Where(x => x.UserId == userId)
                .Include(x => x.User)
                .ToListAsync();
        }

        public Task<User?> GetUserByFaceIdAsync(string faceId)
        {
            return _dbContext.UserFaces
                .Where(x => x.RegisteredFaceId == faceId)
                .Select(x => x.User)
                .FirstOrDefaultAsync();
        }

        public async Task<UserFace?> GetUserFaceByFaceIdAsync(string registeredFaceId)
        {
            return await _dbContext.UserFaces
                .Where(x => x.RegisteredFaceId == registeredFaceId)
                .FirstOrDefaultAsync();
        }

        public async Task<UserFace?> GetUserFaceByFileNameAsync(string fileName)
        {
            return await _dbContext.UserFaces.Where(x => x.ImageUrl.Contains(fileName)).FirstOrDefaultAsync();
        }

        public async Task<UserFace?> GetUserFaceByIdAsync(int userFaceId)
        {
            return await _dbContext.UserFaces
                .Where(x => x.UserFaceId == userFaceId)
                .FirstOrDefaultAsync();
        }

        public Task RegisterFacesAsync(UserFace userFace)
        {
            _dbContext.UserFaces.Add(userFace);
            return _dbContext.SaveChangesAsync();
        }
    }
}
