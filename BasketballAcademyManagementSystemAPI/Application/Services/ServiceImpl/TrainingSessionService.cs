using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.CourtManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using static BasketballAcademyManagementSystemAPI.Common.Messages.ApiResponseMessage;
using static BasketballAcademyManagementSystemAPI.Common.Messages.AuthenticationMessage;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class TrainingSessionService : ITrainingSessionService
    {
        private readonly ITrainingSessionRepository _trainingSessionRepository;
        private readonly ICourtRepository _courtRepository;
        private readonly IMailTemplateRepository _mailTemplateRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly ICancelCreateTrainingSessionRequestSchedulerService _cancelCreateTrainingSessionRequestSchedulerService;

        public TrainingSessionService(ITrainingSessionRepository trainingSessionRepository
            , ICourtRepository courtRepository
            , IMailTemplateRepository mailTemplateRepository
            , IUserRepository userRepository
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration
            , IEmailService emailHelper
            , ICancelCreateTrainingSessionRequestSchedulerService cancelCreateTrainingSessionRequestSchedulerService)
        {
            _trainingSessionRepository = trainingSessionRepository;
            _courtRepository = courtRepository;
            _mailTemplateRepository = mailTemplateRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _emailService = emailHelper;
            _cancelCreateTrainingSessionRequestSchedulerService = cancelCreateTrainingSessionRequestSchedulerService;
        }

        public static string GetCurrentUserId(IHttpContextAccessor _httpContextAccessor)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity?.IsAuthenticated == true)
            {
                throw new UnauthorizedAccessException(AuthenticationErrorMessage.AuthenticatedRequired);
            }

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
             ?? user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception(AuthenticationErrorMessage.UserNotFound);
            }

            return userId;
        }

        #region create addtitional training session

        public async Task<ApiMessageModelV2<TrainingSession>> CreateAddtitionalTrainingSessionAsync(CreateTrainingSessionRequest request)
        {
            var response = new ApiMessageModelV2<TrainingSession>();

            var validationResult = await ValidateCreateTrainingSessionRequestAsync(request);
            if (validationResult != null)
            {
                return validationResult;
            }

            // Lấy ra id của người dùng hiện tại
            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            var trainingSession = new TrainingSession
            {
                TrainingSessionId = Guid.NewGuid().ToString(),
                TeamId = request.TeamId,
                CourtId = request.CourtId,
                ScheduledDate = request.ScheduledDate,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Status = TrainingSessionConstant.Status.PENDING,
                CreatedAt = DateTime.Now,
                CreatedByUserId = currentUserId
            };

            // Thêm buổi tập vào cơ sở dữ liệu
            bool isSuccess = await _trainingSessionRepository.AddTrainingSessionAsync(trainingSession);
            if (!isSuccess) {
                response.Message = TrainingSessionMessage.Error.TrainingSessionCreationFailed;
                return response;
            } 

            // Gửi mail thông báo cho các manager
            var managerEmails = await _trainingSessionRepository.GetTeamManagerEmailsAsync(request.TeamId);

            var createdByUser = await _userRepository.GetUserByIdAsync(currentUserId);

            var createdTrainingSession = await _trainingSessionRepository.GetTrainingSessionDetailAsync(trainingSession.TrainingSessionId);

            if (createdTrainingSession != null)
            {
                await SendAddAdditionalSessionPendingNotificationMailAsync(createdTrainingSession, createdByUser.Username, managerEmails);
                await _cancelCreateTrainingSessionRequestSchedulerService.ScheduleCancelIfNotExistsAsync(createdTrainingSession);
            }
            // Trả về response thành công
            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = TrainingSessionMessage.Success.TrainingSessionCreatedSuccessfully;
            response.Data = new TrainingSession
            {
                TrainingSessionId = trainingSession.TrainingSessionId,
                TeamId = trainingSession.TeamId,
                CourtId = trainingSession.CourtId,
                ScheduledDate = trainingSession.ScheduledDate,
                StartTime = trainingSession.StartTime,
                EndTime = trainingSession.EndTime,
                Status = trainingSession.Status
            };
            return response;
        }

        private async Task SendAddAdditionalSessionPendingNotificationMailAsync(TrainingSession trainingSession, string createdByName, List<string> managerMails)
        {
            var mailTemplate = await _mailTemplateRepository.GetByIdAsync(MailTemplateConstant.AdditionalTrainingSessionPending);
            if (mailTemplate != null)
            {
                var cultureInfo = new CultureInfo("vi-VN");
                mailTemplate.Content = mailTemplate.Content
                    .Replace("{{TEAM_NAME}}", trainingSession.Team.TeamName)
                    .Replace("{{SCHEDULED_DATE}}", trainingSession.ScheduledDate.ToString("dddd, dd/MM/yyyy", cultureInfo))
                    .Replace("{{START_TIME}}", trainingSession.StartTime.ToString("HH:mm"))
                    .Replace("{{END_TIME}}", trainingSession.EndTime.ToString("HH:mm"))
                    .Replace("{{COURT_NAME}}", trainingSession.Court.CourtName)
                    .Replace("{{COURT_ADDRESS}}", trainingSession.Court.Address)
                    .Replace("{{COURT_CONTACT}}", trainingSession.Court.Contact)
                    .Replace("{{COURT_PRICE}}", CurrencyHelper.GetFormattedMoney(trainingSession.Court.RentPricePerHour) + "/giờ")
                    .Replace("{{TRAINING_SESSION_ID}}", trainingSession.TrainingSessionId)
                    .Replace("{{CREATED_NAME}}", createdByName);

                if (managerMails.Any())
                {
                    var t = new Thread(() => _emailService.SendEmailMultiThread($"{mailTemplate.TemplateTitle} {trainingSession.ScheduledDate.ToString("dd/MM/yyyy")}", mailTemplate.Content, managerMails));
                    t.Start();
                }
            }
        }

        public async Task<ApiResponseModel<TrainingSessionDto>> CheckTrainingSessionConflictAsync(CheckTrainingSessionConflictRequest request)
        {
            var response = new ApiResponseModel<TrainingSessionDto>();

            var conflictingSession = await _trainingSessionRepository.GetConflictingTrainingSessionOfTeamInDateTime(
                request.TeamId, request.ScheduledDate, request.StartTime, request.EndTime);

            if (conflictingSession != null)
            {
                response.Status = ApiResponseStatusConstant.SuccessStatus;
                response.Message = TrainingSessionMessage.Error.TrainingSessionConflict;
                response.Data = new TrainingSessionDto
                {
                    TrainingSessionId = conflictingSession.TrainingSessionId,
                    TeamId = conflictingSession.TeamId,
                    TeamName = conflictingSession.Team.TeamName,
                    CourtId = conflictingSession.CourtId,
                    CourtName = conflictingSession.Court.CourtName,
                    ScheduledDate = conflictingSession.ScheduledDate,
                    ScheduledStartTime = conflictingSession.StartTime,
                    ScheduledEndTime = conflictingSession.EndTime
                };
                return response;
            }

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = ApiResponseSuccessMessage.ApiSuccessMessage;
            return response;
        }


        private async Task<ApiMessageModelV2<TrainingSession>?> ValidateCreateTrainingSessionRequestAsync(CreateTrainingSessionRequest request)
        {
            // Khởi tạo danh sách lỗi
            var errors = new Dictionary<string, string>();
            var trainingSessionSettingsSection = _configuration.GetSection("TrainingSessionSettings");
            var minimumDurationOfASession = 1.0; // Default value
            var minimumAdvanceScheduling = 24.0; // Default value

            if (trainingSessionSettingsSection != null)
            {
                double.TryParse(trainingSessionSettingsSection["MinimumDurationOfASession"], out minimumDurationOfASession);
                double.TryParse(trainingSessionSettingsSection["MinimumAdvanceScheduling"], out minimumAdvanceScheduling);
            }

            // Kiểm tra thời gian tối thiểu của một buổi tập
            if (request.EndTime < request.StartTime.AddHours(minimumDurationOfASession))
            {
                errors.Add(nameof(request.EndTime), TrainingSessionMessage.Error.MinimumDurationNotMet(minimumDurationOfASession));
            }

            // Kiểm tra thời gian tối thiểu bắt đầu buổi tập trước khi tạo lịch
            var scheduledDateTime = request.ScheduledDate.ToDateTime(request.StartTime);
            if (scheduledDateTime < DateTime.Now.AddHours(minimumAdvanceScheduling))
            {
                errors.Add(nameof(request.ScheduledDate), TrainingSessionMessage.Error.MinimumAdvanceSchedulingRequired(minimumAdvanceScheduling));
            }

            // Kiểm tra xem đội có tồn tại không
            if (!await _trainingSessionRepository.IsValidTeamAsync(request.TeamId))
            {
                errors.Add("isValidTeam" , TrainingSessionMessage.Error.TeamDoesNotExist);
            }

            // Kiểm tra xem sân có tồn tại không
            if (!await _trainingSessionRepository.IsValidCourtAsync(request.CourtId))
            {
                errors.Add(nameof(request.CourtId), TrainingSessionMessage.Error.CourtDoesNotExist);
            }

            // Kiểm tra xem sân có khả dụng không
            var isCourtAvailable = await _trainingSessionRepository.IsCourtAvailableAsync(request.CourtId, request.ScheduledDate, request.StartTime, request.EndTime);
            if (!isCourtAvailable)
            {
                errors.Add(nameof(isCourtAvailable), TrainingSessionMessage.Error.CourtUnavailable);
            }

            // Kiểm tra xem người dùng có phải là huấn luyện viên của đội không
            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            if (!await _trainingSessionRepository.IsUserCoachOfTeamAsync(currentUserId, request.TeamId))
            {
                errors.Add(nameof(currentUserId), TrainingSessionMessage.Error.OnlyCoachOfTeamCanCreateTrainingSession);
            }

            // Kiểm tra xem đội có khả dụng không vào thời gian đã chọn không
            if (!await _trainingSessionRepository.IsTeamAvailableAsync(request.TeamId, request.ScheduledDate, request.StartTime, request.EndTime))
            {
                errors.Add(nameof(request.TeamId), TrainingSessionMessage.Error.TrainingSessionConflict);
            }

            // Kiểm tra xem có lỗi nào không
            if (errors.Any())
            {
                return ErrorResponse(errors, TrainingSessionMessage.Error.TrainingSessionCreationFailed);
            }

            return null;
        }

        private ApiMessageModelV2<TrainingSession> ErrorResponse(Dictionary<string, string> errors, string message)
        {
            return new ApiMessageModelV2<TrainingSession>
            {
                Status = ApiResponseStatusConstant.FailedStatus,
                Message = message,
                Errors = errors
            };
        }

        #endregion

        #region get pending training session
        public async Task<ApiResponseModel<List<TrainingSessionDto>>> GetPendingTrainingSession()
        {
            var response = new ApiResponseModel<List<TrainingSessionDto>>()
            {
                Message = TrainingSessionMessage.Error.NotFoundTrainingSession
            };

            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            var user = await _userRepository.GetUserWithRoleByIdAsync(currentUserId);
            if (user == null)
            {
                response.Message = AuthenticationErrorMessage.InvalidCredentials;
                return response;
            }

            if ((user.Manager == null || user.Manager.TeamId == null) && (user.Coach == null || user.Coach.TeamId == null))
            {
                response.Data = new List<TrainingSessionDto>();
                response.Status = ApiResponseStatusConstant.SuccessStatus;
                return response;
            }

            var teamId = user.Manager?.TeamId ?? user.Coach?.TeamId;

            var sessions = await _trainingSessionRepository.GetPendingTrainingSessionOfATeamAsync(teamId);
            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = ApiResponseSuccessMessage.ApiSuccessMessage;
            response.Data = new List<TrainingSessionDto>();
            response.Data = sessions.Select(ts => new TrainingSessionDto
            {
                TrainingSessionId = ts.TrainingSessionId,
                TeamId = ts.TeamId,
                TeamName = ts.Team?.TeamName ?? string.Empty,
                CourtId = ts.CourtId,
                CourtName = ts.Court?.CourtName ?? string.Empty,
                CourtAddress = ts.Court?.Address ?? string.Empty,
                CourtContact = ts.Court?.Contact ?? string.Empty,
                CourtPrice = ts.Court?.RentPricePerHour ?? 0,
                ScheduledDate = ts.ScheduledDate,
                ScheduledStartTime = ts.StartTime,
                ScheduledEndTime = ts.EndTime,
                CreatedAt = ts.CreatedAt,
                RequestRemainingTime = Math.Max(0, (int)(ts.CreatedAt.AddHours(12) - DateTime.Now).TotalSeconds),
                Note = null
            }).OrderBy(ts => ts.ScheduledDate).ToList();

            return response;
        }

        public async Task<ApiResponseModel<List<RequestUpdateTrainingSessionDto>>> GetPendingRequestUpdateTrainingSession()
        {
            var response = new ApiResponseModel<List<RequestUpdateTrainingSessionDto>>()
            {
                Message = TrainingSessionMessage.Error.NotFoundTrainingSessionUpdateRequest
            };

            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            var user = await _userRepository.GetUserWithRoleByIdAsync(currentUserId);
            if (user == null)
            {
                response.Message = AuthenticationErrorMessage.PleaseLogin;
                return response;
            }

            if ((user.Manager == null || user.Manager.TeamId == null) && (user.Coach == null || user.Coach.TeamId == null))
            {
                response.Data = new List<RequestUpdateTrainingSessionDto>();
                response.Status = ApiResponseStatusConstant.SuccessStatus;
                return response;
            }

            var teamId = user.Manager?.TeamId ?? user.Coach?.TeamId;

            var requests = await _trainingSessionRepository.GetTrainingSessionPendingChangeRequestOfATeamAsync(teamId, TrainingSessionConstant.StatusChangeRequestType.UPDATE);
            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = ApiResponseSuccessMessage.ApiSuccessMessage;
            var vietnamCulture = new CultureInfo("vi-VN");

            response.Data = requests.Select(scrq => new RequestUpdateTrainingSessionDto
            {
                StatusChangeRequestId = scrq.StatusChangeRequestId,
                TrainingSessionId = scrq.TrainingSessionId,
                RequestedByCoachId = scrq.RequestedByCoachId,
                RequestedByCoachUsername = scrq.RequestedByCoach?.User?.Username ?? string.Empty,
                RequestReason = scrq.RequestReason,
                RejectedReason = scrq.RejectedReason,
                OldCourtId = scrq.TrainingSession.CourtId,
                OldCourtName = scrq.TrainingSession.Court.CourtName,
                OldCourtAddress = scrq.TrainingSession.Court.Address,
                OldCourtContact = scrq.TrainingSession.Court.Contact ?? "Không xác định",
                OldCourtRentPrice = scrq.TrainingSession.Court.RentPricePerHour,
                OldScheduledDate = scrq.TrainingSession.ScheduledDate.ToString("dddd, dd/MM/yyyy", vietnamCulture),
                OldStartTime = scrq.TrainingSession.StartTime.ToString("HH:mm"),
                OldEndTime = scrq.TrainingSession.EndTime.ToString("HH:mm"),
                OldCourtPrice = scrq.TrainingSession.CourtPrice ?? 0,
                NewCourtId = scrq.NewCourtId,
                NewCourtName = scrq.NewCourt?.CourtName ?? string.Empty,
                NewCourtAddress = scrq.NewCourt?.Address ?? string.Empty,
                NewCourtContact = scrq.NewCourt?.Contact ?? string.Empty,
                NewCourtRentPrice = scrq.TrainingSession.Court.RentPricePerHour,
                NewScheduledDate = scrq.NewScheduledDate?.ToString("dddd, dd/MM/yyyy", vietnamCulture),
                NewStartTime = scrq.NewStartTime?.ToString("HH:mm"),
                NewEndTime = scrq.NewEndTime?.ToString("HH:mm"),
                RequestedAt = scrq.RequestedAt.ToString("dd/MM/yyyy HH:mm"),
                RequestRemainingTime = Math.Max(0, (int)(scrq.RequestedAt.AddHours(8) - DateTime.Now).TotalSeconds),
                Status = TrainingSessionConstant.StatusChangeRequestStatus.GetStatus(scrq.Status),
                DecisionByManagerId = scrq.DecisionByManagerId,
                DecisionByManagerUsername = scrq.DecisionByManager?.User?.Username ?? string.Empty,
                DecisionAt = scrq.DecisionAt?.ToString("dd/MM/yyyy HH:mm"),
            }).OrderBy(ts => ts.OldScheduledDate).ToList();

            return response;
        }
        #endregion

        #region accept create training session
        public async Task<ApiResponseModel<TrainingSessionDto>> ApproveTrainingSessionAsync(ApproveTrainingSessionRequest request)
        {
            var response = new ApiResponseModel<TrainingSessionDto>()
            {
                Message = TrainingSessionMessage.Error.TrainingSessionUpdateFailed
            };

            // Kiểm tra xem buổi tập có tồn tại không
            var trainingSession = await _trainingSessionRepository.GetTrainingSessionDetailAsync(request.TrainingSessionId);
            if (trainingSession == null)
            {
                response.Message = TrainingSessionMessage.Error.TrainingSessionNotFound;
                return response;
            }

            if (trainingSession.Status != TrainingSessionConstant.Status.PENDING)
            {
                response.Message = TrainingSessionMessage.Error.TrainingSessionNotPending;
                return response;
            }

            // Kiểm tra xem người dùng có phải là quản lý của đội không
            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            if (!await _trainingSessionRepository.IsUserManagerOfTeamAsync(currentUserId, trainingSession.TeamId))
            {
                response.Message = TrainingSessionMessage.Error.OnlyManagerOfTeamCanAcceptTrainingSession;
                return response;
            }

            if (trainingSession.Status == TrainingSessionConstant.Status.PENDING)
            {
                trainingSession.Status = TrainingSessionConstant.Status.ACTIVE;
                trainingSession.CreatedDecisionByManagerId = currentUserId;
                trainingSession.CreatedDecisionAt = DateTime.Now;
                trainingSession.CourtPrice = request.CourtPrice;

                var result = await _trainingSessionRepository.UpdateTrainingSessionAsync(trainingSession);
                if (result)
                {
                    response.Status = ApiResponseStatusConstant.SuccessStatus;
                    response.Message = TrainingSessionMessage.Success.TrainingSessionAcceptedSuccessfully;
                    response.Data = new TrainingSessionDto
                    {
                        TrainingSessionId = trainingSession.TrainingSessionId,
                        TeamId = trainingSession.TeamId,
                        TeamName = trainingSession.Team.TeamName,
                        CourtId = trainingSession.CourtId,
                        CourtName = trainingSession.Court.CourtName,
                        ScheduledDate = trainingSession.ScheduledDate,
                        ScheduledStartTime = trainingSession.StartTime,
                        ScheduledEndTime = trainingSession.EndTime
                    };

                    // Gửi mail thông báo cho toàn đội ở đây
                    var teamEmails = await _trainingSessionRepository.GetTeamEmailsAsync(trainingSession.TeamId);

                    var createdByUser = await _userRepository.GetUserByIdAsync(currentUserId);

                    var createdTrainingSession = await _trainingSessionRepository.GetTrainingSessionDetailAsync(trainingSession.TrainingSessionId);

                    if (createdTrainingSession != null)
                    {
                        await SendAddAdditionalSessionNotificationMailAsync(createdTrainingSession, createdByUser.Username, teamEmails);
                    }
                }
            }
            return response;
        }

        private async Task SendAddAdditionalSessionNotificationMailAsync(TrainingSession trainingSession, string createdByName, List<string> teamEmails)
        {
            var mailTemplate = await _mailTemplateRepository.GetByIdAsync(MailTemplateConstant.CreateAdditionalTrainingSession);
            if (mailTemplate != null)
            {
                var cultureInfo = new CultureInfo("vi-VN");
                mailTemplate.Content = mailTemplate.Content
                    .Replace("{{TEAM_NAME}}", trainingSession.Team.TeamName)
                    .Replace("{{SCHEDULED_DATE}}", trainingSession.ScheduledDate.ToString("dddd, dd/MM/yyyy", cultureInfo))
                    .Replace("{{START_TIME}}", trainingSession.StartTime.ToString("HH:mm"))
                    .Replace("{{END_TIME}}", trainingSession.EndTime.ToString("HH:mm"))
                    .Replace("{{COURT_NAME}}", trainingSession.Court.CourtName)
                    .Replace("{{CREATED_NAME}}", createdByName);

                var t = new Thread(() => _emailService.SendEmailMultiThread($"{mailTemplate.TemplateTitle} {trainingSession.ScheduledDate.ToString("dd/MM/yyyy")}", mailTemplate.Content, teamEmails));
                t.Start();
            }
        }

        #endregion

        #region reject create training session

        public async Task<ApiResponseModel<TrainingSessionDto>> RejectTrainingSessionAsync(CancelTrainingSessionRequest request)
        {
            var response = new ApiResponseModel<TrainingSessionDto>()
            {
                Message = TrainingSessionMessage.Error.TrainingSessionUpdateFailed
            };

            // Kiểm tra xem buổi tập có tồn tại không
            var trainingSession = await _trainingSessionRepository.GetTrainingSessionDetailAsync(request.TrainingSessionId);
            if (trainingSession == null)
            {
                response.Message = TrainingSessionMessage.Error.TrainingSessionNotFound;
                return response;
            }

            if (trainingSession.Status != TrainingSessionConstant.Status.PENDING)
            {
                response.Message = TrainingSessionMessage.Error.TrainingSessionNotPending;
                return response;
            }

            // Kiểm tra xem người dùng có phải là quản lý của đội không
            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            if (!await _trainingSessionRepository.IsUserManagerOfTeamAsync(currentUserId, trainingSession.TeamId))
            {
                response.Status = ApiResponseStatusConstant.FailedStatus;
                response.Message = TrainingSessionMessage.Error.OnlyManagerOfTeamCanRejectTrainingSession;
                return response;
            }

            trainingSession.Status = TrainingSessionConstant.Status.CANCELED;
            trainingSession.CreatedDecisionByManagerId = currentUserId;
            trainingSession.CreatedDecisionAt = DateTime.Now;
            trainingSession.CreateRejectedReason = request.Reason;
            var result = await _trainingSessionRepository.UpdateTrainingSessionAsync(trainingSession);
            if (result)
            {
                response.Status = ApiResponseStatusConstant.SuccessStatus;
                response.Message = TrainingSessionMessage.Success.TrainingSessionRejectedSuccessfully;
                response.Data = new TrainingSessionDto
                {
                    TrainingSessionId = trainingSession.TrainingSessionId,
                    TeamId = trainingSession.TeamId,
                    TeamName = trainingSession.Team.TeamName,
                    CourtId = trainingSession.CourtId,
                    CourtName = trainingSession.Court.CourtName,
                    ScheduledDate = trainingSession.ScheduledDate,
                    ScheduledStartTime = trainingSession.StartTime,
                    ScheduledEndTime = trainingSession.EndTime
                };

                // Gửi mail thông báo cho người tạo session ở đây
                var createdUser = await _userRepository.GetUserByIdAsync(trainingSession.CreatedByUserId);

                var rejectedByUser = await _userRepository.GetUserByIdAsync(currentUserId);

                await SendRejectedCreateSessionNotificationMailAsync(trainingSession, rejectedByUser.Username, createdUser);
            }
            return response;
        }

        private async Task SendRejectedCreateSessionNotificationMailAsync(TrainingSession trainingSession, string rejectedByName, User createdBy)
        {
            var mailTemplate = await _mailTemplateRepository.GetByIdAsync(MailTemplateConstant.RejectPendingTrainingSession);
            if (mailTemplate != null)
            {
                var cultureInfo = new CultureInfo("vi-VN");
                mailTemplate.Content = mailTemplate.Content
                    .Replace("{{COACH_NAME}}", createdBy.Username)
                    .Replace("{{DATE}}", trainingSession.ScheduledDate.ToString("dddd, dd/MM/yyyy", cultureInfo))
                    .Replace("{{START_TIME}}", trainingSession.StartTime.ToString("HH:mm"))
                    .Replace("{{END_TIME}}", trainingSession.EndTime.ToString("HH:mm"))
                    .Replace("{{COURT_NAME}}", trainingSession.Court.CourtName)
                    .Replace("{{REASON}}", trainingSession.CreateRejectedReason)
                    .Replace("{{REJECTED_NAME}}", rejectedByName);

                var receiverEmails = new List<string>();
                if (createdBy.Email != null)
                {
                    receiverEmails.Add(createdBy.Email);
                    var t = new Thread(() => _emailService.SendEmailMultiThread($"{(mailTemplate.TemplateTitle).Replace("{{DATE}}", trainingSession.ScheduledDate.ToString("dd/MM/yyyy"))}", mailTemplate.Content, receiverEmails));
                    t.Start();
                }
            }
        }

        #endregion

        #region get training session schedule
        public async Task<ApiMessageModelV2<List<TrainingSessionDto>>> GetTrainingSessionsAsync(DateTime startDate, DateTime endDate, string? teamId, string? courtId)
        {
            var response = new ApiMessageModelV2<List<TrainingSessionDto>>();
            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            var userRole = await _trainingSessionRepository.GetUserRoleAsync(currentUserId);

            List<TrainingSession> trainingSessions;

            // Lấy ra danh sách buổi tập tương ứng với vai trò của người dùng
            switch (userRole)
            {
                // Nếu người dùng là cầu thủ
                case RoleCodeConstant.PlayerCode:
                    trainingSessions = await _trainingSessionRepository.GetTrainingSessionsByPlayerAsync(currentUserId, startDate, endDate);
                    break;
                // Nếu người dùng là huấn luyện viên
                case RoleCodeConstant.ManagerCode:
                    trainingSessions = await _trainingSessionRepository.GetTrainingSessionsByManagerAsync(currentUserId, startDate, endDate);
                    break;
                // Nếu người dùng là phụ huynh
                case RoleCodeConstant.ParentCode:
                    trainingSessions = await _trainingSessionRepository.GetTrainingSessionsByParentAsync(currentUserId, startDate, endDate);
                    break;
                // Nếu người dùng là huấn luyện viên
                case RoleCodeConstant.CoachCode:
                    trainingSessions = await _trainingSessionRepository.GetTrainingSessionsByCoachAsync(currentUserId, startDate, endDate, teamId, courtId);
                    break;
                // Nếu người dùng là chủ tịch
                case RoleCodeConstant.PresidentCode:
                    trainingSessions = await _trainingSessionRepository.GetAllTrainingSessionsAsync(startDate, endDate);
                    break;
                // Nếu người dùng không thuộc bất kỳ vai trò nào
                default:
                    trainingSessions = new List<TrainingSession>();
                    break;
            }

            if (trainingSessions == null || trainingSessions.Count == 0)
            {
                response.Status = ApiResponseStatusConstant.SuccessStatus;
                response.Message = TrainingSessionMessage.Error.NotFoundTrainingSession;
                response.Data = new List<TrainingSessionDto>();
                return response;
            }

            var trainingSessionDtos = new List<TrainingSessionDto>();

            // Nếu người dùng là phụ huynh, lấy lịch theo player của họ
            if (userRole == RoleCodeConstant.ParentCode)
            {
                var players = await _trainingSessionRepository.GetPlayersOfParentAsync(currentUserId);
                if (players != null && players.Count > 0)
                {
                    foreach (var player in players)
                    {
                        foreach (var ts in trainingSessions)
                        {
                            var mappedTrainingSession = new TrainingSessionDto
                            {
                                TrainingSessionId = ts.TrainingSessionId,
                                TeamId = ts.TeamId,
                                TeamName = ts.Team?.TeamName ?? string.Empty,
                                CourtId = ts.CourtId,
                                CourtName = ts.Court?.CourtName ?? string.Empty,
                                CourtAddress = ts.Court?.Address ?? string.Empty,
                                CourtContact = ts.Court?.Contact ?? string.Empty,
                                CourtPrice = ts.CourtPrice ?? 0,
                                ScheduledDate = ts.ScheduledDate,
                                ScheduledStartTime = ts.StartTime,
                                ScheduledEndTime = ts.EndTime,
                                Note = null
                            };

                            if(player.TeamId == ts.TeamId)
                            {
                                mappedTrainingSession.PlayerId = player.UserId;
                                mappedTrainingSession.PlayerName = player.User?.Username ?? string.Empty;


                                // Thêm DTO vào danh sách
                                trainingSessionDtos.Add(mappedTrainingSession);
                            }
                        }
                    }
                }
            }
            else
            {
                // Duyệt qua từng buổi tập và tạo ra DTO tương ứng
                foreach (var ts in trainingSessions)
                {
                    var mappedTrainingSession = new TrainingSessionDto
                    {
                        TrainingSessionId = ts.TrainingSessionId,
                        TeamId = ts.TeamId,
                        TeamName = ts.Team?.TeamName ?? string.Empty,
                        CourtId = ts.CourtId,
                        CourtName = ts.Court?.CourtName ?? string.Empty,
                        CourtAddress = ts.Court?.Address ?? string.Empty,
                        CourtContact = ts.Court?.Contact ?? string.Empty,
                        CourtPrice = ts.CourtPrice ?? 0,
                        ScheduledDate = ts.ScheduledDate,
                        ScheduledStartTime = ts.StartTime,
                        ScheduledEndTime = ts.EndTime,
                        Note = null
                    };

                    // Thêm DTO vào danh sách
                    trainingSessionDtos.Add(mappedTrainingSession);
                }
            }

            // Sắp xếp danh sách buổi tập theo thời gian
            trainingSessionDtos = trainingSessionDtos
                .OrderBy(ts => ts.ScheduledDate)
                .ThenBy(ts => ts.ScheduledStartTime)
                .ToList();

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = ApiResponseSuccessMessage.ApiSuccessMessage;
            response.Data = trainingSessionDtos;

            return response;
        }
        #endregion

        #region get training session detail
        public async Task<ApiResponseModel<TrainingSessionDetailDto>> GetTrainingSessionByIdAsync(string trainingSessionId)
        {
            var trainingSession = await _trainingSessionRepository.GetTrainingSessionDetailAsync(trainingSessionId);

            if (trainingSession == null)
            {
                return new ApiResponseModel<TrainingSessionDetailDto>
                {
                    Message = TrainingSessionMessage.Error.TrainingSessionNotFound
                };
            }

            // Tạo ra DTO từ training session
            var cultureInfo = new CultureInfo("vi-VN");
            var trainingSessionDetailDto = new TrainingSessionDetailDto
            {
                ScheduledDate = trainingSession.ScheduledDate.ToString("dddd, dd/MM/yyyy", cultureInfo),
                Time = $"{trainingSession.StartTime:hh\\:mm} - {trainingSession.EndTime:hh\\:mm}",
                TeamId = trainingSession.TeamId,
                TeamName = trainingSession.Team.TeamName,
                Status = TrainingSessionConstant.Status.GetStatus(trainingSession.Status),
                CourtPrice = CurrencyHelper.GetFormattedMoney(trainingSession.CourtPrice ?? 0),
                Court = new CourtDto
                {
                    CourtId = trainingSession.CourtId,
                    CourtName = trainingSession.Court.CourtName,
                    RentPricePerHour = trainingSession.Court.RentPricePerHour,
                    Address = trainingSession.Court.Address,
                    Contact = trainingSession.Court.Contact,
                    Status = trainingSession.Court.Status,
                    Type = trainingSession.Court.Type,
                    Kind = trainingSession.Court.Kind,
                    ImageUrl = trainingSession.Court.ImageUrl
                },
                Exercises = trainingSession.Exercises
                .OrderBy(e => e.CreatedAt)
                .Select(e => new ExerciseDto
                {
                    ExerciseId = e.ExerciseId,
                    ExerciseName = e.ExerciseName,
                    Duration = e.Duration,
                    Description = e.Description,
                    CoachId = e.Coach?.User.UserId ?? ExerciseMessage.Error.ExerciseNotAssignedToCoach,
                    CoachUsername = e.Coach?.User.Username ?? ExerciseMessage.Error.ExerciseNotAssignedToCoach
                }).ToList()
            };

            return new ApiResponseModel<TrainingSessionDetailDto>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = ApiResponseSuccessMessage.ApiSuccessMessage,
                Data = trainingSessionDetailDto
            };
        }
        #endregion

        #region cancel a training session
        public async Task<ApiResponseModel<bool>> RequestToCancelTrainingSessionAsync(CancelTrainingSessionRequest request)
        {
            var response = new ApiResponseModel<bool>()
            {
                Message = TrainingSessionMessage.Error.CancelTrainingSessionFailed
            };
            // Kiểm tra xem buổi tập có tồn tại không
            var trainingSession = await _trainingSessionRepository.GetTrainingSessionDetailAsync(request.TrainingSessionId);
            if (trainingSession == null)
            {
                response.Errors = [TrainingSessionMessage.Error.TrainingSessionNotFound];
                return response;
            }

            // Kiểm tra xem người dùng có phải là cầu thủ của đội không
            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            if (!await _trainingSessionRepository.IsUserCoachOfTeamAsync(currentUserId, trainingSession.TeamId))
            {
                response.Errors = [TrainingSessionMessage.Error.OnlyCoachOfTeamCanCancelTrainingSession];
                return response;
            }

            // Kiểm tra xem buổi tập đã bị hủy chưa, nếu đã bị huỷ thì không thể hủy lại
            if (trainingSession.Status == 0)
            {
                response.Errors = [TrainingSessionMessage.Error.TrainingSessionAlreadyCancelled];
                return response;
            }

            // Kiểm tra xem có yêu cầu huỷ hay không
            var trainingSessionStatusChangeRequest = await _trainingSessionRepository.IsTrainingSessionHaveRequestExistsAsync(request.TrainingSessionId);
            if (trainingSessionStatusChangeRequest)
            {
                response.Errors = [TrainingSessionMessage.Error.ThisSessionAlreadyHaveAnRequest];
                return response;
            }

            // tạo yêu cầu hủy buổi tập
            TrainingSessionStatusChangeRequest statusChangeRequest = new TrainingSessionStatusChangeRequest
            {
                TrainingSessionId = trainingSession.TrainingSessionId,
                RequestType = TrainingSessionConstant.StatusChangeRequestType.CANCEL,
                RequestReason = request.Reason,
                RequestedAt = DateTime.Now,
                Status = TrainingSessionConstant.StatusChangeRequestStatus.PENDING,
                RequestedByCoachId = currentUserId
            };
            await _trainingSessionRepository.CreateRequestToChangeTrainingSessionStatus(statusChangeRequest);

            // Trả về response thành công
            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = TrainingSessionMessage.Success.RequestToCancelTrainingSessionSuccessfully;
            response.Data = true;

            return response;
        }

        public async Task<ApiResponseModel<List<RequestCancelTrainingSessionDto>>> GetRequestCancelTrainingSession()
        {
            var response = new ApiResponseModel<List<RequestCancelTrainingSessionDto>>()
            {
                Message = TrainingSessionMessage.Error.NotFoundTrainingSessionCancelRequest
            };

            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            var user = await _userRepository.GetUserWithRoleByIdAsync(currentUserId);
            if (user == null)
            {
                response.Message = AuthenticationErrorMessage.InvalidCredentials;
                return response;
            }

            if ((user.Manager == null || user.Manager.TeamId == null) && (user.Coach == null || user.Coach.TeamId == null))
            {
                response.Data = new List<RequestCancelTrainingSessionDto>();
                response.Status = ApiResponseStatusConstant.SuccessStatus;
                return response;
            }

            var teamId = user.Manager?.TeamId ?? user.Coach?.TeamId;

            var requests = await _trainingSessionRepository.GetTrainingSessionPendingChangeRequestOfATeamAsync(teamId, TrainingSessionConstant.StatusChangeRequestType.CANCEL);
            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = ApiResponseSuccessMessage.ApiSuccessMessage;
            var vietnamCulture = new CultureInfo("vi-VN");

            response.Data = requests.Select(scrq => new RequestCancelTrainingSessionDto
            {
                StatusChangeRequestId = scrq.StatusChangeRequestId,
                TrainingSessionId = scrq.TrainingSessionId,
                RequestedByCoachId = scrq.RequestedByCoachId,
                RequestedByCoachUsername = scrq.RequestedByCoach?.User?.Username ?? string.Empty,
                RequestReason = scrq.RequestReason,
                RejectedReason = scrq.RejectedReason,
                OldCourtId = scrq.TrainingSession.CourtId,
                OldCourtName = scrq.TrainingSession.Court.CourtName,
                OldCourtAddress = scrq.TrainingSession.Court.Address,
                OldCourtContact = scrq.TrainingSession.Court.Contact ?? "Không xác định",
                OldCourtRentPrice = scrq.TrainingSession.Court.RentPricePerHour,
                OldScheduledDate = scrq.TrainingSession.ScheduledDate.ToString("dddd, dd/MM/yyyy", vietnamCulture),
                OldStartTime = scrq.TrainingSession.StartTime.ToString("HH:mm"),
                OldEndTime = scrq.TrainingSession.EndTime.ToString("HH:mm"),
                OldCourtPrice = scrq.TrainingSession.CourtPrice ?? 0,
                RequestedAt = scrq.RequestedAt.ToString("dd/MM/yyyy HH:mm"),
                RequestRemainingTime = Math.Max(0, (int)(scrq.RequestedAt.AddHours(8) - DateTime.Now).TotalSeconds),
                Status = TrainingSessionConstant.StatusChangeRequestStatus.GetStatus(scrq.Status),
                DecisionByManagerId = scrq.DecisionByManagerId,
                DecisionByManagerUsername = scrq.DecisionByManager?.User?.Username ?? string.Empty,
                DecisionAt = scrq.DecisionAt?.ToString("dd/MM/yyyy HH:mm"),
            }).OrderBy(ts => ts.OldScheduledDate).ToList();

            return response;
        }

        public async Task<ApiResponseModel<string>> CancelTrainingSessionAsync(string trainingSessionId)
        {
            var response = new ApiResponseModel<string>()
            {
                Message = TrainingSessionMessage.Error.CancelTrainingSessionFailed
            };

            // Kiểm tra xem buổi tập có tồn tại không
            var trainingSession = await _trainingSessionRepository.GetTrainingSessionDetailAsync(trainingSessionId);
            if (trainingSession == null)
            {
                response.Errors = [TrainingSessionMessage.Error.TrainingSessionNotFound];
                return response;
            }

            // Kiểm tra xem người dùng có phải là manager của đội không
            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            if (!await _trainingSessionRepository.IsUserManagerOfTeamAsync(currentUserId, trainingSession.TeamId))
            {
                response.Errors = [TrainingSessionMessage.Error.OnlyManagerOfTeamCanApproveCancelRequest];
                return response;
            }

            // Kiểm tra xem buổi tập đã bị hủy chưa, nếu đã bị huỷ thì không thể hủy lại
            if (trainingSession.Status == 0)
            {
                response.Errors = [TrainingSessionMessage.Error.TrainingSessionAlreadyCancelled];
                return response;
            }

            // Kiểm tra xem có yêu cầu huỷ hay không
            var trainingSessionStatusChangeRequest = await _trainingSessionRepository.GetSessionStatusChangeRequestByTrainingSessionIdAsync(trainingSessionId);
            if (trainingSessionStatusChangeRequest == null)
            {
                response.Errors = [TrainingSessionMessage.Error.CancelTrainingSessionRequestNotFound];
                return response;
            }

            if (trainingSessionStatusChangeRequest.RequestType != TrainingSessionConstant.StatusChangeRequestType.CANCEL)
            {
                response.Errors = [TrainingSessionMessage.Error.ThisRequestIsNotForCancel];
                return response;
            }

            // Huỷ buổi tập và lưu vào csdl
            trainingSession.Status = 0;
            var updateResult = await _trainingSessionRepository.UpdateTrainingSessionAsync(trainingSession);
            if (!updateResult)
            {
                response.Errors = [TrainingSessionMessage.Error.CancelTrainingSessionFailed];
                return response;
            }

            // Cập nhật yêu cầu huỷ buổi tập
            trainingSessionStatusChangeRequest.Status = TrainingSessionConstant.StatusChangeRequestStatus.APPROVED;
            trainingSessionStatusChangeRequest.DecisionAt = DateTime.Now;
            trainingSessionStatusChangeRequest.DecisionByManagerId = currentUserId;
            await _trainingSessionRepository.UpdateTrainingSessionStatusChangeRequestAsync(trainingSessionStatusChangeRequest);

            // Gửi thông báo qua email cho cả đội
            var teamEmails = await _trainingSessionRepository.GetTeamEmailsAsync(trainingSession.TeamId);
            var user = await _userRepository.GetUserByIdAsync(currentUserId);

            await SendCancelTrainingSessionNotificationMailAsync(trainingSession, user.Username, teamEmails);

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = TrainingSessionMessage.Success.CancelTrainingSessionSuccessfully;
            response.Data = TrainingSessionMessage.Success.CancelTrainingSessionSuccessfully;
            return response;
        }

        private async Task SendCancelTrainingSessionNotificationMailAsync(TrainingSession trainingSession, string cancelledByName, List<string> teamEmails)
        {
            var mailTemplate = await _mailTemplateRepository.GetByIdAsync(MailTemplateConstant.CancelTrainingSession);
            if (mailTemplate != null)
            {
                var cultureInfo = new CultureInfo("vi-VN");
                mailTemplate.Content = mailTemplate.Content
                    .Replace("{{TEAM_NAME}}", trainingSession.Team.TeamName)
                    .Replace("{{SCHEDULED_DATE}}", trainingSession.ScheduledDate.ToString("dddd, dd/MM/yyyy", cultureInfo))
                    .Replace("{{START_TIME}}", trainingSession.StartTime.ToString("HH:mm"))
                    .Replace("{{END_TIME}}", trainingSession.EndTime.ToString("HH:mm"))
                    .Replace("{{COURT_NAME}}", trainingSession.Court.CourtName)
                    .Replace("{{CANCELED_NAME}}", cancelledByName);

                var t = new Thread(() => _emailService.SendEmailMultiThread($"{mailTemplate.TemplateTitle} {trainingSession.ScheduledDate.ToString("dd/MM/yyyy")}", mailTemplate.Content, teamEmails));
                t.Start();
            }
        }

        public async Task<ApiResponseModel<string>> RejectCancelTrainingSessionRequestAsync(CancelTrainingSessionChangeStatusRequest request)
        {
            var response = new ApiResponseModel<string>()
            {
                Message = TrainingSessionMessage.Error.CancelTrainingSessionFailed
            };

            // Kiểm tra xem buổi tập có tồn tại không
            var trainingSession = await _trainingSessionRepository.GetTrainingSessionDetailAsync(request.TrainingSessionId);
            if (trainingSession == null)
            {
                response.Errors = [TrainingSessionMessage.Error.TrainingSessionNotFound];
                return response;
            }

            // Kiểm tra xem người dùng có phải là manager của đội không
            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            if (!await _trainingSessionRepository.IsUserManagerOfTeamAsync(currentUserId, trainingSession.TeamId))
            {
                response.Errors = [TrainingSessionMessage.Error.OnlyManagerOfTeamCanRejectCancelRequest];
                return response;
            }

            // Kiểm tra xem có yêu cầu huỷ hay không
            var trainingSessionStatusChangeRequest = await _trainingSessionRepository.GetSessionStatusChangeRequestByTrainingSessionIdAsync(request.TrainingSessionId);
            if (trainingSessionStatusChangeRequest == null)
            {
                response.Errors = [TrainingSessionMessage.Error.CancelTrainingSessionRequestNotFound];
                return response;
            }

            if (trainingSessionStatusChangeRequest.RequestType != TrainingSessionConstant.StatusChangeRequestType.CANCEL)
            {
                response.Errors = [TrainingSessionMessage.Error.ThisRequestIsNotForCancel];
                return response;
            }

            // Cập nhật yêu cầu huỷ buổi tập
            trainingSessionStatusChangeRequest.Status = TrainingSessionConstant.StatusChangeRequestStatus.REJECTED;
            trainingSessionStatusChangeRequest.DecisionAt = DateTime.Now;
            trainingSessionStatusChangeRequest.DecisionByManagerId = currentUserId;
            trainingSessionStatusChangeRequest.RejectedReason = request.Reason;
            await _trainingSessionRepository.UpdateTrainingSessionStatusChangeRequestAsync(trainingSessionStatusChangeRequest);

            // Gửi mail thông báo cho người tạo request ở đây
            var createdUser = await _userRepository.GetUserByIdAsync(trainingSessionStatusChangeRequest.RequestedByCoachId);
            var rejectedByUser = await _userRepository.GetUserByIdAsync(currentUserId);

            await SendRejectedCancelSessionNotificationMailAsync(trainingSession, rejectedByUser.Username, createdUser);

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = TrainingSessionMessage.Success.RejectCancelTrainingSessionRequestSuccessfully;
            response.Data = TrainingSessionMessage.Success.RejectCancelTrainingSessionRequestSuccessfully;
            return response;
        }

        private async Task SendRejectedCancelSessionNotificationMailAsync(TrainingSession trainingSession, string rejectedByName, User createdBy)
        {
            var mailTemplate = await _mailTemplateRepository.GetByIdAsync(MailTemplateConstant.RejectCancelTrainingSessionRequest);
            if (mailTemplate != null)
            {
                var cultureInfo = new CultureInfo("vi-VN");
                mailTemplate.Content = mailTemplate.Content
                    .Replace("{{COACH_NAME}}", createdBy.Username)
                    .Replace("{{DATE}}", trainingSession.ScheduledDate.ToString("dddd, dd/MM/yyyy", cultureInfo))
                    .Replace("{{START_TIME}}", trainingSession.StartTime.ToString("HH:mm"))
                    .Replace("{{END_TIME}}", trainingSession.EndTime.ToString("HH:mm"))
                    .Replace("{{COURT_NAME}}", trainingSession.Court.CourtName)
                    .Replace("{{REASON}}", trainingSession.CreateRejectedReason)
                    .Replace("{{REJECTED_NAME}}", rejectedByName);

                var receiverEmails = new List<string>();
                if (createdBy.Email != null)
                {
                    receiverEmails.Add(createdBy.Email);
                    var t = new Thread(() => _emailService.SendEmailMultiThread($"{(mailTemplate.TemplateTitle).Replace("{{DATE}}", trainingSession.ScheduledDate.ToString("dd/MM/yyyy"))}", mailTemplate.Content, receiverEmails));
                    t.Start();
                }
            }
        }
        #endregion

        #region update training session
        public async Task<ApiMessageModelV2<TrainingSession>> RequestToUpdateTrainingSessionAsync(UpdateTrainingSessionRequest request)
        {
            var response = new ApiMessageModelV2<TrainingSession>();

            // Kiểm tra xem buổi tập có tồn tại không
            var trainingSession = await _trainingSessionRepository.GetTrainingSessionByIdAsync(request.TrainingSessionId);
            if (trainingSession == null)
            {
                response.Status = ApiResponseStatusConstant.FailedStatus;
                response.Message = TrainingSessionMessage.Error.TrainingSessionNotFound;
                return response;
            }

            // Kiểm tra xem người dùng có phải là huấn luyện viên của đội không
            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            if (!await _trainingSessionRepository.IsUserCoachOfTeamAsync(currentUserId, trainingSession.TeamId))
            {
                response.Message = TrainingSessionMessage.Error.OnlyCoachOfTeamCanUpdateTrainingSession;
                return response;
            }

            var oldUpdateRequest = await _trainingSessionRepository.IsTrainingSessionHaveRequestExistsAsync(request.TrainingSessionId);
            if (oldUpdateRequest)
            {
                response.Status = ApiResponseStatusConstant.FailedStatus;
                response.Message = TrainingSessionMessage.Error.ThisSessionAlreadyHaveAnRequest;
                return response;
            }

            // Validate data truyền vào
            var validationResult = await ValidateUpdateTrainingSessionRequestAsync(trainingSession, request);
            if (validationResult != null)
            {
                return validationResult;
            }

            // tạo yêu cầu cập nhật buổi tập
            TrainingSessionStatusChangeRequest statusChangeRequest = new TrainingSessionStatusChangeRequest
            {
                TrainingSessionId = trainingSession.TrainingSessionId,
                RequestType = TrainingSessionConstant.StatusChangeRequestType.UPDATE,
                RequestReason = request.UpdateReason,
                NewScheduledDate = request.ScheduledDate,
                NewStartTime = request.StartTime,
                NewEndTime = request.EndTime,
                NewCourtId = request.CourtId,
                RequestedAt = DateTime.Now,
                Status = TrainingSessionConstant.StatusChangeRequestStatus.PENDING,
                RequestedByCoachId = currentUserId
            };

            // Lưu thông tin yêu cầu vào cơ sở dữ liệu
            await _trainingSessionRepository.CreateRequestToChangeTrainingSessionStatus(statusChangeRequest);

            // Thông báo đến manager
            // Gửi mail thông báo cho các manager
            var managerEmails = await _trainingSessionRepository.GetTeamManagerEmailsAsync(trainingSession.TeamId);

            var createdByUser = await _userRepository.GetUserByIdAsync(currentUserId);

            var createdTrainingSession = await _trainingSessionRepository.GetTrainingSessionDetailAsync(trainingSession.TrainingSessionId);

            if (createdTrainingSession != null)
            {
                //await SendAddAdditionalSessionPendingNotificationMailAsync(createdTrainingSession, createdByUser.Username, managerEmails);
                // Trả về response thành công
                response.Status = ApiResponseStatusConstant.SuccessStatus;
                response.Message = TrainingSessionMessage.Success.RequestUpdateTrainingSessionSuccessfully;
                response.Data = new TrainingSession
                {
                    TrainingSessionId = trainingSession.TrainingSessionId,
                    TeamId = trainingSession.TeamId,
                    CourtId = trainingSession.CourtId,
                    ScheduledDate = trainingSession.ScheduledDate,
                    StartTime = trainingSession.StartTime,
                    EndTime = trainingSession.EndTime,
                    Status = trainingSession.Status
                };
            }

            return response;
        }

        public async Task<ApiResponseModel<TrainingSession>> UpdateTrainingSessionAsync(ApproveTrainingSessionRequest request)
        {
            var response = new ApiResponseModel<TrainingSession>();

            // Kiểm tra xem buổi tập có tồn tại không
            var trainingSession = await _trainingSessionRepository.GetTrainingSessionByIdAsync(request.TrainingSessionId);
            if (trainingSession == null)
            {
                response.Message = TrainingSessionMessage.Error.TrainingSessionNotFound;
                return response;
            }

            // Kiểm tra xem người dùng có phải là quản lý của đội không
            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            if (!await _trainingSessionRepository.IsUserManagerOfTeamAsync(currentUserId, trainingSession.TeamId))
            {
                response.Message = TrainingSessionMessage.Error.OnlyManagerOfTeamCanApproveUpdateTrainingSession;
                return response;
            }

            var oldTrainingSession = new TrainingSession()
            {
                ScheduledDate = trainingSession.ScheduledDate,
                StartTime = trainingSession.StartTime,
                EndTime = trainingSession.EndTime,
                Court = trainingSession.Court
            };

            // lấy request data từ db
            var changeRequest = await _trainingSessionRepository.GetSessionStatusChangeRequestByTrainingSessionIdAsync(request.TrainingSessionId);
            if (changeRequest == null)
            {
                response.Message = TrainingSessionMessage.Error.TrainingSessionRequestNotFound;
                return response;
            }

            if (changeRequest.Status != TrainingSessionConstant.StatusChangeRequestStatus.PENDING)
            {
                response.Message = TrainingSessionMessage.Error.UpdateRequestNotPending;
                return response;
            }

            if (changeRequest.RequestType != TrainingSessionConstant.StatusChangeRequestType.UPDATE)
            {
                response.Message = TrainingSessionMessage.Error.ThisRequestIsNotForUpdate;
                return response;
            }

            if (changeRequest.NewScheduledDate == null
                || changeRequest.NewCourtId == null
                || changeRequest.NewStartTime == null
                || changeRequest.NewEndTime == null)
            {
                response.Message = TrainingSessionMessage.Error.UpdateRequestDataNotFound;
                return response;
            }

            // Cập nhật thông tin buổi tập
            trainingSession.CourtId = changeRequest.NewCourtId;
            trainingSession.ScheduledDate = (DateOnly)changeRequest.NewScheduledDate;
            trainingSession.StartTime = (TimeOnly)changeRequest.NewStartTime;
            trainingSession.EndTime = (TimeOnly)changeRequest.NewEndTime;
            trainingSession.CourtPrice = request.CourtPrice;
            trainingSession.UpdatedAt = DateTime.Now;
            trainingSession.Status = TrainingSessionConstant.Status.ACTIVE;

            // Lưu thông tin vào cơ sở dữ liệu
            var updateResult = await _trainingSessionRepository.UpdateTrainingSessionAsync(trainingSession);
            if (!updateResult)
            {
                response.Status = ApiResponseStatusConstant.FailedStatus;
                response.Message = TrainingSessionMessage.Error.TrainingSessionCreationFailed;
                return response;
            }

            changeRequest.Status = TrainingSessionConstant.StatusChangeRequestStatus.APPROVED;
            changeRequest.DecisionAt = DateTime.Now;
            changeRequest.DecisionByManagerId = currentUserId;
            await _trainingSessionRepository.UpdateTrainingSessionStatusChangeRequestAsync(changeRequest);

            // Thông báo đến email của toàn đội
            var teamEmails = await _trainingSessionRepository.GetTeamEmailsAsync(trainingSession.TeamId);
            var newTrainingSession = await _trainingSessionRepository.GetTrainingSessionDetailAsync(trainingSession.TrainingSessionId);
            var updatedByName = await _userRepository.GetUserByIdAsync(currentUserId);
            var addedTrainingSession = await _trainingSessionRepository.GetTrainingSessionDetailAsync(trainingSession.TrainingSessionId);

            await SendUpdateTrainingSessionNotificationMailAsync(oldTrainingSession, newTrainingSession, updatedByName.Username, teamEmails);

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = TrainingSessionMessage.Success.TrainingSessionUpdatedSuccessfully;

            return response;
        }

        private async Task<ApiMessageModelV2<TrainingSession>?> ValidateUpdateTrainingSessionRequestAsync(TrainingSession trainingSession, UpdateTrainingSessionRequest request)
        {
            var errors = new Dictionary<string, string>();
            var trainingSessionSettingsSection = _configuration.GetSection("TrainingSessionSettings");

            // Kiểm tra thời gian tối thiểu của một buổi tập
            var minimumDurationOfASession = Double.Parse(trainingSessionSettingsSection["MinimumDurationOfASession"] ?? "0.5");
            if (request.EndTime < request.StartTime.AddHours(minimumDurationOfASession))
            {
                errors.Add(nameof(request.EndTime), TrainingSessionMessage.Error.MinimumDurationNotMet(minimumDurationOfASession));
            }

            // Kiểm tra thời gian tối thiểu bắt đầu buổi tập trước khi tạo lịch
            var scheduledDateTime = request.ScheduledDate.ToDateTime(request.StartTime);
            var minimumAdvanceScheduling = Double.Parse(trainingSessionSettingsSection["MinimumAdvanceScheduling"] ?? "12");
            if (scheduledDateTime < DateTime.Now.AddHours(minimumAdvanceScheduling))
            {
                errors.Add(nameof(request.ScheduledDate), TrainingSessionMessage.Error.MinimumAdvanceSchedulingRequired(minimumAdvanceScheduling));
                return ErrorResponse(errors, TrainingSessionMessage.Error.TrainingSessionUpdateFailed);
            }

            // Kiểm tra thời gian tối thiểu để thay đổi lịch tập
            var oldScheduledDateTime = trainingSession.ScheduledDate.ToDateTime(trainingSession.StartTime);
            var minimumTimeToChangeSchedule = Double.Parse(trainingSessionSettingsSection["MinimumTimeToChangeSchedule"] ?? "12");
            if (oldScheduledDateTime < DateTime.Now.AddHours(minimumTimeToChangeSchedule))
            {
                errors.Add(nameof(request.ScheduledDate), TrainingSessionMessage.Error.MinimumUpdateSchedulingRequired(minimumTimeToChangeSchedule));
            }

            // Kiểm tra xem thông tin cần cập nhật có giống với thông tin cũ không
            if (request.ScheduledDate == trainingSession.ScheduledDate
                && request.StartTime == trainingSession.StartTime
                && request.EndTime == trainingSession.EndTime
                && request.CourtId == trainingSession.CourtId)
            {
                errors.Add(nameof(request.TrainingSessionId), TrainingSessionMessage.Error.NoChangesMade);
            }

            // Kiểm tra xem sân có tồn tại không
            if (!await _trainingSessionRepository.IsValidCourtAsync(request.CourtId))
            {
                errors.Add(nameof(request.CourtId), TrainingSessionMessage.Error.CourtDoesNotExist);
            }

            // Kiểm tra xem người dùng có phải là huấn luyện viên của đội không
            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            if (!await _trainingSessionRepository.IsUserCoachOfTeamAsync(currentUserId, trainingSession.TeamId))
            {
                errors.Add(nameof(trainingSession.TeamId), TrainingSessionMessage.Error.OnlyCoachOfTeamCanCreateTrainingSession);
            }

            // Kiểm tra xem sân có khả dụng không
            var isCourtAvailable = await _trainingSessionRepository.IsCourtAvailableAsync(trainingSession.TrainingSessionId, request.CourtId, request.ScheduledDate, request.StartTime, request.EndTime);
            if (!isCourtAvailable)
            {
                var conflictingSession = await _trainingSessionRepository.GetConflictingTrainingSessionByCourtAndTimeAsync(request.CourtId, request.ScheduledDate, request.StartTime, request.EndTime);
                if (conflictingSession != null && conflictingSession.Status == 1)
                {
                    errors.Add(nameof(request.CourtId), TrainingSessionMessage.Error.CourtUnavailable);
                }
            }

            // Kiểm tra xem đội có khả dụng không vào thời gian đã chọn không
            if (!await _trainingSessionRepository.IsTeamAvailableAsync(trainingSession.TrainingSessionId, trainingSession.TeamId, request.ScheduledDate, request.StartTime, request.EndTime))
            {
                errors.Add(nameof(trainingSession.TeamId), TrainingSessionMessage.Error.TrainingSessionConflict);
            }

            // Kiểm tra xem có lỗi nào không
            if (errors.Any())
            {
                return ErrorResponse(errors, TrainingSessionMessage.Error.TrainingSessionUpdateFailed);
            }

            return null;
        }

        private async Task SendUpdateTrainingSessionNotificationMailAsync(TrainingSession oldTrainingSession
            , TrainingSession newTrainingSession
            , string updatedByName
            , List<string> teamEmails)
        {
            var mailTemplate = await _mailTemplateRepository.GetByIdAsync(MailTemplateConstant.UpdateTrainingSession);
            if (mailTemplate != null)
            {
                var cultureInfo = new CultureInfo("vi-VN");
                mailTemplate.Content = mailTemplate.Content
                    .Replace("{{TEAM_NAME}}", newTrainingSession.Team.TeamName)
                    .Replace("{{OLD_DATE}}", oldTrainingSession.ScheduledDate.ToString("dddd, dd/MM/yyyy", cultureInfo))
                    .Replace("{{OLD_START_TIME}}", oldTrainingSession.StartTime.ToString("HH:mm"))
                    .Replace("{{OLD_END_TIME}}", oldTrainingSession.EndTime.ToString("HH:mm"))
                    .Replace("{{NEW_DATE}}", newTrainingSession.ScheduledDate.ToString("dddd, dd/MM/yyyy", cultureInfo))
                    .Replace("{{NEW_START_TIME}}", newTrainingSession.StartTime.ToString("HH:mm"))
                    .Replace("{{NEW_END_TIME}}", newTrainingSession.EndTime.ToString("HH:mm"))
                    .Replace("{{NEW_COURT}}", newTrainingSession.Court.CourtName)
                    .Replace("{{UPDATED_BY}}", updatedByName);

                var t = new Thread(() => _emailService.SendEmailMultiThread($"{mailTemplate.TemplateTitle} {oldTrainingSession.ScheduledDate.ToString("dd/MM/yyyy")}", mailTemplate.Content, teamEmails));
                t.Start();
            }
        }


        public async Task<ApiResponseModel<TrainingSessionDto>> RejectUpdateTrainingSessionRequestAsync(CancelTrainingSessionChangeStatusRequest request)
        {
            var response = new ApiResponseModel<TrainingSessionDto>
            {
                Message = TrainingSessionMessage.Error.RejectUpdateTrainingSessionFailed
            };
            // Kiểm tra xem buổi tập có tồn tại không
            var trainingSession = await _trainingSessionRepository.GetTrainingSessionDetailAsync(request.TrainingSessionId);
            if (trainingSession == null)
            {
                response.Errors = [TrainingSessionMessage.Error.TrainingSessionNotFound];
                return response;
            }
            // Kiểm tra xem người dùng có phải là huấn luyện viên của đội không
            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            if (!await _trainingSessionRepository.IsUserManagerOfTeamAsync(currentUserId, trainingSession.TeamId))
            {
                response.Errors = [TrainingSessionMessage.Error.OnlyManagerOfTeamCanRejectUpdateRequest];
                return response;
            }
            // Kiểm tra xem có yêu cầu cập nhật hay không
            var trainingSessionStatusChangeRequest = await _trainingSessionRepository.GetSessionStatusChangeRequestByTrainingSessionIdAsync(request.TrainingSessionId);
            if (trainingSessionStatusChangeRequest == null)
            {
                response.Errors = [TrainingSessionMessage.Error.UpdateRequestNotFound];
                return response;
            }
            if (trainingSessionStatusChangeRequest.Status != TrainingSessionConstant.StatusChangeRequestStatus.PENDING)
            {
                response.Errors = [TrainingSessionMessage.Error.UpdateRequestNotPending];
                return response;
            }
            if (trainingSessionStatusChangeRequest.RequestType != TrainingSessionConstant.StatusChangeRequestType.UPDATE)
            {
                response.Errors = [TrainingSessionMessage.Error.ThisRequestIsNotForUpdate];
                return response;
            }

            // Cập nhật yêu cầu huỷ buổi tập
            trainingSessionStatusChangeRequest.Status = TrainingSessionConstant.StatusChangeRequestStatus.REJECTED;
            trainingSessionStatusChangeRequest.DecisionAt = DateTime.Now;
            trainingSessionStatusChangeRequest.DecisionByManagerId = currentUserId;
            trainingSessionStatusChangeRequest.RejectedReason = request.Reason;
            await _trainingSessionRepository.UpdateTrainingSessionStatusChangeRequestAsync(trainingSessionStatusChangeRequest);

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = TrainingSessionMessage.Success.RejectUpdateTrainingSessionRequestSuccessfully;
            response.Data = new TrainingSessionDto
            {
                TrainingSessionId = trainingSession.TrainingSessionId,
                TeamId = trainingSession.TeamId,
                CourtId = trainingSession.CourtId,
                ScheduledDate = trainingSession.ScheduledDate,
                ScheduledStartTime = trainingSession.StartTime,
                ScheduledEndTime = trainingSession.EndTime
            };

            // Gửi mail thông báo tới người yêu cầu huỷ
            var createdUser = await _userRepository.GetUserByIdAsync(trainingSessionStatusChangeRequest.RequestedByCoachId);
            var rejectedByUser = await _userRepository.GetUserByIdAsync(currentUserId);

            await SendRejectedUpdateSessionNotificationMailAsync(trainingSession, rejectedByUser.Username, createdUser);

            return response;
        }

        private async Task SendRejectedUpdateSessionNotificationMailAsync(TrainingSession trainingSession, string rejectedByName, User createdBy)
        {
            var mailTemplate = await _mailTemplateRepository.GetByIdAsync(MailTemplateConstant.RejectUpdateTrainingSessionRequest);
            if (mailTemplate != null)
            {
                var cultureInfo = new CultureInfo("vi-VN");
                mailTemplate.Content = mailTemplate.Content
                    .Replace("{{COACH_NAME}}", createdBy.Username)
                    .Replace("{{DATE}}", trainingSession.ScheduledDate.ToString("dddd, dd/MM/yyyy", cultureInfo))
                    .Replace("{{START_TIME}}", trainingSession.StartTime.ToString("HH:mm"))
                    .Replace("{{END_TIME}}", trainingSession.EndTime.ToString("HH:mm"))
                    .Replace("{{COURT_NAME}}", trainingSession.Court.CourtName)
                    .Replace("{{REASON}}", trainingSession.CreateRejectedReason)
                    .Replace("{{REJECTED_NAME}}", rejectedByName);

                var receiverEmails = new List<string>();
                if (createdBy.Email != null)
                {
                    receiverEmails.Add(createdBy.Email);
                    var t = new Thread(() => _emailService.SendEmailMultiThread($"{(mailTemplate.TemplateTitle).Replace("{{DATE}}", trainingSession.ScheduledDate.ToString("dd/MM/yyyy"))}", mailTemplate.Content, receiverEmails));
                    t.Start();
                }
            }
        }

        #endregion

        public async Task<ApiResponseModel<List<CourtDto>>> GetAvailableCourtsAsync(GetAvailableCourtsRequest request)
        {
            var response = new ApiResponseModel<List<CourtDto>>();
            var dates = GetDatesForDayOfWeek(request.StartDate, request.EndDate, request.DayOfWeek);

            var availableCourts = await _trainingSessionRepository.GetAvailableCourtsAsync(dates, request.StartTime, request.EndTime);

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = ApiResponseSuccessMessage.ApiSuccessMessage;
            response.Data = availableCourts.Select(c => new CourtDto
            {
                CourtId = c.CourtId,
                CourtName = c.CourtName,
                RentPricePerHour = c.RentPricePerHour,
                Address = c.Address,
                Contact = c.Contact,
                Status = c.Status,
                Type = c.Type,
                Kind = c.Kind,
                ImageUrl = c.ImageUrl,
                UsagePurpose = c.UsagePurpose
            }).ToList();

            return response;
        }

        private List<DateOnly> GetDatesForDayOfWeek(DateOnly startDate, DateOnly endDate, DayOfWeek? dayOfWeek)
        {
            var dates = new List<DateOnly>();
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (dayOfWeek == null)
                {
                    dates.Add(date);
                    continue;
                }

                if (date.DayOfWeek == dayOfWeek)
                {
                    dates.Add(date);
                }
            }
            return dates;
        }

        public async Task<ApiMessageModelV2<GenerateTrainingSessionsResponse>> GenerateTrainingSessionsAsync(GenerateTrainingSessionsRequest request)
        {
            // Khởi tạo response model
            var response = new ApiMessageModelV2<GenerateTrainingSessionsResponse>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Data = new GenerateTrainingSessionsResponse
                {
                    SuccessfulSessions = new List<TrainingSessionInADayResponseDto>(),
                    FailedSessions = new List<TrainingSessionInADayResponseDto>()
                },
                Errors = new Dictionary<string, string>()
            };

            // Kiểm tra data đầu vào
            // Kiểm tra xem ngày bắt đầu có nhỏ hơn ngày hiện tại không
            if (request.StartDate <= DateOnly.FromDateTime(DateTime.Now))
            {
                response.Status = ApiResponseStatusConstant.FailedStatus;
                response.Message = TrainingSessionMessage.Error.InvalidStartDate;
                response.Errors.Add(nameof(request.StartDate), TrainingSessionMessage.Error.InvalidStartDate);
                return response;
            }

            // Kiểm tra xem ngày bắt đầu có lớn hơn ngày kết thúc không
            if (request.StartDate > request.EndDate)
            {
                response.Status = ApiResponseStatusConstant.FailedStatus;
                response.Message = TrainingSessionMessage.Error.InvalidRangeOfDate;
                response.Errors.Add(nameof(request.EndDate), TrainingSessionMessage.Error.InvalidRangeOfDate);
                return response;
            }

            // Kiểm tra xem người dùng có phải là huấn luyện viên của đội không
            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            if (!await _trainingSessionRepository.IsUserCoachOfTeamAsync(currentUserId, request.TeamId))
            {
                response.Status = ApiResponseStatusConstant.FailedStatus;
                response.Errors.Add(nameof(currentUserId), TrainingSessionMessage.Error.OnlyCoachOfTeamCanCreateTrainingSession);
                return response;
            }

            // Duyệt qua từng chi tiết buổi tập
            foreach (var detail in request.TrainingSessionInADayOfWeekDetails)
            {
                // Kiểm tra xem sân có tồn tại không
                var court = await _courtRepository.GetCourtById(detail.CourtId);
                if (court == null)
                {
                    response.Errors.Add(nameof(detail.CourtId), TrainingSessionMessage.Error.CourtDoesNotExist);
                    continue;
                }

                // Lấy ra các ngày trong tuần mà người dùng muốn tạo buổi tập
                var scheduledDates = GetDatesForDayOfWeek(request.StartDate, request.EndDate, detail.DayOfWeek);
                // Duyệt qua từng ngày
                foreach (var date in scheduledDates)
                {
                    // Kiểm tra xem đội đã có buổi tập vào thời gian đã chọn chưa
                    var teamTrainingSessionInThisTime = await _trainingSessionRepository.GetConflictingTrainingSessionOfTeamInDateTime(request.TeamId, date, detail.StartTime, detail.EndTime);
                    if (teamTrainingSessionInThisTime != null)
                    {
                        // Thêm buổi tập vào danh sách buổi tập thất bại
                        response.Data.FailedSessions.Add(new TrainingSessionInADayResponseDto
                        {
                            SelectedCourtId = detail.CourtId,
                            ScheduledDate = date,
                            DayOfWeek = date.DayOfWeek,
                            StartTime = detail.StartTime,
                            EndTime = detail.EndTime
                        });

                        // Thêm lỗi vào danh sách lỗi
                        response.Errors.Add(date.ToString("yyyy-MM-dd"), TrainingSessionMessage.Error
                                .Unavailable(request.TeamId
                                , court.CourtName
                                , date
                                , detail.StartTime
                                , detail.EndTime
                                , teamTrainingSessionInThisTime.Team));

                        // Bỏ qua buổi tập này
                        continue;
                    }

                    // Kiểm tra xem sân có khả dụng không
                    var isCourtAvailable = await _trainingSessionRepository.IsCourtAvailableAsync(detail.CourtId, date, detail.StartTime, detail.EndTime);
                    if (isCourtAvailable)
                    {
                        var trainingSession = new TrainingSession
                        {
                            TrainingSessionId = Guid.NewGuid().ToString(),
                            TeamId = request.TeamId,
                            CourtId = detail.CourtId,
                            ScheduledDate = date,
                            StartTime = detail.StartTime,
                            EndTime = detail.EndTime,
                            CreatedAt = DateTime.Now,
                            CreatedByUserId = currentUserId,
                            Status = TrainingSessionConstant.Status.PENDING
                        };

                        // Thêm buổi tập vào cơ sở dữ liệu
                        await _trainingSessionRepository.AddAsync(trainingSession);

                        // Thêm buổi tập vào danh sách buổi tập thành công
                        response.Data.SuccessfulSessions.Add(new TrainingSessionInADayResponseDto
                        {
                            SelectedCourtId = trainingSession.CourtId,
                            ScheduledDate = trainingSession.ScheduledDate,
                            DayOfWeek = date.DayOfWeek,
                            StartTime = trainingSession.StartTime,
                            EndTime = trainingSession.EndTime
                        });
                    }
                    else
                    {

                        // Thêm buổi tập vào danh sách buổi tập thất bại
                        response.Data.FailedSessions.Add(new TrainingSessionInADayResponseDto
                        {
                            SelectedCourtId = detail.CourtId,
                            ScheduledDate = date,
                            DayOfWeek = date.DayOfWeek,
                            StartTime = detail.StartTime,
                            EndTime = detail.EndTime
                        });

                        // Thêm lỗi vào danh sách lỗi
                        var conflictingSession = await _trainingSessionRepository
                            .GetConflictingTrainingSessionByCourtAndTimeAsync(detail.CourtId, date, detail.StartTime, detail.EndTime);
                        if (conflictingSession != null)
                        {
                            response.Errors.Add(date.ToString("yyyy-MM-dd"), TrainingSessionMessage.Error
                                .Unavailable(request.TeamId
                                , court.CourtName
                                , date
                                , conflictingSession.StartTime
                                , conflictingSession.EndTime
                                , conflictingSession.Team));
                        }
                    }
                }

                // Sắp xếp danh sách buổi tập thành công và thất bại theo thời gian
                response.Data.SuccessfulSessions = response.Data.SuccessfulSessions
                    .OrderBy(ts => ts.ScheduledDate)
                    .ThenBy(ts => ts.StartTime)
                    .ToList();
                response.Data.FailedSessions = response.Data.FailedSessions.OrderBy(ts => ts.ScheduledDate)
                    .ThenBy(ts => ts.StartTime)
                    .ToList();
            }

            await _cancelCreateTrainingSessionRequestSchedulerService.SyncPendingRequestsFromDatabaseAsync();
            return response;
        }

        public async Task<ApiMessageModelV2<GenerateTrainingSessionsResponse>> BulkCreateTrainingSessionsAsync(List<CreateTrainingSessionRequest> requests)
        {
            var response = new ApiMessageModelV2<GenerateTrainingSessionsResponse>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Data = new GenerateTrainingSessionsResponse
                {
                    SuccessfulSessions = new List<TrainingSessionInADayResponseDto>(),
                    FailedSessions = new List<TrainingSessionInADayResponseDto>()
                },
                Errors = new Dictionary<string, string>()
            };

            // Duyệt qua từng create request
            foreach (var request in requests)
            {
                // Validate data truyền vào
                var validationResult = await ValidateCreateTrainingSessionRequestAsync(request);
                if (validationResult != null)
                {
                    // Nếu có lỗi thì thêm vào danh sách buổi tập thất bại
                    response.Data.FailedSessions.Add(new TrainingSessionInADayResponseDto
                    {
                        SelectedCourtId = request.CourtId,
                        ScheduledDate = request.ScheduledDate,
                        DayOfWeek = request.ScheduledDate.DayOfWeek,
                        StartTime = request.StartTime,
                        EndTime = request.EndTime
                    });
                    response.Errors = validationResult.Errors;
                    continue;
                }

                // Kiểm tra xem người dùng có phải là huấn luyện viên của đội không
                var currentUserId = GetCurrentUserId(_httpContextAccessor);
                if (!await _trainingSessionRepository.IsUserCoachOfTeamAsync(currentUserId, request.TeamId))
                {
                    // Nếu không phải là huấn luyện viên thì thêm vào danh sách buổi tập thất bại và bỏ qua buổi tâp này
                    response.Data.FailedSessions.Add(new TrainingSessionInADayResponseDto
                    {
                        SelectedCourtId = request.CourtId,
                        ScheduledDate = request.ScheduledDate,
                        DayOfWeek = request.ScheduledDate.DayOfWeek,
                        StartTime = request.StartTime,
                        EndTime = request.EndTime
                    });
                    response.Errors?.Add(request.ScheduledDate.ToString("yyyy-MM-dd"), TrainingSessionMessage.Error.OnlyCoachOfTeamCanCreateTrainingSession);
                    continue;
                }

                // Tạo buổi tập
                var trainingSession = new TrainingSession
                {
                    TrainingSessionId = Guid.NewGuid().ToString(),
                    TeamId = request.TeamId,
                    CourtId = request.CourtId,
                    ScheduledDate = request.ScheduledDate,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    Status = TrainingSessionConstant.Status.PENDING,
                    CreatedAt = DateTime.Now,
                    CreatedByUserId = currentUserId
                };

                // Thêm buổi tập vào cơ sở dữ liệu
                await _trainingSessionRepository.AddTrainingSessionAsync(trainingSession);

                // Thêm buổi tập vào danh sách buổi tập thành công
                response.Data.SuccessfulSessions.Add(new TrainingSessionInADayResponseDto
                {
                    SelectedCourtId = trainingSession.CourtId,
                    ScheduledDate = trainingSession.ScheduledDate,
                    DayOfWeek = trainingSession.ScheduledDate.DayOfWeek,
                    StartTime = trainingSession.StartTime,
                    EndTime = trainingSession.EndTime
                });
            }

            await _cancelCreateTrainingSessionRequestSchedulerService.SyncPendingRequestsFromDatabaseAsync();
            return response;
        }

        
    }
}
