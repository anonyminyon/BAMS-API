namespace BasketballAcademyManagementSystemAPI.Common.Messages
{
    public static class ExerciseMessage
    {
        public static class Success
        {
            public const string ExerciseCreatedSuccessfully = "Tạo nội dung tập luyện thành công";
            public const string ExerciseUpdatedSuccessfully = "Cập nhật nội dung tập luyện thành công";
            public const string ExerciseRemovedSuccessfully = "Xoá nội dung tập luyện thành công";
        }

        public static class Error
        {
            public const string ExerciseCreationFailed = "Tạo nội dung tập luyện thất bại.";
            public const string ExerciseNotFound = "Không tìm thấy nội dung tập luyện này.";
            public const string ExerciseForSessionNotFound = "Không có nội dung tập luyện nào cho buổi tập này.";
            public const string ExerciseNameCannotBeEmpty = "Tên nội dung tập luyện không được để trống.";
            public const string ExerciseDurationCannotBeNegative = "Thời lượng không thể là số âm.";
            public const string InvalidTrainingSessionID = "Buổi tập không hợp lệ.";
            public const string OnlyTeamCoachCanAddExercise = "Chỉ huấn luyện viên của đội mới có thể thêm nội dung tập luyện cho đội.";
            public const string OnlyTeamCoachCanRemoveExercise = "Chỉ huấn luyện viên của đội mới có thể xoá nội dung tập luyện của đội.";
            public const string CoachAssignmentError = "Bạn chỉ có thể chỉ định huấn luyện viên của đội cho nội dung tập luyện.";
            public const string ExerciseNameAlreadyExists = "Tên nội dung tập luyện bị đã tồn tại trong buổi tập này.";
            public const string ExerciseDurationExceedsSessionDuration = "Tổng thời lượng của các nội dung tập luyện không được vượt quá thời lượng của buổi tập.";
            public const string ExerciseNotAssignedToCoach = "Chưa giao";
            public static string OnlyTeamCoachCanEditExercise = "Chỉ huấn luyện viên của đội mới có thể sửa nội dung tập luyện cho đội.";
            public static string ExerciseUpdateFailed = "Cập nhật nội dung tập luyện thất bại.";
            public static string ExerciseRemoveFailed = "Xoá nội dung tập luyện thất bại.";
        }
    }

}
