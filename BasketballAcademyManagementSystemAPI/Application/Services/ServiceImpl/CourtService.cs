using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.CourtManagement;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Models;
using log4net;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class CourtService : ICourtService
    {
        private readonly ICourtRepository _courtRepository;
        private readonly IMapper _mapper;
        private static readonly ILog _logger = LogManager.GetLogger(LogConstant.LogName.CourtManagementFeature);

        public CourtService(ICourtRepository courtRepository, IMapper mapper)
        {
            _courtRepository = courtRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponseDto<CourtDto>> GetAllCourts(CourtFilterDto filter)
        {
            var result = await _courtRepository.GetAllCourts(filter);
            _logger.Info($"[GetAllCourts] Retrieved paged court list with {result.TotalRecords} total records");

            var courtDtos = _mapper.Map<List<CourtDto>>(result.Items);

            return new PagedResponseDto<CourtDto>
            {
                Items = courtDtos,
                TotalRecords = result.TotalRecords,
                TotalPages = result.TotalPages,
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize
            };
        }

        public async Task<ApiMessageModelV2<CourtDto>> GetCourtById(string courtId)
        {
            var court = await _courtRepository.GetCourtById(courtId);
            _logger.Info($"[GetCourtById] Fetched court with ID: {courtId}");

            if (court == null)
            {
                return new ApiMessageModelV2<CourtDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = CourtMessage.Error.CourtNotFound
                };
            }

            var courtDto = _mapper.Map<CourtDto>(court);
            return new ApiMessageModelV2<CourtDto>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Data = courtDto
            };
        }

        public async Task<ApiMessageModelV2<CourtDto>> CreateCourt(CourtDto courtDto)
        {
            if (courtDto == null)
            {
                return new ApiMessageModelV2<CourtDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = CourtMessage.Error.InvalidData
                };
            }

            var duplicateCheckResult = await CheckDuplicateCourt(courtDto);
            if (duplicateCheckResult.Status == ApiResponseStatusConstant.FailedStatus)
            {
                return new ApiMessageModelV2<CourtDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = duplicateCheckResult.Message,
                    Errors = duplicateCheckResult.Errors
                };
            }

            courtDto.CourtId = Guid.NewGuid().ToString();
            courtDto.Status = CourtConstant.Status.ACTIVE;
            switch (courtDto.UsagePurpose)
            {
                case 1:
                    courtDto.UsagePurpose = CourtConstant.UsagePurpose.COMPETE;
                    break;
                case 2:
                    courtDto.UsagePurpose = CourtConstant.UsagePurpose.TRAINING;
                    break;
                case 3:
                    courtDto.UsagePurpose = CourtConstant.UsagePurpose.COMPETE_AND_TRAINING;
                    break;
                default:
                    courtDto.UsagePurpose = CourtConstant.UsagePurpose.COMPETE_AND_TRAINING;
                    break;
            }

            var court = _mapper.Map<Court>(courtDto);
            var createdCourt = await _courtRepository.CreateCourt(court);
            var createdCourtDto = _mapper.Map<CourtDto>(createdCourt);
            _logger.Info($"[CreateCourt] Created new court with ID: {createdCourt.CourtId}");

            return new ApiMessageModelV2<CourtDto>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = CourtMessage.Success.CourtCreatedSuccess,
                Data = createdCourtDto
            };
        }

        public async Task<ApiMessageModelV2<CourtDto>> UpdateCourt(CourtDto courtDto)
        {
            if (string.IsNullOrEmpty(courtDto.CourtId))
            {
                return new ApiMessageModelV2<CourtDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = CourtMessage.Error.CourtIDNotFound
                };
            }

            var existingCourt = await _courtRepository.GetCourtById(courtDto.CourtId);
            if (existingCourt == null)
            {
                return new ApiMessageModelV2<CourtDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = CourtMessage.Error.CourtNotFound
                };
            }

            var duplicateCheckResult = await CheckDuplicateCourt(courtDto);
            if (duplicateCheckResult.Status == ApiResponseStatusConstant.FailedStatus)
            {
                return new ApiMessageModelV2<CourtDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = duplicateCheckResult.Message,
                    Errors = duplicateCheckResult.Errors
                };
            }

            _mapper.Map(courtDto, existingCourt);

            var updatedCourt = await _courtRepository.UpdateCourt(existingCourt);
            var updatedCourtDto = _mapper.Map<CourtDto>(updatedCourt);
            _logger.Info($"[UpdateCourt] Updated court with ID: {updatedCourt.CourtId}");

            return new ApiMessageModelV2<CourtDto>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = CourtMessage.Success.CourtUpdatedSuccess,
                Data = updatedCourtDto
            };
        }

        public async Task<ApiMessageModelV2<bool>> CheckDuplicateCourt(CourtDto courtDto)
        {
            var errors = new Dictionary<string, string>();

            var existingCourtWithName = await _courtRepository.GetCourtByNameExceptCourtID(courtDto.CourtName, courtDto.CourtId);
            if (existingCourtWithName != null)
            {
                errors.Add("errorName", CourtMessage.Error.DuplicateCourtName);
            }

            if (errors.Any())
            {
                if (existingCourtWithName != null)
                {
                    return new ApiMessageModelV2<bool>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = CourtMessage.Error.CourtAlreadyExist,
                        Errors = errors
                    };
                }
            }

            return new ApiMessageModelV2<bool>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Data = true
            };
        }

        public async Task<ApiMessageModelV2<bool>> DisableCourt(string courtId)
        {
            var result = await _courtRepository.DisableCourt(courtId);
            _logger.Info($"[DisableCourt] Disable court with ID: {courtId} - Result: {result}");

            if (!result)
            {
                return new ApiMessageModelV2<bool>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = CourtMessage.Error.CourtNotFound
                };
            }

            return new ApiMessageModelV2<bool>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = CourtMessage.Success.CourtDisabledSuccess,
                Data = true
            };
        }
    }
}
