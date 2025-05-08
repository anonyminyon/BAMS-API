using BasketballAcademyManagementSystemAPI.Application.DTOs.Chatbot;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using static BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl.QdrantService;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IChatbotService
    {
        Task<ApiResponseModel<ChatbotResponse>> AskAsync(ChatbotRequest request, string useFor);
        Task<ApiResponseModel<UpdateChatbotDocumentResponse>> UpdateChatBotDocument(UpdateChatbotDocumentRequest request);
        Task<ApiResponseModel<FileResponse>> DownloadChatbotDocumentAsync();
    }
}
