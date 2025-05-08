namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Match
{
    public class UploadMatchArticleFileRequest
    {
        public IFormFile? File { get; set; }
        public string? ArticleType { get; set; }
    }

}
