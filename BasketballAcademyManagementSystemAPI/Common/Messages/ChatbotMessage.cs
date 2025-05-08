using DocumentFormat.OpenXml.InkML;

namespace BasketballAcademyManagementSystemAPI.Common.Messages
{
    public static class ChatbotMessage
    {
        public static class Success
        {
            public const string AskByGuestSuccess = "AI đã tìm thấy câu trả lời.";
            public const string FoundRevelentQuestionInCache = "Đã tìm thấy câu hỏi liên quan trong bộ nhớ cache.";

            public const string UpdateChatbotDocumentSuccess = "Cập nhật tài liệu chatbot thành công";
        }

        public static class Error
        {
            public const string NotFoundAnswerForThisQuestion = "Không tìm thấy nội dung cho câu hỏi này, vui lòng liên hệ trực tiếp với CLB để nhận được giải đáp.";
            public const string NotFoundRelevantInformationInHandbook = "Không tìm thấy thông tin phù hợp trong sổ tay.";

            public const string UpdateChatbotDocumentFailed = "Cập nhật tài liệu chatbot thất bại";
            public const string DocumentFileSizeExceeded = "Kích thước tệp tài liệu vượt quá giới hạn cho phép";
            public const string DocumentFileTypeNotSupported = "Tài liệu không hợp lệ, vui lòng chọn tệp docx.";
            public const string DocumentEmpty = "Tài liệu không có nội dung nào để cập nhật.";
            public const string DocumentFileUploadFailed = "Tải lên tệp tài liệu thất bại";
            public const string ErrorWhenSavingDocument = "Có lỗi xảy ra trong quá trình cập nhật tài liệu.";
        }
    }
}
