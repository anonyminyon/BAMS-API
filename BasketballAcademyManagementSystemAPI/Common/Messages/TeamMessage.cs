namespace BasketballAcademyManagementSystemAPI.Common.Messages
{
	public class TeamMessage
	{
		public static class Errors
		{
			public const string TeamNameEmpty = "Tên đội không được để trống.";
			public const string LargerTeamNameLength = "Tên đội không được quá 50 ký tự.";
			public const string ExistTeamName = "Tên đội đã tồn tại";
			public const string NotExistTeam = "Không tìm thấy đội.";
			public const string TeamIdEmpty = "Mã đội bóng không được để trống.";
			public const string PlayerIdsEmpty = "Danh sách mã cầu thủ không được để trống.";
			public const string CoachIdsEmpty = "Danh sách mã huấn luyện viên không được để trống.";
			public const string ManagerIdsEmpty = "Danh sách mã quản lý không được để trống.";
			public const string CoachesNotFound = "Không tìm thấy huấn luyện viên nào trong đội bóng này.";
			public const string PlayersNotFound = "Không tìm thấy học viên nào trong đội bóng này.";
			public const string ManagersNotFound = "Không tìm thấy quản lý nào trong đội bóng này.";
			public const string NotFoundFilter = "Không tìm thấy đội bóng nào với tiêu chí tìm kiếm.";

			//Message for Disband Team feature
			public const string DisbandedTeam = "Đội này đã giải thể.";

		}

		public static class Success
		{
			public const string CreateSuccessTeam = "Tạo đội thành công";
			public const string GetSuccessTeam = "Lấy thông tin đội bóng thành công.";
			public static string UpdateSuccessTeam = "Cập nhật thông tin đội bóng thành công.";
			public static string RemoveCoachSuccess = "Xóa huấn luyện viên khỏi đội bóng thành công.";
			public static string RemovePlayerSuccess = "Xóa học viên khỏi đội bóng thành công.";
			public static string RemoveManagerSuccess = "Xóa quản lý khỏi đội bóng thành công.";
			public static string DisbandTeamSuccess = "Giải thể đội thành công.";

		}
	}
}
