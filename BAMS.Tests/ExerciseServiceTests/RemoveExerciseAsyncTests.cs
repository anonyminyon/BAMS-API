using System.Security.Claims;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Moq;

namespace BAMS.Tests.ExerciseServiceTests
{
    public class RemoveExerciseAsyncTests
    {
        private readonly Mock<IExerciseRepository> _exerciseRepoMock;
        private readonly Mock<ITrainingSessionRepository> _trainingSessionRepoMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly ExerciseService _service;

        public RemoveExerciseAsyncTests()
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

        public async Task RemoveExerciseAsync_Should_ReturnError_When_ExerciseNotFound()
        {
            SetupHttpContext("user1");

            _exerciseRepoMock.Setup(r => r.GetExerciseByIdAsync("invalidId")).ReturnsAsync((Exercise?)null);

            var result = await _service.RemoveExerciseAsync("invalidId");

            Assert.Equal("Failed", result.Status);
            Assert.Contains("Exercise not found.", result.Errors);
        }

        [Fact]
        public async Task RemoveExerciseAsync_Should_ReturnError_When_UserNotCoach()
        {
            SetupHttpContext("user1");

            var exercise = new Exercise
            {
                ExerciseId = "ex1",
                TrainingSession = new TrainingSession { TeamId = "team1" }
            };

            _exerciseRepoMock.Setup(r => r.GetExerciseByIdAsync("ex1")).ReturnsAsync(exercise);
            _trainingSessionRepoMock.Setup(r => r.IsUserCoachOfTeamAsync("user1", "team1")).ReturnsAsync(false);

            var result = await _service.RemoveExerciseAsync("ex1");

            Assert.Equal("Failed", result.Status);
            Assert.Contains(ExerciseMessage.Error.OnlyTeamCoachCanRemoveExercise, result.Errors);
        }

        [Fact]
        public async Task RemoveExerciseAsync_Should_ReturnSuccess_When_RemoveSucceeds()
        {
            SetupHttpContext("coach1");

            var exercise = new Exercise
            {
                ExerciseId = "ex1",
                TrainingSession = new TrainingSession { TeamId = "team1" }
            };

            _exerciseRepoMock.Setup(r => r.GetExerciseByIdAsync("ex1")).ReturnsAsync(exercise);
            _trainingSessionRepoMock.Setup(r => r.IsUserCoachOfTeamAsync("coach1", "team1")).ReturnsAsync(true);
            _exerciseRepoMock.Setup(r => r.RemoveExerciseAsync(exercise)).ReturnsAsync(true);

            var result = await _service.RemoveExerciseAsync("ex1");

            Assert.Equal("Success", result.Status);
            Assert.True(result.Data);
        }

        [Fact]
        public async Task RemoveExerciseAsync_Should_ReturnError_When_RemoveFails()
        {
            SetupHttpContext("coach1");

            var exercise = new Exercise
            {
                ExerciseId = "ex1",
                TrainingSession = new TrainingSession { TeamId = "team1" }
            };

            _exerciseRepoMock.Setup(r => r.GetExerciseByIdAsync("ex1")).ReturnsAsync(exercise);
            _trainingSessionRepoMock.Setup(r => r.IsUserCoachOfTeamAsync("coach1", "team1")).ReturnsAsync(true);
            _exerciseRepoMock.Setup(r => r.RemoveExerciseAsync(exercise)).ReturnsAsync(false);

            var result = await _service.RemoveExerciseAsync("ex1");

            Assert.Equal("Failed", result.Status);
            Assert.Contains(ExerciseMessage.Error.ExerciseRemoveFailed, result.Errors);
        }
    }

}
