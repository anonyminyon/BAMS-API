using System.Security.Claims;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using BasketballAcademyManagementSystemAPI.Common.Messages;

namespace BAMS.Tests.ExerciseServiceTests
{
    public class AddExerciseForTrainingSessionAsyncTests
    {
        private readonly Mock<IExerciseRepository> _exerciseRepoMock;
        private readonly Mock<ITrainingSessionRepository> _trainingSessionRepoMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly ExerciseService _service;

        public AddExerciseForTrainingSessionAsyncTests()
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
        public async Task AddExerciseForTrainingSessionAsync_Should_ReturnValidationError_When_ValidationFails()
        {
            var request = new CreateExerciseRequest { ExerciseName = " ", Duration = 10, TrainingSessionId = "session1" };

            var result = await _service.AddExerciseForTrainingSessionAsync(request);

            Assert.NotNull(result);
            Assert.Equal("Failed", result.Status);
            Assert.True(result.Errors.ContainsKey(nameof(request.ExerciseName)));
            _exerciseRepoMock.Verify(r => r.AddExerciseAsync(It.IsAny<Exercise>()), Times.Never);
        }

        [Fact]
        public async Task AddExerciseForTrainingSessionAsync_Should_AddExercise_When_ValidationPasses()
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

            var result = await _service.AddExerciseForTrainingSessionAsync(request);

            Assert.NotNull(result);
            Assert.Equal("Success", result.Status);
            _exerciseRepoMock.Verify(r => r.AddExerciseAsync(It.IsAny<Exercise>()), Times.Once);
        }

        [Fact]
        public async Task AddExerciseForTrainingSessionAsync_Should_ReturnSuccessResponse()
        {
            var request = new CreateExerciseRequest { ExerciseName = "Agility", Duration = 15, TrainingSessionId = "session1" };
            SetupHttpContext("coach1");

            var trainingSession = new TrainingSession
            {
                TeamId = "team1",
                StartTime = TimeOnly.FromDateTime(DateTime.Now),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddMinutes(90)),
                Exercises = new List<Exercise>()
            };

            _trainingSessionRepoMock.Setup(r => r.GetTrainingSessionWithExcerciseBySessionIdAsync(request.TrainingSessionId)).ReturnsAsync(trainingSession);
            _trainingSessionRepoMock.Setup(r => r.IsUserCoachOfTeamAsync("coach1", "team1")).ReturnsAsync(true);
            _exerciseRepoMock.Setup(r => r.GetExerciseByNameAsync(request.TrainingSessionId, request.ExerciseName)).ReturnsAsync((Exercise?)null);

            var result = await _service.AddExerciseForTrainingSessionAsync(request);

            Assert.NotNull(result);
            Assert.Equal("Success", result.Status);
            Assert.Equal(ExerciseMessage.Success.ExerciseCreatedSuccessfully, result.Message);
            Assert.Equal(request, result.Data);
        }
    }
}
