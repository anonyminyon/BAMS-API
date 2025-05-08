using BasketballAcademyManagementSystemAPI.Common.Constants;

namespace BasketballAcademyManagementSystemAPI.Common.Messages
{
    public static class MatchMessage
    {
        public static class Success
        {
            public const string MatchCreated = "Tạo trận đấu thành công.";
            public const string MatchCanceled = "Hủy trận đấu thành công.";
            public const string PlayerRemoved = "Loại cầu thủ khỏi đội hình thi đấu thành công.";
            public const string ArticleRemoved = "Xoá tài liệu khỏi trận đấu thành công.";
            public const string ArticleAdded = "Thêm tài liệu vào trận đấu thành công.";
            public const string FileUploaded = "Tải lên tệp tài liệu thành công.";
            public const string AddPlayerToMatchLineupSuccessfull = "Thêm cầu thủ vào đội hình thi đấu thành công.";
            public const string FileDeleted = "Xoá tệp tài liệu thành công.";
            public const string GetAvailablePlayersSuccess = "Lấy danh sách cầu thủ thành công.";
            public const string MatchUpdatedSuccessfully = "Cập nhật trận đấu thành công.";
        }

        public static class Error
        {
            // Match message
            public const string CreateMatchFailed = "Tạo trận đấu thất bại.";
            public const string MatchNameRequired = "Tên trận đấu không được để trống.";
            public const string CourtRequired = "Sân đấu không được để trống.";
            public const string CourtDoesNotExist = "Sân đấu không tồn tại.";
            public const string CourtUnavailable = "Sân đấu không khả dụng vào thời gian đã chọn.";
            public const string HomeTeamIdOrAwayTeamIdRequired = "Đội nhà và đội khách không được để trống.";
            public const string HomeTeamIdDoesNotExist = "Đội nhà không tồn tại.";
            public const string AwayTeamIdDoesNotExist = "Đội khách không tồn tại.";
            public const string HomeTeamUnavailable = "Đội nhà không khả dụng vào thời gian đã chọn.";
            public const string AwayTeamUnavailable = "Đội khách không khả dụng vào thời gian đã chọn.";
            public const string HomeTeamIdAndAwayTeamIdCannotBeTheSame = "Đội nhà và đội khách không thể giống nhau.";
            public const string OpponentTeamNameCannotBeEmpty = "Tên đối thủ không được bỏ trống nếu đội nhà hoặc khách trống.";
            public const string MatchNotFound = "Không tìm thấy trận đấu này.";
            public const string PlayerPositionNotAssigned = "Chưa sắp xếp vị trí thi đấu cho cầu thủ này.";
            public const int PlayerShirtNumberNotAssigned = -1;
            public const string MatchCancelFailed = "Hủy trận đấu thất bại.";
            public const string OnlyTeamCoachCanCancelMatch = "Chỉ HLV của một trong hai đội có thể hủy trận đấu.";
            public const string MatchAlreadyCanceled = "Trận đấu đã bị hủy trước đó.";
            public const string NoCourtsAvailableAtSelectedTime = "Không có sân nào khả dụng vào thời gian đã chọn.";
            public const string MatchUpdateFailed = "Cập nhật trận đấu thất bại.";
            public const string InvalidMatchDate = "Ngày trận đấu không hợp lệ.";
            public const string HomeTeamDoesNotExist = "Đội nhà không tồn tại.";
            public const string AwayTeamDoesNotExist = "Đội khách không tồn tại.";
            public const string OnlyTeamCoachCanUpdateMatch = "Chỉ HLV của một trong hai đội có thể cập nhật trận đấu.";
            public const string TeamNotActive = "Đội không hoạt động.";
            public const string OnlyTeamCoachCanCreateMatch = "Chỉ HLV của một trong hai đội có thể tạo trận đấu.";

            public static string MatchDateTooSoon(double hours)
            {
                if (hours < 1)
                {
                    return $"Thời gian bắt đầu trận đấu phải cách tối thiểu {60 * hours} phút kể từ thời điểm hiện tại.";
                }
                else if (hours >= 1 && hours < 24)
                {
                    return $"Thời gian bắt đầu trận đấu phải cách tối thiểu {hours} giờ kể từ thời điểm hiện tại.";
                }
                else
                {
                    return $"Thời gian bắt đầu trận đấu phải cách tối thiểu {hours / 24} ngày kể từ thời điểm hiện tại.";
                }
            }

            // Match lineup message
            public const string OnlyTeamCoachCanRemovePlayer = "Chỉ HLV của đội có thể loại cầu thủ khỏi đội hình thi đấu.";
            public const string PlayerInLineupRemoveFailed = "Loại cầu thủ khỏi đội hình thi đấu thất bại.";
            public const string PlayerNotInMatchLineup = "Cầu thủ không có trong đội hình thi đấu.";
            public const string YouCanOnlyRemoveUnStartedMatch = "Chỉ có thể loại cầu thủ khỏi đội hình thi đấu khi trận đấu chưa bắt đầu.";
            public const string PlayerNotInYourTeam = "Cầu thủ không thuộc đội của bạn.";
            public const string AddPlayerToMatchLineupFailed = "Thêm cầu thủ vào đội hình thi đấu thất bại.";
            public const string OnlyTeamCoachCanCallPlayer = "Chỉ HLV của một trong hai đội có thể gọi cầu thủ vào đội hình thi đấu.";
            public const string PlayerAlreadyInLineup = "Cầu thủ đã có trong đội hình thi đấu.";
            public const string PlayerNotFound = "Không tìm thấy cầu thủ này.";
            public const string TooManyStartingPlayers = "Đội hình thi đấu đã đủ số lượng cầu thủ thi đấu chính.";
            public const string TooManyPlayersInTeam = "Đội hình thi đấu đã đủ số lượng cầu thủ.";
            public const string YouCanOnlyCallPlayerForUnStartedMatch = "Chỉ có thể gọi cầu thủ vào đội hình thi đấu khi trận đấu chưa bắt đầu.";
            public const string PlayerNotInMatchTeam = "Cầu thủ không thuộc đội của bạn.";
            public const string GetAvailablePlayersFailed = "Lấy danh sách cầu thủ không thành công.";
            public const string CoachNotFound = "Không tìm thấy HLV này.";

            // Match article message
            public const string OnlyTeamCoachCanRemoveArticle = "Chỉ HLV của một trong hai đội có thể xoá tài liệu khỏi trận đấu.";
            public const string ArticleRemoveFailed = "Xoá tài liệu khỏi trận đấu thất bại.";
            public const string ArticleNotFound = "Không tìm thấy tài liệu này.";
            public const string OnlyTeamCoachCanAddArticle = "Chỉ HLV của một trong hai đội có thể thêm tài liệu vào trận đấu.";
            public const string ArticleAddFailed = "Thêm tài liệu vào trận đấu thất bại.";
            public const string ArticleTitleRequired = "Tiêu đề tài liệu không được để trống.";
            public const string ArticleUrlRequired = "Đường dẫn tài liệu không được để trống.";
            public const string ArticleUrlInvalid = "Đường dẫn tài liệu không hợp lệ.";
            public const string ArticleTypeRequired = "Loại tài liệu không được để trống.";
            public const string ArticleTypeInvalid = $"Loại tài liệu không hợp lệ. Loại tài liệu hợp lệ bao gồm " +
                $"{MatchConstant.ArticleType.VIDEO}, {MatchConstant.ArticleType.DOCUMENT}, {MatchConstant.ArticleType.IMAGE}.";
            public const string ArticleAddPartialSuccess = "Thêm các tài liệu vào trận đấu thành công nhưng một số tài liệu không được thêm.";
            public const string OnlyTeamCoachCanUploadFile = "Chỉ HLV của một trong hai đội có thể tải lên tệp tài liệu cho trận đấu.";
            public const string FileUploadFailed = "Tải lên tệp tài liệu thất bại.";
            public const string FileDeleteFailed = "Xoá tệp tài liệu thất bại.";
            public const string FilePathRequired = "Đường dẫn tệp không được để trống.";
            public const string FileNotFound = "Không tìm thấy tệp này.";
            public const string InvalidMatchScore = "Điểm số trận đấu không hợp lệ.";

            public static string ArticleTitleAddFailed(string title)
            {
                return $"Thêm tài liệu {title} vào trận đấu thất bại.";
            }
        }
    }
}
