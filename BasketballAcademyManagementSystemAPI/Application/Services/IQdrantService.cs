using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using static BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl.QdrantService;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IQdrantService
    {
        Task<bool> InsertChunksAsync(List<(string text, float[] vector)> chunks, string useFor);
        Task<string?> SearchRelevantContextAsync(float[] vector, string useFor);
        Task DeleteAllAsync(string useFor);
        Task<List<VectorItem>> ListVectorsAsync(string useFor, int limit = 20);
        Task<bool> DeleteCollectionAsync(string useFor);
        Task<List<QdrantService.VectorItem>> ListChatbotVectorsAsync(string useFor, int limit = 20);
    }
}
