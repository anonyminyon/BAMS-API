using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using static BasketballAcademyManagementSystemAPI.Common.Messages.ApiResponseMessage;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly ITrainingSessionRepository _trainingSessionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExerciseService(IExerciseRepository exerciseRepository, ITrainingSessionRepository trainingSessionRepository, IHttpContextAccessor httpContextAccessor)
        {
            _exerciseRepository = exerciseRepository;
            _trainingSessionRepository = trainingSessionRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiMessageModelV2<CreateExerciseRequest>> AddExerciseForTrainingSessionAsync(CreateExerciseRequest request)
        {
            var response = new ApiMessageModelV2<CreateExerciseRequest>();

            var validationResult = await ValidateCreateExerciseRequestAsync(request);
            if (validationResult != null)
            {
                return validationResult;
            }

            var currentUserId = TokenHelper.GetCurrentUserId(_httpContextAccessor);
            var exercise = new Exercise
            {
                ExerciseId = Guid.NewGuid().ToString(),
                TrainingSessionId = request.TrainingSessionId,
                ExerciseName = request.ExerciseName,
                Description = request.Description,
                Duration = request.Duration,
                CoachId = request.CoachId,
                CreatedAt = DateTime.Now
            };

            await _exerciseRepository.AddExerciseAsync(exercise);

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = ExerciseMessage.Success.ExerciseCreatedSuccessfully;
            response.Data = request;

            return response;
        }

        private async Task<ApiMessageModelV2<CreateExerciseRequest>?> ValidateCreateExerciseRequestAsync(CreateExerciseRequest request)
        {
            var errors = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(request.ExerciseName))
            {
                errors.Add(nameof(request.ExerciseName), ExerciseMessage.Error.ExerciseNameCannotBeEmpty);
            }

            if (request.Duration < 0)
            {
                errors.Add(nameof(request.Duration), ExerciseMessage.Error.ExerciseDurationCannotBeNegative);
            }

            var trainingSession = await _trainingSessionRepository.GetTrainingSessionWithExcerciseBySessionIdAsync(request.TrainingSessionId);
            if (trainingSession == null)
            {
                errors.Add(nameof(request.TrainingSessionId), ExerciseMessage.Error.InvalidTrainingSessionID);
            }
            else
            {
                var currentUserId = TokenHelper.GetCurrentUserId(_httpContextAccessor);
                if (!await _trainingSessionRepository.IsUserCoachOfTeamAsync(currentUserId, trainingSession.TeamId))
                {
                    errors.Add(nameof(trainingSession.TeamId), ExerciseMessage.Error.OnlyTeamCoachCanAddExercise);
                }
                else
                {
                    if (request.CoachId != null && currentUserId != request.CoachId)
                    {
                        if (!await _trainingSessionRepository.IsUserCoachOfTeamAsync(request.CoachId, trainingSession.TeamId))
                        {
                            errors.Add(nameof(request.CoachId), ExerciseMessage.Error.CoachAssignmentError);
                        }
                    }

                    var existingExercise = await _exerciseRepository.GetExerciseByNameAsync(request.TrainingSessionId, request.ExerciseName);
                    if (existingExercise != null)
                    {
                        errors.Add(nameof(request.ExerciseName), ExerciseMessage.Error.ExerciseNameAlreadyExists);
                    }

                    var totalDuration = (decimal)(trainingSession.EndTime - trainingSession.StartTime).TotalMinutes;
                    var existingDuration = trainingSession.Exercises.Sum(e => e.Duration) ?? 0;
                    var newExerciseDuration = request.Duration ?? 0;

                    if ((existingDuration + newExerciseDuration) > totalDuration)
                    {
                        errors.Add(nameof(request.Duration), ExerciseMessage.Error.ExerciseDurationExceedsSessionDuration);
                    }
                }
            }

            if (errors.Any())
            {
                return AddExerciseErrorResponse(errors, ExerciseMessage.Error.ExerciseCreationFailed);
            }

            return null;
        }

        private ApiMessageModelV2<CreateExerciseRequest> AddExerciseErrorResponse(Dictionary<string, string> errors, string message)
        {
            return new ApiMessageModelV2<CreateExerciseRequest>
            {
                Status = ApiResponseStatusConstant.FailedStatus,
                Message = message,
                Errors = errors
            };
        }

        public async Task<ApiMessageModelV2<UpdateExerciseRequest>> UpdateExerciseAsync(UpdateExerciseRequest request)
        {
            var response = new ApiMessageModelV2<UpdateExerciseRequest>();

            // Fetch existing exercise
            var existingExercise = await _exerciseRepository.GetExerciseByIdAsync(request.ExerciseId);
            if (existingExercise == null)
            {
                response.Status = ApiResponseStatusConstant.FailedStatus;
                response.Errors = new Dictionary<string, string> { { nameof(request.ExerciseId), ExerciseMessage.Error.ExerciseNotFound } };
                return response;
            }

            // Validate request
            var validationResult = await ValidateUpdateExerciseRequestAsync(request, existingExercise);
            if (validationResult != null)
            {
                return validationResult;
            }

            // Update exercise properties
            existingExercise.ExerciseName = request.ExerciseName;
            existingExercise.Description = request.Description;
            existingExercise.Duration = request.Duration;
            existingExercise.CoachId = request.CoachId;
            existingExercise.UpdatedAt = DateTime.Now;

            // Save changes
            var updateResult = await _exerciseRepository.UpdateExerciseAsync(existingExercise);
            if (!updateResult)
            {
                response.Status = ApiResponseStatusConstant.FailedStatus;
                response.Message = ExerciseMessage.Error.ExerciseUpdateFailed;
                return response;
            }

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = ExerciseMessage.Success.ExerciseUpdatedSuccessfully;
            response.Data = request;
            return response;
        }

        private async Task<ApiMessageModelV2<UpdateExerciseRequest>?> ValidateUpdateExerciseRequestAsync(UpdateExerciseRequest request, Exercise existingExercise)
        {
            var errors = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(request.ExerciseName))
            {
                errors.Add(nameof(request.ExerciseName), ExerciseMessage.Error.ExerciseNameCannotBeEmpty);
            }

            if (request.Duration < 0)
            {
                errors.Add(nameof(request.Duration), ExerciseMessage.Error.ExerciseDurationCannotBeNegative);
            }

            if (existingExercise == null)
            {
                errors.Add(nameof(request.ExerciseId), ExerciseMessage.Error.ExerciseNotFound);
            }
            else
            {
                var trainingSession = await _trainingSessionRepository.GetTrainingSessionWithExcerciseBySessionIdAsync(existingExercise.TrainingSessionId);
                if (trainingSession == null)
                {
                    errors.Add(nameof(existingExercise.TrainingSessionId), ExerciseMessage.Error.InvalidTrainingSessionID);
                }
                else
                {
                    var currentUserId = TokenHelper.GetCurrentUserId(_httpContextAccessor);
                    if (!await _trainingSessionRepository.IsUserCoachOfTeamAsync(currentUserId, trainingSession.TeamId))
                    {
                        errors.Add(nameof(trainingSession.TeamId), ExerciseMessage.Error.OnlyTeamCoachCanEditExercise);
                    }
                    else
                    {
                        if (request.CoachId != null && currentUserId != request.CoachId)
                        {
                            if (!await _trainingSessionRepository.IsUserCoachOfTeamAsync(request.CoachId, trainingSession.TeamId))
                            {
                                errors.Add(nameof(request.CoachId), ExerciseMessage.Error.CoachAssignmentError);
                            }
                        }

                        var existingExerciseWithName = await _exerciseRepository.GetExerciseByNameAsync(existingExercise.TrainingSessionId, request.ExerciseName);
                        if (existingExerciseWithName != null && existingExerciseWithName.ExerciseId != request.ExerciseId)
                        {
                            errors.Add(nameof(request.ExerciseName), ExerciseMessage.Error.ExerciseNameAlreadyExists);
                        }

                        var totalDuration = (decimal)(trainingSession.EndTime - trainingSession.StartTime).TotalMinutes;
                        var existingDuration = trainingSession.Exercises.Where(e => e.ExerciseId != request.ExerciseId).Sum(e => e.Duration) ?? 0;
                        var newExerciseDuration = request.Duration ?? 0;

                        if ((existingDuration + newExerciseDuration) > totalDuration)
                        {
                            errors.Add(nameof(request.Duration), ExerciseMessage.Error.ExerciseDurationExceedsSessionDuration);
                        }
                    }
                }
            }

            if (errors.Any())
            {
                return new ApiMessageModelV2<UpdateExerciseRequest>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ExerciseMessage.Error.ExerciseUpdateFailed,
                    Errors = errors
                };
            }

            return null;
        }

        // Remove an exercise
        public async Task<ApiResponseModel<bool>> RemoveExerciseAsync(string exerciseId)
        {
            var response = new ApiResponseModel<bool>();
            var currentUserId = TokenHelper.GetCurrentUserId(_httpContextAccessor);
            var exercise = await _exerciseRepository.GetExerciseByIdAsync(exerciseId);

            // Check is exercise exists
            if (exercise == null)
            {
                response.Message = ExerciseMessage.Error.ExerciseRemoveFailed;
                response.Errors = [ExerciseMessage.Error.ExerciseNotFound];
                return response;
            }

            // Check if user is coach of the team
            if (!await _trainingSessionRepository.IsUserCoachOfTeamAsync(currentUserId, exercise.TrainingSession.TeamId))
            {
                response.Message = ExerciseMessage.Error.ExerciseRemoveFailed;
                response.Errors = [ExerciseMessage.Error.OnlyTeamCoachCanRemoveExercise];
                return response;
            }

            // Remove exercise
            var result = await _exerciseRepository.RemoveExerciseAsync(exercise);

            // Check if exercise is removed
            if (result)
            {
                response.Status = ApiResponseStatusConstant.SuccessStatus;
                response.Message = ExerciseMessage.Success.ExerciseRemovedSuccessfully;
                response.Data = true;
            }
            else
            {
                response.Status = ApiResponseStatusConstant.FailedStatus;
                response.Message = ExerciseMessage.Error.ExerciseRemoveFailed;
                response.Errors = [ExerciseMessage.Error.ExerciseRemoveFailed];
                response.Data = false;
            }

            return response;
        }
    }
}
