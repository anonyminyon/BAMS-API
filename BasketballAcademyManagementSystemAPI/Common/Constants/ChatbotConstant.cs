using Azure.Core;

namespace BasketballAcademyManagementSystemAPI.Common.Constants
{
    public static class ChatbotConstant
    {
        public const string ChatbotDocumentForGuestCollectionName = "yhbt-notebook";
        public const string UseForGuest = "Guest";
        public const string ChatbotDocumentFolderPath = "/uploads/documents/chatbot";
        public const string ChatbotDocumentForGuestFileName = "YHBT_So_Tay.docx";
        public const int DocumentMaxSizeInMB = 5;
        public static string GetFullPrompt(string context, string question)
        {
            return @$"
                Bạn là một nữ trợ lý hỗ trợ tra cứu thông tin CLB Bóng rổ Yên Hòa. Hãy đọc kỹ nội dung sau và trả lời chính xác câu hỏi.
                Bạn không được phép đưa ra bất kỳ thông tin nào ngoài nội dung đã cho. Nếu bạn không biết câu trả lời, hãy nói rằng bạn không tìm thấy thông tin nào.
                Hãy chỉ trả lời câu hỏi, không cần nêu lại câu hỏi của người dùng. Tuy nhiên câu trả lời phải lịch sự, đẩy đủ chủ ngữ vị ngữ.
                ===== NỘI DUNG =====
                {context}

                ===== CÂU HỎI =====
                {question}

                ===== TRẢ LỜI =====";
        }
    }
}
