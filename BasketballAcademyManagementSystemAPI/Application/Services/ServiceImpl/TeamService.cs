using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.CourtManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.PlayerManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Team;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.ExtendedProperties;
using System.Numerics;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
	public class TeamService : ITeamService
	{
		private readonly ITeamRepository _teamRepository;
		private readonly IMapper _mapper; // Sử dụng AutoMapper để map entity sang DTO
		private readonly GetLoginUserHelper _getLoginUserHelper;
		private readonly IUserTeamHistoryService _userTeamHistoryService;
		private readonly IManagerRepository _managerRepository;

		public TeamService(ITeamRepository teamRepository, 
			IMapper mapper,
			GetLoginUserHelper getLoginUserHelper,
			IUserTeamHistoryService userTeamHistoryService,
			IManagerRepository managerRepository
			)
		{
			_teamRepository = teamRepository;
			_mapper = mapper;
			_getLoginUserHelper = getLoginUserHelper;
			_userTeamHistoryService = userTeamHistoryService;
			_managerRepository = managerRepository;	
		
		}
		public async Task<ApiMessageModelV2<TeamDto>> CreateTeamAsync(string teamName, int? status)
		{
			var errors = new Dictionary<string, string>();

			// Validate input
			if (string.IsNullOrWhiteSpace(teamName))
				errors.Add("teamEmpty", TeamMessage.Errors.TeamNameEmpty);

			if (teamName.Length > 50)
				errors.Add("largerTeamNameLength", TeamMessage.Errors.LargerTeamNameLength); // <- Bạn đang copy nhầm message (TeamNameEmpty), cần đúng lại message

			bool isExist = await _teamRepository.IsTeamNameExistsAsync(teamName);
			if (isExist)
				errors.Add("existTeamName", TeamMessage.Errors.ExistTeamName);

			// Nếu có lỗi validate thì trả luôn lỗi
			if (errors.Any())
			{
				return new ApiMessageModelV2<TeamDto>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors,
					Data = new TeamDto
					{
						TeamName = teamName,
						Status = status ?? 1,
						CreateAt = DateTime.Now,
					}
				};
			}

			// Nếu validate thành công thì tiếp tục
			TeamDto newTeam = new TeamDto
			{
				TeamId = Guid.NewGuid().ToString(),
				TeamName = teamName,
				Status = status ?? 1,
				CreateAt = DateTime.Now,
			};

			try
			{
				await _teamRepository.AddTeamAsync(_mapper.Map<Team>(newTeam));
			}
			catch (Exception ex)
			{
				// Khi lỗi xảy ra, phải trả ngay Failed
				return new ApiMessageModelV2<TeamDto>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Message = "Đã xảy ra lỗi hệ thống.",
					Errors = new Dictionary<string, string> { { "exception", ex.Message } }
				};
			}

			// Nếu không có lỗi thì return success
			return new ApiMessageModelV2<TeamDto>
			{
				Status = ApiResponseStatusConstant.SuccessStatus,
				Message = "Tạo đội thành công",
				Data = newTeam
			};
		}

		public async Task<ApiMessageModelV2<TeamDetailsDto>> GetTeamDetailsAsync(string teamId)
		{
			var errors = new Dictionary<string, string>();
			dynamic? team = new TeamDetailsDto();
			try
			{
				team = await _teamRepository.GetTeamDtoByIdAsync(teamId);
				if (team == null)
				{
					errors.Add("notExistTeam", TeamMessage.Errors.NotExistTeam);
				}

				//Nếu tìm thấy lỗi thì trả về danh sách lỗi
				if (errors.Any())
				{
					return new ApiMessageModelV2<TeamDetailsDto>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors,
					};
				}

			}
			catch (Exception ex)
			{
				errors.Add("exception", ex.Message);
			}
			//trả về nếu thành công
			return new ApiMessageModelV2<TeamDetailsDto>
			{
				Status = ApiResponseStatusConstant.SuccessStatus,
				Message = TeamMessage.Success.GetSuccessTeam,
				Data = _mapper.Map<TeamDetailsDto>(team)
			};

		}

		//President
		public async Task<ApiMessageModelV2<TeamDto>> UpdateTeamInfoAsync(string teamId, string teamName, int status)
		{
			var errors = new Dictionary<string, string>();

			try
			{
				// Kiểm tra xem team có tồn tại không
				var team = await _teamRepository.GetTeamByIdAsync(teamId);

				if (team == null)
				{
					errors.Add("notFound", TeamMessage.Errors.NotExistTeam);
					return new ApiMessageModelV2<TeamDto>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors
					};
				}

				// Kiểm tra tên đội rỗng
				if (string.IsNullOrWhiteSpace(teamName))
					errors.Add("teamEmpty", TeamMessage.Errors.TeamNameEmpty);

				// Kiểm tra độ dài tên đội
				if (teamName.Length > 50)
					errors.Add("largerTeamNameLength", TeamMessage.Errors.LargerTeamNameLength);

				// Kiểm tra xem tên đội đã tồn tại chưa
				bool isExist = await _teamRepository.IsTeamNameExistsAsync(teamName, teamId);
				if (isExist)
					errors.Add("existTeamName", TeamMessage.Errors.ExistTeamName);
				// Nếu có lỗi thì trả về
				if (errors.Any())
				{
					return new ApiMessageModelV2<TeamDto>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors,
						Data = new TeamDto
						{
							TeamName = teamName,
							Status = status
						}
					};
				}

				// Cập nhật team
				team.TeamName = teamName;
				team.Status = status;
				await _teamRepository.UpdateTeamAsync(team);
				await _teamRepository.SaveChangesAsync();

				// Trả về kết quả thành công
				return new ApiMessageModelV2<TeamDto>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = TeamMessage.Success.UpdateSuccessTeam,
					Data = new TeamDto
					{
						TeamId = teamId,
						TeamName = teamName,
						Status = status
					}
				};
			}
			catch (Exception ex)
			{
				errors.Add("exception", ex.Message);

				return new ApiMessageModelV2<TeamDto>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors
				};
			}

		}

		public async Task<ApiMessageModelV2<List<PlayerRemoveResultDto>>> RemovePlayersAsync(string teamId, List<string> playerIds, DateTime leftDate)
		{
			var resultList = new List<PlayerRemoveResultDto>();
			var errors = new Dictionary<string, string>();

			// Kiểm tra đầu vào
			if (string.IsNullOrWhiteSpace(teamId))
				errors.Add("teamIdEmpty", TeamMessage.Errors.TeamIdEmpty);

			if (playerIds == null || !playerIds.Any())
				errors.Add("playerIdsEmpty", TeamMessage.Errors.PlayerIdsEmpty);

			if (errors.Any())
			{
				return new ApiMessageModelV2<List<PlayerRemoveResultDto>>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors
				};
			}

			try
			{
				var players = await _teamRepository.GetPlayersByTeamAsync(teamId, playerIds);

				if (!players.Any())
				{
					errors.Add("playersNotFound", TeamMessage.Errors.PlayersNotFound);
					return new ApiMessageModelV2<List<PlayerRemoveResultDto>>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors
					};
				}

				foreach (var player in players)
				{
					var result = new PlayerRemoveResultDto { PlayerId = player.UserId };
					try
					{
						player.TeamId = null;

						// update thông tin rời đội ở user team history
						await _userTeamHistoryService.UpdateLeftDateByUserIdAndTeamId(player.UserId, teamId, leftDate);

						result.Success = true;
						result.Message = "Xoá khỏi đội thành công.";
					}
					catch (Exception ex)
					{
						result.Success = false;
						result.Message = $"Lỗi khi cập nhật lịch sử rời đội: {ex.Message}";
					}
					resultList.Add(result);
				}

				// Lưu thay đổi vào database
				await _teamRepository.SaveChangesAsync();

				return new ApiMessageModelV2<List<PlayerRemoveResultDto>>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = TeamMessage.Success.RemovePlayerSuccess,
					Data = resultList
				};
			}
			catch (Exception ex)
			{
				errors.Add("exception", ex.Message);
				return new ApiMessageModelV2<List<PlayerRemoveResultDto>>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Message = "Có lỗi xảy ra khi xử lý yêu cầu.",
					Errors = errors
				};
			}
		}



		//Xóa 1 hoặc nhiều coaches theo id
		public async Task<ApiMessageModelV2<bool>> RemoveCoachesAsync(string teamId, List<string> coachIds, DateTime leftDate)
		{
			var errors = new Dictionary<string, string>();

			try
			{
				// Kiểm tra Team ID hợp lệ
				if (string.IsNullOrWhiteSpace(teamId))
					errors.Add("teamIdEmpty", TeamMessage.Errors.TeamIdEmpty);

				// Kiểm tra danh sách Coach ID hợp lệ
				if (coachIds == null || !coachIds.Any())
					errors.Add("coachIdsEmpty", TeamMessage.Errors.CoachIdsEmpty);

				// Nếu có lỗi validation, trả về lỗi ngay
				if (errors.Any())
				{
					return new ApiMessageModelV2<bool>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors
					};
				}

				// Lấy danh sách các huấn luyện viên trong team
				var coaches = await _teamRepository.GetCoachesByTeamAsync(teamId, coachIds);

				// Nếu không tìm thấy huấn luyện viên
				if (!coaches.Any())
				{
					errors.Add("coachesNotFound", TeamMessage.Errors.CoachesNotFound);
					return new ApiMessageModelV2<bool>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors
					};
				}

				// Xóa huấn luyện viên khỏi team
				foreach (var coach in coaches)
				{
					coach.TeamId = null;
                    //update thông tin rời đội ở user team history
                    await _userTeamHistoryService.UpdateLeftDateByUserIdAndTeamId(coach.UserId, teamId, leftDate);
                }

				// Lưu thay đổi vào database
				await _teamRepository.SaveChangesAsync();

				// Trả về kết quả thành công
				return new ApiMessageModelV2<bool>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = TeamMessage.Success.RemoveCoachSuccess,
					Data = true
				};
			}
			catch (Exception ex)
			{
				errors.Add("exception", ex.Message);
				return new ApiMessageModelV2<bool>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors
				};
			}
		}

		public async Task<ApiMessageModelV2<bool>> RemoveManagersAsync(string teamId, List<string> managerIds, DateTime leftDate)
		{
			var errors = new Dictionary<string, string>();

			try
			{
				// Kiểm tra Team ID hợp lệ
				if (string.IsNullOrWhiteSpace(teamId))
					errors.Add("teamIdEmpty", TeamMessage.Errors.TeamIdEmpty);

				// Kiểm tra danh sách Manager ID hợp lệ
				if (managerIds == null || !managerIds.Any())
					errors.Add("managerIdsEmpty", TeamMessage.Errors.ManagerIdsEmpty);

				// Nếu có lỗi validation, trả về lỗi ngay
				if (errors.Any())
				{
					return new ApiMessageModelV2<bool>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors
					};
				}

				// Lấy danh sách các quản lý trong team
				var managers = await _teamRepository.GetManagersByTeamAsync(teamId, managerIds);

				// Nếu không tìm thấy quản lý
				if (!managers.Any())
				{
					errors.Add("managersNotFound", TeamMessage.Errors.ManagersNotFound);
					return new ApiMessageModelV2<bool>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors
					};
				}

				// Xóa quản lý khỏi team
				foreach (var manager in managers)
				{
					manager.TeamId = null;
                    //update thông tin rời đội ở user team history
                    await _userTeamHistoryService.UpdateLeftDateByUserIdAndTeamId(manager.UserId, teamId, leftDate);
                }

				// Lưu thay đổi vào database
				await _teamRepository.SaveChangesAsync();

				// Trả về kết quả thành công
				return new ApiMessageModelV2<bool>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = TeamMessage.Success.RemoveManagerSuccess,
					Data = true
				};
			}
			catch (Exception ex)
			{
				errors.Add("exception", ex.Message);
				return new ApiMessageModelV2<bool>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors
				};
			}
		}
		public async Task<ApiMessageModelV2<object>> UpdateFundManagerAsync(string teamId, string managerUserId)
		{
			var errors = new Dictionary<string, string>();

			try
			{
				var team = await _teamRepository.GetTeamByIdAsync(teamId);
				if (team == null)
				{
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Message = "Không tìm thấy đội bóng."
					};
				}

				var user = await _managerRepository.GetManagerByUserIdAsync(managerUserId);
				if (user == null)
				{
					errors["UserId"] = "Người dùng không tồn tại.";
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Message = "Người dùng không tồn tại.",
						Errors = errors
					};
				}

				var manager = await _managerRepository.GetManagerByUserIdAsync(managerUserId);
				if (manager == null || manager.TeamId != teamId)
				{
					errors["Manager"] = "Người dùng không phải là quản lý thuộc đội bóng này.";
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Message = "Người dùng không thuộc đội bóng hoặc không phải quản lý.",
						Errors = errors
					};
				}

				var updated = await _teamRepository.UpdateFundManagerIdAsync(teamId, managerUserId);

				return new ApiMessageModelV2<object>
				{
					Status = updated ? ApiResponseStatusConstant.SuccessStatus : ApiResponseStatusConstant.FailedStatus,
					Message = updated ? "Cập nhật người quản lý quỹ thành công." : "Cập nhật thất bại."
				};
			}
			catch (Exception ex)
			{
				errors["Exception"] = ex.Message;
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Message = "Lỗi hệ thống.",
					Errors = errors
				};
			}
		}
		#region ===DisbandTeam===
		public async Task<ApiMessageModelV2<object>> DisbandTeamAsync(string teamId, string note)
		{
			var removeByUserId = _getLoginUserHelper.GetUserIdLoggedIn();
			var errors = new Dictionary<string, string>();

			// Kiểm tra sự tồn tại của đội
			var team = await _teamRepository.GetTeamByIdAsync(teamId);
			if (team == null)
			{
				errors.Add("teamNotFound", TeamMessage.Errors.NotExistTeam);
				
			}
			//Nếu team đã bị giải thể
			if(team.Status == TeamStatusConstant.DISBAND)
			{
				errors.Add("disbandedTeam", TeamMessage.Errors.DisbandedTeam);

			}

			if (errors.Any())
			{
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors
				};
			}

			// Cập nhật trạng thái đội 
			await UpdateTeamStatus(team);

			// Lưu lịch sử người dùng rời đội vào UserTeamHistory
			await SaveUserTeamHistory(teamId, note, removeByUserId);

			await UpdateTeamForMembers(teamId);

			//Tính khoản chi từng thành viên trong đội
			return new ApiMessageModelV2<object>
			{
				Status = ApiResponseStatusConstant.SuccessStatus,
				Message = TeamMessage.Success.DisbandTeamSuccess
			};
		}

		private async Task UpdateTeamStatus(Team team)
		{
			// Cập nhật trạng thái đội thành bằng TeamStatusConstant.DISBAND
			team.Status = TeamStatusConstant.DISBAND;
			await _teamRepository.SaveChangesAsync();

		}

		private async Task SaveUserTeamHistory(string teamId, string note, string removeByUserId)
		{		
			var members = await _teamRepository.GetAllPlayersByTeamAsync(teamId);
			foreach (var member in members)
			{
				var userTeamHistory = new UserTeamHistory
				{
					UserId = member.UserId,
					TeamId = teamId,
					LeftDate = DateTime.Now,  // Ngày rời đội
					JoinDate = member.ClubJoinDate.ToDateTime(new TimeOnly(0, 0)),
					Note = note,
					RemovedByUserId = removeByUserId  // Có thể thêm người thực hiện nếu cần
				};

				await _teamRepository.AddUserTeamHistoryAsync(userTeamHistory);
			}
			await _teamRepository.SaveChangesAsync();
		}

		private async Task UpdateTeamForMembers(string teamId)
		{
			// Xóa tất cả cầu thủ khỏi đội
			var players = await _teamRepository.GetAllPlayersByTeamAsync(teamId);
			foreach (var player in players)
			{
				player.TeamId = null;
			}

			// Xóa tất cả huấn luyện viên khỏi đội
			var coaches = await _teamRepository.GetAllCoachesByTeamAsync(teamId);
			foreach (var coach in coaches)
			{
				coach.TeamId = null;
			}

			// Xóa tất cả quản lý khỏi đội
			var managers = await _teamRepository.GetAllManagersByTeamAsync(teamId);
			foreach (var manager in managers)
			{
				manager.TeamId = null;
			}
			await _teamRepository.SaveChangesAsync();

		}
		#endregion

		//Filter + paging team
		public async Task<ApiMessageModelV2<PagedResponseDto<TeamDto>>> GetTeamsAsync(TeamFilterDto filter)
		{
			var errors = new Dictionary<string, string>();

			try
			{
				// Gọi repository để lấy dữ liệu phân trang
				var result = await _teamRepository.GetTeamsAsync(filter);

				// Kiểm tra nếu không có dữ liệu
				if (result.Items == null || !result.Items.Any())
				{
					errors.Add("teamInfoNotFound", TeamMessage.Errors.NotFoundFilter);
					return new ApiMessageModelV2<PagedResponseDto<TeamDto>>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors
					};
				}

				// Trả về kết quả thành công
				return new ApiMessageModelV2<PagedResponseDto<TeamDto>>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = TeamMessage.Success.GetSuccessTeam,
					Data = result
				};
			}
			catch (Exception ex)
			{
				errors.Add("exception", ex.Message);
				return new ApiMessageModelV2<PagedResponseDto<TeamDto>>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors
				};
			}
		}



	}
}
