namespace BasketballAcademyManagementSystemAPI.Common.Messages
{
    public static class EmailVerificationMessage
    {
        public static class Success
        {
            public const string CodeSent = "Mã xác minh đã được gửi đến email.";
            public const string EmailVerified = "Email đã xác minh thành công.";
        }

        public static class Error
        {
            public const string FailedToSend = "Gửi email xác minh thất bại.";
            public const string InvalidOrExpiredCode = "Mã xác minh không hợp lệ hoặc đã hết hạn.";
            public const string SendEmailFailed = "Đã có lỗi khi gửi email.";
            public const string MailTemplateNotFound = "Mẫu thư không tìm thấy.";
            public const string ExistedInUser = "Email đã được tạo tài khoản.Vui lòng thử email khác.";
            public const string ExistedInRegisterForm = "Email này đã được nộp đơn trước đó. Vui lòng thử email khác.";
            public const string InvalidPurpose = "Xác minh email sai mục đích";
            public const string EmailRegisteredFormError = "Email đã đăng kí đơn. Hãy nhập email khác";

        }

        public static class Key
        {
            public const string InvalidPurpose  = "invalidPurpose";
            public const string EmailRegistered = "emailRegistered";
            public const string InvalidCode = "invalidCode";

        }
    }
}
