using System.Security.Claims;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Moq;

namespace BAMS.Tests.ExerciseServiceTests
{
    public class ValidateUpdateExerciseRequestAsyncTests
    {
        private readonly Mock<IExerciseRepository> _exerciseRepoMock;
        private readonly Mock<ITrainingSessionRepository> _trainingSessionRepoMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly ExerciseService _service;

        public ValidateUpdateExerciseRequestAsyncTests()
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
        public async Task ValidateUpdateExerciseRequestAsync_Should_ReturnError_When_ExerciseNameIsEmpty()
        {
            var request = new UpdateExerciseRequest { ExerciseId = "1", ExerciseName = "", Duration = 10 };

            var result = await _service.InvokeValidateUpdateExerciseRequestAsync(request, new Exercise());

            Assert.NotNull(result);
            Assert.True(result.Errors.ContainsKey(nameof(request.ExerciseName)));
        }

        [Fact]
        public async Task ValidateUpdateExerciseRequestAsync_Should_ReturnError_When_DurationIsNegative()
        {
            var request = new UpdateExerciseRequest { ExerciseId = "1", ExerciseName = "Passing", Duration = -5 };

            var result = await _service.InvokeValidateUpdateExerciseRequestAsync(request, new Exercise());

            Assert.NotNull(result);
            Assert.True(result.Errors.ContainsKey(nameof(request.Duration)));
        }

        [Fact]
        public async Task ValidateUpdateExerciseRequestAsync_Should_ReturnError_When_ExistingExerciseNull()
        {
            var request = new UpdateExerciseRequest { ExerciseId = "1", ExerciseName = "Shooting", Duration = 10 };

            var result = await _service.InvokeValidateUpdateExerciseRequestAsync(request, null);

            Assert.NotNull(result);
            Assert.True(result.Errors.ContainsKey(nameof(request.ExerciseId)));
        }

        [Fact]
        public async Task ValidateUpdateExerciseRequestAsync_Should_ReturnError_When_TrainingSessionNotFound()
        {
            var request = new UpdateExerciseRequest { ExerciseId = "1", ExerciseName = "Dribbling", Duration = 10 };

            _trainingSessionRepoMock.Setup(r => r.GetTrainingSessionWithExcerciseBySessionIdAsync(It.IsAny<string>())).ReturnsAsync((TrainingSession?)null);

            var result = await _service.InvokeValidateUpdateExerciseRequestAsync(request, new Exercise { TrainingSessionId = "session1" });

            Assert.NotNull(result);
            Assert.True(result.Errors.ContainsKey("TrainingSessionId"));
        }

        [Fact]
        public async Task ValidateUpdateExerciseRequestAsync_Should_ReturnError_When_CurrentUserNotCoach()
        {
            var request = new UpdateExerciseRequest { ExerciseId = "1", ExerciseName = "Defense", Duration = 10 };
            SetupHttpContext("user1");

            _trainingSessionRepoMock.Setup(r => r.GetTrainingSessionWithExcerciseBySessionIdAsync("session1"))
                .ReturnsAsync(new TrainingSession { TeamId = "team1" });

            _trainingSessionRepoMock.Setup(r => r.IsUserCoachOfTeamAsync("user1", "team1")).ReturnsAsync(false);

            var result = await _service.InvokeValidateUpdateExerciseRequestAsync(request, new Exercise { TrainingSessionId = "session1" });

            Assert.NotNull(result);
            Assert.True(result.Errors.ContainsKey("TeamId"));
        }

        [Fact]
        public async Task ValidateUpdateExerciseRequestAsync_Should_ReturnError_When_CoachAssignmentInvalid()
        {
            var request = new UpdateExerciseRequest { ExerciseId = "1", ExerciseName = "Attack", Duration = 10, CoachId = "coach2" };
            SetupHttpContext("coach1");

            _trainingSessionRepoMock.Setup(r => r.GetTrainingSessionWithExcerciseBySessionIdAsync("session1"))
                .ReturnsAsync(new TrainingSession { TeamId = "team1" });

            _trainingSessionRepoMock.Setup(r => r.IsUserCoachOfTeamAsync("coach1", "team1")).ReturnsAsync(true);
            _trainingSessionRepoMock.Setup(r => r.IsUserCoachOfTeamAsync("coach2", "team1")).ReturnsAsync(false);

            var result = await _service.InvokeValidateUpdateExerciseRequestAsync(request, new Exercise { TrainingSessionId = "session1" });

            Assert.NotNull(result);
            Assert.True(result.Errors.ContainsKey(nameof(request.CoachId)));
        }

        [Fact]
        public async Task ValidateUpdateExerciseRequestAsync_Should_ReturnError_When_ExerciseNameAlreadyExists()
        {
            var request = new UpdateExerciseRequest { ExerciseId = "1", ExerciseName = "Defense", Duration = 10 };
            SetupHttpContext("coach1");

            _trainingSessionRepoMock.Setup(r => r.GetTrainingSessionWithExcerciseBySessionIdAsync("session1"))
                .ReturnsAsync(new TrainingSession { TeamId = "team1" });

            _trainingSessionRepoMock.Setup(r => r.IsUserCoachOfTeamAsync("coach1", "team1")).ReturnsAsync(true);
            _exerciseRepoMock.Setup(r => r.GetExerciseByNameAsync("session1", "Defense"))
                .ReturnsAsync(new Exercise { ExerciseId = "2" });

            var result = await _service.InvokeValidateUpdateExerciseRequestAsync(request, new Exercise { TrainingSessionId = "session1" });

            Assert.NotNull(result);
            Assert.True(result.Errors.ContainsKey(nameof(request.ExerciseName)));
        }

        [Fact]
        public async Task ValidateUpdateExerciseRequestAsync_Should_ReturnError_When_DurationExceedsSessionDuration()
        {
            var request = new UpdateExerciseRequest { ExerciseId = "1", ExerciseName = "Agility", Duration = 70 };
            SetupHttpContext("coach1");

            var trainingSession = new TrainingSession
            {
                TeamId = "team1",
                StartTime = TimeOnly.FromDateTime(DateTime.Now),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddMinutes(60)),
                Exercises = new List<Exercise> { new Exercise { ExerciseId = "2", Duration = 10 } }
            };

            _trainingSessionRepoMock.Setup(r => r.GetTrainingSessionWithExcerciseBySessionIdAsync("session1")).ReturnsAsync(trainingSession);
            _trainingSessionRepoMock.Setup(r => r.IsUserCoachOfTeamAsync("coach1", "team1")).ReturnsAsync(true);

            var result = await _service.InvokeValidateUpdateExerciseRequestAsync(request, new Exercise { TrainingSessionId = "session1" });

            Assert.NotNull(result);
            Assert.True(result.Errors.ContainsKey(nameof(request.Duration)));
        }

        [Fact]
        public async Task ValidateUpdateExerciseRequestAsync_Should_ReturnNull_When_AllValid()
        {
            var request = new UpdateExerciseRequest { ExerciseId = "1", ExerciseName = "Shooting", Duration = 10 };
            SetupHttpContext("coach1");

            var trainingSession = new TrainingSession
            {
                TeamId = "team1",
                StartTime = TimeOnly.FromDateTime(DateTime.Now),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddMinutes(90)),
                Exercises = new List<Exercise>()
            };

            _trainingSessionRepoMock.Setup(r => r.GetTrainingSessionWithExcerciseBySessionIdAsync("session1")).ReturnsAsync(trainingSession);
            _trainingSessionRepoMock.Setup(r => r.IsUserCoachOfTeamAsync("coach1", "team1")).ReturnsAsync(true);
            _exerciseRepoMock.Setup(r => r.GetExerciseByNameAsync("session1", "Shooting")).ReturnsAsync((Exercise?)null);

            var result = await _service.InvokeValidateUpdateExerciseRequestAsync(request, new Exercise { TrainingSessionId = "session1" });

            Assert.Null(result);
        }
    }
}
