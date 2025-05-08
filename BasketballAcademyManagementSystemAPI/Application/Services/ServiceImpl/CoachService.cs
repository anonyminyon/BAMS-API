using Amazon.Rekognition.Model;
using Amazon.Runtime.Internal.Transform;
using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.CoachManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using log4net;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class CoachService : ICoachService
    {
        private readonly ICoachRepository _coachRepository;
        private readonly ISendMailService _sendMailService;
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IUserTeamHistoryService _userTeamHistoryService;
        private readonly IUserRepository _userRepository;
        private static readonly ILog _logger = LogManager.GetLogger(LogConstant.LogName.MemberRegistrationSessionFeature);
        private readonly AccountGenerateHelper _accountGenerateHelper;
        public CoachService(ICoachRepository coachRepository, IMapper mapper, ISendMailService sendMailService,
            ITeamRepository teamRepository, IAuthService authService, IUserTeamHistoryService userTeamHistoryService,
            IUserRepository userRepository, AccountGenerateHelper accountGenerateHelper)
        {
            _coachRepository = coachRepository;
            _mapper = mapper;
            _sendMailService = sendMailService;
            _teamRepository = teamRepository;
            _authService = authService;
            _userTeamHistoryService = userTeamHistoryService;
            _userRepository = userRepository;
            _accountGenerateHelper = accountGenerateHelper;
        }

        public async Task<ApiMessageModelV2<UserAccountDto<CoachDetailDto>>> CreateCoachAsync(CreateCoachDto coachDto)
        {
            try
            {
                _logger.Info("Starting CreateCoachAsync method.");
                //check regex Phone is vietnam Email
                var errors = ValidateRegexInputEmailPhone(coachDto.Email, coachDto.Phone);

                if (!errors.IsNullOrEmpty())
                {
                    return new ApiMessageModelV2<UserAccountDto<CoachDetailDto>>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = CommonMessage.ValidateError,
                        Errors = errors
                    };
                }

                // Check if email and phone number already exist in User table
                errors = await _coachRepository.CheckDuplicateCoachAsync(coachDto.Phone, coachDto.Email);

                if (!errors.IsNullOrEmpty())
                {
                    return new ApiMessageModelV2<UserAccountDto<CoachDetailDto>>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = CommonMessage.ValidateError,
                        Errors = errors
                    };
                }

                // Get info of the current logged-in user (President)
                dynamic userLoggedDynamic = await _authService.GetCurrentLoggedInUserInformationAsync();

                // Create data for User table
                var newUserId = Guid.NewGuid().ToString();
                var generateUsername = _accountGenerateHelper.GetUniqueUsername(coachDto.Fullname);
                var generatePassword = _accountGenerateHelper.GeneratePassword();
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(generatePassword);

                DateOnly? dateOfBirth = coachDto.DateOfBirth != null
                        ? DateOnly.FromDateTime(coachDto.DateOfBirth)
                        : (DateOnly?)null;

                var user = new Models.User
                {
                    UserId = newUserId,
                    Username = generateUsername,
                    Password = hashedPassword,
                    Fullname = coachDto.Fullname,
                    Email = coachDto.Email,
                    Phone = coachDto.Phone,
                    Address = coachDto.Address,
                    DateOfBirth = dateOfBirth,
                    RoleCode = RoleCodeConstant.CoachCode,
                    CreatedAt = DateTime.Now,
                    IsEnable = true
                };

                // Create data for Coach table
                var contractStartDate = coachDto.ContractStartDate != null
                     ? DateOnly.FromDateTime(coachDto.ContractStartDate)
                     : DateOnly.FromDateTime(DateTime.Now);

                var contractEndDate = coachDto.ContractEndDate != null
                    ? DateOnly.FromDateTime(coachDto.ContractStartDate)
                    : DateOnly.FromDateTime(DateTime.Now.AddMonths(3));

                var coach = new Coach
                {
                    UserId = newUserId,
                    CreatedByPresidentId = userLoggedDynamic.UserId,
                    ContractStartDate = contractStartDate,
                    ContractEndDate = contractEndDate
                };

                try
                {
                    await _coachRepository.AddCoachAsync(user, coach);
                    _logger.Info("Successfully created a coach.");
                }
                catch (Exception ex)
                {
                    _logger.Error("An error occurred in CreateCoachAsync.", ex);
                    return new ApiMessageModelV2<UserAccountDto<CoachDetailDto>>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = CoachMessage.Error.CreateCoachError,
                        Errors = new Dictionary<string, string> { { CommonMessage.Key.ErrorGeneral, ex.Message } }
                    };
                }

                var entity = await _coachRepository.GetCoachByUserIdAsync(newUserId);
                entity.Password = generatePassword;

                // Send mail to announce user
                await _sendMailService.SendMailByMailTemplateIdAsync(
                    MailTemplateConstant.CoachRegistrationSuccess,
                    entity.Email,
                     new
                     {
                         Fullname = entity.Fullname,
                         Username = generateUsername,
                         Password = generatePassword,
                     });
                _logger.Info("Send mail for created a coach sucessfully.");

                return new ApiMessageModelV2<UserAccountDto<CoachDetailDto>>
                {
                    Status = ApiResponseStatusConstant.SuccessStatus,
                    Message = CoachMessage.Success.CoachCreateSuccessfully,
                    Data = _mapper.Map<UserAccountDto<CoachDetailDto>>(entity)
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new ApiMessageModelV2<UserAccountDto<CoachDetailDto>>
            {
                Status = ApiResponseStatusConstant.FailedStatus,
                Data = null
            };
        }

        private Dictionary<string, string> ValidateRegexInputEmailPhone(string email, string phone)
        {
            var errors = new Dictionary<string, string>();

            if (!Regex.IsMatch(email, RegexConstant.EmailRegex))
            {
                errors.Add(CoachMessage.Key.InvalidEmail, CoachMessage.Error.InvalidEmailFormat);
            }

            if (!Regex.IsMatch(phone, RegexConstant.VietnamPhoneRegex))
            {
                errors.Add(CoachMessage.Key.InvalidPhone, CoachMessage.Error.InvalidPhoneFormat);
            }

            return errors;
        }

        public async Task<ApiMessageModelV2<PagedResponseDto<UserAccountDto<CoachAccountDto>>>> GetCoachListAsync(CoachFilterDto filter)
        {
            var apiResponse = new ApiMessageModelV2<PagedResponseDto<UserAccountDto<CoachAccountDto>>>
            {
                Errors = new Dictionary<string, string>(),
                Message = CoachMessage.Error.CoachNotFound
            };

            try
            {
                _logger.Info($"Fetching details for coach with UserId: {filter}");
                var result = await _coachRepository.GetFilteredPagedCoachesAsync(filter);

                var coachesDto = result.Items.Select(c => new UserAccountDto<CoachAccountDto>
                {
                    UserId = c.User.UserId,
                    Username = c.User.Username,
                    Fullname = c.User.Fullname,
                    Email = c.User.Email,
                    ProfileImage = c.User.ProfileImage,
                    Phone = c.User.Phone,
                    Address = c.User.Address,
                    DateOfBirth = c.User.DateOfBirth?.ToString("dd-MM-yyyy") ?? String.Empty,
                    RoleCode = c.User.RoleCode,
                    CreatedAt = c.User.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
                    UpdatedAt = c.User.UpdatedAt?.ToString("dd-MM-yyyy HH:mm:ss"),
                    IsEnable = c.User.IsEnable,
                    RoleInformation = new CoachAccountDto
                    {
                        UserId = c.UserId,
                        TeamId = c.TeamId,
                        TeamName = c.Team?.TeamName != null ? c.Team?.TeamName : "Chưa có đội" ,
                        CreatedByPresidentId = c.CreatedByPresidentId,
                        Bio = c.Bio ?? String.Empty,
                        ContractStartDate = c.ContractStartDate.ToString("dd-MM-yyyy"),
                        ContractEndDate = c.ContractEndDate.ToString("dd-MM-yyyy"),
                    }
                }).ToList();
                // Lấy thông tin user đang đăng nhập
                dynamic userLoggedDynamic = await _authService.GetCurrentLoggedInUserInformationAsync();

                // Filter theo role
                if (userLoggedDynamic.RoleCode.Equals(RoleCodeConstant.ManagerCode))
                {
                    result = null;
                }
                else if (userLoggedDynamic.RoleCode.Equals(RoleCodeConstant.CoachCode))
                {
                    var user = await _coachRepository.GetCoachByUserIdAsync(userLoggedDynamic.UserId);
                    coachesDto = coachesDto.Where(c => c.RoleInformation.TeamId == user?.Coach?.TeamId || c.UserId == userLoggedDynamic.UserId).ToList();
                    if (!coachesDto.Any())
                    {
                        apiResponse.Message = CoachMessage.Error.AccessDeniedDifferentTeam; // Cập nhật message
                        return apiResponse;
                    }
                }
                else if (userLoggedDynamic.RoleCode.Equals(RoleCodeConstant.PresidentCode))
                {
                    // President có quyền xem tất cả, không cần filter
                }

                var pagedResponse = new PagedResponseDto<UserAccountDto<CoachAccountDto>>
                {
                    Items = coachesDto,
                    TotalRecords = result.TotalRecords,
                    PageSize = result.PageSize,
                    CurrentPage = result.CurrentPage,
                    TotalPages = result.TotalPages
                };

                apiResponse.Data = pagedResponse;
                apiResponse.Status = ApiResponseStatusConstant.SuccessStatus;
                apiResponse.Message = CoachMessage.Success.ListRetrieved;

            }
            catch (Exception ex)
            {
                apiResponse.Errors.Add(CommonMessage.Key.ErrorGeneral, ex.Message);
            }

            return apiResponse;
        }

        public async Task<ApiMessageModelV2<UserAccountDto<CoachDetailDto>>> GetCoachDetailAsync(string userId)
        {
            try
            {
                _logger.Info($"Starting GetCoachDetailAsync for user ID: {userId}");

                // Lấy thông tin user đang đăng nhập
                dynamic userLoggedDynamic = await _authService.GetCurrentLoggedInUserInformationAsync();

                // Kiểm tra nếu là Coach
                if (userLoggedDynamic.RoleCode.Equals(RoleCodeConstant.CoachCode))
                {
                    var loggedInCoach = await _coachRepository.GetCoachByUserIdAsync(userLoggedDynamic.UserId);
                    var coach = await _coachRepository.GetCoachByUserIdAsync(userId);

                    if (coach == null ||
                        (coach.Coach.TeamId != loggedInCoach.Coach.TeamId &&
                         coach.UserId != userLoggedDynamic.UserId))
                    {
                        return new ApiMessageModelV2<UserAccountDto<CoachDetailDto>>
                        {
                            Status = ApiResponseStatusConstant.FailedStatus,
                            Message = CoachMessage.Error.ViewDetailsDenied,
                            Errors = new Dictionary<string, string>
                    {
                        { CommonMessage.Key.ErrorGeneral, CoachMessage.Error.ViewDetailsDenied }
                    }
                        };
                    }
                }

                var coachDetail = await _coachRepository.GetCoachByUserIdAsync(userId);
                if (coachDetail == null)
                {
                    return new ApiMessageModelV2<UserAccountDto<CoachDetailDto>>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = CoachMessage.Error.CoachNotFound,
                        Errors = new Dictionary<string, string>
                {
                    { CommonMessage.Key.ErrorGeneral, CoachMessage.Error.CoachNotFound }
                }
                    };
                }

                var coachDto = _mapper.Map<UserAccountDto<CoachDetailDto>>(coachDetail);
                _logger.Info("Successfully retrieved coach details.");
                return new ApiMessageModelV2<UserAccountDto<CoachDetailDto>>
                {
                    Status = ApiResponseStatusConstant.SuccessStatus,
                    Message = CoachMessage.Success.GetCoachSuccessfully,
                    Data = coachDto
                };
            }
            catch (Exception ex)
            {
                _logger.Error("An error occurred in GetCoachDetailAsync", ex);
                return new ApiMessageModelV2<UserAccountDto<CoachDetailDto>>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = CommonMessage.Key.ErrorGeneral,
                    Errors = new Dictionary<string, string>
            {
                { CommonMessage.Key.ErrorGeneral, ex.Message }
            }
                };
            }
        }


        public async Task<ApiMessageModelV2<UserAccountDto<CoachAccountDto>>> ChangeCoachAccountStatusAsync(string userId)
        {
            try
            {
                _logger.Info($"Starting ChangeCoachAccountStatusAsync for user ID: {userId}");
                var success = await _coachRepository.ChangeStatusCoachAsync(userId);
                if (!success)
                {
                    return new ApiMessageModelV2<UserAccountDto<CoachAccountDto>>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = CoachMessage.Error.CoachNotFound,
                        Errors = new Dictionary<string, string> { { CommonMessage.Key.ErrorGeneral, CoachMessage.Error.CoachNotFound } }
                    };
                }
                _logger.Info("Successfully changed coach account status.");
                var entity = await _coachRepository.GetCoachByUserIdAsync(userId);
                if (entity != null)
                {
                    if (entity.IsEnable)
                        await _sendMailService.SendMailByMailTemplateIdAsync(MailTemplateConstant.EnableAccount, entity.Email, entity);
                    else
                        await _sendMailService.SendMailByMailTemplateIdAsync(MailTemplateConstant.DisableAccount, entity.Email, entity);
                }
            }
            catch (Exception ex) { _logger.Error("An error occurred in [MethodName]", ex); }
            return new ApiMessageModelV2<UserAccountDto<CoachAccountDto>>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = CoachMessage.Success.CoachStatusUpdated
            };
        }

        public async Task<ApiMessageModelV2<CoachAccountDto>> AssignCoachToTeamAsync(string userId, string teamId)
        {
            _logger.Info($"Starting AssignCoachToTeamAsync for user ID: {userId} and team ID: {teamId}");
            if (string.IsNullOrEmpty(userId))
            {
                return new ApiMessageModelV2<CoachAccountDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = CoachMessage.Error.InvalidUserId,
                    Errors = new Dictionary<string, string> { { CoachMessage.Key.ErrorUserId, CoachMessage.Error.InvalidUserId } }
                };
            }

            var coach = await _coachRepository.GetCoachByUserIdAsync(userId);
            if (coach == null)
            {
                return new ApiMessageModelV2<CoachAccountDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = CoachMessage.Error.CoachNotFound,
                    Errors = new Dictionary<string, string> { { CommonMessage.Key.ErrorGeneral, CoachMessage.Error.CoachNotFound } }
                };
            }

            if (!coach.IsEnable)
            {
                return new ApiMessageModelV2<CoachAccountDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = CoachMessage.Error.CoachDisabled,
                    Errors = new Dictionary<string, string> { { CoachMessage.Key.ErrorCoachDisabled, CoachMessage.Error.CoachDisabled } }
                };
            }

            // Check if the user is already in the same team
            if (coach.Coach.TeamId != null && coach.Coach.TeamId == teamId)
            {
                return new ApiMessageModelV2<CoachAccountDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Errors = new Dictionary<string, string> { { "errorTeamId", "Bạn đã đang ở trong đội này rồi." } }
                };
            }

            var team = await _teamRepository.GetTeamByIdAsync(teamId);
            if (string.IsNullOrEmpty(teamId) || team == null)
            {
                return new ApiMessageModelV2<CoachAccountDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ManagerMessage.Errors.CanNotFindTeamId,
                    Errors = new Dictionary<string, string> { { CoachMessage.Key.ErrorTeamId, ManagerMessage.Errors.CanNotFindTeamId } }
                };
            }

            coach.Coach.TeamId = teamId;

            try
            {
                var userTeamHistory = await _userTeamHistoryService.UserAssignToNewTeamHistory(userId, teamId);
                _logger.Info("Successfully assigned coach to team.");
            }
            catch (Exception ex)
            {
                _logger.Error("An error occurred in [MethodName]", ex);
                return new ApiMessageModelV2<CoachAccountDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = CoachMessage.Error.AssignCoachError,
                    Errors = new Dictionary<string, string> { { CommonMessage.Key.ErrorGeneral, ex.Message } }
                };
            }

            await _coachRepository.UpdateCoachAsync(coach.Coach);
            await _sendMailService.SendMailAssignToTeamAsync(coach, team, MailTemplateConstant.CoachAssignToTeamSuccess);
            return new ApiMessageModelV2<CoachAccountDto>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = CoachMessage.Success.CoachAssignToTeamSuccessfully
            };
        }

        public async Task<ApiMessageModelV2<UserAccountDto<CoachDetailDto>>> UpdateCoachAsync(UpdateCoachDto coachDto)
        {
            _logger.Info($"Starting UpdateCoachAsync for user ID: {coachDto.UserId}");
            var errors = new Dictionary<string, string>();

            var user = await _userRepository.GetUserByIdAsync(coachDto.UserId);
            if (user == null)
            {
                errors.Add(CoachMessage.Key.ErrorUserId, CoachMessage.Error.UserNotFound);
            }

            var coach = await _coachRepository.GetCoachByUserIdAsync(coachDto.UserId);
            if (coach == null)
            {
                errors.Add(CommonMessage.Key.ErrorGeneral, CoachMessage.Error.CoachNotFound);
            }

            if (errors.Count > 0)
            {
                return new ApiMessageModelV2<UserAccountDto<CoachDetailDto>>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = CommonMessage.ValidateError,
                    Errors = errors
                };
            }

            //check regex Phone is vietnam Email
            errors = ValidateRegexInputEmailPhone(coachDto.Email, coachDto.Phone);

            if (!errors.IsNullOrEmpty())
            {
                return new ApiMessageModelV2<UserAccountDto<CoachDetailDto>>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = CommonMessage.ValidateError,
                    Errors = errors
                };
            }

            // Check for duplicate email and fullname
            errors = await _coachRepository.CheckDuplicateCoachAsync(coachDto.Phone, coachDto.Email, coachDto.Username, coachDto.UserId);

            if (errors.Count > 0)
            {
                return new ApiMessageModelV2<UserAccountDto<CoachDetailDto>>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = CommonMessage.ValidateError,
                    Errors = errors
                };
            }

            // Update other user attributes
            if (!string.IsNullOrEmpty(coachDto.Username) && coachDto.Username != user.Username)
            {
                user.Username = coachDto.Username;
            }
            if (!string.IsNullOrEmpty(coachDto.Fullname) && coachDto.Fullname != user.Fullname)
            {
                user.Fullname = coachDto.Fullname;
            }
            if (!string.IsNullOrEmpty(coachDto.ProfileImage) && coachDto.ProfileImage != user.ProfileImage)
            {
                user.ProfileImage = coachDto.ProfileImage;
            }
            if (!string.IsNullOrEmpty(coachDto.Phone) && coachDto.Phone != user.Phone)
            {
                user.Phone = coachDto.Phone;
            }
            if (!string.IsNullOrEmpty(coachDto.Address) && coachDto.Address != user.Address)
            {
                user.Address = coachDto.Address;
            }
            if (!string.IsNullOrEmpty(coachDto.DateOfBirth) && DateOnly.Parse(coachDto.DateOfBirth) != user.DateOfBirth)
            {
                user.DateOfBirth = DateOnly.Parse(coachDto.DateOfBirth);
            }
            //if (!string.IsNullOrEmpty(coachDto.RoleCode) && coachDto.RoleCode != user.RoleCode)
            //{
            //    user.RoleCode = coachDto.RoleCode;
            //}
            user.UpdatedAt = DateTime.Now;

            // Update coach attributes
            //if (!string.IsNullOrEmpty(coachDto.TeamId) && coachDto.TeamId != coach.Coach.TeamId)
            //{
            coach.Coach.TeamId = coach.Coach.TeamId;
            //}
            if (!string.IsNullOrEmpty(coachDto.Bio) && coachDto.Bio != coach.Coach.Bio)
            {
                coach.Coach.Bio = coachDto.Bio;
            }
            if (!string.IsNullOrEmpty(coachDto.ContractStartDate) && DateOnly.Parse(coachDto.ContractStartDate) != coach.Coach.ContractStartDate)
            {
                coach.Coach.ContractStartDate = DateOnly.Parse(coachDto.ContractStartDate);
            }
            if (!string.IsNullOrEmpty(coachDto.ContractEndDate) && DateOnly.Parse(coachDto.ContractEndDate) != coach.Coach.ContractEndDate)
            {
                coach.Coach.ContractEndDate = DateOnly.Parse(coachDto.ContractEndDate);
            }

            try
            {
                await _userRepository.UpdateUserAsync(user);
                await _coachRepository.UpdateCoachAsync(coach.Coach);
            }
            catch (Exception ex) { _logger.Error("An error occurred in [MethodName]", ex); }



            var entity = await _coachRepository.GetCoachByUserIdAsync(coach.UserId);
            return new ApiMessageModelV2<UserAccountDto<CoachDetailDto>>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = CoachMessage.Success.CoachStatusUpdated,
                Data = _mapper.Map<UserAccountDto<CoachDetailDto>>(entity)
            };
        }
    }
}