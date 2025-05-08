namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Chatbot
{
    public class UpdateChatbotDocumentRequest
    {
        public IFormFile DocumentFile { get; set; }
        public string UseFor { get; set; } = "Guest";
    }
}
