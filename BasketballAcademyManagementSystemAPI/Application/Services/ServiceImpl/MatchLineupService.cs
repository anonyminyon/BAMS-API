using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Match;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Team;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class MatchLineupService : IMatchLineupService
    {

        private readonly IMatchRepository _matchRepository;
        private readonly IMatchLineupRepository _matchLineupRepository;
        private readonly ICoachRepository _coachRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MatchLineupService(IMatchRepository matchRepository
            , IMatchLineupRepository matchLineupRepository
            , ICoachRepository coachRepository
            , IPlayerRepository playerRepository
            , IConfiguration configuration
            , IHttpContextAccessor httpContextAccessor)
        {
            _matchRepository = matchRepository;
            _matchLineupRepository = matchLineupRepository;
            _coachRepository = coachRepository;
            _playerRepository = playerRepository;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponseModel<bool>> CallPlayerForMatchAsync(CallPlayerForMatchRequest request)
        {
            var response = new ApiResponseModel<bool>()
            {
                Status = ApiResponseStatusConstant.FailedStatus,
                Data = false,
                Message = MatchMessage.Error.AddPlayerToMatchLineupFailed
            };
            var match = await _matchRepository.GetMatchByIdAsync(request.MatchId);

            if (match == null)
            {
                response.Errors = new List<string> { MatchMessage.Error.MatchNotFound };
                return response;
            }

            // Check if the coach of home team or away team can call the player
            var currentUserId = TokenHelper.GetCurrentUserId(_httpContextAccessor);
            var currentCoach = await _coachRepository.GetACoachByUserIdAsync(currentUserId);
            if (currentCoach == null || (currentCoach.TeamId != match.HomeTeamId && currentCoach.TeamId != match.AwayTeamId))
            {
                response.Errors = new List<string> { MatchMessage.Error.OnlyTeamCoachCanCallPlayer };
                return response;
            }

            // Check if the match is not started
            if (match.Status != MatchConstant.Status.UPCOMING)
            {
                response.Errors = new List<string> { MatchMessage.Error.YouCanOnlyCallPlayerForUnStartedMatch };
                return response;
            }

            // Check is player exist
            var player = await _matchLineupRepository.GetPlayerByIdAsync(request.PlayerId);
            if (player == null)
            {
                response.Errors = new List<string> { MatchMessage.Error.PlayerNotFound };
                return response;
            }

            // Check if the player is not belong to home team or away team
            if (player.TeamId != match.HomeTeamId && player.TeamId != match.AwayTeamId)
            {
                response.Errors = new List<string> { MatchMessage.Error.PlayerNotInMatchTeam };
                return response;
            }

            // Check if the player is belong to the team that coach is coaching
            if (currentCoach.TeamId != player.TeamId)
            {
                response.Errors = new List<string> { MatchMessage.Error.PlayerNotInYourTeam };
                return response;
            }

            // Check if the player is already in the match lineup
            var existingLineup = await _matchLineupRepository.GetMatchLineupByMatchIdAndPlayerIdAsync(request.MatchId, request.PlayerId);
            if (existingLineup != null)
            {
                response.Errors = new List<string> { MatchMessage.Error.PlayerAlreadyInLineup };
                return response;
            }

            var matchSettingsSection = _configuration.GetSection(MatchConstant.Setting.MatchSettingsSection);
            // Check if the number of starting players in the team is already maximum
            var maxStartingPlayers = Double.Parse(matchSettingsSection[MatchConstant.Setting.MaxStartingPlayers] ?? "5");
            var startingPlayersCount = await _matchLineupRepository.CountStartingPlayersAsync(request.MatchId, player.TeamId);
            if (request.IsStarting && startingPlayersCount >= maxStartingPlayers)
            {
                response.Errors = new List<string> { MatchMessage.Error.TooManyStartingPlayers };
                return response;
            }

            // Check if the number of players in the team is already maximum

            var playersInTeam = await _matchLineupRepository.GetPlayersByMatchAndTeamAsync(request.MatchId, player.TeamId);
            var maxRegisteredPlayers = Double.Parse(matchSettingsSection[MatchConstant.Setting.MaxRegistraionPlayers] ?? "12");
            if (playersInTeam.Count >= maxRegisteredPlayers)
            {
                response.Errors = new List<string> { MatchMessage.Error.TooManyPlayersInTeam };
                return response;
            }

            // Add player to match lineup
            var matchLineup = new MatchLineup
            {
                MatchId = request.MatchId,
                PlayerId = request.PlayerId,
                IsStarting = request.IsStarting
            };

            var addResult = await _matchLineupRepository.AddPlayerToMatchLineupAsync(matchLineup);

            if (!addResult)
            {
                response.Errors = new List<string> { MatchMessage.Error.AddPlayerToMatchLineupFailed };
                return response;
            }

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = MatchMessage.Success.AddPlayerToMatchLineupSuccessfull;
            response.Data = true;
            return response;
        }

        public async Task<ApiResponseModel<List<AvailablePlayerForAMatchDto>>> GetAvailablePlayersAsync(int matchId)
        {
            var response = new ApiResponseModel<List<AvailablePlayerForAMatchDto>>()
            {
                Status = ApiResponseStatusConstant.FailedStatus,
                Data = null,
                Message = MatchMessage.Error.GetAvailablePlayersFailed
            };

            var match = await _matchRepository.GetMatchByIdAsync(matchId);

            if (match == null)
            {
                response.Errors = new List<string> { MatchMessage.Error.MatchNotFound };
                return response;
            }

            // Get the current coach
            var currentUserId = TokenHelper.GetCurrentUserId(_httpContextAccessor);
            var currentCoach = await _coachRepository.GetACoachByUserIdAsync(currentUserId);
            if (currentCoach == null)
            {
                response.Errors = new List<string> { MatchMessage.Error.CoachNotFound };
                return response;
            }

            // Get available players
            var availablePlayers = await _matchLineupRepository.GetAvailablePlayersOfATeamInAMatchAsync(matchId, currentCoach.TeamId);

            var playerDtos = availablePlayers.Select(p => new AvailablePlayerForAMatchDto
            {
                UserId = p.UserId,
                TeamId = p.TeamId,
                PlayerName = p.User.Fullname,
                Position = p.Position == null ? MatchMessage.Error.PlayerPositionNotAssigned : p.Position,
                ShirtNumber = p.ShirtNumber == null ? MatchMessage.Error.PlayerShirtNumberNotAssigned : p.ShirtNumber,
                Weight = p.Weight,
                Height = p.Height,
                ClubJoinDate = p.ClubJoinDate
            }).ToList();

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = MatchMessage.Success.GetAvailablePlayersSuccess;
            response.Data = playerDtos;
            return response;
        }



        public async Task<ApiResponseModel<bool>> RemovePlayerFromMatchLineupAsync(int matchId, string playerId)
        {
            var response = new ApiResponseModel<bool>()
            {
                Status = ApiResponseStatusConstant.FailedStatus,
                Data = false,
                Message = MatchMessage.Error.PlayerInLineupRemoveFailed
            };
            var match = await _matchRepository.GetMatchByIdAsync(matchId);

            if (match == null)
            {
                response.Status = ApiResponseStatusConstant.FailedStatus;
                response.Errors = new List<string> { MatchMessage.Error.MatchNotFound };
                return response;
            }

            // Check if the coach of home team or away team can remove the player
            var currentUserId = TokenHelper.GetCurrentUserId(_httpContextAccessor);
            var currentCoach = await _coachRepository.GetACoachByUserIdAsync(currentUserId);
            if (currentCoach == null || (currentCoach.TeamId != match.HomeTeamId && currentCoach.TeamId != match.AwayTeamId))
            {
                response.Errors = new List<string> { MatchMessage.Error.OnlyTeamCoachCanRemovePlayer };
                return response;
            }

            // Check if the match is not started
            if (match.Status != MatchConstant.Status.UPCOMING)
            {
                response.Errors = new List<string> { MatchMessage.Error.YouCanOnlyRemoveUnStartedMatch };
                return response;
            }

            // Check is the player in the match lineup
            var deletedMatchLineup = await _matchLineupRepository.GetMatchLineupByMatchIdAndPlayerIdAsync(matchId, playerId);
            if (deletedMatchLineup == null)
            {
                response.Errors = new List<string> { MatchMessage.Error.PlayerNotInMatchLineup };
                return response;
            }

            // Check is the player belong to the team that coach is coaching
            if (currentCoach.TeamId != deletedMatchLineup.Player.TeamId)
            {
                response.Errors = new List<string> { MatchMessage.Error.PlayerNotInYourTeam };
                return response;
            }

            // Remove the player from the match lineup
            var removeResult = await _matchLineupRepository.RemovePlayerFromMatchLineupAsync(deletedMatchLineup);

            if (!removeResult)
            {
                response.Errors = new List<string> { MatchMessage.Error.PlayerInLineupRemoveFailed };
                return response;
            }

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = MatchMessage.Success.PlayerRemoved;
            response.Data = true;
            return response;
        }

    }
}
