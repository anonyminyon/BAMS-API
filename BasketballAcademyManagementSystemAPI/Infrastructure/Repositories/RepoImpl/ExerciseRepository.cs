using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly BamsDbContext _dbContext;

        public ExerciseRepository(BamsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Exercise?> GetExerciseByNameAsync(string trainingSessionId, string exerciseName)
        {
            return await _dbContext.Exercises
                .FirstOrDefaultAsync(e => e.TrainingSessionId == trainingSessionId && e.ExerciseName == exerciseName);
        }

        public async Task<IEnumerable<Exercise>> GetExercisesByTrainingSessionIdAsync(string trainingSessionId)
        {
            return await _dbContext.Exercises
                .Where(e => e.TrainingSessionId == trainingSessionId)
                .ToListAsync();
        }

        public async Task AddExerciseAsync(Exercise exercise)
        {
            await _dbContext.Exercises.AddAsync(exercise);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Exercise?> GetExerciseByIdAsync(string exerciseId)
        {
            return await _dbContext.Exercises
                .Where(e => e.ExerciseId == exerciseId)
                .Include(e => e.TrainingSession)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateExerciseAsync(Exercise exercise)
        {
            _dbContext.Exercises.Update(exercise);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveExerciseAsync(Exercise exercise)
        {
            _dbContext.Exercises.Remove(exercise);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
