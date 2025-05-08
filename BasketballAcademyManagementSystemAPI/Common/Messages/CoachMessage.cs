namespace BasketballAcademyManagementSystemAPI.Common.Messages
{
    public static class CoachMessage
    {
        public static class Success
        {
            public const string CoachAssignToTeamSuccessfully = "Huấn luyện viên đã được chỉ định vào đội thành công";
            public const string CoachCreateSuccessfully = "Tạo huấn luyện viên thành công.";
            public const string GetCoachSuccessfully = "Lấy thông tin huấn luyện viên thành công";
            public const string CoachStatusUpdated = "Cập nhật trạng thái huấn luyện viên thành công";
            public const string ListRetrieved = "Danh sách huấn luyện viên đã được lấy thành công";
        }

        public static class Error
        {
            public const string CoachNotFound = "Không tìm thấy huấn luyện viên.";
            public const string EmailAlreadyRegistered = "Email này đã được đăng ký";
            public const string PhoneAlreadyRegistered = "Số điện thoại này đã được đăng ký";
            public const string CreateCoachError = "Đã xảy ra lỗi khi tạo huấn luyện viên.";
            public const string CoachDisabled = "Huấn luyện viên đã bị vô hiệu hóa";
            public const string AssignCoachError = "Đã xảy ra lỗi khi chỉ định huấn luyện viên vào đội.";
            public const string InvalidUserId = "ID người dùng không hợp lệ";
            public const string DuplicateEmail = "Email đã được sử dụng";
            public const string DuplicateFullname = "Tên đầy đủ đã được sử dụng";
            public const string UserNotFound = "Không tìm thấy người dùng";
            public const string InvalidEmailFormat = "Định dạng email không hợp lệ.";
            public const string InvalidPhoneFormat = "Định dạng số điện thoại không hợp lệ.";
            public const string UsernameAlreadyRegistered = "Tên người dùng đã được sử dụng";
            // Thêm message mới
            public const string AccessDeniedDifferentTeam = "Bạn không được xem danh sách coach của team khác.";
            public const string ViewDetailsDenied = "Coach này không cùng team với bạn nên không được xem thông tin chi tiết.";
        }

        // Giữ nguyên keys (không thay đổi)
        public static class Key
        {
            public const string ErrorPhoneNum = "errorPhoneNum";
            public const string ErrorEmail = "errorEmail";
            public const string ErrorUserId = "errorUserId";
            public const string ErrorCoachDisabled = "errorCoachDisabled";
            public const string ErrorTeamId = "errorTeamId";
            public const string InvalidEmail = "InvalidEmail";
            public const string InvalidPhone = "InvalidPhone";
            public const string ErrorUsername = "errorUsername";
        }
    }
}