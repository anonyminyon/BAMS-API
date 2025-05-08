using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class FileRepository : IFileRepository
    {
        private readonly BamsDbContext _dbContext;

        public FileRepository(BamsDbContext context)
        {
            _dbContext = context;
        }

        public string FindExistingFile(string fileHash, string targetFolder)
        {

            var existingHash = _dbContext.FileHashes.FirstOrDefault(f => f.Hash == fileHash);

            if (existingHash != null)
            {
                string existingPath = existingHash.FilePath;
                if (existingPath.StartsWith(targetFolder) && File.Exists(existingPath))
                {
                    return existingPath;
                }
            }
            return null;
        }

        public void SaveFileHash(string fileHash, string filePath, IFormFile file)
        {

            var fileHashEntity = new FileHash
            {
                Hash = fileHash,
                FilePath = filePath,
                CreatedDate = DateTime.Now,
                FileType = Path.GetExtension(file.FileName),
                FileSize = file.Length
            };

            _dbContext.FileHashes.Add(fileHashEntity);
            _dbContext.SaveChanges();
        }

        public void DeleteFileHash(string filePath)
        {
            var fileHashRecord = _dbContext.FileHashes.FirstOrDefault(f => f.FilePath.Contains(filePath));

            if (fileHashRecord != null)
            {
                _dbContext.FileHashes.Remove(fileHashRecord);
                _dbContext.SaveChanges();
            }
        }
    }
}
