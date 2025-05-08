

namespace BasketballAcademyManagementSystemAPI.Common.Messages
{
    public static class FaceRecognitionMessage
    {
        public static class Success
        {
            public const string RegisterUserFaceSuccessfully = "Đăng ký khuôn mặt cho người dùng thành công!";
            public const string DetectFaceSuccessfully = "Nhận diện khuôn mặt thành công!";
            public const string UpdateUserFaceSuccessfully = "Đã cập nhật khuôn mặt cho người dùng thành công!";
            public const string DeleteRegisteredFaceSuccessfully = "Đã xóa khuôn mặt đã đăng ký thành công!";
        }

        public static class  Error
        {
            // Register face
            public const string RegisterUserFaceFailed = "Đăng ký khuôn mặt cho người dùng thất bại!";
            public const string ThereAreNoFaceDetectedInTheImage = "Không tìm thấy khuôn mặt nào trong ảnh. Vui lòng thử lại với ảnh khác.";
            public const string ManagerNotFound = "Xác thực tài khoản thất bại!";
            public const string YouCanOnlyRegisterFaceForYourTeamMember = "Bạn chỉ có thể đăng ký khuôn mặt cho thành viên đội mình.";
            public const string UserNotFound = "Không tìm thấy cầu thủ này";
            public const string ImageFileInValidFormat = "Định dạng ảnh không hợp lệ. Vui lòng sử dụng định dạng JPG, JPEG hoặc PNG.";
            public const string OnlyOneFaceInImageRequired = "Có quá nhiều khuôn mặt trong ảnh.";
            public const string SomeThingWentWrongWhenRegisterFaceWithAWS = "Có lỗi xảy ra trong quá trình đăng ký ảnh {imageName} với AWS.";
            public const string ThisImageAlreadyUsed = "Ảnh đã được sử dụng để đăng ký khuôn mặt trước đó.";
            public const string ImageFileNotFound = "Không tìm thấy ảnh. Vui lòng kiểm tra lại.";

            public static string ImageFileExceedMaxSize(double imageMaxSize)
            {
                return $"Ảnh đã vượt quá kích thước tối đa là {imageMaxSize}MB";
            }

            // Detect face
            public const string DetectFaceFailed = "Nhận diện khuôn mặt thất bại!";

            // Update face
            public const string RegisteredFaceNotFound = "Không tìm thấy mã khuôn mặt đã đăng ký.";
            public const string RegisteredFaceNotBelongToUser = "Mã khuôn mặt đã đăng ký không thuộc về người dùng này";

            // Delete face
            public const string YouCanOnlyDeleteFaceForYourTeamMember = "Bạn chỉ có thể xoá khuôn mặt cho thành viên đội mình.";
            public const string UserHasNoRegisteredFace = "Người dùng này không có khuôn mặt nào đã đăng ký. Vui lòng kiểm tra lại.";

            // vỉew registered faces
            public const string YouCanOnlyViewRegisteredFaceOfYourTeam = "Bạn chỉ có thể xem khuôn mặt đã đăng ký của thành viên đội mình.";
            public const string UserIdOrTeamIdRequired = "Cần có ID người dùng hoặc ID đội để thực hiện thao tác này.";
            public const string OnlyUserIdOrTeamIdAllowed = "Chỉ có thể lựa chọn xem theo người dùng hoặc team.";

        }
    }
}
