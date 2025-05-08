using BasketballAcademyManagementSystemAPI.Models;
using System.Globalization;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BasketballAcademyManagementSystemAPI.Common.Messages
{
    public static class TrainingSessionMessage
    {
        public static class Success
        {
            public const string TrainingSessionCreatedSuccessfully = "Yêu cầu tạo buổi tập luyện thành công.";
            public const string RequestToCancelTrainingSessionSuccessfully = "Yêu cầu huỷ buổi tập luyện thành công. Vui lòng chờ quản lý duyệt.";
            public const string CancelTrainingSessionSuccessfully = "Huỷ buổi tập luyện thành công.";
            public const string TrainingSessionUpdatedSuccessfully = "Cập nhật buổi tập luyện thành công.";
            public const string TrainingSessionAcceptedSuccessfully = "Phê duyệt buổi tập luyện thành công.";
            public const string TrainingSessionRejectedSuccessfully = "Đã từ chối buổi tập luyện thành công.";
            public const string RejectCancelTrainingSessionRequestSuccessfully = "Đã từ chối yêu cầu huỷ buổi tập luyện thành công.";
            public const string RequestUpdateTrainingSessionSuccessfully = "Yêu cầu thay đổi buổi tập luyện thành công. Vui lòng chờ quản lý duyệt.";
            public const string RejectUpdateTrainingSessionRequestSuccessfully = "Đã từ chối yêu cầu thay đổi buổi tập luyện thành công.";
        }

        public static class Error
        {
            public const string TrainingSessionNotFound = "Không tìm thấy buổi tập này.";
            public const string TrainingSessionCreationFailed = "Tạo buổi tập luyện thất bại.";
            public const string TrainingSessionUpdateFailed = "Cập nhật buổi tập luyện thất bại.";
            public const string CancelTrainingSessionFailed = "Huỷ buổi tập luyện thất bại.";
            public const string TeamDoesNotExist = "Đội này không tồn tại.";
            public const string CourtDoesNotExist = "Sân này không tồn tại.";
            public const string CourtUnavailable = "Sân không khả dụng vào thời gian đã chọn.";
            public const string OnlyCoachOfTeamCanCreateTrainingSession = "Chỉ huấn luyện viên của đội mới có thể tạo buổi tập luyện cho đội.";
            public const string OnlyCoachOfTeamCanUpdateTrainingSession = "Chỉ huấn luyện viên của đội mới có thể cập nhật tập luyện cho đội.";
            public const string OnlyCoachOfTeamCanCancelTrainingSession = "Chỉ huấn luyện viên của đội mới có thể huỷ buổi tập luyện của đội.";
            public const string TrainingSessionConflict = "Đội đã có một buổi tập luyện vào thời gian đã chọn.";
            public const string TrainingSessionAlreadyCancelled = "Buổi tập luyện đã được huỷ trước đó.";
            public const string InvalidRangeOfDate = "Ngày bắt đầu không thể sau ngày kết thúc.";
            public const string InvalidStartDate = "Thời gian bắt đầu không thể ở quá khứ.";

            public const string OnlyManagerOfTeamCanAcceptTrainingSession = "Chỉ quản lý của đội mới có thể chấp nhận buổi tập luyện của đội.";
            public const string OnlyManagerOfTeamCanRejectTrainingSession = "Chỉ quản lý của đội mới có thể từ chối buổi tập luyện của đội.";
            public const string TrainingSessionNotPending = "Buổi tập luyện không ở trạng thái chờ duyệt.";
            public const string CancelTrainingSessionRequestNotFound = "Không tìm thấy yêu cầu huỷ buổi tập luyện.";
            public const string ThisSessionAlreadyHaveAnRequest = "Buổi tập này đang có yêu cầu thay đổi trạng thái khác.";
            public const string ThisRequestIsNotForCancel = "Yêu cầu này không phải là yêu cầu huỷ buổi tập luyện.";
            public const string OnlyManagerOfTeamCanRejectCancelRequest = "Chỉ quản lý của đội mới có thể từ chối yêu cầu huỷ buổi tập luyện của đội.";
            public const string OnlyManagerOfTeamCanApproveCancelRequest = "Chỉ quản lý của đội mới có thể phê duyệt yêu cầu huỷ buổi tập luyện của đội.";
            public const string OnlyManagerOfTeamCanRejectUpdateRequest = "Chỉ quản lý của đội mới có thể từ chối yêu cầu cập nhật buổi tập luyện của đội.";
            public const string UpdateRequestNotFound = "Không tìm thấy yêu cầu thay đổi buổi tập luyện.";
            public const string NotFoundTrainingSession = "Không tìm thấy buổi tập luyện.";
            public const string TrainingSessionRequestNotFound = "Không tìm thấy yêu cầu thay đổi của buổi tập luyện.";
            public const string UpdateRequestNotPending = "Yêu cầu thay đổi buổi tập luyện không ở trạng thái chờ duyệt.";
            public const string ThisRequestIsNotForUpdate = "Yêu cầu này không phải là yêu cầu thay đổi buổi tập luyện.";
            public const string OnlyManagerOfTeamCanApproveUpdateTrainingSession = "Chỉ quản lý của đội mới có thể phê duyệt yêu cầu thay đổi buổi tập luyện của đội.";
            public const string RejectUpdateTrainingSessionFailed = "Từ chối yêu cầu thay đổi buổi tập luyện thất bại.";
            public const string UpdateRequestDataNotFound = "Không tìm thấy dữ liệu yêu cầu thay đổi buổi tập luyện.";
            public const string NoChangesMade = "Không có thay đổi nào được thực hiện.";

            public const string NotFoundTrainingSessionCancelRequest = "Không tìm thấy yêu cầu huỷ buổi tập luyện nào.";
            public const string NotFoundTrainingSessionUpdateRequest = "Không tìm thấy yêu cập nhật buổi tập luyện nào.";

            public static string MinimumDurationNotMet(double hours)
            {
                if (hours < 1)
                {
                    return $"Thời lượng tối thiểu của buổi tập luyện là {60 * hours} phút.";
                }
                else
                {
                    return $"Thời lượng tối thiểu của buổi tập luyện là {hours} giờ.";
                }
            }


            public static string MinimumAdvanceSchedulingRequired(double hours)
            {
                if (hours < 1)
                {
                    return $"Thời gian bắt đầu buổi tập phải cách tối thiểu {60 * hours} phút kể từ thời điểm hiện tại.";
                }
                else if (hours >= 1 && hours < 24)
                {
                    return $"Thời gian bắt đầu buổi tập phải cách tối thiểu {hours} giờ kể từ thời điểm hiện tại.";
                }
                else
                {
                    return $"Thời gian bắt đầu buổi tập phải cách tối thiểu {hours / 24} ngày kể từ thời điểm hiện tại.";
                }
            }

            public static string MinimumUpdateSchedulingRequired(double hours)
            {
                if (hours < 1)
                {

                    return $"Chỉ có thể thay đổi lịch tập trước tối thiểu {60 * hours} phút.";
                }
                else if (hours >= 1 && hours < 24)
                {
                    return $"Chỉ có thể thay đổi lịch tập trước tối thiểu {hours} giờ.";
                }
                else
                {
                    return $"Chỉ có thể thay đổi lịch tập trước tối thiểu {hours / 24} ngày.";
                }
            }

            public static string Unavailable(string yourTeamId, string courtName, DateOnly scheduledDate, TimeOnly startTime, TimeOnly endTime, Team conflictTeam)
            {
                var cultureInfo = new CultureInfo("vi-VN");
                if (conflictTeam.TeamId == yourTeamId)
                {
                    return $"Từ {startTime.ToString("HH:mm")} đến {endTime.ToString("HH:mm")} {scheduledDate.ToString("dddd, dd/MM/yyyy", cultureInfo)} đội của bạn đã có một buổi tập ở {courtName}";
                }
                else
                {
                    return $"Sân {courtName} đã được sử dụng từ {startTime.ToString("HH:mm")} đến {endTime.ToString("HH:mm")} {scheduledDate.ToString("dddd, dd/MM/yyyy", cultureInfo)} bởi {conflictTeam.TeamName}.";

                }
            }

            public static class Info
            {
                public static string TrainingSessionNotFound = "Không tìm thấy buổi tập luyện.";
            }
        }
    }

}
