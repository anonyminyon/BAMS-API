namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface IFileRepository
    {
        string FindExistingFile(string fileHash, string targetFolder);
        void SaveFileHash(string fileHash, string filePath, IFormFile file);
        void DeleteFileHash(string filePath);
    }
}
