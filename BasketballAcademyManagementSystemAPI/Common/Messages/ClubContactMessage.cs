namespace BasketballAcademyManagementSystemAPI.Common.Messages;

public class ClubContactMessage
{
    public static class Error
    {
        public const string ClubContactNotFound = "Không tìm thấy thông tin liên hệ.";
        public static string NotUpdatedYet { get; set; } = "Chưa cập nhật";
    }

    public static class Success
    {
        public const string ClubContactUpdated = "Thông tin câu lập bộ cập nhập thành công.";
    }
}