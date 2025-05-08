using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
    public interface IExerciseRepository
    {
        Task<Exercise?> GetExerciseByNameAsync(string trainingSessionId, string exerciseName);
        Task<IEnumerable<Exercise>> GetExercisesByTrainingSessionIdAsync(string trainingSessionId);
        Task AddExerciseAsync(Exercise exercise);
        Task<Exercise?> GetExerciseByIdAsync(string exerciseId);
        Task<bool> UpdateExerciseAsync(Exercise exercise);
        Task<bool> RemoveExerciseAsync(Exercise exercise);
    }
}
