using BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Models;

namespace BAMS.Tests.ExerciseServiceTests
{
    internal static class ExerciseServiceTestExtensions
    {
        public static Task<ApiMessageModelV2<CreateExerciseRequest>?> InvokeValidateCreateExerciseRequestAsync(this ExerciseService service, CreateExerciseRequest request)
        {
            return (Task<ApiMessageModelV2<CreateExerciseRequest>?>)typeof(ExerciseService)
                .GetMethod("ValidateCreateExerciseRequestAsync", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(service, new object[] { request });
        }

        public static Task<ApiMessageModelV2<UpdateExerciseRequest>?> InvokeValidateUpdateExerciseRequestAsync(this ExerciseService service, UpdateExerciseRequest request, Exercise existingExercise)
        {
            return (Task<ApiMessageModelV2<UpdateExerciseRequest>?>)typeof(ExerciseService)
                .GetMethod("ValidateUpdateExerciseRequestAsync", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(service, new object[] { request, existingExercise });
        }
    }
}
