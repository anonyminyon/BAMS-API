using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Amazon.Rekognition.Model;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Enums;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using ClosedXML.Excel;
using static BasketballAcademyManagementSystemAPI.Common.Constants.TryOutScoreConstant;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class TryOutScorecardService : ITryOutScorecardService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITryOutScorecardRepository _tryOutScorecardRepository;
        private readonly ITryOutMeasurementScaleRepository _tryOutMeasurementScaleRepository;
        private readonly IPlayerRegistrationRepository _playerRegistrationRepository;
        private readonly IMemberRegistrationSessionRepository _memberRegistrationSessionRepository;

        public TryOutScorecardService(IHttpContextAccessor httpContextAccessor
            , ITryOutScorecardRepository tryOutScorecardRepository
            , ITryOutMeasurementScaleRepository tryOutMeasurementScaleRepository
            , IPlayerRegistrationRepository playerRegistrationRepository
            , IMemberRegistrationSessionRepository memberRegistrationSessionRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _tryOutScorecardRepository = tryOutScorecardRepository;
            _tryOutMeasurementScaleRepository = tryOutMeasurementScaleRepository;
            _playerRegistrationRepository = playerRegistrationRepository;
            _memberRegistrationSessionRepository = memberRegistrationSessionRepository;
        }

        public async Task<ApiResponseModel<string>> AddOrUpdateScoresAsync(BulkPlayerScoreInputDto input)
        {
            ApiResponseModel<string> apiResponse = new ApiResponseModel<string>()
            {
                Errors = new List<string>()
            };

            // Get current user
            var user = _httpContextAccessor.HttpContext?.User;
            var currentUserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
             ?? user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrEmpty(currentUserId))
            {
                throw new Exception(AuthenticationMessage.AuthenticationErrorMessage.PleaseLogin);
            }

            foreach (var player in input.Players)
            {
                foreach (var score in player.Scores)
                {
                    if (!IsValidScore(score.SkillCode, score.Score))
                    {
                        apiResponse.Errors.Add(TryOutScoreMessage.Error.SaveScoreFailed(score.Score, score.SkillCode, player.PlayerRegistrationId));
                    }
                    else
                    {
                        var entity = new TryOutScorecard
                        {
                            PlayerRegistrationId = player.PlayerRegistrationId,
                            MeasurementScaleCode = score.SkillCode,
                            Score = score.Score,
                            ScoredBy = currentUserId,
                            ScoredAt = DateTime.Now
                        };
                        await _tryOutScorecardRepository.AddOrUpdateAsync(entity);

                        apiResponse.Status = ApiResponseStatusConstant.SuccessStatus;
                    }
                }
            }

            if (apiResponse.Status == ApiResponseStatusConstant.SuccessStatus)
            {
                if (apiResponse.Errors.Any())
                {
                    apiResponse.Message = TryOutScoreMessage.Success.SaveSomeScoresSuccess;
                }
                else
                {
                    apiResponse.Message = TryOutScoreMessage.Success.SaveAllScoresSuccess;
                }
            }
            else
            {
                apiResponse.Message = TryOutScoreMessage.Error.SaveAllScoresFailed;
            }

            return apiResponse;
        }

        public bool IsValidScore(string measurementScaleCode, string score)
        {
            if (!TryOutScoreConstant.ValidationRules.TryGetValue(measurementScaleCode, out var validationType))
            {
                return false; // Nếu không tìm thấy mã, coi như không hợp lệ
            }

            return validationType switch
            {
                TryOutScoreValidationType.ThreeLevelsGrade => IsValidThreeLevelsGrade(score),
                TryOutScoreValidationType.PhysicalFitnessValue => IsValidFitnessValue(score),
                TryOutScoreValidationType.Range1To5 => IsValidRange1To5(score),
                _ => false
            };
        }

        private bool IsValidThreeLevelsGrade(string score)
        {
            return score is "T" or "K" or "TB";
        }

        private bool IsValidFitnessValue(string score)
        {
            return double.TryParse(score, out var value) && value > 0;
        }

        private bool IsValidRange1To5(string score)
        {
            return int.TryParse(score, out var value) && value >= 1 && value <= 5;
        }

        public async Task<ApiResponseModel<string>> UpdateScoresAsync(BulkPlayerScoreInputDto input)
        {
            return await AddOrUpdateScoresAsync(input);
        }

        public async Task<ApiResponseModel<PlayerTryOutScoreCardDto>> GetScoresByPlayerRegistrationIdAsync(int playerRegistrationId)
        {
            var apiResponse = new ApiResponseModel<PlayerTryOutScoreCardDto>();

            try
            {
                var playerRegistration = await _playerRegistrationRepository.GetPlayerRegistrationByIdAsync(playerRegistrationId);
                if (playerRegistration == null)
                {
                    apiResponse.Errors = [TryOutScoreMessage.Error.SessionRegistrationPlayerDoesNotExist];
                    return apiResponse;
                }

                var scores = await _tryOutScorecardRepository.GetScoresByPlayerIdAsync(playerRegistrationId);

                var resultScore = scores.Select(s => new TryOutScorecardDto
                {
                    TryOutScorecardId = s.TryOutScorecardId,
                    PlayerRegistrationId = s.PlayerRegistrationId,
                    MeasurementScaleCode = s.MeasurementScaleCode,
                    MeasurementName = s.MeasurementScaleCodeNavigation.MeasurementName,
                    Score = s.Score,
                    Note = s.Note,
                    ScoredBy = s.ScoredByNavigation.Fullname + " (" + s.ScoredByNavigation.Username + ")",
                    ScoredAt = s.ScoredAt,
                    UpdatedAt = s.UpdatedAt == null ? s.ScoredAt : (DateTime) s.UpdatedAt
                }).ToList();

                var playerScorecard = new PlayerTryOutScoreCardDto
                {
                    PlayerRegistrationId = playerRegistration.PlayerRegistrationId,
                    CandidateNumber = playerRegistration.CandidateNumber,
                    FullName = playerRegistration.FullName,
                    Gender = playerRegistration.Gender,
                    DateOfBirth = playerRegistration.DateOfBirth,
                    Scores = resultScore
                };

                if (!scores.Any())
                {
                    apiResponse.Errors = [TryOutScoreMessage.Error.SessionRegistrationPlayerScoreDoesNotExist];
                }

                apiResponse.Status = ApiResponseStatusConstant.SuccessStatus;
                apiResponse.Message = ApiResponseMessage.ApiResponseSuccessMessage.ApiSuccessMessage;
                apiResponse.Data = playerScorecard;
                return apiResponse;

            }
            catch (Exception ex)
            {
                apiResponse.Errors = [ex.Message];
            }

            return apiResponse;
        }

        public async Task<ApiResponseModel<PlayerSkillScoreReportDto>> GetReportByPlayerRegistrationIdAsync(int playerRegistrationId)
        {
            var apiResponse = new ApiResponseModel<PlayerSkillScoreReportDto>()
            {
                Errors = new List<string>(),
            };

            try
            {
                // Bước 1: Lấy ra PlayerRegistrations cùng các thông tin liên quan
                var player = await _playerRegistrationRepository.GetPlayerRegistrationAsync(playerRegistrationId);

                if (player == null)
                {
                    apiResponse.Errors.Add(TryOutScoreMessage.Error.SessionRegistrationPlayerDoesNotExist);
                    return apiResponse;
                }

                // Bước 2: Lấy ra các kỹ năng lá trong csdl
                var leafSkills = await _tryOutMeasurementScaleRepository.GetLeafSkillsAsync();

                var playerResult = new PlayerSkillScoreReportDto();
                var playerSkillScores = new List<PlayerSkillScoreDto>();

                // Bước 3: Mapping điểm của player đó với toàn bộ kỹ năng lá, nếu player đó chưa có điểm của kỹ năng lá đó thì đặt là 0 điểm
                foreach (var leafSkill in leafSkills)
                {
                    var scorecard = player.TryOutScorecards
                        .FirstOrDefault(s => s.MeasurementScaleCode == leafSkill.MeasurementScaleCode);

                    if (scorecard != null)
                    {
                        // Bước 4: Lấy ra thông tin TryOutScoreCriterion dựa theo MeasurementScaleCode và gender của player
                        var criterion = await _tryOutMeasurementScaleRepository.GetCriterionBySkillAndGenderAsync(leafSkill.MeasurementScaleCode, player.Gender);

                        if (criterion != null)
                        {
                            // Bước 5: Lấy ra các TryOutScoreLevel dựa theo ScoreCriteriaId
                            var scoreLevels = await _tryOutMeasurementScaleRepository.GetScoreLevelsByCriterionIdAsync(criterion.ScoreCriteriaId);

                            // Bước 6: Mapping điểm của player từ TryOutScorecard, dựa theo FivePointScaleScore của TryOutScoreLevel
                            var score = MapScoreToFivePointScale(scorecard.Score, scoreLevels);

                            playerSkillScores.Add(new PlayerSkillScoreDto
                            {
                                PlayerRegistrationId = player.PlayerRegistrationId,
                                MeasurementScaleCode = leafSkill.MeasurementScaleCode,
                                Score = score
                            });
                        }
                    }
                    else
                    {
                        // Nếu player chưa có điểm của kỹ năng lá đó thì đặt là 0 điểm
                        playerSkillScores.Add(new PlayerSkillScoreDto
                        {
                            PlayerRegistrationId = player.PlayerRegistrationId,
                            MeasurementScaleCode = leafSkill.MeasurementScaleCode,
                            Score = 0
                        });
                    }

                    // Bước 6: Thực hiện đệ quy tính điểm từ lá lên gốc
                    var basketballSkill = await _tryOutMeasurementScaleRepository.GetSkillTreeAsync(TryOutScoreConstant.BasketballSkill);
                    var physicalSkill = await _tryOutMeasurementScaleRepository.GetSkillTreeAsync(TryOutScoreConstant.PhysicalFitness);

                    var basketballSkillAverage = basketballSkill != null ? Math.Round(CalculateSkillAverage(playerSkillScores, basketballSkill), 2) : 0;
                    var physicalSkillAverage = physicalSkill != null ? Math.Round(CalculateSkillAverage(playerSkillScores, physicalSkill), 2) : 0;

                    var overallAverage = Math.Round((basketballSkillAverage * 2 + physicalSkillAverage) / 3, 2);

                    // Tạo model kết quả mapping, tính toán của player
                    playerResult = new PlayerSkillScoreReportDto
                    {
                        PlayerRegistrationId = player.PlayerRegistrationId,
                        CandidateNumber = player.CandidateNumber ?? 0,
                        FullName = player.FullName,
                        Gender = player.Gender,
                        DateOfBirth = player.DateOfBirth,
                        AverageBasketballSkill = basketballSkillAverage,
                        AveragePhysicalFitness = physicalSkillAverage,
                        OverallAverage = overallAverage,
                        ScoreList = GetSkillDetails(playerSkillScores, basketballSkill!, physicalSkill!)
                    };

                }

                apiResponse.Data = playerResult;
                apiResponse.Status = ApiResponseStatusConstant.SuccessStatus;
                apiResponse.Message = ApiResponseMessage.ApiResponseSuccessMessage.ApiSuccessMessage;
            }
            catch (Exception ex)
            {
                apiResponse.Errors.Add(ex.Message);
            }

            return apiResponse;
        }

        public async Task<ApiResponseModel<List<RegistrationSessionScoresDto>>> GetScoresByRegistrationSessionIdAsync(int sessionId, PlayerRegistrationScoresFilterDto filter)
        {
            var apiResponse = new ApiResponseModel<List<RegistrationSessionScoresDto>>();

            try
            {
                var players = await _tryOutScorecardRepository.GetFilteredPlayersAsync(sessionId, filter);
                if (!players.Any())
                {
                    apiResponse.Errors = [TryOutScoreMessage.Error.SessionRegistrationPlayersDoesNotExist];
                    return apiResponse;
                }

                var scores = players.Select(p => new RegistrationSessionScoresDto
                {
                    PlayerRegistrationId = p.PlayerRegistrationId,
                    FullName = p.FullName,
                    CandidateNumber = p.CandidateNumber,
                    Gender = p.Gender,
                    DateOfBirth = p.DateOfBirth,
                    Scores = p.TryOutScorecards.Select(s => new TryOutScorecardDto
                    {
                        TryOutScorecardId = s.TryOutScorecardId,
                        PlayerRegistrationId = s.PlayerRegistrationId,
                        MeasurementScaleCode = s.MeasurementScaleCode,
                        MeasurementName = s.MeasurementScaleCodeNavigation.MeasurementName,
                        Score = s.Score,
                        Note = s.Note,
                        ScoredBy = s.ScoredByNavigation.Fullname + " (" + s.ScoredByNavigation.Username + ")",
                        ScoredAt = s.ScoredAt,
                        UpdatedAt = s.UpdatedAt == null ? s.ScoredAt : (DateTime)s.UpdatedAt
                    }).ToList()
                }).ToList();

                apiResponse.Status = ApiResponseStatusConstant.SuccessStatus;
                apiResponse.Message = ApiResponseMessage.ApiResponseSuccessMessage.ApiSuccessMessage;
                apiResponse.Data = scores;
            }
            catch (Exception ex)
            {
                apiResponse.Errors = [ex.Message];
            }

            return apiResponse;
        }

        public async Task<ApiResponseModel<List<PlayerSkillScoreReportDto>>> GetReportByRegistrationSessionIdAsync(int sessionId, PlayerRegistrationScoresFilterDto filter)
        {
            var apiResponse = new ApiResponseModel<List<PlayerSkillScoreReportDto>>()
            {
                Errors = new List<string>(),
            };

            try
            {
                // Bước 1: Lấy ra toàn bộ PlayerRegistrations trong 1 session cùng các thông tin liên quan
                var players = await _tryOutScorecardRepository.GetFilteredPlayersAsync(sessionId, filter);

                if (!players.Any())
                {
                    apiResponse.Errors.Add(TryOutScoreMessage.Error.SessionRegistrationPlayersDoesNotExist);
                    return apiResponse;
                }

                // Bước 2: Lấy ra các kỹ năng lá trong csdl
                var leafSkills = await _tryOutMeasurementScaleRepository.GetLeafSkillsAsync();

                // Bước 3: Mapping điểm của player đó với toàn bộ kỹ năng lá, nếu player đó chưa có điểm của kỹ năng lá đó thì đặt là 0 điểm
                var playerResults = new List<PlayerSkillScoreReportDto>();

                foreach (var player in players)
                {
                    var playerSkillScores = new List<PlayerSkillScoreDto>();

                    foreach (var leafSkill in leafSkills)
                    {
                        var scorecard = player.TryOutScorecards
                            .FirstOrDefault(s => s.MeasurementScaleCode == leafSkill.MeasurementScaleCode);

                        if (scorecard != null)
                        {
                            // Bước 4: Lấy ra thông tin TryOutScoreCriterion dựa theo MeasurementScaleCode và gender của player
                            var criterion = await _tryOutMeasurementScaleRepository.GetCriterionBySkillAndGenderAsync(leafSkill.MeasurementScaleCode, player.Gender);

                            if (criterion != null)
                            {
                                // Bước 5: Lấy ra các TryOutScoreLevel dựa theo ScoreCriteriaId
                                var scoreLevels = await _tryOutMeasurementScaleRepository.GetScoreLevelsByCriterionIdAsync(criterion.ScoreCriteriaId);

                                // Bước 6: Mapping điểm của player từ TryOutScorecard, dựa theo FivePointScaleScore của TryOutScoreLevel
                                var score = MapScoreToFivePointScale(scorecard.Score, scoreLevels);

                                playerSkillScores.Add(new PlayerSkillScoreDto
                                {
                                    PlayerRegistrationId = player.PlayerRegistrationId,
                                    MeasurementScaleCode = leafSkill.MeasurementScaleCode,
                                    Score = score
                                });
                            }
                        }
                        else
                        {
                            // Nếu player chưa có điểm của kỹ năng lá đó thì đặt là 0 điểm
                            playerSkillScores.Add(new PlayerSkillScoreDto
                            {
                                PlayerRegistrationId = player.PlayerRegistrationId,
                                MeasurementScaleCode = leafSkill.MeasurementScaleCode,
                                Score = 0
                            });
                        }
                    }

                    // Bước 6: Thực hiện đệ quy tính điểm từ lá lên gốc
                    var basketballSkill = await _tryOutMeasurementScaleRepository.GetSkillTreeAsync(TryOutScoreConstant.BasketballSkill);
                    var physicalSkill = await _tryOutMeasurementScaleRepository.GetSkillTreeAsync(TryOutScoreConstant.PhysicalFitness);

                    var basketballSkillAverage = basketballSkill != null ? Math.Round(CalculateSkillAverage(playerSkillScores, basketballSkill), 2) : 0;
                    var physicalSkillAverage = physicalSkill != null ? Math.Round(CalculateSkillAverage(playerSkillScores, physicalSkill), 2) : 0;

                    var overallAverage = Math.Round((basketballSkillAverage * 2 + physicalSkillAverage) / 3, 2);

                    // Tạo model kết quả mapping, tính toán của player
                    var playerResult = new PlayerSkillScoreReportDto
                    {
                        PlayerRegistrationId = player.PlayerRegistrationId,
                        CandidateNumber = player.CandidateNumber ?? 0,
                        FullName = player.FullName,
                        Gender = player.Gender,
                        DateOfBirth = player.DateOfBirth,
                        AverageBasketballSkill = basketballSkillAverage,
                        AveragePhysicalFitness = physicalSkillAverage,
                        OverallAverage = overallAverage,
                        ScoreList = GetSkillDetails(playerSkillScores, basketballSkill!, physicalSkill!)
                    };

                    playerResults.Add(playerResult);
                }

                var sortedPlayerResults = playerResults
                    .OrderBy(p => p.CandidateNumber)
                    .ThenByDescending(p => p.OverallAverage)
                    .ToList();
                apiResponse.Data = sortedPlayerResults;
                apiResponse.Status = ApiResponseStatusConstant.SuccessStatus;
                apiResponse.Message = ApiResponseMessage.ApiResponseSuccessMessage.ApiSuccessMessage;
            }
            catch (Exception ex)
            {
                apiResponse.Errors.Add(ex.Message);
            }

            return apiResponse;
        }

        private decimal MapScoreToFivePointScale(string score, List<TryOutScoreLevel> scoreLevels)
        {
            // Nếu MinValue và MaxValue đồng thời là null, mapping dựa theo ScoreLevel
            if (scoreLevels.All(l => l.MinValue == null && l.MaxValue == null))
            {
                var level = scoreLevels.FirstOrDefault(l => l.ScoreLevel == score);
                return level?.FivePointScaleScore ?? 0;
            }

            // Nếu có MinValue hoặc MaxValue, parse score sang decimal và so sánh
            if (decimal.TryParse(score, out var decimalScore))
            {
                decimalScore = Math.Round(decimalScore, 2);

                var level = scoreLevels.FirstOrDefault(l =>
                    (l.MinValue == null || decimalScore >= l.MinValue) &&
                    (l.MaxValue == null || decimalScore <= l.MaxValue));

                return level?.FivePointScaleScore ?? 0;
            }

            return 0;
        }

        private decimal CalculateSkillAverage(List<PlayerSkillScoreDto> playerSkillScores, TryOutMeasurementScale skill)
        {
            // Kiểm tra nếu skill là null
            if (skill == null)
            {
                return 0;
            }

            // Nếu là kỹ năng lá, trả về điểm số đã được mapping
            if (skill.InverseParentMeasurementScaleCodeNavigation == null || !skill.InverseParentMeasurementScaleCodeNavigation.Any())
            {
                var score = playerSkillScores?
                    .FirstOrDefault(s => s.MeasurementScaleCode == skill.MeasurementScaleCode)?
                    .Score ?? 0;

                return score;
            }

            // Nếu không phải là kỹ năng lá, tính trung bình các kỹ năng con
            decimal totalScore = 0;
            int count = 0;

            foreach (var subSkill in skill.InverseParentMeasurementScaleCodeNavigation)
            {
                var subSkillAverage = CalculateSkillAverage(playerSkillScores, subSkill);

                // Thêm điểm của kỹ năng con vào tổng điểm
                totalScore += subSkillAverage;
                count++;
            }

            // Tính điểm trung bình của kỹ năng cha
            var average = count > 0 ? totalScore / count : 0;
            return Math.Round(average, 2);
        }

        private List<SkillDetailDto> GetSkillDetails(List<PlayerSkillScoreDto> playerSkillScores, TryOutMeasurementScale basketballSkill, TryOutMeasurementScale physicalSkill)
        {
            var skillDetails = new List<SkillDetailDto>();

            // Nếu có kỹ năng bóng rổ, thêm các chi tiết kỹ năng tương ứng vào danh sách
            if (basketballSkill != null)
            {
                skillDetails.AddRange(GetSkillDetailsRecursive(playerSkillScores, basketballSkill));
            }

            // Nếu có kỹ năng thể chất, thêm các chi tiết kỹ năng tương ứng vào danh sách
            if (physicalSkill != null)
            {
                skillDetails.AddRange(GetSkillDetailsRecursive(playerSkillScores, physicalSkill));
            }

            // Sắp xếp danh sách kỹ năng theo thứ tự ưu tiên (SortOrder) và trả về
            return skillDetails.OrderBy(s => s.SortOrder).ToList();
        }

        // Đệ quy lấy danh sách chi tiết kỹ năng, bao gồm cả các kỹ năng con
        private List<SkillDetailDto> GetSkillDetailsRecursive(List<PlayerSkillScoreDto> playerSkillScores, TryOutMeasurementScale skill)
        {
            var skillDetails = new List<SkillDetailDto>();

            // Tạo một đối tượng SkillDetailDto từ kỹ năng hiện tại
            var skillDetail = new SkillDetailDto
            {
                MeasurementScaleCode = skill.MeasurementScaleCode,
                MeasurementName = skill.MeasurementName,
                SortOrder = skill.SortOrder,
                Score = playerSkillScores.FirstOrDefault(s => s.MeasurementScaleCode == skill.MeasurementScaleCode)?.Score ?? 0,
                AverageScore = CalculateSkillAverage(playerSkillScores, skill)
            };

            // Thêm đối tượng kỹ năng vào danh sách
            skillDetails.Add(skillDetail);

            // Kiểm tra xem kỹ năng này có kỹ năng con hay không
            if (skill.InverseParentMeasurementScaleCodeNavigation != null && skill.InverseParentMeasurementScaleCodeNavigation.Any())
            {
                // Nếu có, gọi đệ quy để lấy thông tin cho các kỹ năng con
                foreach (var subSkill in skill.InverseParentMeasurementScaleCodeNavigation)
                {
                    skillDetails.AddRange(GetSkillDetailsRecursive(playerSkillScores, subSkill));
                }
            }

            return skillDetails;
        }

        public async Task<ApiResponseModel<ReportFileResponse>> GetPlayerRegistrationSessionScoreReportAsync(int registrationSessionId, bool? gender)
        {
            var apiResponse = new ApiResponseModel<ReportFileResponse>();

            try
            {
                // Tạo file Excel
                using var workbook = new XLWorkbook();
                var leafSkillPointColIndexes = new Dictionary<string, int>();

                // Lấy thông tin phiên đăng ký
                var registrationSession = await _memberRegistrationSessionRepository.GetByIdAsync(registrationSessionId);
                if (registrationSession == null)
                {
                    apiResponse.Errors = new List<string> { MemberRegistrationSessionMessage.MemberRegistrationSessionErrorMessage.NotFoundRegistrationSession };
                    return apiResponse;
                }

                // Tạo file Excel
                await GenerateExcelFile(workbook, leafSkillPointColIndexes, registrationSession.RegistrationName);
                // In dữ liệu báo cáo
                await PrintTryOutReportData(gender, registrationSessionId, workbook, leafSkillPointColIndexes);

                // Lưu file vào MemoryStream
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Position = 0;

                // Tạo phản hồi với file báo cáo
                apiResponse.Data = new ReportFileResponse
                {
                    FileName = $"{TryOutScoreConstant.TryOutScoreReportFileName}_{registrationSession.RegistrationName}_{DateTime.Now:yyyyMMddHHmmss}.xlsx",
                    Data = stream.ToArray(),
                    ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                };
            }
            catch (Exception ex)
            {
                apiResponse.Errors = new List<string> { ex.Message };
            }

            return apiResponse;
        }

        private async Task GenerateExcelFile(XLWorkbook workbook, Dictionary<string, int> leafSkillPointColIndexes, string registrationSessionName)
        {
            // Lấy danh sách kỹ năng từ cơ sở dữ liệu
            var skills = await _tryOutMeasurementScaleRepository.GetSkillsToPrintAsync();
            // Xây dựng cây kỹ năng từ danh sách phẳng
            var skillTree = BuildSkillTree(skills);

            int depth = skillTree.Any() ? skillTree.Max(GetTreeDepth) : 0;
            int lastRowOfTitle = 2 + depth - 1;

            // Tạo sheet báo cáo
            var reportSheet = workbook.Worksheets.Add(TryOutScoreConstant.TryOutScoreReportSheetName);

            // In các thông tin cơ bản
            reportSheet.Range(2, TryOutReportPlayerInfColIndex.CandidateColumnIndex, lastRowOfTitle, TryOutReportPlayerInfColIndex.CandidateColumnIndex).Merge().Value = TryOutReportPlayerInfColHeaders.CandidateHeader;
            reportSheet.Range(2, TryOutReportPlayerInfColIndex.PlayerRegistrationIdColumnIndex, lastRowOfTitle, TryOutReportPlayerInfColIndex.PlayerRegistrationIdColumnIndex).Merge().Value = TryOutReportPlayerInfColHeaders.OrderCodeHeader;
            reportSheet.Range(2, TryOutReportPlayerInfColIndex.FullNameColumnIndex, lastRowOfTitle, TryOutReportPlayerInfColIndex.FullNameColumnIndex).Merge().Value = TryOutReportPlayerInfColHeaders.FullNameHeader;
            reportSheet.Range(2, TryOutReportPlayerInfColIndex.GenderColumnIndex, lastRowOfTitle, TryOutReportPlayerInfColIndex.GenderColumnIndex).Merge().Value = TryOutReportPlayerInfColHeaders.GenderHeader;
            reportSheet.Range(2, TryOutReportPlayerInfColIndex.BirthDateColumnIndex, lastRowOfTitle, TryOutReportPlayerInfColIndex.BirthDateColumnIndex).Merge().Value = TryOutReportPlayerInfColHeaders.BirthDateHeader;
            reportSheet.Range(2, TryOutReportPlayerInfColIndex.AverageScoreColumnIndex, lastRowOfTitle, TryOutReportPlayerInfColIndex.AverageScoreColumnIndex).Merge().Value = TryOutReportPlayerInfColHeaders.AverageScoreHeader;
            reportSheet.Range(2, TryOutReportPlayerInfColIndex.BasketballSkillAverageScoreColumnIndex, lastRowOfTitle, TryOutReportPlayerInfColIndex.BasketballSkillAverageScoreColumnIndex).Merge().Value = TryOutReportPlayerInfColHeaders.SkillScoreHeader;
            reportSheet.Range(2, TryOutReportPlayerInfColIndex.PhysicalFitnessAverageScoreColumnIndex, lastRowOfTitle, TryOutReportPlayerInfColIndex.PhysicalFitnessAverageScoreColumnIndex).Merge().Value = TryOutReportPlayerInfColHeaders.PhysicalScoreHeader;

            // In các kỹ năng
            int startCol = 9;
            foreach (var root in skillTree)
            {
                startCol = WriteSkillTreeToExcel(root, reportSheet, 2, startCol, skills, lastRowOfTitle, leafSkillPointColIndexes);
            }

            // In tiêu đề
            int lastUsedColumn = reportSheet.LastColumnUsed()!.ColumnNumber();
            reportSheet.Range(1, 1, 1, lastUsedColumn).Merge().Value = $"{TryOutScoreConstant.TryOutScoreReportFileName} {registrationSessionName}";

            // Căn giữa cho toàn bộ sheet
            reportSheet.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            reportSheet.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
        }

        private static int GetTreeDepth(TryOutMeasurementScale measurementScale)
        {
            if (!measurementScale.InverseParentMeasurementScaleCodeNavigation.Any())
                return 1;

            return 1 + measurementScale.InverseParentMeasurementScaleCodeNavigation.Max(GetTreeDepth);
        }

        private static List<TryOutMeasurementScale> BuildSkillTree(List<TryOutMeasurementScale> skills)
        {
            // Tạo một dic để tra cứu nhanh kỹ năng theo code
            var lookup = skills.ToDictionary(s => s.MeasurementScaleCode);

            foreach (var skill in skills)
            {
                // Nếu kỹ năng có cha, tìm kỹ năng cha trong dic
                if (skill.ParentMeasurementScaleCode != null && lookup.TryGetValue(skill.ParentMeasurementScaleCode, out var parent))
                {
                    // Đảm bảo danh sách các con đã được khởi tạo trước khi thêm để tránh lỗi null
                    parent.InverseParentMeasurementScaleCodeNavigation ??= new List<TryOutMeasurementScale>();

                    // Thêm kỹ năng hiện tại vào danh sách con của kỹ năng cha
                    parent.InverseParentMeasurementScaleCodeNavigation.Add(skill);
                }
            }

            // Trả về danh sách các kỹ năng gốc (không có kỹ năng cha)
            return skills.Where(s => s.ParentMeasurementScaleCode == null).ToList();
        }

        private static int WriteSkillTreeToExcel(TryOutMeasurementScale skill, IXLWorksheet worksheet, int row, int col, List<TryOutMeasurementScale> skills, int lastRowOfTitle, Dictionary<string, int> leafSkillPointColIndexes)
        {
            if (skill.InverseParentMeasurementScaleCodeNavigation.Any())
            {
                int startCol = col;
                foreach (var child in skill.InverseParentMeasurementScaleCodeNavigation)
                {
                    col = WriteSkillTreeToExcel(child, worksheet, row + 1, col, skills, lastRowOfTitle, leafSkillPointColIndexes);
                }

                // Gộp ô cho kỹ năng cha
                worksheet.Range(row, startCol, row, col - 1).Merge().Value = skill.MeasurementName;
            }
            else
            {
                leafSkillPointColIndexes.Add(skill.MeasurementScaleCode, col);
                worksheet.Range(row, col, lastRowOfTitle, col).Merge().Value = skill.MeasurementName;
                col++;
            }
            return col;
        }

        private async Task PrintTryOutReportData(bool? gender, int registrationSessionId, XLWorkbook workbook, Dictionary<string, int> leafSkillPointColIndexes)
        {
            var filter = new PlayerRegistrationScoresFilterDto { Gender = gender };
            var reportSheet = workbook.Worksheet(TryOutScoreConstant.TryOutScoreReportSheetName);
            var lastRowReportSheet = reportSheet.LastRowUsed()?.RowNumber() ?? 1;

            var reportData = await GetReportByRegistrationSessionIdAsync(registrationSessionId, filter);
            if (reportData.Data != null)
            {
                var players = reportData.Data.OrderByDescending(d => d.OverallAverage);
                var currentRow = lastRowReportSheet + 1;
                foreach (var player in players)
                {
                    reportSheet.Cell(currentRow, TryOutReportPlayerInfColIndex.CandidateColumnIndex).Value = player.CandidateNumber;
                    reportSheet.Cell(currentRow, TryOutReportPlayerInfColIndex.PlayerRegistrationIdColumnIndex).Value = player.PlayerRegistrationId;
                    reportSheet.Cell(currentRow, TryOutReportPlayerInfColIndex.FullNameColumnIndex).Value = player.FullName;
                    reportSheet.Cell(currentRow, TryOutReportPlayerInfColIndex.GenderColumnIndex).Value = player.Gender ? "Nam" : "Nữ";
                    reportSheet.Cell(currentRow, TryOutReportPlayerInfColIndex.BirthDateColumnIndex).Value = player.DateOfBirth.ToString("dd/MM/yyyy");
                    reportSheet.Cell(currentRow, TryOutReportPlayerInfColIndex.AverageScoreColumnIndex).Value = player.OverallAverage;
                    reportSheet.Cell(currentRow, TryOutReportPlayerInfColIndex.BasketballSkillAverageScoreColumnIndex).Value = player.AverageBasketballSkill;
                    reportSheet.Cell(currentRow, TryOutReportPlayerInfColIndex.PhysicalFitnessAverageScoreColumnIndex).Value = player.AveragePhysicalFitness;

                    foreach (var score in player.ScoreList)
                    {
                        if (leafSkillPointColIndexes.TryGetValue(score.MeasurementScaleCode, out int skillColIndex))
                        {
                            reportSheet.Cell(currentRow, skillColIndex).Value = score.Score;
                        }
                    }
                    currentRow++;
                }
            }

            var usedRange = reportSheet.RangeUsed();
            if (usedRange != null)
            {
                // Tô viền cho toàn bộ vùng
                usedRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                usedRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            }
        }
    }
}
