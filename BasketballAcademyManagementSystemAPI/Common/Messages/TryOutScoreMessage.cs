namespace BasketballAcademyManagementSystemAPI.Common.Messages
{
    public class TryOutScoreMessage
    {
        public static class Success
        {
            public const string SaveAllScoresSuccess = "Lưu điểm thành công.";
            public const string SaveSomeScoresSuccess = "Lưu một số điểm thành công! Tuy nhiên một số điểm đã xảy ra lỗi.";
        }

        public static class Error
        {
            public const string SaveAllScoresFailed = "Lưu điểm không thành công.";
            public const string SessionRegistrationPlayerDoesNotExist = "Không tìm thấy thí sinh này.";
            public const string SessionRegistrationPlayerScoreDoesNotExist = "Không tìm thấy điểm của thí sinh này.";
            public const string SessionRegistrationPlayersDoesNotExist = "Không tìm thấy thí sinh nào.";
            public static string SaveScoreFailed(string score, string measurementScaleCode, int playerRegistrationId)
            {
                return $"Lưu điểm '{score}' cho kỹ năng '{measurementScaleCode}' của đơn đăng ký số '{playerRegistrationId}' thất bại. Vui lòng nhập dữ liệu hợp lệ!";
            }
        }
    }
}
