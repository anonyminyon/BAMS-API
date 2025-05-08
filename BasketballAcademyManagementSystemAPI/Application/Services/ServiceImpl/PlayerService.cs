using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.PlayerManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
	public class PlayerService:IPlayerService
	{
		private readonly IPlayerRepository _playerRepository;
		private readonly IUserTeamHistoryService _userTeamHistoryService;
		private readonly ISendMailService _sendMailService;
		private readonly ITeamRepository _teamRepository;
		public PlayerService(
			IPlayerRepository playerRepository, 
			IUserTeamHistoryService userTeamHistoryService, 
			ISendMailService sendMailService,
			ITeamRepository teamRepository)
		{ 

				_playerRepository = playerRepository;
				_userTeamHistoryService = userTeamHistoryService;
				_sendMailService = sendMailService;
				_teamRepository = teamRepository;
		}

		public async Task<ApiMessageModelV2<object>> AssignPlayersToTeamAsync(List<string> playerIds, string teamId)
		{
			var errors = new Dictionary<string, string>();
			var resultList = new List<PlayerAssignResultDto>();

			try
			{
				// Kiểm tra nếu playerIds rỗng
				if (playerIds == null || !playerIds.Any())
				{
					errors.Add("playerIdsEmpty", "Danh sách cầu thủ rỗng.");
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors
					};
				}

				// Kiểm tra team tồn tại
				var team = await _teamRepository.GetTeamByIdAsync(teamId);
				if (team == null)
				{
					errors.Add("teamNotFound", $"Không tìm thấy đội với ID {teamId}.");
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors
					};
				}

				foreach (var playerId in playerIds)
				{
					var result = new PlayerAssignResultDto { PlayerId = playerId };
					try
					{
						var player = await _playerRepository.GetPlayerByIdAsync(playerId);
						if (player == null)
						{
							result.Success = false;
							result.Message = "Không tìm thấy cầu thủ.";
							resultList.Add(result);
							continue;
						}

						// Nếu cầu thủ tồn tại ở đội trước đó thì báo lỗi
						if (player.Player.TeamId == teamId)
						{
							result.Success = false;
							result.Message = "Cầu thủ đã ở trong đội này trước đó.";
							resultList.Add(result);
							continue;
						}

						// Gán team
						player.Player.TeamId = teamId;
						await _playerRepository.SaveChangesAsync();

						try
						{
							await _userTeamHistoryService.UserAssignToNewTeamHistory(playerId, teamId);
							result.Success = true;
							result.Message = "Gán đội thành công.";
						}
						catch (InvalidOperationException ex)
						{
							result.Success = false;
							result.Message = $"Lỗi ghi lịch sử đội: {ex.Message}";
						}
						catch (Exception ex)
						{
							result.Success = false;
							result.Message = $"Lỗi không xác định khi ghi lịch sử đội: {ex.Message}";
						}
					}
					catch (Exception ex)
					{
						result.Success = false;
						result.Message = $"Lỗi xử lý cầu thủ {playerId}: {ex.Message}";
					}
					resultList.Add(result);
				}

				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = "Kết quả gán cầu thủ vào đội.",
					Data = resultList
				};
			}
			catch (Exception ex)
			{
				// Global catch for any unhandled exception in the method
				errors.Add("generalError", $"Lỗi xử lý yêu cầu: {ex.Message}");
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors
				};
			}
		}


		public async Task<bool> DisablePlayer(string playerId)
		{
			var user = await _playerRepository.DisablePlayer(playerId);
			if(!user)
			{
				throw new InvalidOperationException($"Không tìm thấy cầu thủ với.");

			}
			return true;
		}

		public async Task<bool> RemoveParentFromPlayerAsync(string playerId)
		{
			return await _playerRepository.RemoveParentFromPlayerAsync(playerId);
		}

		public async Task<PagedResponseDto<PlayerResponse>> GetFilteredPlayersAsync(PlayerFilterDto filter)
		{
			return await _playerRepository.GetFilteredPlayersAsync(filter);
		}

		public async Task<ApiMessageModelV2<object>> GetPlayersWithoutTeamByGenderAsync(string teamId)
		{
			var errors = new Dictionary<string, string>();

			try
			{
				// Truy vấn tên đội từ teamId
				var teams = await _teamRepository.GetTeamByIdAsync(teamId);

				// Kiểm tra nếu không tìm thấy tên đội
				if (string.IsNullOrEmpty(teams.TeamName))
				{
					errors.Add("teamNotFound", $"Không tìm thấy đội với ID {teamId}.");
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Message = "Không tìm thấy đội.",
						Errors = errors
					};
				}

				// Kiểm tra tên đội có chứa "Nam" hoặc "Nữ"
				bool isValidTeamName = teams.TeamName.ToLower().Contains("nam") || teams.TeamName.ToLower().Contains("nữ");
				if (!isValidTeamName)
				{
					errors.Add("invalidTeamName", "Đội chứa tên không hợp lệ.");
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Message = "Đội chứa tên không hợp lệ.",
						Errors = errors
					};
				}

				// Gọi repository để lấy danh sách các player chưa có team và có giới tính từ teamName
				var players = await _playerRepository.GetPlayersWithoutTeamByGenderAsync(teamId);

				if (players == null || !players.Any())
				{
					errors.Add("noPlayersFound", "Không tìm thấy cầu thủ chưa có đội.");
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Message = "Không tìm thấy cầu thủ chưa có đội.",
						Errors = errors
					};
				}

				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = "Lấy danh sách cầu thủ chưa có đội thành công.",
					Data = players
				};
			}
			catch (Exception ex)
			{
				// Nếu có lỗi, trả về thông báo lỗi
				errors.Add("exception", $"Lỗi xảy ra: {ex.Message}");
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Message = "Đã có lỗi xảy ra khi lấy danh sách cầu thủ.",
					Errors = errors
				};
			}
		}

		

    }
}
