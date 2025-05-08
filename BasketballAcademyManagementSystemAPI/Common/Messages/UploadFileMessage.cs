namespace BasketballAcademyManagementSystemAPI.Common.Messages
{
    public static class UploadFileMessage
    {
        public const string InvalidFile = "Tệp không hợp lệ.";
        public const string FileSaveError = "Lỗi khi lưu tệp.";
        public const string FileUploadSuccess = "Tải tệp lên thành công.";
        public const string DuplicateFileFound = "Đã tìm thấy tệp trùng lặp. Trả về URL của tệp hiện có.";
        public const string DangerousFileType = "Phát hiện loại tệp tiềm ẩn nguy hiểm";
        public const string MaliciousContent = "Tệp chứa nội dung độc hại";
        public const string ArchiveNotAllowed = "Không cho phép tải lên tệp nén (ZIP/RAR)";
        public const string FileTooLarge = "file không được quá 1 GB";
        
    }
}
