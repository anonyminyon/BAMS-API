using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface IUserFaceRepository
    {
        Task RegisterFacesAsync(UserFace userFace);
        Task<UserFace?> GetUserFaceByFileNameAsync(string fileName);
        Task<User?> GetUserByFaceIdAsync(string faceId);
        Task<UserFace?> GetUserFaceByFaceIdAsync(string registeredFaceId);
        Task<UserFace?> GetUserFaceByIdAsync(int userFaceId);
        Task<bool> DeleteRegisteredFaceAsync(UserFace oldFace);
        Task<List<UserFace>> GetRegisteredFacesByUserIdAsync(string userId);
        Task<List<UserFace>> GetRegisteredFacesByTeamIdIdAsync(string teamId);
    }
}
