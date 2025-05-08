namespace BasketballAcademyManagementSystemAPI.Common.Messages
{
    public class AccountMessage
    {
        public static class AccountErrorMessage
        {
            public const string UpdateAccountFailed = "Đã xảy ra lỗi trong quá trình cập nhật hồ sơ, vui lòng thử lại sau.";
            public const string ResetAccountPasswordFailed = "Đặt lại mật khẩu cho tài khoản thất bại.";
            public const string NotFoundCurrentAccount = "Phiên đăng nhập không hợp lệ.";
            public const string NotFoundTargetAccount = "Không tìm thấy tài khoản này.";
            public const string PresidentDoNotHavePermissionToReset = "Bạn chỉ có thể đặt lại mật khẩu cho các quản lý.";
            public const string ManagerDoNotHavePermissionToResetPlayer = "Bạn chỉ có thể đặt lại mật khẩu cho cầu thủ do bạn quản lý.";
            public const string ManagerDoNotHavePermissionToResetParent = "Bạn chỉ có thể đặt lại mật khẩu cho phụ huynh do bạn quản lý.";
            public const string ManagerDoNotHavePermissionToResetCoach = "Bạn chỉ có thể đặt lại mật khẩu cho huấn luyện viên do bạn quản lý.";
        }

        public static class AccountSuccessMessage
        {
            public const string UpdateAccountSuccess = "Cập nhật hồ sơ thành công.";
            public const string ResetAccountPasswordSuccess = "Đặt lại mật khẩu cho tài khoản thành công.";
        }
    }
}
