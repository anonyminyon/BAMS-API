using Amazon.Rekognition.Model;
using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Parent;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Team;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl;
using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Parent = BasketballAcademyManagementSystemAPI.Models.Parent;
using User = BasketballAcademyManagementSystemAPI.Models.User;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
	public class ParentService : IParentService
	{
		private readonly IParentRepository _parentRepository;
		private readonly IMapper _mapper;
		private readonly ISendMailService _sendMailService;
		private readonly GetLoginUserHelper _loginUserHelper;
		private readonly AccountGenerateHelper _accountGenerateHelper;

		public ParentService(IParentRepository parentRepository,IMapper mapper, GetLoginUserHelper loginUserHelper, AccountGenerateHelper accountGenerateHelper, ISendMailService sendMailService)
		{
			_parentRepository = parentRepository;
			_mapper = mapper;
			_loginUserHelper = loginUserHelper;
			_accountGenerateHelper = accountGenerateHelper;
			_sendMailService = sendMailService;
		}

		public async Task<ApiMessageModelV2<object>> AddParentForPlayerAsync(string playerId, string parentId)
		{
			var errors = new Dictionary<string, string>();

			try
			{
				// Kiểm tra cầu thủ có tồn tại không
				var player = await _parentRepository.GetPlayerByIdAsync(playerId);
				if (player == null)
				{
					errors.Add("playerNotFound", "Cầu thủ không tồn tại.");
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors
					};
				}
				if (player.ParentId != null)
				{
					errors.Add("playerHadParent", "Cầu thủ này đã có phụ huynh quản lí.");
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors
					};
				}
				// Kiểm tra phụ huynh có tồn tại không
				var parent = await _parentRepository.GetParentByIdAsync(parentId);
				if (parent == null)
				{
					errors.Add("parentNotFound", "Phụ huynh không tồn tại.");
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors
					};
				}

				// Cập nhật phụ huynh cho cầu thủ
				var isAdded = await _parentRepository.AddParentForPlayerAsync(playerId, parentId);
				if (!isAdded)
				{
					errors.Add("updateFailed", "Không thể cập nhật phụ huynh cho cầu thủ.");
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors
					};
				}

				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = "Đã thêm phụ huynh cho cầu thủ thành công."
				};
			}
			catch (Exception ex)
			{
				// Xử lý ngoại lệ không xác định
				errors.Add("exception", ex.Message);
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors
				};
			}
		}


		public async Task<ApiMessageModelV2<object>> GetParentDetailsAsync(string userId)
		{
			var errors = new Dictionary<string, string>();

			try
			{
				var parent = await _parentRepository.GetParentByIdAsync(userId);

				if (parent == null && !parent.User.IsEnable)
				{
					errors.Add("notFoundParent", "Không tìm thấy thông tin phụ huynh.");
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors
					};
				}

				// Nếu bạn có navigation property User trong Parent
				var result = new
				{
					UserId = parent.UserId,
					CitizenId = parent.CitizenId,
					CreatedByManagerId = parent.CreatedByManagerId,
					Username = parent.User?.Username,
					Fullname = parent.User?.Fullname,
					Email = parent.User?.Email,
					Phone = parent.User?.Phone,
					Address = parent.User?.Address,
					ProfileImage = parent.User?.ProfileImage,
					DateOfBirth = parent.User?.DateOfBirth,
					IsEnable = parent.User?.IsEnable
				};

				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = "Lấy thông tin phụ huynh thành công.",
					Data = result
				};
			}catch(Exception ex)
			{
				errors.Add("exception", ex.Message);
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors,
					Data = null
				};
			}
		}

		public async Task<ApiMessageModelV2<List<PlayerDto>>> GetPlayersByParentIdAsync(string parentId)
		{
			var players = await _parentRepository.GetPlayersOfParentAsync(parentId);

			if (players == null || players.Count == 0)
			{
				return new ApiMessageModelV2<List<PlayerDto>>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Message = "Không tìm thấy học viên nào thuộc phụ huynh này.",
					Data = new List<PlayerDto>()
				};
			}

			var result = players.Select(p => new PlayerDto
			{
				UserId = p.UserId,
				Fullname = p.User?.Fullname,
				Email = p.User?.Email,
				Phone = p.User?.Phone,
				TeamId = p.TeamId,
				TeamName = p.Team?.TeamName,
				RelationshipWithParent = p.RelationshipWithParent,
				Weight = p.Weight,
				Height = p.Height,
				Position = p.Position,
				ShirtNumber = p.ShirtNumber,
				ClubJoinDate = p.ClubJoinDate,
				Address = p.User?.Address,
				DateOfBirth = p.User?.DateOfBirth,
				ProfileImage = p.User?.ProfileImage
			}).ToList();

			return new ApiMessageModelV2<List<PlayerDto>>
			{
				Status = ApiResponseStatusConstant.SuccessStatus,
				Message = "Lấy danh sách học viên thành công.",
				Data = result
			};
		}

		public async Task<ApiMessageModelV2<object>> CreateParentAccountAsync(CreateParentRequestDto dto)
		{
			var errors = new Dictionary<string, string>();

			if (await _parentRepository.IsEmailExistsAsync(dto.Email))
				errors.Add("email", "Email này đã tồn tại");

			if (!string.IsNullOrWhiteSpace(dto.CitizenId) && await _parentRepository.IsCitizenIdExistsAsync(dto.CitizenId))
				errors.Add("citizenId", "Căn cước đã tồn tại trong hệ thống");

			if (errors.Any())
			{
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors
				};
			}
			//tạo tk phụ huynh
			var userId = Guid.NewGuid().ToString();
			var now = DateTime.Now;
			var generateParentUsername = _accountGenerateHelper.GetUniqueUsername(dto.Fullname);
			var generateParentPassword = _accountGenerateHelper.GeneratePassword();
			var generateParentPasswordEncrypt = BCrypt.Net.BCrypt.HashPassword(generateParentPassword);
			var user = new User
			{
				UserId = userId,
				Username = generateParentUsername,
				Password = generateParentPasswordEncrypt, // Lưu ý: nên hash password!
				Fullname = dto.Fullname,
				Email = dto.Email,
				Phone = dto.Phone,
				Address = dto.Address,
				DateOfBirth = dto.DateOfBirth,
				RoleCode = RoleCodeConstant.ParentCode,
				CreatedAt = now,
				IsEnable = true
			};

			var parent = new Parent
			{
				UserId = userId,
				CitizenId = dto.CitizenId,
				CreatedByManagerId = _loginUserHelper.GetUserIdLoggedIn()

			};
			
			await _parentRepository.AddUserAndParentAsync(user, parent);
			await _parentRepository.SaveChangesAsync();
			// Kiểm tra cầu thủ có tồn tại không
			var player = await _parentRepository.GetPlayerByIdAsync(dto.PlayerId);
			if (player == null)
			{
				errors.Add("playerNotFound", "Cầu thủ không tồn tại.");
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors
				};
			}
		
			// Cập nhật phụ huynh cho cầu thủ
			var isAdded = await _parentRepository.AddParentForPlayerAsync(dto.PlayerId, userId);
			if (!isAdded)
			{
				errors.Add("updateFailed", "Không thể cập nhật phụ huynh cho cầu thủ.");
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors
				};
			}
			//_sendMailService.SendMailByMailTemplateIdAsync(user.Email,)
			return new ApiMessageModelV2<object>
			{
				Status = ApiResponseStatusConstant.SuccessStatus,
				Message = "Đã tạo thành công phụ huynh"
			};

		}

		public async Task<ApiMessageModelV2<List<ParentDto>>> FilterParentsAsync(ParentFilterDto filter)
		{
			var errors = new Dictionary<string, string>();

			try
			{
				var parents = await _parentRepository.FilterParentsAsync(filter);
				var parentDtos = parents.Select(p => new ParentDto
				{
					UserId = p.UserId,
					Fullname = p.User?.Fullname,
					Email = p.User?.Email,
					Phone = p.User?.Phone,
					CitizenId = p.CitizenId,
					Address = p.User?.Address,
					DateOfBirth = p.User?.DateOfBirth
				}).ToList();


				return new ApiMessageModelV2<List<ParentDto>>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = "Lọc danh sách phụ huynh thành công.",
					Data = parentDtos
				};
			}
			catch (Exception ex)
			{
				errors.Add("exception", ex.Message);
				return new ApiMessageModelV2<List<ParentDto>>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Message = "Lỗi khi lọc danh sách phụ huynh.",
					Errors = errors
				};
			}

		}
	}
}
