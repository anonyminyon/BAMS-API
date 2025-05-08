using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using System.Security.Cryptography;
using log4net;
namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class FileUploadService : IFileUploadService
    {
        private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), UploadFileConstant.UploadPath);
        private const long MaxFileSize = 1 * 1024 * 1024 * 1024; // 1GB in bytes
        private readonly IFileRepository _fileRepository;
        private static readonly ILog _logger = LogManager.GetLogger(LogConstant.LogName.FileUploadManagementFeature);

        private static readonly string[] DangerousExtensions = {
            ".exe", ".bat", ".cmd", ".ps1", ".vbs", ".js", ".jar", ".dll", ".msi"
        };

        private static readonly byte[][] DangerousMagicNumbers = {
            new byte[] { 0x4D, 0x5A }, // EXE/DLL
            new byte[] { 0x50, 0x4B, 0x03, 0x04 }, // ZIP (including JAR)
            new byte[] { 0x23, 0x21 }, // Script files (#!)
            new byte[] { 0x7F, 0x45, 0x4C, 0x46 } // ELF (Linux executables)
        };

        public FileUploadService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));

            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        public async Task<string> UploadFileAsync(IFormFile file, string url)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException(UploadFileMessage.InvalidFile);

            if (file.Length > MaxFileSize)
                throw new ArgumentException(UploadFileMessage.FileTooLarge);

            await PerformSecurityChecks(file);

            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }

            string targetFolder = GetTargetFolderFromUrl(url);
            string fileHash = await CalculateFileHashAsync(file);

            string existingFile = _fileRepository.FindExistingFile(fileHash, targetFolder);
            if (existingFile != null)
            {
                _logger.Info($"[UploadFileAsync] Found existing file for hash: {fileHash} in {targetFolder}");
                return GetRelativePath(existingFile);
            }

            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(targetFolder, fileName);
            string relativeFilePath = GetRelativePath(filePath);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                _fileRepository.SaveFileHash(fileHash, relativeFilePath, file);
                _logger.Info($"[UploadFileAsync] Saved new file to {relativeFilePath} with hash: {fileHash}");
            }
            catch (Exception ex)
            {
                _logger.Error("[UploadFileAsync] Failed to save file", ex);
                throw new Exception(UploadFileMessage.FileSaveError, ex);
            }

            return relativeFilePath;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string url, string fileName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException(UploadFileMessage.InvalidFile);

            if (file.Length > MaxFileSize)
                throw new ArgumentException(UploadFileMessage.FileTooLarge);

            await PerformSecurityChecks(file);

            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }

            string targetFolder = GetTargetFolderFromUrl(url);
            string fileHash = await CalculateFileHashAsync(file);

            string existingFile = _fileRepository.FindExistingFile(fileHash, targetFolder);
            if (existingFile != null)
            {
                _logger.Info($"[UploadFileAsyncWithName] Found existing file for hash: {fileHash} in {targetFolder}");
                return GetRelativePath(existingFile);
            }

            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            fileName = $"{fileName}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(targetFolder, fileName);
            string relativeFilePath = GetRelativePath(filePath);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                _fileRepository.SaveFileHash(fileHash, relativeFilePath, file);
                _logger.Info($"[UploadFileAsyncWithName] Saved new file to {relativeFilePath} with hash: {fileHash}");
            }
            catch (Exception ex)
            {
                _logger.Error("[UploadFileAsyncWithName] Failed to save file", ex);
                throw new Exception(UploadFileMessage.FileSaveError, ex);
            }

            return relativeFilePath;
        }

        public void DeleteFile(string filePath)
        {
            try
            {
                var deletingPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);
                if (File.Exists(deletingPath))
                {
                    File.Delete(deletingPath);
                    _logger.Info($"[DeleteFile] Deleted file at {filePath}");
                }

                _fileRepository.DeleteFileHash(filePath);
                _logger.Info($"[DeleteFile] Deleted hash for file {filePath}");

                string fullFilePath = Path.GetFullPath(deletingPath);
                string directoryPath = Path.GetDirectoryName(fullFilePath);
                string rootUploadPath = Path.GetFullPath(_uploadPath);

                while (!string.IsNullOrEmpty(directoryPath) && directoryPath != rootUploadPath)
                {
                    var directoryInfo = new DirectoryInfo(directoryPath);

                    if (directoryInfo.GetFiles().Length == 0 && directoryInfo.GetDirectories().Length == 0)
                    {
                        directoryInfo.Delete();
                        _logger.Info($"[DeleteFile] Deleted empty folder: {directoryPath}");
                        directoryPath = Path.GetDirectoryName(directoryPath);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("[DeleteFile] Failed to delete file or folder", ex);
            }
        }

        private async Task PerformSecurityChecks(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            string[] allowedExtensions = { ".docx", ".xlsx", ".pptx", ".pdf", ".jpg", ".jpeg", ".png", ".gif" };

            if (DangerousExtensions.Contains(fileExtension) && !allowedExtensions.Contains(fileExtension))
            {
                throw new ArgumentException(UploadFileMessage.DangerousFileType);
            }

            await using (var stream = file.OpenReadStream())
            {
                byte[] header = new byte[20];
                await stream.ReadAsync(header, 0, 20);
                stream.Position = 0;

                if (!allowedExtensions.Contains(fileExtension))
                {
                    foreach (var magic in DangerousMagicNumbers)
                    {
                        if (header.Take(magic.Length).SequenceEqual(magic))
                        {
                            throw new ArgumentException(UploadFileMessage.MaliciousContent);
                        }
                    }
                }

                if ((fileExtension == ".zip" || fileExtension == ".rar") && !allowedExtensions.Contains(fileExtension))
                {
                    throw new ArgumentException(UploadFileMessage.ArchiveNotAllowed);
                }
            }
        }

        private string GetTargetFolderFromUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException(UploadFileMessage.InvalidFile);
            }

            var segments = url.Trim('/').Split('/');
            foreach (var segment in segments)
            {
                if (string.IsNullOrWhiteSpace(segment) ||
                    segment.Any(c => Path.GetInvalidFileNameChars().Contains(c)))
                {
                    throw new ArgumentException(UploadFileMessage.InvalidFile);
                }
            }

            return Path.Combine(_uploadPath, Path.Combine(segments));
        }

        private string GetRelativePath(string fullPath)
        {
            var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var relativePath = Path.GetRelativePath(wwwrootPath, fullPath);
            return relativePath.Replace("\\", "/");
        }

        private async Task<string> CalculateFileHashAsync(IFormFile file)
        {
            using (var sha256 = SHA256.Create())
            {
                using (var stream = file.OpenReadStream())
                {
                    byte[] hashBytes = await sha256.ComputeHashAsync(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }
        }

    }
}
