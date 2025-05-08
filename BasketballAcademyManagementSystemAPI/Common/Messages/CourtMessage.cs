namespace BasketballAcademyManagementSystemAPI.Common.Messages
{
    public static class CourtMessage
    {
        public static class Error
        {
            public const string CourtNotFound = "Không tìm thấy sân.";
            public const string CourtAlreadyExist = "Sân đã được tạo.";
            public const string CourtIDNotFound = "Cần có ID của sân.";
            public const string DuplicateCourtAddress = "Đã tồn tại sân với địa chỉ này.";
            public const string DuplicateCourtName = "Đã tồn tại sân với tên này.";
            public const string DuplicateCourtKind = "Đã tồn tại sân với loại này.";
            public const string InvalidData = "Dữ liệu bị để trống. Không hợp lệ.";
        }

        public static class Success
        {
            public const string CourtCreatedSuccess = "Tạo sân thành công.";
            public const string CourtUpdatedSuccess = "Cập nhật sân thành công.";
            public const string CourtDisabledSuccess = "Vô hiệu hóa sân thành công.";
        }
    }
}
