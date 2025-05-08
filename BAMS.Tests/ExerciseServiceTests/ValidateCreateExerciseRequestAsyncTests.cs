using System.Security.Claims;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Moq;

namespace BAMS.Tests.ExerciseServiceTests
{
    public class ValidateCreateExerciseRequestAsyncTests
    {
        private readonly Mock<IExerciseRepository> _exerciseRepoMock;
        private readonly Mock<ITrainingSessionRepository> _trainingSessionRepoMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly ExerciseService _service;

        public ValidateCreateExerciseRequestAsyncTests()
        {
            _exerciseRepoMock = new Mock<IExerciseRepository>();
            _trainingSessionRepoMock = new Mock<ITrainingSessionRepository>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _service = new ExerciseService(_exerciseRepoMock.Object, _trainingSessionRepoMock.Object, _httpContextAccessorMock.Object);
        }

        private void SetupHttpContext(string userId)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId)
            }, "mock"));

            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(user);
        }

        [Fact]
        public async Task ValidateCreateExerciseRequestAsync_Should_ReturnError_When_ExerciseNameIsEmpty()
        {
            // Arrange
            var request = new CreateExerciseRequest { ExerciseName = " ", Duration = 10, TrainingSessionId = "session1" };

            // Act
            var result = await _service.InvokeValidateCreateExerciseRequestAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Errors.ContainsKey(nameof(request.ExerciseName)));
        }

        [Fact]
        public async Task ValidateCreateExerciseRequestAsync_Should_ReturnError_When_DurationIsNegative()
        {
            var request = new CreateExerciseRequest { ExerciseName = "Dribbling", Duration = -5, TrainingSessionId = "session1" };

            var result = await _service.InvokeValidateCreateExerciseRequestAsync(request);

            Assert.NotNull(result);
            Assert.True(result.Errors.ContainsKey(nameof(request.Duration)));
        }

        [Fact]
        public async Task ValidateCreateExerciseRequestAsync_Should_ReturnError_When_TrainingSessionNotFound()
        {
            var request = new CreateExerciseRequest { ExerciseName = "Dribbling", Duration = 10, TrainingSessionId = "invalidSession" };

            _trainingSessionRepoMock.Setup(r => r.GetTrainingSessionWithExcerciseBySessionIdAsync(request.TrainingSessionId)).ReturnsAsync((TrainingSession?)null);

            var result = await _service.InvokeValidateCreateExerciseRequestAsync(request);

            Assert.NotNull(result);
            Assert.True(result.Errors.ContainsKey(nameof(request.TrainingSessionId)));
        }

        [Fact]
        public async Task ValidateCreateExerciseRequestAsync_Should_ReturnError_When_CurrentUserNotCoach()
        {
            var request = new CreateExerciseRequest { ExerciseName = "Shooting", Duration = 10, TrainingSessionId = "session1" };
            SetupHttpContext("user1");

            _trainingSessionRepoMock.Setup(r => r.GetTrainingSessionWithExcerciseBySessionIdAsync(request.TrainingSessionId))
                .ReturnsAsync(new TrainingSession { TeamId = "team1", StartTime = TimeOnly.FromDateTime(DateTime.Now), EndTime = TimeOnly.FromDateTime(DateTime.Now.AddMinutes(60)) });

            _trainingSessionRepoMock.Setup(r => r.IsUserCoachOfTeamAsync("user1", "team1")).ReturnsAsync(false);

            var result = await _service.InvokeValidateCreateExerciseRequestAsync(request);

            Assert.NotNull(result);
            Assert.True(result.Errors.ContainsKey("TeamId"));
        }

        [Fact]
        public async Task ValidateCreateExerciseRequestAsync_Should_ReturnError_When_CoachAssignmentInvalid()
        {
            var request = new CreateExerciseRequest { ExerciseName = "Defense", Duration = 10, TrainingSessionId = "session1", CoachId = "coach2" };
            SetupHttpContext("coach1");

            _trainingSessionRepoMock.Setup(r => r.GetTrainingSessionWithExcerciseBySessionIdAsync(request.TrainingSessionId))
                .ReturnsAsync(new TrainingSession { TeamId = "team1", StartTime = TimeOnly.FromDateTime(DateTime.Now), EndTime = TimeOnly.FromDateTime(DateTime.Now.AddMinutes(60)) });

            _trainingSessionRepoMock.Setup(r => r.IsUserCoachOfTeamAsync("coach1", "team1")).ReturnsAsync(true);
            _trainingSessionRepoMock.Setup(r => r.IsUserCoachOfTeamAsync("coach2", "team1")).ReturnsAsync(false);

            var result = await _service.InvokeValidateCreateExerciseRequestAsync(request);

            Assert.NotNull(result);
            Assert.True(result.Errors.ContainsKey(nameof(request.CoachId)));
        }

        [Fact]
        public async Task ValidateCreateExerciseRequestAsync_Should_ReturnError_When_ExerciseNameAlreadyExists()
        {
            var request = new CreateExerciseRequest { ExerciseName = "Defense", Duration = 10, TrainingSessionId = "session1" };
            SetupHttpContext("coach1");

            _trainingSessionRepoMock.Setup(r => r.GetTrainingSessionWithExcerciseBySessionIdAsync(request.TrainingSessionId))
                .ReturnsAsync(new TrainingSession { TeamId = "team1", StartTime = TimeOnly.FromDateTime(DateTime.Now), EndTime = TimeOnly.FromDateTime(DateTime.Now.AddMinutes(60)) });

            _trainingSessionRepoMock.Setup(r => r.IsUserCoachOfTeamAsync("coach1", "team1")).ReturnsAsync(true);
            _exerciseRepoMock.Setup(r => r.GetExerciseByNameAsync(request.TrainingSessionId, request.ExerciseName))
                .ReturnsAsync(new Exercise());

            var result = await _service.InvokeValidateCreateExerciseRequestAsync(request);

            Assert.NotNull(result);
            Assert.True(result.Errors.ContainsKey(nameof(request.ExerciseName)));
        }

        [Fact]
        public async Task ValidateCreateExerciseRequestAsync_Should_ReturnError_When_DurationExceedsSessionDuration()
        {
            var request = new CreateExerciseRequest { ExerciseName = "Endurance", Duration = 50, TrainingSessionId = "session1" };
            SetupHttpContext("coach1");

            var trainingSession = new TrainingSession
            {
                TeamId = "team1",
                StartTime = TimeOnly.FromDateTime(DateTime.Now),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddMinutes(60)),
                Exercises = new List<Exercise> { new Exercise { Duration = 15 } }
            };

            _trainingSessionRepoMock.Setup(r => r.GetTrainingSessionWithExcerciseBySessionIdAsync(request.TrainingSessionId)).ReturnsAsync(trainingSession);
            _trainingSessionRepoMock.Setup(r => r.IsUserCoachOfTeamAsync("coach1", "team1")).ReturnsAsync(true);

            var result = await _service.InvokeValidateCreateExerciseRequestAsync(request);

            Assert.NotNull(result);
            Assert.True(result.Errors.ContainsKey(nameof(request.Duration)));
        }

        [Fact]
        public async Task ValidateCreateExerciseRequestAsync_Should_ReturnNull_When_AllValid()
        {
            var request = new CreateExerciseRequest { ExerciseName = "Strength", Duration = 10, TrainingSessionId = "session1" };
            SetupHttpContext("coach1");

            var trainingSession = new TrainingSession
            {
                TeamId = "team1",
                StartTime = TimeOnly.FromDateTime(DateTime.Now),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddMinutes(60)),
                Exercises = new List<Exercise>()
            };

            _trainingSessionRepoMock.Setup(r => r.GetTrainingSessionWithExcerciseBySessionIdAsync(request.TrainingSessionId)).ReturnsAsync(trainingSession);
            _trainingSessionRepoMock.Setup(r => r.IsUserCoachOfTeamAsync("coach1", "team1")).ReturnsAsync(true);
            _exerciseRepoMock.Setup(r => r.GetExerciseByNameAsync(request.TrainingSessionId, request.ExerciseName)).ReturnsAsync((Exercise?)null);

            var result = await _service.InvokeValidateCreateExerciseRequestAsync(request);

            Assert.Null(result);
        }
    }
}
