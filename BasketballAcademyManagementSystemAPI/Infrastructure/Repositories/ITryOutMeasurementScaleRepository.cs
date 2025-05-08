using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface ITryOutMeasurementScaleRepository
    {
        Task<List<TryOutMeasurementScale>> GetAllAsync();
        Task<TryOutMeasurementScale?> GetByCodeAsync(string measurementScaleCode);
        Task<List<TryOutMeasurementScale>> GetLeafSkillsAsync();
        Task<TryOutScoreCriterion> GetCriterionBySkillAndGenderAsync(string measurementScaleCode, bool gender);
        Task<List<TryOutScoreLevel>> GetScoreLevelsByCriterionIdAsync(int scoreCriteriaId);
        Task<TryOutMeasurementScale?> GetSkillTreeAsync(string measurementScaleCode);
        Task<List<TryOutMeasurementScale>> GetSkillsToPrintAsync();
    }
}
