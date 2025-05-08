using BasketballAcademyManagementSystemAPI.Models;
using BasketballAcademyManagementSystemAPI.Application.Repositories;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class UserTeamHistoryService : IUserTeamHistoryService
    {
        private readonly IUserTeamHistoryRepository _userTeamHistoryRepository;
        private readonly IAuthService _authService;

        public UserTeamHistoryService(IUserTeamHistoryRepository userTeamHistoryRepository, IAuthService authService)
        {
            _userTeamHistoryRepository = userTeamHistoryRepository;
            _authService = authService;
        }

        public async Task<UserTeamHistory> UpdateLeftDateByUserIdAndTeamId(string userId, string teamId, DateTime leftDate)
        {
            // Get the most recent UserTeamHistory for the user
            var recentHistory = await _userTeamHistoryRepository.GetMostRecentUserTeamHistoryAsync(userId);

            // User is currently in a different team, update the LeftDate and create a new history
            recentHistory.LeftDate = leftDate;
            dynamic userLoggedDynamic = await _authService.GetCurrentLoggedInUserInformationAsync();
            recentHistory.RemovedByUserId = userLoggedDynamic.UserId;

            await _userTeamHistoryRepository.UpdateUserTeamHistoryAsync(recentHistory);
            return recentHistory;
        }

		public async Task<UserTeamHistory> UserAssignToNewTeamHistory(string userId, string teamId)
		{
			try
			{
				// Lấy UserTeamHistory gần nhất của người dùng
				var recentHistory = await _userTeamHistoryRepository.GetMostRecentUserTeamHistoryAsync(userId);

				// Kiểm tra nếu user chưa bao giờ tham gia đội hoặc đã rời đội cũ
				if (recentHistory == null || recentHistory.LeftDate != null)
				{
					// Tạo UserTeamHistory mới
					var newHistory = new UserTeamHistory
					{
						UserId = userId,
						TeamId = teamId,
						JoinDate = DateTime.Now
					};

					// Thêm vào lịch sử đội
					await _userTeamHistoryRepository.AddUserTeamHistoryAsync(newHistory);
					return newHistory;
				}
				else
				{
					// Nếu người dùng đang ở đội khác, cập nhật LeftDate và tạo lịch sử mới
					recentHistory.LeftDate = DateTime.Now;
					dynamic userLoggedDynamic = await _authService.GetCurrentLoggedInUserInformationAsync();
					recentHistory.RemovedByUserId = userLoggedDynamic.UserId;

					// Cập nhật lịch sử người dùng cũ
					await _userTeamHistoryRepository.UpdateUserTeamHistoryAsync(recentHistory);

					// Tạo lịch sử mới cho đội mới
					var newHistory = new UserTeamHistory
					{
						UserId = userId,
						TeamId = teamId,
						JoinDate = DateTime.Now
					};

					// Thêm lịch sử mới vào cơ sở dữ liệu
					await _userTeamHistoryRepository.AddUserTeamHistoryAsync(newHistory);
					return newHistory;
				}
			}
			catch (Exception ex)
			{
				// Log lỗi và ném lại exception nếu cần thiết
				// _logger.Error($"Error occurred while assigning user to new team: {ex.Message}");
				throw new Exception("An error occurred while assigning the user to a new team", ex);
			}
		}

	}
}
