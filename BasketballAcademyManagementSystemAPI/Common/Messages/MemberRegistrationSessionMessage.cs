namespace BasketballAcademyManagementSystemAPI.Common.Messages
{
    public class MemberRegistrationSessionMessage
    {
        public static class MemberRegistrationSessionErrorMessage
        {
            public const string InvalidDateTimeFormat = "Thời gian phải đúng định dạng dd/MM/yyyy HH:mm:ss.";
            public const string InvalidMinimumSessionOpenHours = "Thời gian đóng đơn phải lớn hơn thời gian mở đơn ít nhất 72 giờ.";
            public const string InvalidTypeOfRecruits = "Bạn phải chọn 1 trong 2 đối tượng tuyển quân.";
            public const string NotFoundRegistrationSession = "Không tìm thấy đợt tuyển quân này.";
            public const string CreateSessionFailed = "Tạo đợt tuyển quân thất bại.";
            public const string UpdateSessionFailed = "Cập nhật đợt tuyển quân thất bại.";
            public const string CanNotToggleStatusOfOutDateSession = "Không thể mở hoặc đóng đợt tuyển quân đã hết hạn nộp đơn.";
            public const string FilteredError = "Có lỗi xảy ra khi lấy dữ liệu.";
            public const string CanNotDeleteOpeningSession = "Không thể xoá một đợt tuyển quân đang được mở";
            public const string DeleteSessionFailed = "Xóa đợt tuyển quân thất bại.";
            public const string CanNotDeleteSessionWithPendingRegistrations = "Không thể xoá một đợt tuyển quân có đơn đăng ký đang chờ duyệt.";
            public const string OutDateOrDisable = "Đợi tuyển quân này đã hết hạn nộp đơn hoặc không còn hiệu lực";
            public static string RegistrationNameCannotBeEmpty = "Vui lòng nhập tên đợt tuyển quân.";
            public static string RegistrationNameTooLong = "Tên đợt tuyển quân không được vượt quá 255 ký tự.";
            public static string InvalidPageNumber = "Trang không hợp lệ. Vui lòng nhập số lớn hơn 0.";
            public static string InvalidPageSize = "Số phần từ không thể bé hơn 1.";
            public static string InvalidFilterData = "Dữ liệu lọc hợp lệ không hợp lệ.";
        }

        public static class MemberRegistrationSessionSuccessMessage
        {
            public const string CreateSessionSuccessfully = "Tạo đợt tuyển quân thành công.";
            public const string UpdateSessionSuccessfully = "Cập nhật đợt tuyển quân thành công.";
            public const string EnableSessionSuccessfully = "Mở đợt tuyển thành công.";
            public const string DisableSessionSuccessfully = "Đóng đợt tuyển quân thành công.";
            public const string FilteredSuccess = "Lấy dữ liệu thành công.";
            public const string DeleteSessionSuccessfully = "Xóa đợt tuyển quân thành công.";
        }

        public static class LogMessage
        {
            public static string CreateRegistrationSessionSuccess(int sessionId, string name)
            {
                return $"Tạo {name} với id: {sessionId} thành công!";
            }

            public static string UpdateRegistrationSessionSuccess(int sessionId, string name)
            {
                return $"Cập nhật {name} với id: {sessionId} thành công!";
            }

            public static string ToogleRegistrationSessionSuccess(int sessionId, string name, bool status)
            {
                return $"Đã cập nhật trạng thái của {name} với id: {sessionId} trở thành {status}";
            }

            public static string DeleteRegistrationSessionSuccess(int sessionId, string name)
            {
                return $"Xoá {name} với id: {sessionId} thành công!";
            }
        }
    }
}
