namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file, string url);
        Task<string> UploadFileAsync(IFormFile file, string url, string fileName);
        void DeleteFile(string filePath);
    }
}
