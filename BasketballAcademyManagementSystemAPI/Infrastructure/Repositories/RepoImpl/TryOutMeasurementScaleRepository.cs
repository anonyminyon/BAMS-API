using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class TryOutMeasurementScaleRepository : ITryOutMeasurementScaleRepository
    {
        private readonly BamsDbContext _dbContext;

        public TryOutMeasurementScaleRepository(BamsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TryOutMeasurementScale>> GetAllAsync()
        {
            return await _dbContext.TryOutMeasurementScales
                .Include(s => s.InverseParentMeasurementScaleCodeNavigation)
                .Include(s => s.TryOutScoreCriteria)
                    .ThenInclude(c => c.TryOutScoreLevels)
                .OrderBy(s => s.SortOrder)
                .ToListAsync();
        }

        public async Task<TryOutMeasurementScale?> GetByCodeAsync(string measurementScaleCode)
        {
            return await _dbContext.TryOutMeasurementScales
                .Include(s => s.InverseParentMeasurementScaleCodeNavigation)
                .Include(s => s.TryOutScoreCriteria)
                    .ThenInclude(c => c.TryOutScoreLevels)
                .FirstOrDefaultAsync(s => s.MeasurementScaleCode == measurementScaleCode);
        }

        public async Task<List<TryOutMeasurementScale>> GetLeafSkillsAsync()
        {
            return await _dbContext.TryOutMeasurementScales
                .Where(s => !s.InverseParentMeasurementScaleCodeNavigation.Any())
                .Include(s => s.TryOutScoreCriteria)
                    .ThenInclude(c => c.TryOutScoreLevels)
                .OrderBy(s => s.SortOrder)
                .ToListAsync();
        }

        public async Task<TryOutScoreCriterion?> GetCriterionBySkillAndGenderAsync(string measurementScaleCode, bool gender)
        {
            return await _dbContext.TryOutScoreCriteria
                .FirstOrDefaultAsync(c => c.MeasurementScaleCode == measurementScaleCode && c.Gender == gender);
        }

        public async Task<List<TryOutScoreLevel>> GetScoreLevelsByCriterionIdAsync(int scoreCriteriaId)
        {
            return await _dbContext.TryOutScoreLevels
                .Where(l => l.ScoreCriteriaId == scoreCriteriaId)
                .ToListAsync();
        }

        public async Task<TryOutMeasurementScale?> GetSkillTreeAsync(string measurementScaleCode)
        {
            return await _dbContext.TryOutMeasurementScales
                .Include(ms => ms.InverseParentMeasurementScaleCodeNavigation)
                .ThenInclude(subSkill => subSkill.InverseParentMeasurementScaleCodeNavigation)
                .AsSplitQuery()
                .FirstOrDefaultAsync(ms => ms.MeasurementScaleCode == measurementScaleCode);
        }

        public async Task<List<TryOutMeasurementScale>> GetSkillsToPrintAsync()
        {
            return await _dbContext.TryOutMeasurementScales
            .AsNoTracking()
            .OrderBy(s => s.SortOrder)
            .ToListAsync();
        }
    }
}
