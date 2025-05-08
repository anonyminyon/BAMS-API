
using Azure.Core;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Match;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.Extensions.Configuration;
using static BasketballAcademyManagementSystemAPI.Common.Constants.MatchConstant;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class MatchArticleService : IMatchArticleService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ICoachRepository _coachRepository;
        private readonly IMatchArticleRepository _matchArticleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileUploadService _fileUploadService;

        public MatchArticleService(IMatchRepository matchRepository
            , ICoachRepository coachRepository
            , IMatchArticleRepository matchArticleRepository
            , IHttpContextAccessor httpContextAccessor
            , IFileUploadService fileUploadService)
        {
            _matchRepository = matchRepository;
            _coachRepository = coachRepository;
            _matchArticleRepository = matchArticleRepository;
            _httpContextAccessor = httpContextAccessor;
            _fileUploadService = fileUploadService;
        }

        public async Task<ApiResponseModel<bool>> RemoveMatchArticleAsync(int matchId, int articleId)
        {
            var response = new ApiResponseModel<bool>()
            {
                Status = ApiResponseStatusConstant.FailedStatus,
                Message = MatchMessage.Error.ArticleRemoveFailed
            };
            var match = await _matchRepository.GetMatchByIdAsync(matchId);

            // Check if the match exists
            if (match == null)
            {
                response.Errors = new List<string> { MatchMessage.Error.MatchNotFound };
                return response;
            }

            // Check if the coach of home team or away team can remove the article
            var currentUserId = TokenHelper.GetCurrentUserId(_httpContextAccessor);
            var currentCoach = await _coachRepository.GetACoachByUserIdAsync(currentUserId);
            if (currentCoach == null || (currentCoach.TeamId != match.HomeTeamId && currentCoach.TeamId != match.AwayTeamId))
            {
                response.Errors = new List<string> { MatchMessage.Error.OnlyTeamCoachCanRemoveArticle };
                return response;
            }

            // Get deleting article
            var article = await _matchArticleRepository.GetMatchArticleByIdAsync(matchId, articleId);
            if (article == null) {
                response.Errors = new List<string> { MatchMessage.Error.ArticleNotFound };
                return response;
            }

            // Remove article file in server
            if (!string.IsNullOrEmpty(article.Url))
            {
                DeleteMatchArticleFile(article.Url);
            }

            // Remove article
            var removeResult = await _matchArticleRepository.RemoveMatchArticleAsync(article);

            if (!removeResult)
            {
                response.Errors = new List<string> { MatchMessage.Error.ArticleRemoveFailed };
                return response;
            }

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = MatchMessage.Success.ArticleRemoved;
            response.Data = true;
            return response;
        }

        public async Task<ApiMessageModelV2<bool>> AddMatchArticlesAsync(int matchId, List<AddMatchArticleRequest> requests)
        {
            var response = new ApiMessageModelV2<bool>()
            {
                Status = ApiResponseStatusConstant.FailedStatus,
                Message = MatchMessage.Error.ArticleAddFailed,
                Errors = new Dictionary<string, string>()
            };

            var match = await _matchRepository.GetMatchByIdAsync(matchId);

            if (match == null)
            {
                response.Errors[nameof(matchId)] = MatchMessage.Error.MatchNotFound;
                return response;
            }

            // Check if the coach of home team or away team can add the article
            var currentUserId = TokenHelper.GetCurrentUserId(_httpContextAccessor);
            var currentCoach = await _coachRepository.GetACoachByUserIdAsync(currentUserId);
            if (currentCoach == null || (currentCoach.TeamId != match.HomeTeamId && currentCoach.TeamId != match.AwayTeamId))
            {
                response.Errors[nameof(currentUserId)] = MatchMessage.Error.OnlyTeamCoachCanAddArticle;
                return response;
            }

            int index = 0;
            foreach (var request in requests)
            {
                var validationResult = ValidateAddMatchArticleRequestAsync(request, index);
                if (validationResult != null)
                {
                    foreach (var error in validationResult)
                    {
                        response.Errors[error.Key] = error.Value;
                    }
                    index++;
                    continue;
                }

                // Add article
                var matchArticle = new MatchArticle
                {
                    MatchId = matchId,
                    Title = request.Title,
                    Url = request.Url,
                    ArticleType = request.ArticleType
                };

                var addResult = await _matchArticleRepository.AddMatchArticleAsync(matchArticle);

                if (!addResult)
                {
                    response.Errors[nameof(matchArticle) + "_" + index] = MatchMessage.Error.ArticleTitleAddFailed(request.Title);
                }

                index++;
            }

            if (response.Errors.Count > 0)
            {
                if (response.Errors.Count == requests.Count)
                {
                    response.Message = MatchMessage.Error.ArticleAddFailed;
                }
                else
                {
                    response.Message = MatchMessage.Error.ArticleAddPartialSuccess;
                }
                return response;
            }

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = MatchMessage.Success.ArticleAdded;
            response.Data = true;
            return response;
        }

        public Dictionary<string, string>? ValidateAddMatchArticleRequestAsync(AddMatchArticleRequest request, int index)
        {
            var errors = new Dictionary<string, string>();

            // Validate title
            if (string.IsNullOrEmpty(request.Title))
            {
                errors.Add(nameof(request.Title) + "_" + index, MatchMessage.Error.ArticleTitleRequired);
            }

            // Validate url
            if (string.IsNullOrEmpty(request.Url))
            {
                errors.Add(nameof(request.Url) + "_" + index, MatchMessage.Error.ArticleUrlRequired);
            }
            else if (!Uri.IsWellFormedUriString(request.Url, UriKind.Absolute))
            {
                errors.Add(nameof(request.Url) + "_" + index, MatchMessage.Error.ArticleUrlInvalid);
            }

            // Validate article type
            if (string.IsNullOrEmpty(request.ArticleType))
            {
                errors.Add(nameof(request.ArticleType) + "_" + index, MatchMessage.Error.ArticleTypeRequired);
            } else if (!MatchConstant.ArticleType.IsValid(request.ArticleType))
            {
                errors.Add(nameof(request.ArticleType) + "_" + index, MatchMessage.Error.ArticleTypeInvalid);
            }

            if (errors.Any())
            {
                return errors;
            }

            return null;
        }

        public async Task<ApiResponseModel<string>> UploadMatchArticleFileAsync(int matchId, UploadMatchArticleFileRequest request)
        {
            var response = new ApiResponseModel<string>()
            {
                Status = ApiResponseStatusConstant.FailedStatus,
                Message = MatchMessage.Error.FileUploadFailed,
                Errors = new List<string>()
            };

            if (request.File == null)
            {
                response.Errors = new List<string> { MatchMessage.Error.FileUploadFailed };
                return response;
            }

            if (request.ArticleType == null)
            {
                response.Errors = new List<string> { MatchMessage.Error.ArticleTypeRequired };
                return response;
            }

            var match = await _matchRepository.GetMatchByIdAsync(matchId);

            if (match == null)
            {
                response.Errors = new List<string> { MatchMessage.Error.MatchNotFound };
                return response;
            }

            // Check if the coach of home team or away team can upload the file
            var currentUserId = TokenHelper.GetCurrentUserId(_httpContextAccessor);
            var currentCoach = await _coachRepository.GetACoachByUserIdAsync(currentUserId);
            if (currentCoach == null || (currentCoach.TeamId != match.HomeTeamId && currentCoach.TeamId != match.AwayTeamId))
            {
                response.Errors = new List<string> { MatchMessage.Error.OnlyTeamCoachCanUploadFile };
                return response;
            }

            // Validate ArticleType
            if (!MatchConstant.ArticleType.IsValid(request.ArticleType))
            {
                response.Errors = new List<string> { MatchMessage.Error.ArticleTypeInvalid };
                return response;
            }

            // Determine the folder path based on ArticleType
            string folderPath = request.ArticleType switch
            {
                MatchConstant.ArticleType.VIDEO => ArticleSaveLocaltion.VideoPath(matchId),
                MatchConstant.ArticleType.DOCUMENT => ArticleSaveLocaltion.DocumentPath(matchId),
                MatchConstant.ArticleType.IMAGE => ArticleSaveLocaltion.ImagePath(matchId),
                _ => throw new ArgumentException(MatchMessage.Error.ArticleTypeInvalid)
            };

            var fileUrl = await _fileUploadService.UploadFileAsync(request.File, folderPath);

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = MatchMessage.Success.FileUploaded;
            response.Data = fileUrl;
            return response;
        }

        public ApiResponseModel<bool> DeleteMatchArticleFile(string filePath)
        {
            var response = new ApiResponseModel<bool>()
            {
                Status = ApiResponseStatusConstant.FailedStatus,
                Message = MatchMessage.Error.FileDeleteFailed,
                Errors = new List<string>()
            };

            if (string.IsNullOrEmpty(filePath))
            {
                response.Errors.Add(MatchMessage.Error.FilePathRequired);
                return response;
            }

            try
            {
                filePath = "wwwroot/" + filePath;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    response.Status = ApiResponseStatusConstant.SuccessStatus;
                    response.Message = MatchMessage.Success.FileDeleted;
                    response.Data = true;
                }
                else
                {
                    response.Errors.Add(MatchMessage.Error.FileNotFound);
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
            }

            return response;
        }

    }
}
