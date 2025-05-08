using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface IClubContactRepository
    {
        Task<IEnumerable<ClubContact>> GetClubContactMethodsAsync();
        Task<ClubContact> GetClubContactByIdAsync(int id);
        Task UpdateClubContactAsync(ClubContact clubContact);

    }
}
