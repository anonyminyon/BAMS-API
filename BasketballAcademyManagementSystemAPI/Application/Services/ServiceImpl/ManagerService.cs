using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.ManagerManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl;
using BasketballAcademyManagementSystemAPI.Models;
using log4net;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ISendMailService _sendMailService;
        private readonly IMapper _mapper;
        private readonly IUserTeamHistoryService _userTeamHistoryService;
        private static readonly ILog _logger = LogManager.GetLogger(LogConstant.LogName.ManagerManagementFeature);

        public ManagerService(IManagerRepository managerRepository, IUserRepository userRepository, ITeamRepository teamRepository, IMapper mapper,
            ISendMailService sendMailService, IUserTeamHistoryService userTeamHistoryService)
        {
            _managerRepository = managerRepository;
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _mapper = mapper;
            _sendMailService = sendMailService;
            _userTeamHistoryService = userTeamHistoryService;
        }

        public async Task<ApiMessageModelV2<ManagerDto>> AssignManagerToTeamAsync(ManagerDto managerDto)
        {
            var user = await _managerRepository.GetUserWithManagerDetailAsync(managerDto.UserId);

            if (managerDto.TeamId == null)
            {
                return new ApiMessageModelV2<ManagerDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ManagerMessage.Errors.CanNotFindUserId
                };
            }

            var team = await _teamRepository.GetTeamByIdAsync(managerDto.TeamId);

            if (managerDto.UserId == null || user == null)
            {
                return new ApiMessageModelV2<ManagerDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ManagerMessage.Errors.CanNotFindUserId
                };
            }
            else if (managerDto.TeamId == null || team == null)
            {
                return new ApiMessageModelV2<ManagerDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ManagerMessage.Errors.CanNotFindTeamId
                };
            }

            if (user.Manager.TeamId != null && user.Manager.TeamId == managerDto.TeamId)
            {
                return new ApiMessageModelV2<ManagerDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Errors = new Dictionary<string, string> { { "errorTeamId", "Bạn đã đang ở trong đội này rồi." } }
                };
            }

            await _managerRepository.UpdateManagerByUserIdAsync(managerDto);
            _logger.Info($"[AssignManagerToTeamAsync] Updated manager TeamId for UserId: {managerDto.UserId}");

            try
            {
                var userTeamHistory = await _userTeamHistoryService.UserAssignToNewTeamHistory(managerDto.UserId, managerDto.TeamId);
                _logger.Info($"[AssignManagerToTeamAsync] Created user team history for UserId: {managerDto.UserId}, TeamId: {managerDto.TeamId}");
            }
            catch (Exception ex)
            {
                return new ApiMessageModelV2<ManagerDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Errors = new Dictionary<string, string> { { CommonMessage.Key.ErrorGeneral, ex.Message } }
                };
            }

            await _sendMailService.SendMailAssignToTeamAsync(user, team,  MailTemplateConstant.ManagerAssignToTeamSuccess);
            _logger.Info($"[AssignManagerToTeamAsync] Sent assign-to-team email to UserId: {managerDto.UserId}");

            return new ApiMessageModelV2<ManagerDto>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = ManagerMessage.Success.ManagerAssignToTeamSuccessfully,
                Data = managerDto
            };
        }

        public async Task<ApiMessageModelV2<UserAccountDto<ManagerDto>>> GetManagerDetailAsync(string userId)
        {
            var user = await _managerRepository.GetUserWithManagerDetailAsync(userId);
            _logger.Info($"[GetManagerDetailAsync] Retrieved manager detail for UserId: {userId}");

            if (user == null || user.Manager == null)
            {
                return new ApiMessageModelV2<UserAccountDto<ManagerDto>>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ManagerMessage.Errors.ManagerNotFound,
                    Data = null
                };
            }

            var managerDetail = _mapper.Map<UserAccountDto<ManagerDto>>(user);

            return new ApiMessageModelV2<UserAccountDto<ManagerDto>>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = ManagerMessage.Success.GetManagerSuccessfully,
                Data = managerDetail
            };
        }

        public async Task<ApiMessageModelV2<UserAccountDto<ManagerDto>>> UpdateManagerAsync(UserAccountDto<ManagerDto> managerDto)
        {
            var user = await _managerRepository.GetUserByIdAsync(managerDto.UserId);
            var manager = await _managerRepository.GetManagerByUserIdAsync(managerDto.UserId);
            var isEmailOrFullNameExist = await _managerRepository.IsEmailFullNameExists(managerDto);

            // logic cập nhật...

            var isUserUpdated = await _managerRepository.UpdateUserAsync(user);
            var isManagerUpdated = await _managerRepository.UpdateManagerAsync(manager);
            _logger.Info($"[UpdateManagerAsync] Updated user: {isUserUpdated}, manager: {isManagerUpdated} for UserId: {managerDto.UserId}");

            var userUpdate = await _managerRepository.GetUserWithManagerDetailAsync(managerDto.UserId);

            var managerUserUpdated = _mapper.Map<UserAccountDto<ManagerDto>>(userUpdate);
            return new ApiMessageModelV2<UserAccountDto<ManagerDto>>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = ManagerMessage.Success.ManagerUpdatedSuccessfully,
                Data = managerUserUpdated
            };
        }

        public async Task<ApiMessageModelV2<bool>> DisableManagerAsync(string userId)
        {
            var isDisabled = await _managerRepository.ChangeStatusManagerAsync(userId);
            _logger.Info($"[DisableManagerAsync] Changed status for UserId: {userId} - Success: {isDisabled}");

            var entity = await _managerRepository.GetUserByIdAsync(userId);
            if (entity != null)
            {
                if (entity.IsEnable)
                {
                    await _sendMailService.SendMailByMailTemplateIdAsync(MailTemplateConstant.EnableAccount, entity.Email,entity);
                    _logger.Info($"[DisableManagerAsync] Sent enable account email to: {entity.Email}");
                }
                else
                {
                    await _sendMailService.SendMailByMailTemplateIdAsync(MailTemplateConstant.DisableAccount, entity.Email, entity);
                    _logger.Info($"[DisableManagerAsync] Sent disable account email to: {entity.Email}");
                }
            }

            return new ApiMessageModelV2<bool>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = ManagerMessage.Success.ManagerDisabledSuccessfully,
                Data = true,
                Errors = null
            };
        }

        public async Task<ApiMessageModelV2<PagedResponseDto<UserAccountDto<ManagerDto>>>> GetFilteredPagedManagersAsync(ManagerFilterDto filter)
        {
            var apiResponse = new ApiMessageModelV2<PagedResponseDto<UserAccountDto<ManagerDto>>>
            {
                Errors = new Dictionary<string, string>(),
                Message = ManagerMessage.Errors.ManagerNotFound
            };

            try
            {
                var result = await _managerRepository.GetFilteredPagedManagersAsync(filter);
                _logger.Info($"[GetFilteredPagedManagersAsync] Retrieved paged manager list with {result.TotalRecords} total records");

                var pagedResponse = new PagedResponseDto<UserAccountDto<ManagerDto>>
                {
                    Items = result.Items.Select(m => new UserAccountDto<ManagerDto>
                    {
                        UserId = m.User.UserId,
                        Username = m.User.Username,
                        Fullname = m.User.Fullname,
                        Email = m.User.Email,
                        ProfileImage = m.User.ProfileImage,
                        Phone = m.User.Phone,
                        Address = m.User.Address,
                        DateOfBirth = m.User.DateOfBirth?.ToString("dd-MM-yyyy") ?? String.Empty,
                        RoleCode = m.User.RoleCode,
                        CreatedAt = m.User.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
                        UpdatedAt = m.User.UpdatedAt?.ToString("dd-MM-yyyy HH:mm:ss"),
                        IsEnable = m.User.IsEnable,
                        RoleInformation = new ManagerDto
                        {
                            UserId = m.UserId,
                            TeamId = m.TeamId,
                            BankName = m.BankName,
                            BankAccountNumber = m.BankAccountNumber,
                            PaymentMethod = m.PaymentMethod,
                            BankBinId = m.BankBinId
                        }
                    }).ToList(),
                    TotalRecords = result.TotalRecords,
                    PageSize = result.PageSize,
                    CurrentPage = result.CurrentPage,
                    TotalPages = result.TotalPages
                };

                apiResponse.Data = pagedResponse;
                apiResponse.Status = ApiResponseStatusConstant.SuccessStatus;
                apiResponse.Message = ManagerMessage.Success.GetManagerSuccessfully;
            }
            catch (Exception ex)
            {
                apiResponse.Errors.Add(ManagerMessage.Key.ManagerNotFound, ex.Message);
            }

            return apiResponse;
        }
    }
}
