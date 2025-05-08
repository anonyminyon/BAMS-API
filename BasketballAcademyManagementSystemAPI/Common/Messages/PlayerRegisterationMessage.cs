namespace BasketballAcademyManagementSystemAPI.Common.Messages
{
	public class PlayerRegistrationMessage
	{
		public static class Errors
		{
			public const string ParentNameRequired = "Tên phụ huynh là bắt buộc với người dưới 18 tuổi.";
			public const string ExistedEmailParentInUser = "Đã tồn tại thông tin phụ huynh trong hệ thống.";
			public const string ParentPhoneRequired = "Số điện thoại phụ huynh là bắt buộc với người dưới 18 tuổi.";
			public const string InvalidParentPhoneNumber = "Số điện thoại của phụ huynh không đúng định dạng.";
			public const string ParentEmailRequired = "Email phụ huynh là bắt buộc với người dưới 18 tuổi.";
			public const string InvalidParentEmail = "Email phụ huynh không hợp lệ.";
			public const string DuplicateEmailRegister = "Email hoặc số điện thoại của học sinh và phụ huynh không được trùng nhau.";
			public const string EmailOrPhoneNumberExists = "Email hoặc số điện thoại đã được đăng ký.";
			public const string EmailOrPhoneNumberCanNotNull = "Email và số điện thoại không được để trống.";
			public const string EmailInvalid = "Email của người chơi không hợp lệ.";
			public const string EmailNeedVerified = "Xin vui lòng xác minh email trước khi đăng kí đơn.";
			public const string UpdateStatusFailed = "Cập nhật trạng thái thất bại. Không tìm thấy đơn đăng ký.";
			public const string ManagerRegistrationNotFound = "Không tìm thấy đơn đăng ký.";
			public const string EmailNotVerifyOtpYet = "Email chưa được xác minh bằng OTP.";
			public const string CreateUserAndManagerFailed = "Có lỗi xảy ra. Không thể tạo học viên và quản lý.";
			public const string NotFoundPlayerEmail = "Không tìm thấy tài khoản với email này";
			public const string IsExistedParentCitizenId = "Đã tồn tại thông tin căn cước này trong hệ thống";

			public const string InvalidHeightWeight = "Chiều cao và cân nặng phải lớn hơn 0.";
			public const string PositionRequired = "Vị trí thi đấu là bắt buộc.";
			public const string DuplicateEmail = "Email của học sinh và phụ huynh không được trùng nhau.";
			public const string InvalidDateOfBirth = "Ngày sinh không hợp lệ.";

			public const string AgeAlert = "Học viên dưới 18 tuổi cần điền thông tin phụ huynh";
			public const string InvalidCode = "Mã đã hết hạn hoặc đã được sử dụng, vui lòng thử lại";
			public const string InvalidSessionId = "Không tồn tại đợt sinh viên";
			public const string InvalidStatusForm = "Trạng thái đơn không hợp lệ";
		}

		public static class Success
		{
			public const string RegistrationSubmitSuccess = "Bạn đã nộp đơn thành công.";
			public const string ManagerRegistrationApproveSuccess = "Bạn đã phê duyệt khách thành công.";
			public const string PlayerRegistrationApproveSuccess = "Bạn đã phê duyệt học viên thành công.";

		}
	}

	}