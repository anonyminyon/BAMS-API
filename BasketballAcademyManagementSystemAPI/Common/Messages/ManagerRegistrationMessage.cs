namespace BasketballAcademyManagementSystemAPI.Common.Messages
{
    public static class ManagerRegistrationMessage
    {
        public static class Error
        {
            public const string EmailOrPhoneNumberExists = "Email đã được đăng ký.";
            public const string EmailOrPhoneNumberCanNotNull = "Email và số điện thoại không được để trống.";
            public const string UpdateStatusFailed = "Cập nhật trạng thái thất bại. Không tìm thấy đơn đăng ký.";
            public const string ManagerRegistrationNotFound = "Không tìm thấy đơn đăng ký.";
            public const string EmailNotVerifyOtpYet = "Email chưa được xác minh bằng OTP.";
            public const string CreateUserAndManagerFailed = "Có lỗi xảy ra. Không thể tạo User và Manager.";
            public const string EmailExistsInUser = "Email này là 1 tài khoản của thành viên câu lập bộ";
            public const string EmailRegisInOtherMemberRegisSessionAndPending = "Email đã được sửa dụng để đăng ký ở mùa đăng ký làm quản lý khác và đang chờ xét duyệt. Vui lòng đợi hoặc đổi email khác";
            public const string PhoneNumberRegisInOtherMemberRegisSessionAndPending = "Số điện thoại này đã được sửa dụng để đăng ký ở mùa đăng ký làm quản lý khác và đang chờ xét duyệt. Vui lòng đợi hoặc đổi số khác";
            public const string EmailExistInPlayerRegistration = "Email này đã được đăng ký ở đơn đăng ký làm người chơi cho câu lập bộ";
            public const string PhoneNumberExistInPlayerRegistration = "Số điện thoại này đã được đăng ký ở đơn đăng ký làm người chơi cho câu lập bộ";
            public const string EmailRegistrationInValid = "Email không hợp lệ";
            public static string EmailRegisInMemberRegisSessionAndReject = "Đơn đăng ký sử dụng email này đã bị từ chối trong mùa này bạn vui lòng đăng ký trong mùa khác!";
            public static string PhoneNumberRegisInMemberRegisSessionAndReject = "Đơn đăng ký sử dụng số này đã bị từ chối trong mùa này bạn vui lòng đăng ký trong mùa khác!";
        }

        public static class Success
        {
            public const string ManagerRegistrationSuccess = "Bạn đã nộp đơn thành công.";
            public const string ManagerRegistrationApproveSuccess = "Bạn đã phê duyệt đơn thành công.";
            public const string ManagerRegistrationRejectSuccess = "Bạn đã từ chối đơn thành công.";
            public const string EmailRegistrationValidAndSendOtp = "Email hợp lệ, chúng tôi đã gửi code otp cho bạn";
            public const string EmailRegistrationValidAndSendOtpAndUpdate = "Bạn đã đăng ký làm quản lý trong mùa này và đang chờ xét duyệt! Bạn có muốn update thông tin này không?";
            public const string ManagerRegistrationUpdateSuccess = "Bạn đã cập nhật đơn thành công";
            public const string SendOtpSuccess = "Chúng tôi đã gửi mã OTP đến email của bạn để xác thực.";
        }

        public static class Key
        {
            public const string ErrorEmail = "errorEmail";
            public const string ErrorPhone = "errorPhone";
        }
    }
}
