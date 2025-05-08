using System.Globalization;
using System.Text.Json;
using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.MemberRegistrationSession;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using log4net;
using Microsoft.AspNetCore.Http;
using static BasketballAcademyManagementSystemAPI.Common.Messages.MemberRegistrationSessionMessage;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class MemberRegistrationSessionService : IMemberRegistrationSessionService
    {
        private readonly IMemberRegistrationSessionRepository _memberRegistrationSessionRepository;
        private readonly IManagerRegistrationRepository _managerRegistrationRepository;
        private readonly IPlayerRegistrationRepository _playerRegistrationRepository;
        private readonly ITryOutScorecardRepository _tryOutScorecardRepository;

        private readonly IMapper _mapper;
        private readonly ILog _log = LogManager.GetLogger(LogConstant.LogName.MemberRegistrationSessionFeature);

        public MemberRegistrationSessionService(IMemberRegistrationSessionRepository memberRegistrationSessionRepository
            , IManagerRegistrationRepository managerRegistrationRepository
            , IPlayerRegistrationRepository playerRegistrationRepository
            , ITryOutScorecardRepository tryOutScorecardRepository
            , IMapper mapper)
        {
            _memberRegistrationSessionRepository = memberRegistrationSessionRepository;
            _managerRegistrationRepository = managerRegistrationRepository;
            _playerRegistrationRepository = playerRegistrationRepository;
            _tryOutScorecardRepository = tryOutScorecardRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponseModel<MemberRegistrationSessionResponseDto>> CreateAsync(CreateMemberRegistrationSessionRequestDto dto)
        {
            var apiResponse = new ApiResponseModel<MemberRegistrationSessionResponseDto>()
            {
                Errors = new List<string>(),
                Message = MemberRegistrationSessionErrorMessage.CreateSessionFailed,
            };

            if (string.IsNullOrEmpty(dto.RegistrationName))
            {
                apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.RegistrationNameCannotBeEmpty);
            }

            if (dto.RegistrationName.Length > 255)
            {
                apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.RegistrationNameTooLong);
            }

            if (!dto.IsAllowPlayerRecruit && !dto.IsAllowManagerRecruit)
            {
                apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.InvalidTypeOfRecruits);
            }

            if (!DateTime.TryParseExact(dto.StartDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDate) ||
                !DateTime.TryParseExact(dto.EndDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var endDate))
            {
                apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.InvalidDateTimeFormat);
                return apiResponse;
            }
            else
            {
                if (endDate < startDate.AddHours(MemberRegistrationSessionConstant.MinimumSessionOpenHours))
                {
                    apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.InvalidMinimumSessionOpenHours);
                }
            }

            if (!apiResponse.Errors.Any())
            {
                var session = new MemberRegistrationSession
                {
                    RegistrationName = dto.RegistrationName,
                    Description = dto.Description,
                    StartDate = startDate,
                    EndDate = endDate,
                    IsAllowPlayerRecruit = dto.IsAllowPlayerRecruit,
                    IsAllowManagerRecruit = dto.IsAllowManagerRecruit,
                    IsEnable = dto.IsEnable,
                    CreatedAt = DateTime.Now
                };

                await _memberRegistrationSessionRepository.AddAsync(session);
                _log.Info(LogMessage.CreateRegistrationSessionSuccess(session.MemberRegistrationSessionId, session.RegistrationName));

                apiResponse.Status = ApiResponseStatusConstant.SuccessStatus;
                apiResponse.Message = MemberRegistrationSessionSuccessMessage.CreateSessionSuccessfully;
                apiResponse.Data = _mapper.Map<MemberRegistrationSessionResponseDto>(session);
            }
            return apiResponse;
        }


        public async Task<MemberRegistrationSessionResponseDto?> GetDetailsAsync(int id)
        {
            var session = await _memberRegistrationSessionRepository.GetByIdAsync(id);
            if (session == null)
            {
                return null;
            }
            return _mapper.Map<MemberRegistrationSessionResponseDto>(session); ;
        }

        public async Task<ApiResponseModel<MemberRegistrationSessionResponseDto>> UpdateAsync(int id, UpdateMemberRegistrationSessionRequestDto dto)
        {
            var apiResponse = new ApiResponseModel<MemberRegistrationSessionResponseDto>()
            {
                Errors = new List<string>(),
                Message = MemberRegistrationSessionErrorMessage.UpdateSessionFailed,
            };

            if (string.IsNullOrEmpty(dto.RegistrationName))
            {
                apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.RegistrationNameCannotBeEmpty);
            }

            if (dto.RegistrationName.Length > 255)
            {
                apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.RegistrationNameTooLong);
            }

            if (!dto.IsAllowPlayerRecruit && !dto.IsAllowManagerRecruit)
            {
                apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.InvalidTypeOfRecruits);
            }

            if (!DateTime.TryParseExact(dto.StartDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDate) ||
                !DateTime.TryParseExact(dto.EndDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var endDate))
            {
                apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.InvalidDateTimeFormat);
                return apiResponse;
            }
            else
            {
                if (endDate < startDate.AddHours(MemberRegistrationSessionConstant.MinimumSessionOpenHours))
                {
                    apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.InvalidMinimumSessionOpenHours);
                }
            }

            if (apiResponse.Errors.Count() == 0)
            {
                var session = await _memberRegistrationSessionRepository.GetByIdAsync(id);
                if (session == null)
                {
                    apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.NotFoundRegistrationSession);
                    return apiResponse;
                }

                session.RegistrationName = dto.RegistrationName;
                session.Description = dto.Description;
                session.StartDate = startDate;
                session.EndDate = endDate;
                session.IsAllowPlayerRecruit = dto.IsAllowPlayerRecruit;
                session.IsAllowManagerRecruit = dto.IsAllowManagerRecruit;
                session.IsEnable = dto.IsEnable;
                session.UpdatedAt = DateTime.Now;

                await _memberRegistrationSessionRepository.UpdateAsync(session);
                _log.Info(LogMessage.UpdateRegistrationSessionSuccess(session.MemberRegistrationSessionId, session.RegistrationName));
                apiResponse.Status = ApiResponseStatusConstant.SuccessStatus;
                apiResponse.Message = MemberRegistrationSessionSuccessMessage.UpdateSessionSuccessfully;
                apiResponse.Data = new MemberRegistrationSessionResponseDto();
                apiResponse.Data = _mapper.Map<MemberRegistrationSessionResponseDto>(session);
            }

            return apiResponse;
        }

        public async Task<ApiResponseModel<MemberRegistrationSessionResponseDto>> ToggleMemberRegistratrionSessionStatusAsync(int id)
        {
            var apiResponse = new ApiResponseModel<MemberRegistrationSessionResponseDto>()
            {
                Errors = new List<string>(),
                Message = MemberRegistrationSessionErrorMessage.UpdateSessionFailed,
            };

            var session = await _memberRegistrationSessionRepository.GetByIdAsync(id);
            if (session == null)
            {
                apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.NotFoundRegistrationSession);
            }
            else
            {
                if (session.EndDate < DateTime.Now)
                {
                    apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.CanNotToggleStatusOfOutDateSession);
                    return apiResponse;
                }

                session.IsEnable = !session.IsEnable;

                bool currentStatus = session.IsEnable;
                if (currentStatus)
                {
                    apiResponse.Message = MemberRegistrationSessionSuccessMessage.EnableSessionSuccessfully;
                }
                else
                {
                    apiResponse.Message = MemberRegistrationSessionSuccessMessage.DisableSessionSuccessfully;
                }

                session.UpdatedAt = DateTime.Now;
                await _memberRegistrationSessionRepository.UpdateAsync(session);
                _log.Info(LogMessage.ToogleRegistrationSessionSuccess(session.MemberRegistrationSessionId, session.RegistrationName, currentStatus));

                apiResponse.Data = _mapper.Map<MemberRegistrationSessionResponseDto>(session);
                apiResponse.Status = ApiResponseStatusConstant.SuccessStatus;
            }

            return apiResponse;
        }

        public async Task<ApiResponseModel<PagedResponseDto<MemberRegistrationSessionResponseDto>>> GetFilteredPagedAsync(MemberRegistrationSessionFilterDto filter)
        {
            var apiResponse = new ApiResponseModel<PagedResponseDto<MemberRegistrationSessionResponseDto>>()
            {
                Errors = new List<string>(),
                Message = MemberRegistrationSessionErrorMessage.FilteredError
            };

            if (filter.PageNumber <= 0)
            {
                apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.InvalidPageNumber);
                return apiResponse;
            }

            if (filter.PageSize <= 0)
            {
                apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.InvalidPageSize);
                return apiResponse;
            }

            try
            {
                var result = await _memberRegistrationSessionRepository.GetFilteredPagedAsync(filter);

                var pagedResponse = new PagedResponseDto<MemberRegistrationSessionResponseDto>
                {
                    Items = result.Items.Select(s => new MemberRegistrationSessionResponseDto
                    {
                        Id = s.MemberRegistrationSessionId,
                        RegistrationName = s.RegistrationName,
                        Description = s.Description,
                        StartDate = s.StartDate,
                        EndDate = s.EndDate,
                        IsAllowPlayerRecruit = s.IsAllowPlayerRecruit,
                        IsAllowManagerRecruit = s.IsAllowManagerRecruit,
                        IsEnable = s.IsEnable,
                        CreatedAt = s.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"),
                        UpdatedAt = s.UpdatedAt?.ToString("dd/MM/yyyy HH:mm:ss") ?? "Chưa cập nhật"
                    }),
                    TotalRecords = result.TotalRecords,
                    PageSize = result.PageSize,
                    CurrentPage = result.CurrentPage,
                    TotalPages = result.TotalPages
                };

                apiResponse.Data = pagedResponse;
                apiResponse.Status = ApiResponseStatusConstant.SuccessStatus;
                apiResponse.Message = MemberRegistrationSessionSuccessMessage.FilteredSuccess;
            }
            catch (Exception)
            {
                apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.InvalidFilterData);
            }
            return apiResponse;
        }

        public async Task<ApiResponseModel<bool>> DeleteAsync(int id)
        {
            var apiResponse = new ApiResponseModel<bool>()
            {
                Errors = new List<string>(),
                Message = MemberRegistrationSessionErrorMessage.DeleteSessionFailed
            };

            try
            {
                var session = await _memberRegistrationSessionRepository.GetToDeleteByIdAsync(id);
                if (session == null)
                {
                    apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.NotFoundRegistrationSession);

                    return apiResponse;
                }

                // Check if EndDate has passed and IsEnable is true
                if (session.EndDate > DateTime.Now && session.IsEnable == true)
                {
                    apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.CanNotDeleteOpeningSession);
                    return apiResponse;
                }

                // Check for pending ManagerRegistrations and PlayerRegistrations
                if (session.ManagerRegistrations.Any(m => m.Status == "Pending") || session.PlayerRegistrations.Any(p => p.Status == "Pending"))
                {
                    apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.CanNotDeleteSessionWithPendingRegistrations);
                    return apiResponse;
                }

                if (apiResponse.Errors.Any())
                {
                    return apiResponse;
                }


                // Delete all related data
                if (session.ManagerRegistrations.Any())
                {
                    await _managerRegistrationRepository.DeleteRangeAsync(session.ManagerRegistrations);
                }

                if (session.PlayerRegistrations.Any())
                {
                    foreach (var playerRegistration in session.PlayerRegistrations)
                    {
                        if (playerRegistration.TryOutScorecards.Any())
                        {
                            await _tryOutScorecardRepository.DeleteRangeAsync(playerRegistration.TryOutScorecards);
                        }
                    }

                    await _playerRegistrationRepository.DeleteRangeAsync(session.PlayerRegistrations);
                }

                // Delete the session
                int result = await _memberRegistrationSessionRepository.DeleteAsync(session);
                _log.Info(LogMessage.DeleteRegistrationSessionSuccess(session.MemberRegistrationSessionId, session.RegistrationName));
                if (result > 0)
                {
                    apiResponse.Status = ApiResponseStatusConstant.SuccessStatus;
                    apiResponse.Message = MemberRegistrationSessionSuccessMessage.DeleteSessionSuccessfully;
                    apiResponse.Data = true;
                }
                else
                {
                    apiResponse.Errors.Add(MemberRegistrationSessionErrorMessage.DeleteSessionFailed);
                }
            }
            catch (Exception ex)
            {
                apiResponse.Errors.Add(ex.Message);
            }
            return apiResponse;
        }
    }
}
