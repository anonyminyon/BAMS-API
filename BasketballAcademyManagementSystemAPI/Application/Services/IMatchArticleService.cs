using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Match;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IMatchArticleService
    {
        Task<ApiResponseModel<bool>> RemoveMatchArticleAsync(int matchId, int articleId);
        Task<ApiMessageModelV2<bool>> AddMatchArticlesAsync(int matchId, List<AddMatchArticleRequest> requests);
        Task<ApiResponseModel<string>> UploadMatchArticleFileAsync(int matchId, UploadMatchArticleFileRequest request);
        ApiResponseModel<bool> DeleteMatchArticleFile(string filePath);
    }
}
