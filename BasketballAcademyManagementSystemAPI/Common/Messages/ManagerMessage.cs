namespace BasketballAcademyManagementSystemAPI.Common.Messages
{
    public static class ManagerMessage
    {
        public static class Errors
        {
            public const string CanNotFindUserId = "Không tìm thấy ID người dùng.";
            public const string CanNotFindTeamId = "Không tìm thấy ID đội.";
            public const string ManagerNotFound = "Không tìm thấy quản lý.";
            public const string UpdateFailed = "Cập nhật quản lý thất bại.";
            public const string UpdateEmailFullNameDuplicate = "Không thể cập nhật vì họ tên và email đã tồn tại.";
        }

        public static class Success
        {
            public const string ManagerAssignToTeamSuccessfully = "Phân công quản lý vào đội thành công.";
            public const string ManagerUpdatedSuccessfully = "Cập nhật quản lý thành công.";
            public const string ManagerDisabledSuccessfully = "Vô hiệu hóa quản lý thành công.";
            public const string GetManagerSuccessfully = "Lấy thông tin quản lý thành công.";
            public const string NoChangesDetected = "Không có thay đổi nào để cập nhật.";

        }
        public static class Key
        {
            public const string CanNotFindUserId = "CanNotFindUserId";
            public const string CanNotFindTeamId = "CanNotFindTeamId";
            public const string ManagerNotFound = "ManagerNotFound";
            public const string UpdateFailed = "UpdateFailed";
            public const string UpdateEmailFullNameDuplicate = "UpdateEmailFullNameDuplicate";
        }
    }
}
