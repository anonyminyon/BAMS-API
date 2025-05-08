using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class ClubContactRepository : IClubContactRepository
    {
        private readonly BamsDbContext _dbContext;
        public ClubContactRepository(BamsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ClubContact>> GetClubContactMethodsAsync()
        {
            return await _dbContext.ClubContacts.ToArrayAsync();
        }

        public async Task<ClubContact> GetClubContactByIdAsync(int id)
        {
            return await _dbContext.ClubContacts.FindAsync(id);
        }

        public async Task UpdateClubContactAsync(ClubContact clubContact)
        {
            _dbContext.ClubContacts.Update(clubContact);
            await _dbContext.SaveChangesAsync();
        }
    }

}
