using AutoMapper;
using BasketballAcademyManagementSystemAPI.API.Controllers;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.CallToTryOut;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Team;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl;
using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Text.RegularExpressions;
using static BasketballAcademyManagementSystemAPI.Common.Messages.AuthenticationMessage;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
	public class PlayerRegistrationService : IPlayerRegistrationService
	{
		private readonly IPlayerRegistrationRepository _playerRegistrationRepository;
		private readonly IMailTemplateRepository _emailRepository;
		private readonly IEmailVerificationRepository _emailVerification;
		private readonly IMapper _mapper;
		private readonly IOtpService _otpService;
		private readonly ISendMailService _sendMailService;
		private readonly EmailHelper _emailHelper;
		private readonly IMemberRegistrationSessionRepository _memberRegistrationSession;
		private readonly GetLoginUserHelper _getLoginUserHelper;
		private readonly AccountGenerateHelper _accountGenerateHelper;
		public PlayerRegistrationService(
			IPlayerRegistrationRepository playerRepository,
			IMailTemplateRepository emailRepository,
			IMapper mapper,
			EmailHelper emailHelper,
			IEmailVerificationRepository emailVerification,
			IOtpService otpService,
			ISendMailService sendMailService,
			IMemberRegistrationSessionRepository memberRegistrationSession,
			GetLoginUserHelper getLoginUserHelper,
			AccountGenerateHelper accountGenerateHelper
			)
		{
			_sendMailService = sendMailService;
			_emailRepository = emailRepository;
			_emailVerification = emailVerification;
			_playerRegistrationRepository = playerRepository;
			_mapper = mapper;
			_emailHelper = emailHelper;
			_otpService = otpService;
			_memberRegistrationSession = memberRegistrationSession;
			_getLoginUserHelper = getLoginUserHelper;
			_accountGenerateHelper = accountGenerateHelper;
		}
		public async Task<ApiMessageModelV2<object>> GetPlayers(
			int? memberRegistrationSessionId,
			string? email,
			DateTime? startDate,
			DateTime? endDate,
			int? minAge,
			int? maxAge,
			bool? gender,
			string? status)
		{
			// Gọi Repository để lấy danh sách players (không phân trang)
			var players = await _playerRegistrationRepository.GetPlayers(
				memberRegistrationSessionId,
				email,
				startDate,
				endDate,
				minAge,
				maxAge,
				gender,
				status
			);

			// Trả về kết quả
			return new ApiMessageModelV2<object>
			{
				Status = ApiResponseStatusConstant.SuccessStatus,
				Message = "Lấy danh sách người chơi thành công.",
				Data = players
			};
		}

		#region đăng kí form player
		public async Task<ApiMessageModelV2<object>> RegisterPlayerAsync(PlayerRegistrationDto playerDto)
		{
			var errors = new Dictionary<string, string>();

			try
			{
				// Chuyển đổi DateOnly sang DateTime để có thể so sánh với Today
				DateOnly today = DateOnly.FromDateTime(DateTime.Today);
				DateOnly dateOfBirth = playerDto.DateOfBirth;

				// Tính tuổi
				int age = today.Year - dateOfBirth.Year;

				// Kiểm tra nếu sinh nhật chưa tới trong năm nay
				if (dateOfBirth > today.AddYears(-age))
				{
					age--; // Nếu sinh nhật chưa tới trong năm nay, giảm tuổi xuống 1
				}

				bool isUnder18 = age < 18; // Kiểm tra xem có dưới 18 tuổi hay không




				// Kiểm tra dữ liệu học sinh
				await ValidateStudentInformation(playerDto, errors);
				// Kiểm tra định dạng email của player

				//Check email có thỏa mãn đc đăng kí hay ko
				await ValidateEmailPlayerRegistrationForm(playerDto.Email, playerDto.MemberRegistrationSessionId.ToString(), RoleCodeConstant.PlayerCode, errors);

				// Kiểm tra dữ liệu phụ huynh (luôn kiểm tra, nhưng bắt buộc nếu học sinh dưới 18 tuổi)
				await ValidateParentInformation(playerDto, isUnder18, errors);

				//Check email phụ huynh có thỏa mãn đc đăng kí hay ko
				await ValidateEmailPlayerRegistrationForm(playerDto.ParentEmail, playerDto.MemberRegistrationSessionId.ToString(), RoleCodeConstant.ParentCode, errors);

				// Nếu có lỗi, trả về dữ liệu đã nhập và thông báo lỗi
				if (errors.Any())
				{
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors,
						Data = playerDto
					};
				}

				// Nếu email đã tồn tại trong bảng playerRegistration,xóa đơn cũ

				//var oldPlayerRegisterForm = await _playerRegistrationRepository.GetPlayersOldForm(playerDto.MemberRegistrationSessionId, playerDto.Email);
				//if (oldPlayerRegisterForm != null)
				//{
				//	await _playerRegistrationRepository.DeleteByEmailAndSessionIdAsync(playerDto.MemberRegistrationSessionId, playerDto.Email);
				//}
				//// Nếu email chưa tồn tại, tạo mới bản ghi

				var player = _mapper.Map<PlayerRegistration>(playerDto);
				await _playerRegistrationRepository.RegisterPlayerAsync(player);

				// Gửi email thông báo đăng ký thành công
				await _sendMailService.SendMailByMailTemplateIdAsync(MailTemplateConstant.SendFormRegistrationSuccess, playerDto.Email, null);

				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = PlayerRegistrationMessage.Success.RegistrationSubmitSuccess,
					Data = null
				};
			}
			catch (Exception ex)
			{
				errors.Add("exception", ex.Message);
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors,
					Data = playerDto
				};
			}
		}


		private async Task ValidateEmailPlayerRegistrationForm(string? email, string memberRegistrationSessionId, string typeEmail, Dictionary<string, string> errors)
		{
			// Xác định errorKey dựa trên loại email
			var errorKey = typeEmail.Equals(RoleCodeConstant.ParentCode) ? "errorParentEmail" : "errorPlayerEmail";


			// Kiểm tra email đã tồn tại trong Manager Registration
			if (await _playerRegistrationRepository.IsEmailExistsInManagerRegistration(email, memberRegistrationSessionId))
			{
				errors.Add(errorKey, "Email đã được nộp đơn với tư cách khác, xin vui lòng sử dụng email mới");
				return;
			}

			// Kiểm tra email đã tồn tại trong User
			if (await _playerRegistrationRepository.IsEmailExistsInUser(email))
			{
				errors.Add(errorKey, "Email học sinh hoặc phụ huynh đã tồn tại trong hệ thống, xin vui lòng sử dụng email khác");
				return;
			}

			// Kiểm tra email đã tồn tại trong Player Registration của mùa hiện tại
			if (await _playerRegistrationRepository.IsEmailExistsInPlayerRegistration(email, memberRegistrationSessionId))
			{
				errors.Add(errorKey, "Email đã nộp đơn trước đó vào mùa này");
				return;
			}
		}

		private async Task ValidateStudentInformation(PlayerRegistrationDto playerRegistrationDto, Dictionary<string, string> errors)
		{

			//Kiểm tra email đã đc verfied chưa
			if (!await _emailVerification.IsEmailVerified(playerRegistrationDto.Email))
			{
				{
					errors.Add("errorEmail", PlayerRegistrationMessage.Errors.EmailNeedVerified);
				}
			}

			// Kiểm tra nếu email học sinh và email phụ huynh giống nhau
			if (!string.IsNullOrWhiteSpace(playerRegistrationDto.ParentEmail) && playerRegistrationDto.Email == playerRegistrationDto.ParentEmail)
			{
				errors.Add("duplicateEmailParentAndPlayer", PlayerRegistrationMessage.Errors.DuplicateEmailRegister);
			}

			// Kiểm tra ngày sinh hợp lệ
			if (playerRegistrationDto.DateOfBirth > DateOnly.FromDateTime(DateTime.Today))
			{
				errors.Add("invalidDateOfBirth", PlayerRegistrationMessage.Errors.InvalidParentPhoneNumber);
			}

			// Kiểm tra chiều cao và cân nặng
			if (playerRegistrationDto.Height <= 0 || playerRegistrationDto.Weight <= 0)
			{
				errors.Add("invalidHeightWeight", PlayerRegistrationMessage.Errors.InvalidHeightWeight);
			}

			// Kiểm tra vị trí thi đấu bắt buộc nhập
			if (string.IsNullOrWhiteSpace(playerRegistrationDto.Position))
			{
				errors.Add("positionRequired", PlayerRegistrationMessage.Errors.PositionRequired);
			}
		}

		private async Task ValidateParentInformation(PlayerRegistrationDto playerRegisterFormDto, bool isUnder18, Dictionary<string, string> errors)
		{
			// Kiểm tra thông tin phụ huynh bắt buộc với người dưới 18 tuổi
			if (isUnder18)
			{

				if (string.IsNullOrWhiteSpace(playerRegisterFormDto.ParentName))
					errors.Add("parentNameRequired", PlayerRegistrationMessage.Errors.ParentNameRequired);

				if (string.IsNullOrWhiteSpace(playerRegisterFormDto.ParentPhoneNumber))
					errors.Add("parentPhoneRequired", PlayerRegistrationMessage.Errors.ParentPhoneRequired);
				else if (!RegexHelper.IsValidVietnamesePhoneNumber(playerRegisterFormDto.ParentPhoneNumber))
					errors.Add("invalidParentPhoneNumber", PlayerRegistrationMessage.Errors.InvalidParentPhoneNumber);

				if (string.IsNullOrWhiteSpace(playerRegisterFormDto.ParentEmail))
					errors.Add("parentEmailRequired", PlayerRegistrationMessage.Errors.ParentEmailRequired);
				else if (!RegexHelper.IsValidEmail(playerRegisterFormDto.ParentEmail))
					errors.Add("invalidParentEmail", PlayerRegistrationMessage.Errors.InvalidParentEmail);

				if (string.IsNullOrWhiteSpace(playerRegisterFormDto.RelationshipWithParent))
					errors.Add("relationshipWithParentRequired", "Vui lòng nhập mối quan hệ với phụ huynh.");

				if (string.IsNullOrWhiteSpace(playerRegisterFormDto.ParentCitizenId))
					errors.Add("parentCitizenIdRequired", "Vui lòng nhập số CCCD của phụ huynh.");
				else if (await _playerRegistrationRepository.IsExistedParentByCitizen(playerRegisterFormDto.ParentCitizenId))
					errors.Add("existedParentCitizenId", PlayerRegistrationMessage.Errors.IsExistedParentCitizenId);
			}
			else // >= 18 tuổi
			{
				if (!string.IsNullOrWhiteSpace(playerRegisterFormDto.ParentPhoneNumber) &&
					!RegexHelper.IsValidVietnamesePhoneNumber(playerRegisterFormDto.ParentPhoneNumber))
				{
					errors.Add("invalidParentPhoneNumber", PlayerRegistrationMessage.Errors.InvalidParentPhoneNumber);
				}

				if (!string.IsNullOrWhiteSpace(playerRegisterFormDto.ParentEmail) &&
					!RegexHelper.IsValidEmail(playerRegisterFormDto.ParentEmail))
				{
					errors.Add("invalidParentEmail", PlayerRegistrationMessage.Errors.InvalidParentEmail);
				}

				if (!string.IsNullOrWhiteSpace(playerRegisterFormDto.ParentCitizenId) &&
					await _playerRegistrationRepository.IsExistedParentByCitizen(playerRegisterFormDto.ParentCitizenId))
				{
					errors.Add("existedParentCitizenId", PlayerRegistrationMessage.Errors.IsExistedParentCitizenId);
				}
			}

			// Check nếu số điện thoại phụ huynh và học sinh giống nhau
			if (!string.IsNullOrWhiteSpace(playerRegisterFormDto.ParentPhoneNumber) &&
				playerRegisterFormDto.PhoneNumber == playerRegisterFormDto.ParentPhoneNumber)
			{
				errors.Add("duplicatePhoneNumber", PlayerRegistrationMessage.Errors.DuplicateEmailRegister);
			}
		}

		#endregion
		public async Task<bool> AddTryOutNote(string playerRegisterId, string tryOutNote)
		{
			return await _playerRegistrationRepository.AddTryOutNote(playerRegisterId, tryOutNote);
		}

		public async Task<ApiMessageModelV2<object>> UpdatePlayerRegisterFormAsync(PlayerRegistrationDto playerDto)
		{
			var errors = new Dictionary<string, string>();

			try
			{
				var existingPlayer = await _playerRegistrationRepository.GetPlayerRegistrationByIdAsync(playerDto.PlayerRegistrationId);

				//khi update đơn, mà trạng thái của đơn khác Pending, tức là đơn đã chuyển sang approved or reject thì báo lỗi
				if (existingPlayer.Status != RegistrationStatusConstant.PENDING)
				{

					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Message = "Đơn liên kết với email này đang trong quá trình xét duyệt. Vui lòng chọn email khác"
					};
				}


				// Sử dụng AutoMapper để ánh xạ toàn bộ các trường từ DTO vào Entity
				_mapper.Map(playerDto, existingPlayer);


				// Cập nhật thông tin vào database
				await _playerRegistrationRepository.UpdatePlayerRegistrationAsync(existingPlayer);

				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = "Đã cập nhật lại thông tin đơn nộp"
				};
			}
			catch (Exception ex)
			{
				// Xử lý lỗi nếu có
				errors.Add("Exception", ex.Message);
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors
				};
			}
		}

		public async Task<ApiMessageModelV2<object>> UpdateStatusForm(int registrationId, string status)
		{
			var registration = await _playerRegistrationRepository.GetRegistrationByIdAsync(registrationId);

			if (registration == null)
			{
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Message = "Không tìm thấy đơn đăng ký."
				};
			}

			var currentStatus = registration.Status;

			// Kiểm tra thứ tự chuyển trạng thái
			var validTransitions = new Dictionary<string, string>
			{
				{ RegistrationStatusConstant.CALLEDTOTRYOUT, RegistrationStatusConstant.CHECKEDIN },
				{ RegistrationStatusConstant.CHECKEDIN, RegistrationStatusConstant.SCOREDTRYOUT }
			};

			if (!validTransitions.TryGetValue(currentStatus, out var expectedNextStatus) || expectedNextStatus != status)
			{
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Message = $"Không thể chuyển từ trạng thái đơn'{currentStatus}' sang '{status}'. Chỉ cho phép chuyển lần lượt theo đúng luồng."
				};
			}

			// Tiến hành cập nhật
			var success = await _playerRegistrationRepository.UpdateRegistrationStatusAsync(
				registrationId,
				status,
				_getLoginUserHelper.GetUserIdLoggedIn()
			);

			if (!success)
			{
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Message = "Không tìm thấy đơn đăng ký hoặc cập nhật thất bại."
				};
			}

			return new ApiMessageModelV2<object>
			{
				Status = ApiResponseStatusConstant.SuccessStatus,
				Message = "Cập nhật trạng thái thành công."
			};
		}

		public async Task<ApiMessageModelV2<object>> ApproveRegistrationAsync(int registrationId)
		{
			var errors = new Dictionary<string, string>();
			//Lấy Id manager logined 
			var managerId = _getLoginUserHelper.GetUserIdLoggedIn();
			try
			{

				// Kiểm tra đơn đăng ký
				var registration = await _playerRegistrationRepository.GetRegistrationByIdAsync(registrationId);
				if (registration == null || registration.Status != RegistrationStatusConstant.SCOREDTRYOUT)
				{
					errors.Add("RegistrationNotFound", "Đơn đăng kí cần được chấm điểm trước khi duyệt");
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors
					};
				}

				// Kiểm tra nếu user đã tồn tại
				var existingUser = await _playerRegistrationRepository.GetUserByEmailAsync(registration.Email);
				if (existingUser != null)
				{
					errors.Add("UserExists", "User đã tồn tại với email " + registration.Email);
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors
					};
				}

				// Tạo tài khoản Player mới
				var generatePlayerUsername = _accountGenerateHelper.GetUniqueUsername(registration.FullName);
				var generatePlayerPassword = _accountGenerateHelper.GeneratePassword();
				var generatePlayerPasswordEncrypt = BCrypt.Net.BCrypt.HashPassword(generatePlayerPassword);
				var playerUser = new User
				{
					UserId = Guid.NewGuid().ToString(),
					Username = generatePlayerUsername,
					Password = generatePlayerPasswordEncrypt,
					Fullname = registration.FullName,
					Email = registration.Email,
					Phone = registration.PhoneNumber,
					Gender = registration.Gender,
					Address = "N/A",
					DateOfBirth = registration.DateOfBirth,
					RoleCode = "Player",
					CreatedAt = DateTime.Now,
					IsEnable = true
				};

				await _playerRegistrationRepository.AddUserAsync(playerUser);

				//------- Xử lí cho parent
				dynamic parentUser = null;
				dynamic generateParentUsername = null;
				dynamic generateParentPassword = null;
				// Tạo tài khoản Parent nếu có
				string parentId = null;
				if (!string.IsNullOrEmpty(registration.ParentEmail))
				{
					generateParentUsername = _accountGenerateHelper.GetUniqueUsername(registration.ParentName);
					generateParentPassword = _accountGenerateHelper.GeneratePassword();
					var generateParentPasswordEncrypt = BCrypt.Net.BCrypt.HashPassword(generateParentPassword);

					//kiểm tra đã tồn tại parent chưa
					var existingParentByEmail = await _playerRegistrationRepository.GetParentByEmailAsync(registration.ParentEmail);
					var existingParentByCitizen = await _playerRegistrationRepository.IsExistedParentByCitizen(registration.ParentCitizenId);
					if (existingParentByEmail == null)
					{
						parentUser = new User
						{
							UserId = Guid.NewGuid().ToString(),
							Username = generateParentUsername,
							Password = generateParentPasswordEncrypt,
							Fullname = registration.ParentName,
							Email = registration.ParentEmail,
							Phone = registration.ParentPhoneNumber,
							Address = "N/A",
							RoleCode = "Parent",
							CreatedAt = DateTime.Now,
							IsEnable = true
						};

						await _playerRegistrationRepository.AddUserAsync(parentUser);

						// Lưu thông tin phụ huynh
						var newParent = new Parent
						{
							UserId = parentUser.UserId,
							CitizenId = registration.ParentCitizenId,
							CreatedByManagerId = managerId
						};

						await _playerRegistrationRepository.AddParentAsync(newParent);
						parentId = newParent.UserId;
					}

				}

				// Tạo Player mới từ PlayerRegistration
				var newPlayer = new Player
				{
					UserId = playerUser.UserId,
					ParentId = parentId,
					TeamId = null,
					RelationshipWithParent = registration.RelationshipWithParent,
					Weight = registration.Weight,
					Height = registration.Height,
					Position = registration.Position,
					ShirtNumber = null,
					ClubJoinDate = DateOnly.FromDateTime(DateTime.Now)
				};

				await _playerRegistrationRepository.AddPlayerAsync(newPlayer);

				// Cập nhật trạng thái đăng ký
				registration.Status = RegistrationStatusConstant.APPROVED;
				registration.ReviewedDate = DateTime.Now;
				_playerRegistrationRepository.Update(registration);

				if (parentUser != null)
				{
					//trong trường hợp đơn có email parent mới gửi email
					if (registration.ParentEmail != null)
					{
						// Gửi email account cho phụ huynh
						await _sendMailService.SendMailByMailTemplateIdAsync(MailTemplateConstant.ApprovedParentEmailRegistration,
																			registration.ParentEmail,
																			
																			new
																			{
																				Fullname = registration.ParentName,
																				Username = generateParentUsername,
																				Password = generateParentPassword
																			});
					}

				}


				// Gửi email account cho học viên
				await _sendMailService.SendMailByMailTemplateIdAsync(MailTemplateConstant.ApprovedPlayerEmailRegistration,
																	playerUser.Email,
																	
																		new
																		{
																			Fullname = registration.FullName,
																			Username = generatePlayerUsername,
																			Password = generatePlayerPassword
																		});

				//// Xóa CandidateNumber sau khi duyệt
				//registration.CandidateNumber = null;

				// Lưu thay đổi
				await _playerRegistrationRepository.SaveAsync();

				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = PlayerRegistrationMessage.Success.PlayerRegistrationApproveSuccess,
					Data = null
				};
			}
			catch (Exception ex)
			{
				errors.Add("Exception", ex.Message);
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors,
					Data = null
				};
			}
		}

		public async Task<ApiMessageModelV2<object>> RejectRegistrationAsync(int registrationId)
		{
			var errors = new Dictionary<string, string>();

			try
			{
				var managerId = _getLoginUserHelper.GetUserIdLoggedIn();

				if (!await _playerRegistrationRepository.IsExistedManagerById(managerId))
				{
					errors.Add("notFoundManager", "Tài khoản của bạn đã bị vô hiệu hóa hoặc gặp lỗi nên không thể duyệt đơn này");
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors
					};
				}
				// Tìm đơn đăng ký theo ID
				var registration = await _playerRegistrationRepository.GetRegistrationByIdAsync(registrationId);
				//để reject đơn hoặc approved thì trạng thái đơn phải là scored
				if (registration == null || registration.Status != RegistrationStatusConstant.SCOREDTRYOUT)
				{
					errors.Add("registrationNotFound", $"Không tìm thấy đơn hoặc đơn đã được duyệt");
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = errors
					};
				}

				// Cập nhật trạng thái đơn thành "Rejected"
				registration.Status = "Rejected";
				registration.ReviewedDate = DateTime.Now;
				registration.FormReviewedBy = managerId;

				// Cập nhật đơn vào database
				_playerRegistrationRepository.Update(registration);
				await _playerRegistrationRepository.SaveAsync();


				// Gửi email reject đơn cho player
				await _sendMailService.SendMailByMailTemplateIdAsync(MailTemplateConstant.RejectPlayerRegistration,
																	registration.Email,
																	
																	registration);
				// Trả về thông báo thành công
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = "Đơn đăng ký đã bị từ chối thành công.",
					Data = null
				};
			}
			catch (Exception ex)
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

		// Gọi học viên đi tham gia buổi đầu vào
		public async Task<ApiMessageModelV2<object>> CallToTryoutList(
		List<int> playerRegistIds,
		string? location,
		DateTime? tryOutDateTime)
		{
			var results = new List<CallToTryoutResultDto>();

			// Kiểm tra ngày hẹn tryout có hợp lệ không
			if (tryOutDateTime.HasValue)
			{
				var now = DateTime.Now;
				if (tryOutDateTime.Value < now)
				{
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Message = "Thời gian hẹn buổi đầu vào không được ở quá khứ.",
						Data = null
					};
				}

				if ((tryOutDateTime.Value - now).TotalHours < 1)
				{
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Message = "Thời gian hẹn đầu vào cho học viên phải ít nhất sau 1 tiếng kể từ bây giờ.",
						Data = null
					};
				}
			}

			foreach (var playerRegistId in playerRegistIds)
			{
				var result = new CallToTryoutResultDto { PlayerRegistId = playerRegistId };

				try
				{
					var playerRegist = await _playerRegistrationRepository.GetRegistrationByIdAsync(playerRegistId);
					if (playerRegist == null)
					{
						result.Status = "Failed";
						result.Message = "Không tìm thấy đơn.";
						results.Add(result);
						continue;
					}

					if (playerRegist.Status != RegistrationStatusConstant.PENDING)
					{
						result.Status = "Failed";
						result.Message = "Đơn đăng kí không thỏa mãn để mời thí sinh tham gia phỏng vấn";
						results.Add(result);
						continue;
					}

					playerRegist.CandidateNumber = await _playerRegistrationRepository
						.GetMaxCandidateNumber(playerRegist.MemberRegistrationSessionId) + 1;

					if (!string.IsNullOrEmpty(location))
						playerRegist.TryOutLocation = location;

					if (tryOutDateTime.HasValue)
						playerRegist.TryOutDate = tryOutDateTime.Value;

					playerRegist.Status = RegistrationStatusConstant.CALLEDTOTRYOUT;

					await _playerRegistrationRepository.UpdatePlayerRegistrationAsync(playerRegist);

					await _sendMailService.SendMailByMailTemplateIdAsync(
						MailTemplateConstant.CallToTryOut,
						playerRegist.Email,
						
						playerRegist);

					result.Status = "Success";
					result.Message = "Gửi lời mời tham gia đầu vào thành công.";
				}
				catch (Exception ex)
				{
					result.Status = "Failed";
					result.Message = $"Lỗi: {ex.Message}";
				}

				results.Add(result);
			}

			var hasError = results.Any(r => r.Status == "Failed");

			return new ApiMessageModelV2<object>
			{
				Status = hasError ? ApiResponseStatusConstant.FailedStatus : ApiResponseStatusConstant.SuccessStatus,
				Message = hasError ? "Một số đơn không thể xử lý." : "Tất cả đơn đã được xử lý thành công.",
				Data = results
			};
		}


		public async Task<ApiMessageModelV2<object>> ValidateEmailRegistrationAsync(string email, string memberSessionId)
		{

			var errors = new Dictionary<string, string>();

			try
			{
				// Kiểm tra định dạng email học sinh
				if (!RegexHelper.IsValidEmail(email))
				{
					errors.Add("errorEmail", PlayerRegistrationMessage.Errors.EmailInvalid);
				}
				//kiểm tra email tồn tại trong bảng user 
				if (await _playerRegistrationRepository.IsEmailExistsInUser(email))
				{
					//Nếu tồn tại trong user, và đc approved

					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = new Dictionary<string, string>() { { "errorEmail", "Email đã là tài khoản, xin vui lòng sử dụng email mới" } },
						Data = email
					};

				}

				// Kiểm tra xem email đã tồn tại trong bảng PlayerRegistration tại mùa đó hay chưa
				if (await _playerRegistrationRepository.IsEmailExistsInPlayerRegistration(email, memberSessionId))
				{
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = new Dictionary<string, string>() { { "errorEmail", "Bạn không thể sử dụng email để đăng kí mùa này" } },
						Data = email
					};
				}   //kiểm tra tồn tại trong bảng Manager Registration hay chưa
				if (await _playerRegistrationRepository.IsEmailExistsInManagerRegistration(email, memberSessionId))
				{
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Errors = new Dictionary<string, string>() { { "errorEmail", "Email đã được nộp đơn với tư cách khác, xin vui lòng sử dụng email mới" } },
						Data = email
					};
				}

				//email hợp lệ thì gửi OTP
				await _otpService.SendOtpCodeAsync(new DTOs.EmailVerification.SendOtpDto { Email = email, Purpose = EmailConstant.PlayerRegistrationForm });
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = "Đã gửi mã xác minh tới email",
					Data = email
				};
			}
			catch (Exception ex)
			{
				// Xử lý lỗi và trả về thông báo lỗi
				errors.Add("exception", ex.Message);
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors,
					Data = null
				};
			}

		}
	}
}

