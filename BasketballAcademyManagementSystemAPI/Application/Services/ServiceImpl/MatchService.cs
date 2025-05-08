using BasketballAcademyManagementSystemAPI.Application.DTOs.Match;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Application.DTOs.CourtManagement;
using static BasketballAcademyManagementSystemAPI.Common.Messages.ApiResponseMessage;
using static BasketballAcademyManagementSystemAPI.Common.Messages.AuthenticationMessage;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ICourtRepository _courtRepository;
        private readonly IMatchLineupRepository _matchLineupRepository;
        private readonly ICoachRepository _coachRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public MatchService(IMatchRepository matchRepository
            , ITeamRepository teamRepository
            , ICourtRepository courtRepository
            , IMatchLineupRepository matchLineupRepository
            , ICoachRepository coachRepository
            , IHttpContextAccessor httpContextAccessor
            , IConfiguration configuration)
        {
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
            _courtRepository = courtRepository;
            _matchLineupRepository = matchLineupRepository;
            _coachRepository = coachRepository;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        private static string GetCurrentUserId(IHttpContextAccessor _httpContextAccessor)
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

        public async Task<ApiResponseModel<List<MatchDetailDto>>> GetMatchesInAWeekAsync(DateTime startDate, DateTime endDate, string? teamId, string? courtId)
        {
            var response = new ApiResponseModel<List<MatchDetailDto>>();
            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            var userRole = await _matchRepository.GetUserRoleAsync(currentUserId);

            List<Match> matches;

            // Retrieve matches based on user role
            switch (userRole)
            {
                case RoleCodeConstant.PlayerCode:
                    matches = await _matchRepository.GetMatchesByPlayerAsync(currentUserId, startDate, endDate);
                    break;
                case RoleCodeConstant.ManagerCode:
                    matches = await _matchRepository.GetMatchesByManagerAsync(currentUserId, startDate, endDate);
                    break;
                case RoleCodeConstant.ParentCode:
                    matches = await _matchRepository.GetMatchesByParentAsync(currentUserId, startDate, endDate);
                    break;
                case RoleCodeConstant.CoachCode:
                    matches = await _matchRepository.GetMatchesByCoachAsync(currentUserId, startDate, endDate, teamId, courtId);
                    break;
                case RoleCodeConstant.PresidentCode:
                    matches = await _matchRepository.GetMatchesByCoachAsync(currentUserId, startDate, endDate, teamId, courtId);
                    break;
                default:
                    response.Status = ApiResponseStatusConstant.FailedStatus;
                    response.Message = AuthenticationErrorMessage.InvalidCurrentRole;
                    return response;
            }

            var matchSettingsSection = _configuration.GetSection(MatchConstant.Setting.MatchSettingsSection);
            var minimumHourDurationOfAMatch = Double.Parse(matchSettingsSection[MatchConstant.Setting.MinimumHourDurationOfAMatch] ?? "1");
            var matchDtos = matches.Select(match => new MatchDetailDto
            {
                MatchId = match.MatchId,
                MatchName = match.MatchName,
                ScheduledDate = DateOnly.FromDateTime(match.MatchDate),
                ScheduledStartTime = TimeOnly.FromDateTime(match.MatchDate),
                ScheduledEndTime = TimeOnly.FromDateTime(match.MatchDate.AddHours(minimumHourDurationOfAMatch)),
                HomeTeamId = match.HomeTeamId,
                HomeTeamName = match.HomeTeamId == null ? match.OpponentTeamName : match.HomeTeam?.TeamName,
                AwayTeamId = match.AwayTeamId,
                AwayTeamName = match.AwayTeamId == null ? match.OpponentTeamName : match.AwayTeam?.TeamName,
                ScoreHome = match.ScoreHome,
                ScoreAway = match.ScoreAway,
                Status = MatchConstant.Status.GetStatusName(match.Status),
                CourtId = match.CourtId,
                CourtName = match.Court?.CourtName ?? "Không xác định",
                CourtAddress = match.Court?.Address ?? "Không xác định",
                CreatedByCoachId = match.CreatedByCoachId
            }).ToList();

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = ApiResponseSuccessMessage.ApiSuccessMessage;
            response.Data = matchDtos.OrderBy(m => m.ScheduledDate).ThenBy(m => m.ScheduledStartTime).ToList();

            return response;
        }

        // create match
        public async Task<ApiMessageModelV2<CreateMatchRequest>> CreateMatchAsync(CreateMatchRequest request)
        {
            var response = new ApiMessageModelV2<CreateMatchRequest>();

            var validationResult = await ValidateCreateMatchRequestAsync(request);
            if (validationResult != null)
            {
                return validationResult;
            }

            // Create Match
            var match = new Match
            {
                MatchName = request.MatchName ?? "GIAO HỮU",
                MatchDate = request.MatchDate,
                HomeTeamId = request.HomeTeamId,
                AwayTeamId = request.AwayTeamId,
                OpponentTeamName = string.IsNullOrEmpty(request.HomeTeamId) || string.IsNullOrEmpty(request.AwayTeamId) ? request.OpponentTeamName : null,
                CourtId = request.CourtId,
                CreatedByCoachId = GetCurrentUserId(_httpContextAccessor)
            };

            await _matchRepository.AddMatchAsync(match);

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = MatchMessage.Success.MatchCreated;
            response.Data = request;

            return response;
        }

        public async Task<ApiMessageModelV2<CreateMatchRequest>?> ValidateCreateMatchRequestAsync(CreateMatchRequest request)
        {
            var errors = new Dictionary<string, string>();
            var matchSettingsSection = _configuration.GetSection(MatchConstant.Setting.MatchSettingsSection);

            // Validate MatchName
            if (string.IsNullOrEmpty(request.MatchName))
            {
                errors.Add(nameof(request.MatchName), MatchMessage.Error.MatchNameRequired);
            }

            // Validate MatchDate
            var minimumAdvanceScheduling = Double.Parse(matchSettingsSection[MatchConstant.Setting.MinimumAdvanceScheduling] ?? "24");
            if (request.MatchDate <= DateTime.Now.AddHours(minimumAdvanceScheduling))
            {
                errors.Add(nameof(request.MatchDate), MatchMessage.Error.MatchDateTooSoon(minimumAdvanceScheduling));
            }

            // Check court exist
            var minimumHourDurationOfAMatch = Double.Parse(matchSettingsSection[MatchConstant.Setting.MinimumHourDurationOfAMatch] ?? "1");
            var matchEndTime = request.MatchDate.AddHours(minimumHourDurationOfAMatch);
            if (string.IsNullOrEmpty(request.CourtId))
            {
                errors.Add(nameof(request.CourtId), MatchMessage.Error.CourtRequired);
            }
            else
            {
                if (!await _courtRepository.CourtExistsAsync(request.CourtId))
                {
                    errors.Add(nameof(request.CourtId), MatchMessage.Error.CourtDoesNotExist);
                } // Check court availability
                else if (!await _matchRepository.IsCourtAvailableAsync(request.CourtId, request.MatchDate, matchEndTime)) 
                {
                    errors.Add(nameof(request.CourtId), MatchMessage.Error.CourtUnavailable);
                }
            }

            // Validate HomeTeamId and AwayTeamId
            if (string.IsNullOrEmpty(request.HomeTeamId) && string.IsNullOrEmpty(request.AwayTeamId))
            {
                errors.Add(MatchConstant.AdditionalErrorField.BothTeamId, MatchMessage.Error.HomeTeamIdOrAwayTeamIdRequired);
            }
            else
            {
                if (request.HomeTeamId == request.AwayTeamId)
                {
                    errors.Add(MatchConstant.AdditionalErrorField.BothTeamId, MatchMessage.Error.HomeTeamIdAndAwayTeamIdCannotBeTheSame);
                }

                // Check is coach of home team or away team
                var currentUserId = GetCurrentUserId(_httpContextAccessor);
                var currentCoach = await _coachRepository.GetACoachByUserIdAsync(currentUserId);
                if (currentCoach == null || (currentCoach.TeamId != request.HomeTeamId && currentCoach.TeamId != request.AwayTeamId))
                {
                    errors.Add(nameof(currentUserId), MatchMessage.Error.OnlyTeamCoachCanCreateMatch);
                }
            }

            // Validate OpponentTeamName
            if (string.IsNullOrEmpty(request.HomeTeamId) || string.IsNullOrEmpty(request.AwayTeamId))
            {
                if (string.IsNullOrEmpty(request.OpponentTeamName))
                {
                    errors.Add(nameof(request.OpponentTeamName), MatchMessage.Error.OpponentTeamNameCannotBeEmpty);
                }
            }

            // Check team exist
            if (!string.IsNullOrEmpty(request.HomeTeamId) && !await _teamRepository.TeamExistsAsync(request.HomeTeamId))
            {
                errors.Add(nameof(request.HomeTeamId), MatchMessage.Error.HomeTeamIdDoesNotExist);
            }
            else
            {
                if (request.HomeTeamId != null)
                {
                    // Check is team active
                    var homeTeam = await _teamRepository.GetTeamByIdAsync(request.HomeTeamId);
                    if (homeTeam == null || homeTeam.Status != TeamStatusConstant.ACTIVE)
                    {
                        errors.Add(nameof(request.HomeTeamId), MatchMessage.Error.TeamNotActive);
                    }
                    else
                    // Check team availability
                    if (!await _matchRepository.IsTeamAvailableAsync(request.HomeTeamId, request.MatchDate, matchEndTime))
                    {
                        errors.Add(nameof(request.HomeTeamId), MatchMessage.Error.HomeTeamUnavailable);
                    }
                }
            }

            if (!string.IsNullOrEmpty(request.AwayTeamId) && !await _teamRepository.TeamExistsAsync(request.AwayTeamId))
            {
                errors.Add(nameof(request.AwayTeamId), MatchMessage.Error.AwayTeamIdDoesNotExist);
            }
            else
            {
                if (request.AwayTeamId != null)
                {
                    // Check is team active
                    var awayTeam = await _teamRepository.GetTeamByIdAsync(request.AwayTeamId);
                    if (awayTeam == null || awayTeam.Status != TeamStatusConstant.ACTIVE)
                    {
                        errors.Add(nameof(request.AwayTeamId), MatchMessage.Error.TeamNotActive);
                    }
                    else
                    // Check team availability
                    if (!await _matchRepository.IsTeamAvailableAsync(request.AwayTeamId, request.MatchDate, matchEndTime))
                    {
                        errors.Add(nameof(request.AwayTeamId), MatchMessage.Error.AwayTeamUnavailable);
                    }
                }
            }

            if (errors.Any())
            {
                return new ApiMessageModelV2<CreateMatchRequest>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = MatchMessage.Error.CreateMatchFailed,
                    Errors = errors
                };
            }

            return null;
        }


        public async Task<ApiMessageModelV2<MatchDetailDto>> UpdateMatchAsync(int matchId, UpdateMatchRequest request)
        {
            var response = new ApiMessageModelV2<MatchDetailDto>()
            {
                Status = ApiResponseStatusConstant.FailedStatus,
                Message = MatchMessage.Error.MatchUpdateFailed,
                Errors = new Dictionary<string, string>()
            };

            // Check if the match exists
            var match = await _matchRepository.GetMatchByIdAsync(matchId);
            if (match == null)
            {
                response.Errors.Add(nameof(matchId), MatchMessage.Error.MatchNotFound);
                return response;
            }

            // Check if the coach of home team or away team can update the match
            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            var currentCoach = await _coachRepository.GetACoachByUserIdAsync(currentUserId);
            if (currentCoach == null || (currentCoach.TeamId != match.HomeTeamId && currentCoach.TeamId != match.AwayTeamId))
            {
                response.Errors.Add(nameof(currentUserId), MatchMessage.Error.OnlyTeamCoachCanUpdateMatch);
                return response;
            }

            // Validate the update request
            var validationResult = await ValidateUpdateMatchRequestAsync(match, request);
            if (validationResult != null)
            {
                return validationResult;
            }

            // Update match
            if (match.Status == MatchConstant.Status.UPCOMING)
            {
                if (request.MatchName != null)
                {
                    match.MatchName = request.MatchName;
                }

                if (request.MatchDate != match.MatchDate)
                {
                    match.MatchDate = request.MatchDate;
                }

                if (request.HomeTeamId != null)
                {
                    match.HomeTeamId = request.HomeTeamId;
                }

                if (request.AwayTeamId != null)
                {
                    match.AwayTeamId = request.AwayTeamId;

                }
                if (request.CourtId != null)
                {
                    match.CourtId = request.CourtId;
                }
            }
            if (match.Status == MatchConstant.Status.FINISHED)
            {
                if (request.HomeTeamScore != null)
                {
                    match.ScoreHome = (int)request.HomeTeamScore;
                }

                if (request.AwayTeamScore != null)
                {
                    match.ScoreAway = (int)request.AwayTeamScore;
                }
            }

            // If the home team or away team is not selected, the opponent team name is required
            if (request.HomeTeamId == null || request.AwayTeamId == null)
            {
                match.OpponentTeamName = request.OpponentTeamName;
            }
            else
            {
                match.OpponentTeamName = null;
            }
             
            // Save changes to the database
            var updateResult = await _matchRepository.UpdateMatchAsync(match);
            if (!updateResult)
            {
                return response;
            }

            var matchSettingsSection = _configuration.GetSection(MatchConstant.Setting.MatchSettingsSection);
            var minimumHourDurationOfAMatch = Double.Parse(matchSettingsSection[MatchConstant.Setting.MinimumHourDurationOfAMatch] ?? "1");
            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = MatchMessage.Success.MatchUpdatedSuccessfully;
            var newMatchInf = await _matchRepository.GetMatchByIdAsync(matchId);
            if(newMatchInf != null)
            {
                response.Data = new MatchDetailDto
                {
                    MatchId = newMatchInf.MatchId,
                    MatchName = newMatchInf.MatchName,
                    ScheduledDate = DateOnly.FromDateTime(newMatchInf.MatchDate),
                    ScheduledStartTime = TimeOnly.FromDateTime(newMatchInf.MatchDate),
                    ScheduledEndTime = TimeOnly.FromDateTime(newMatchInf.MatchDate.AddHours(minimumHourDurationOfAMatch)),
                    HomeTeamId = newMatchInf.HomeTeamId,
                    HomeTeamName = newMatchInf.HomeTeamId == null ? newMatchInf.OpponentTeamName : newMatchInf.HomeTeam?.TeamName,
                    ScoreHome = newMatchInf.ScoreHome,
                    AwayTeamId = newMatchInf.AwayTeamId,
                    AwayTeamName = newMatchInf.AwayTeamId == null ? newMatchInf.OpponentTeamName : newMatchInf.AwayTeam?.TeamName,
                    ScoreAway = newMatchInf.ScoreAway,
                    Status = MatchConstant.Status.GetStatusName(newMatchInf.Status),
                    CourtId = newMatchInf.CourtId,
                    CourtName = newMatchInf.Court.CourtName,
                    CourtAddress = newMatchInf.Court.Address,
                    CreatedByCoachId = newMatchInf.CreatedByCoachId
                };
            }
            return response;
        }

        private async Task<ApiMessageModelV2<MatchDetailDto>?> ValidateUpdateMatchRequestAsync(Match match, UpdateMatchRequest request)
        {
            var errors = new Dictionary<string, string>();


            // Validate Match score
            if ((request.HomeTeamScore != null || request.AwayTeamScore != null) && match.Status == MatchConstant.Status.FINISHED)
            {
                if (request.HomeTeamScore < 0 || request.AwayTeamScore < 0)
                {
                    errors.Add(nameof(request.HomeTeamScore), MatchMessage.Error.InvalidMatchScore);
                    return new ApiMessageModelV2<MatchDetailDto>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = MatchMessage.Error.MatchUpdateFailed,
                        Errors = errors
                    };
                }
                return null;
            }

            var matchSettingsSection = _configuration.GetSection(MatchConstant.Setting.MatchSettingsSection);

            // Validate MatchName
            if (string.IsNullOrEmpty(request.MatchName))
            {
                errors.Add(nameof(request.MatchName), MatchMessage.Error.MatchNameRequired);
            }

            // Validate MatchDate
            var minimumAdvanceScheduling = Double.Parse(matchSettingsSection[MatchConstant.Setting.MinimumAdvanceScheduling] ?? "24");
            if (request.MatchDate <= DateTime.Now.AddHours(minimumAdvanceScheduling) && match.Status != MatchConstant.Status.FINISHED)
            {
                errors.Add(nameof(request.MatchDate), MatchMessage.Error.MatchDateTooSoon(minimumAdvanceScheduling));
            }

            // Check court exist
            var minimumHourDurationOfAMatch = Double.Parse(matchSettingsSection[MatchConstant.Setting.MinimumHourDurationOfAMatch] ?? "1");
            var matchEndTime = request.MatchDate.AddHours(minimumHourDurationOfAMatch);
            if (string.IsNullOrEmpty(request.CourtId))
            {
                errors.Add(nameof(request.CourtId), MatchMessage.Error.CourtRequired);
            }
            else
            {
                if (!await _courtRepository.CourtExistsAsync(request.CourtId))
                {
                    errors.Add(nameof(request.CourtId), MatchMessage.Error.CourtDoesNotExist);
                } // Check court availability
                else if (!await _matchRepository.IsCourtAvailableAsync(match, request.CourtId, request.MatchDate, matchEndTime))
                {
                    errors.Add(nameof(request.CourtId), MatchMessage.Error.CourtUnavailable);
                }
            }

            // Validate HomeTeamId and AwayTeamId
            if (string.IsNullOrEmpty(request.HomeTeamId) && string.IsNullOrEmpty(request.AwayTeamId))
            {
                errors.Add(nameof(request.HomeTeamId), MatchMessage.Error.HomeTeamIdOrAwayTeamIdRequired);
            }
            else
            {
                if (request.HomeTeamId == request.AwayTeamId)
                {
                    errors.Add(MatchConstant.AdditionalErrorField.BothTeamId, MatchMessage.Error.HomeTeamIdAndAwayTeamIdCannotBeTheSame);
                }
            }

            // Validate OpponentTeamName
            if (string.IsNullOrEmpty(request.HomeTeamId) || string.IsNullOrEmpty(request.AwayTeamId))
            {
                if (string.IsNullOrEmpty(request.OpponentTeamName))
                {
                    errors.Add(nameof(request.OpponentTeamName), MatchMessage.Error.OpponentTeamNameCannotBeEmpty);
                }
            }

            // Check team exist
            if (!string.IsNullOrEmpty(request.HomeTeamId) && !await _teamRepository.TeamExistsAsync(request.HomeTeamId))
            {
                errors.Add(nameof(request.HomeTeamId), MatchMessage.Error.HomeTeamIdDoesNotExist);
            }
            else
            {
                if (request.HomeTeamId != null)
                {
                    // Check is team active
                    var homeTeam = await _teamRepository.GetTeamByIdAsync(request.HomeTeamId);
                    if (homeTeam == null || homeTeam.Status != TeamStatusConstant.ACTIVE)
                    {
                        errors.Add(nameof(request.AwayTeamId), MatchMessage.Error.TeamNotActive);
                    }
                    else
                    // Check team availability
                    if (!string.IsNullOrEmpty(request.HomeTeamId) && !await _matchRepository.IsTeamAvailableAsync(match, request.HomeTeamId, request.MatchDate, matchEndTime))
                    {
                        errors.Add(nameof(request.HomeTeamId), MatchMessage.Error.HomeTeamUnavailable);
                    }
                }
            }

            if (!string.IsNullOrEmpty(request.AwayTeamId) && !await _teamRepository.TeamExistsAsync(request.AwayTeamId))
            {
                errors.Add(nameof(request.AwayTeamId), MatchMessage.Error.AwayTeamIdDoesNotExist);
            }
            else
            {
                if (request.AwayTeamId != null)
                {
                    // Check is team active
                    var awayTeam = await _teamRepository.GetTeamByIdAsync(request.AwayTeamId);
                    if (awayTeam == null || awayTeam.Status != TeamStatusConstant.ACTIVE)
                    {
                        errors.Add(nameof(request.AwayTeamId), MatchMessage.Error.TeamNotActive);
                    }
                    else
                    // Check team availability
                    if (!string.IsNullOrEmpty(request.AwayTeamId) && !await _matchRepository.IsTeamAvailableAsync(match, request.AwayTeamId, request.MatchDate, matchEndTime))
                    {
                        errors.Add(nameof(request.AwayTeamId), MatchMessage.Error.AwayTeamUnavailable);
                    }
                }
            }

            if (errors.Any())
            {
                return new ApiMessageModelV2<MatchDetailDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = MatchMessage.Error.MatchUpdateFailed,
                    Errors = errors
                };
            }

            return null;
        }

        // Get available court for match in a specific time
        public async Task<ApiResponseModel<List<CourtDto>>> GetAvailableCourtsAsync(DateTime matchDate)
        {
            var response = new ApiResponseModel<List<CourtDto>>()
            {
                Status = ApiResponseStatusConstant.FailedStatus
            };

            var matchSettingsSection = _configuration.GetSection(MatchConstant.Setting.MatchSettingsSection);
            var minimumHourDurationOfAMatch = Double.Parse(matchSettingsSection[MatchConstant.Setting.MinimumHourDurationOfAMatch] ?? "1");
            var availableCourts = await _matchRepository.GetAvailableCourtsForMatchInARangeOfTimeAsync(matchDate, matchDate.AddHours(minimumHourDurationOfAMatch));

            if (availableCourts == null || !availableCourts.Any())
            {
                response.Message = MatchMessage.Error.NoCourtsAvailableAtSelectedTime;
                return response;
            }

            var courtDtos = availableCourts.Select(c => new CourtDto
            {
                CourtId = c.CourtId,
                CourtName = c.CourtName,
                Address = c.Address,
                UsagePurpose = c.UsagePurpose,
                Status = c.Status,
                ImageUrl = c.ImageUrl
            }).ToList();

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = ApiResponseSuccessMessage.ApiSuccessMessage;
            response.Data = courtDtos;

            return response;
        }

        // Get match details
        public async Task<ApiResponseModel<MatchDetailDto>> GetMatchDetailAsync(int matchId)
        {
            var match = await _matchRepository.GetMatchByIdAsync(matchId);
            if (match == null)
            {
                return new ApiResponseModel<MatchDetailDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = MatchMessage.Error.MatchNotFound,
                    Errors = [MatchMessage.Error.MatchNotFound]
                };
            }

            var homeTeamPlayers = match.HomeTeamId != null
            ? await _matchLineupRepository.GetPlayersByMatchAndTeamAsync(matchId, match.HomeTeamId)
            : new List<Player>();

            var awayTeamPlayers = match.AwayTeamId != null
                ? await _matchLineupRepository.GetPlayersByMatchAndTeamAsync(matchId, match.AwayTeamId)
                : new List<Player>();

            var matchSettingsSection = _configuration.GetSection(MatchConstant.Setting.MatchSettingsSection);
            var minimumHourDurationOfAMatch = Double.Parse(matchSettingsSection[MatchConstant.Setting.MinimumHourDurationOfAMatch] ?? "1");
            var matchDetailDto = new MatchDetailDto
            {
                MatchId = match.MatchId,
                MatchName = match.MatchName,
                ScheduledDate = DateOnly.FromDateTime(match.MatchDate),
                ScheduledStartTime = TimeOnly.FromDateTime(match.MatchDate),
                ScheduledEndTime = TimeOnly.FromDateTime(match.MatchDate.AddHours(minimumHourDurationOfAMatch)),
                HomeTeamId = match.HomeTeamId,
                HomeTeamName = match.HomeTeamId == null ? match.OpponentTeamName : match.HomeTeam?.TeamName,
                ScoreHome = match.ScoreHome,
                AwayTeamId = match.AwayTeamId,
                AwayTeamName = match.AwayTeamId == null ? match.OpponentTeamName : match.AwayTeam?.TeamName,
                ScoreAway = match.ScoreAway,
                Status = MatchConstant.Status.GetStatusName(match.Status),
                CourtId = match.CourtId,
                CourtName = match.Court.CourtName,
                CourtAddress = match.Court.Address,
                CreatedByCoachId = match.CreatedByCoachId,
                HomeTeamPlayers = homeTeamPlayers.Select(p => new PlayerInLineUpDto
                {
                    UserId = p.UserId,
                    TeamId = p.TeamId,
                    PlayerName = p.User.Fullname,
                    Position = p.Position == null ? MatchMessage.Error.PlayerPositionNotAssigned : p.Position,
                    ShirtNumber = p.ShirtNumber == null ? MatchMessage.Error.PlayerShirtNumberNotAssigned : p.ShirtNumber,
                    IsStarting = match.MatchLineups.Where(ml => ml.PlayerId == p.UserId).FirstOrDefault()?.IsStarting
                }).ToList(),
                AwayTeamPlayers = awayTeamPlayers.Select(p => new PlayerInLineUpDto
                {
                    UserId = p.UserId,
                    TeamId = p.TeamId,
                    PlayerName = p.User.Fullname,
                    Position = p.Position == null ? MatchMessage.Error.PlayerPositionNotAssigned : p.Position,
                    ShirtNumber = p.ShirtNumber == null ? MatchMessage.Error.PlayerShirtNumberNotAssigned : p.ShirtNumber,
                    IsStarting = match.MatchLineups.Where(ml => ml.PlayerId == p.UserId).FirstOrDefault()?.IsStarting
                }).ToList(),
                MatchArticles = match.MatchArticles.Select(a => new MatchArticleDto
                {
                    ArticleId = a.ArticleId,
                    ArticleType = a.ArticleType,
                    Url = a.Url,
                    Title = a.Title

                }).ToList()
            };

            return new ApiResponseModel<MatchDetailDto>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = ApiResponseMessage.ApiResponseSuccessMessage.ApiSuccessMessage,
                Data = matchDetailDto
            };
        }

        // Cancel a match
        public async Task<ApiResponseModel<bool>> CancelMatchAsync(int matchId)
        {
            var response = new ApiResponseModel<bool>();
            var match = await _matchRepository.GetMatchByIdAsync(matchId);

            if (match == null)
            {
                response.Status = ApiResponseStatusConstant.FailedStatus;
                response.Message = MatchMessage.Error.MatchCancelFailed;
                response.Errors = [MatchMessage.Error.MatchNotFound];
                return response;
            }
            else if (match.Status == MatchConstant.Status.CANCELED)
            {
                response.Status = ApiResponseStatusConstant.FailedStatus;
                response.Message = MatchMessage.Error.MatchCancelFailed;
                response.Errors = [MatchMessage.Error.MatchAlreadyCanceled];
                return response;
            }   

            // Check if the coach of home team or away team can cancel the match
            var currentUserId = GetCurrentUserId(_httpContextAccessor);
            var currentCoach = await _coachRepository.GetACoachByUserIdAsync(currentUserId);
            if (currentCoach == null || (currentCoach.TeamId != match.HomeTeamId && currentCoach.TeamId != match.AwayTeamId))
            {
                response.Status = ApiResponseStatusConstant.FailedStatus;
                response.Message = MatchMessage.Error.MatchCancelFailed;
                response.Errors = [MatchMessage.Error.OnlyTeamCoachCanCancelMatch];
                return response;
            }


            match.Status = MatchConstant.Status.CANCELED;
            var updateResult = await _matchRepository.UpdateMatchAsync(match);

            if (!updateResult)
            {
                response.Status = ApiResponseStatusConstant.FailedStatus;
                response.Message = MatchMessage.Error.MatchCancelFailed;
                response.Errors = [MatchMessage.Error.MatchCancelFailed];
                return response;
            }

            // Send notification to managers, players, coach, and parents

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = MatchMessage.Success.MatchCanceled;
            response.Data = true;
            return response;
        }
    }
}
