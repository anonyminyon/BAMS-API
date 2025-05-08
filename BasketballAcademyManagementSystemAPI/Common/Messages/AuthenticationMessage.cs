using System.Globalization;

namespace BasketballAcademyManagementSystemAPI.Common.Messages
{
    public static class AuthenticationMessage
    {
        // Error message
        public static class AuthenticationErrorMessage
        {
            public const string UserNotFound = "Không tìm thấy tài khoản.";
            public const string AuthenticatedRequired = "Vui lòng đăng nhập.";
            public const string InvalidCredentials = "Nhập sai tài khoản hoặc mật khẩu. Vui lòng thử lại.";
            public const string AccountDisabled = "Tài khoản của bạn đã bị vô hiệu hóa. Vui lòng liên hệ câu lạc bộ.";
            public const string PleaseLogin = "Vui lòng đăng nhập.";
            public const string NotFoundRefreshToken = "Vui lòng đăng nhập.";
            public const string InvalidRefreshToken = "Phiên đăng nhập không hợp lệ. Vui lòng đăng nhập lại.";
            public const string WrongOldPassword = "Mật khẩu cũ không chính xác. Vui lòng thử lại.";
            public const string WrongConfirmPassword = "Xác nhận mật khẩu mới không khớp. Vui lòng thử lại.";
            public const string WrongPasswordFormat = "Mật khẩu phải có tối thiểu 8 ký tự, bao gồm ít nhất 1 chữ cái in hoa, 1 chữ cái in thường, 1 chữ số, 1 ký tự đặc biệt.";
            public const string NewPasswordSameAsOldPassword = "Mật khẩu cũ và mật khẩu mới không được phép giống nhau.";
            public const string InvalidEmail = "Vui lòng nhập địa chỉ email hợp lệ.";
            public const string NotFoundForgotPasswordToken = "Yêu cầu đổi mật khẩu này không hợp lệ. Vui lòng thử lại.";
            public const string ExpiredForgotPasswordToken = "Yêu cầu đổi mật khẩu này đã hết hạn. Vui lòng thử lại.";
            public const string RevokedForgotPasswordToken = "Yêu cầu đổi mật khẩu này đã hết hiệu lực. Vui lòng thử lại.";
            public const string InvalidCurrentRole = "Vai trò người dùng không hợp lệ.";
            public const string ExpiredRefreshToken = "Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại.";
            public static string UsernameOrEmailCanNotBeNull = "Vui lòng nhập tài khoản hoặc địa chỉ email.";
            public static string PasswordCanNotBeNull = "Mật khẩu không được để trống.";
            public static string ForgotPasswordTokenCanNotBeNull = "Token đổi mật khẩu không được để trống.";
        }

        // Success message
        public static class AuthenticationSuccessMessage
        {
            public const string LoginSuccess = "Đăng nhập thành công.";
            public const string LogoutSuccess = "Đăng xuất thành công.";
            public const string ChangePasswordSuccess = "Đổi mật khẩu thành công.";
            public const string SendForgotPasswordTokenSuccess = "Đã gửi đường dẫn thay đổi mật khẩu tới địa chỉ email của bạn. Vui kiểm tra email và làm theo hướng dẫn bên trong.";
            public const string ValidateForgotPasswordTokenSuccess = "Yêu cầu đổi mật khẩu hợp lệ. Hãy đặt mật khẩu mới cho tài khoản của bạn.";
        }

        public static class LogMessage
        {
            public static class Infor
            {
                public static string LoginFailed_InvalidCredentials(string username)
                {
                    return $"Đăng nhập thất bại với tài khoản {username} do thông tin không chính xác.";
                }

                public static string LoginFailed_AccountDisabled(string username)
                {
                    return $"Đăng nhập thất bại với tài khoản {username} do tài khoản đang bị khóa.";
                }

                public static string SetAccessTokenToCookie(string username)
                {
                    return $"Đã gán access token vào cookie cho tài khoản {username}.";
                }

                public static string DeleteOldRefreshToken(string username)
                {
                    return $"Đã xóa refresh token cũ cho tài khoản {username}.";
                }

                public static string SetRefreshToken(string username)
                {
                    return $"Đã gán refresh token vào cookie và cơ sở dữ liệu cho tài khoản {username}.";
                }

                public static string LoginSuccess(string username)
                {
                    return $"Đăng nhập thành công với tài khoản {username}.";
                }
            }
        }

    }
}
