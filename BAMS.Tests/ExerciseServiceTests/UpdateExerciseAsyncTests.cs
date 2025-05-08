using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Moq;

namespace BAMS.Tests.ExerciseServiceTests
{
    public class UpdateExerciseAsyncTests
    {
        private readonly Mock<IExerciseRepository> _exerciseRepoMock;
        private readonly Mock<ITrainingSessionRepository> _trainingSessionRepoMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly ExerciseService _service;

        public UpdateExerciseAsyncTests()
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
        public async Task UpdateExerciseAsync_Should_ReturnError_When_ExerciseNotFound()
        {
            var request = new UpdateExerciseRequest { ExerciseId = "notfound", ExerciseName = "New Name", Duration = 10 };

            _exerciseRepoMock.Setup(r => r.GetExerciseByIdAsync(request.ExerciseId)).ReturnsAsync((Exercise?)null);

            var result = await _service.UpdateExerciseAsync(request);

            Assert.Equal("Failed", result.Status);
            Assert.True(result.Errors.ContainsKey(nameof(request.ExerciseId)));
        }

        [Fact]
        public async Task UpdateExerciseAsync_Should_ReturnError_When_ValidationFails()
        {
            var request = new UpdateExerciseRequest { ExerciseId = "1", ExerciseName = "", Duration = -1 };

            _exerciseRepoMock.Setup(r => r.GetExerciseByIdAsync(request.ExerciseId)).ReturnsAsync(new Exercise());

            var result = await _service.UpdateExerciseAsync(request);

            Assert.Equal("Failed", result.Status);
            Assert.True(result.Errors.ContainsKey(nameof(request.ExerciseName)));
            Assert.True(result.Errors.ContainsKey(nameof(request.Duration)));
        }

        [Fact]
        public async Task UpdateExerciseAsync_Should_ReturnSuccess_When_UpdateSucceeds()
        {
            var request = new UpdateExerciseRequest { ExerciseId = "1", ExerciseName = "Updated Exercise", Duration = 30 };

            _exerciseRepoMock.Setup(r => r.GetExerciseByIdAsync(request.ExerciseId)).ReturnsAsync(new Exercise { ExerciseId = "1", TrainingSessionId = "session1" });
            _trainingSessionRepoMock.Setup(r => r.GetTrainingSessionWithExcerciseBySessionIdAsync("session1")).ReturnsAsync(new TrainingSession { TeamId = "team1", StartTime = TimeOnly.FromDateTime(DateTime.Now), EndTime = TimeOnly.FromDateTime(DateTime.Now.AddMinutes(60)), Exercises = new List<Exercise>() });
            _trainingSessionRepoMock.Setup(r => r.IsUserCoachOfTeamAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            _exerciseRepoMock.Setup(r => r.GetExerciseByNameAsync("session1", "Updated Exercise")).ReturnsAsync((Exercise?)null);
            _exerciseRepoMock.Setup(r => r.UpdateExerciseAsync(It.IsAny<Exercise>())).ReturnsAsync(true);

            SetupHttpContext("coach1");

            var result = await _service.UpdateExerciseAsync(request);

            Assert.Equal("Success", result.Status);
            Assert.Equal(ExerciseMessage.Success.ExerciseUpdatedSuccessfully, result.Message);
            Assert.Equal(request, result.Data);
        }
    }

}
