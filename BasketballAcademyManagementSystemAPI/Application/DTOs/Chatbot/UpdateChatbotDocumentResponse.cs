namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Chatbot
{
    public class UpdateChatbotDocumentResponse
    {
        public string DocumentName { get; set; } = string.Empty;
        public string DocumentUrl { get; set; } = string.Empty;
        public string UseFor { get; set; } = "Guest";
    }
}
