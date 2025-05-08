using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using static BasketballAcademyManagementSystemAPI.Common.Messages.ApiResponseMessage;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class TryOutMeasurementScaleService : ITryOutMeasurementScaleService
    {
        private readonly ITryOutMeasurementScaleRepository _repository;

        public TryOutMeasurementScaleService(ITryOutMeasurementScaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponseModel<List<TryOutMeasurementScaleDto>>> GetSkillTreeAsync()
        {
            var apiResponse = new ApiResponseModel<List<TryOutMeasurementScaleDto>>();

            try
            {
                var skills = await _repository.GetAllAsync();
                var rootSkills = skills.Where(s => s.ParentMeasurementScaleCode == null)
                                       .OrderBy(s => s.SortOrder)
                                       .Select(MapToDto)
                                       .ToList();

                apiResponse.Data = rootSkills;
                apiResponse.Status = ApiResponseStatusConstant.SuccessStatus;
                apiResponse.Message = ApiResponseSuccessMessage.ApiSuccessMessage;
            }
            catch (Exception ex)
            {
                apiResponse.Errors = [ex.Message];
            }

            return apiResponse;
        }

        // Chuyển đổi từ Entity sang DTO (dùng đệ quy)
        private TryOutMeasurementScaleDto MapToDto(TryOutMeasurementScale skill)
        {
            return new TryOutMeasurementScaleDto
            {
                MeasurementScaleCode = skill.MeasurementScaleCode,
                MeasurementName = skill.MeasurementName,
                Content = skill.Content,
                Duration = skill.Duration,
                Location = skill.Location,
                Description = skill.Description,
                Equipment = skill.Equipment,
                MeasurementScale = skill.MeasurementScale,
                SortOrder = skill.SortOrder,
                SubScales = skill.InverseParentMeasurementScaleCodeNavigation
                    .OrderBy(s => s.SortOrder)
                    .Select(MapToDto)
                    .ToList(),
                ScoreCriteria = skill.TryOutScoreCriteria.Any()
                    ? skill.TryOutScoreCriteria.Select(c => new TryOutScoreCriterionDto
                    {
                        ScoreCriteriaId = c.ScoreCriteriaId,
                        CriteriaName = c.CriteriaName,
                        Unit = c.Unit,
                        Gender = c.Gender,
                        ScoreLevels = c.TryOutScoreLevels.Select(l => new TryOutScoreLevelDto
                        {
                            MinValue = l.MinValue,
                            MaxValue = l.MaxValue,
                            ScoreLevel = l.ScoreLevel,
                            FiveScaleScore = l.FivePointScaleScore.ToString(),
                        }).ToList()
                    }).ToList()
                    : null
            };
        }

        public async Task<ApiResponseModel<TryOutMeasurementScaleDto>> GetSkillByCodeAsync(string measurementScaleCode)
        {
            var apiResponse = new ApiResponseModel<TryOutMeasurementScaleDto>();

            try
            {
                var skill = await _repository.GetByCodeAsync(measurementScaleCode);

                if (skill != null)
                {
                    apiResponse.Data = MapToDto(skill);
                    apiResponse.Status = ApiResponseStatusConstant.SuccessStatus;
                    apiResponse.Message = ApiResponseSuccessMessage.ApiSuccessMessage;
                }

            } catch (Exception ex)
            {
                apiResponse.Errors = [ex.Message];
            }
            
            return apiResponse;
        }

        public async Task<ApiResponseModel<List<TryOutMeasurementScaleDto>>> GetLeafSkillsAsync()
        {
            var apiResponse = new ApiResponseModel<List<TryOutMeasurementScaleDto>>();

            try
            {
                var leafSkills = await _repository.GetLeafSkillsAsync();

                var resultSkills = leafSkills.OrderBy(s => s.SortOrder)
                                       .Select(MapToDto)
                                       .ToList();

                apiResponse.Data = resultSkills;
                apiResponse.Status= ApiResponseStatusConstant.SuccessStatus;
                apiResponse.Message= ApiResponseSuccessMessage.ApiSuccessMessage;
            }
            catch (Exception ex)
            {
                apiResponse.Errors = [ex.Message];
            }

            return apiResponse;
        }

    }
}
