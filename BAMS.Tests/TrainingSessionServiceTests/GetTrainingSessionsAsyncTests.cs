using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.Extensions.Configuration;
using BasketballAcademyManagementSystemAPI.Common.Constants;

namespace BAMS.Tests.TrainingSessionServiceTests
{
    public class GetTrainingSessionsAsyncTests
    {
        private readonly Mock<ITrainingSessionRepository> _trainingSessionRepositoryMock;
        private readonly Mock<ICourtRepository> _courtRepositoryMock;
        private readonly Mock<IMailTemplateRepository> _mailTemplateRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly TrainingSessionService _service;

        public GetTrainingSessionsAsyncTests()
        {
            _trainingSessionRepositoryMock = new Mock<ITrainingSessionRepository>();
            _courtRepositoryMock = new Mock<ICourtRepository>();
            _mailTemplateRepositoryMock = new Mock<IMailTemplateRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _emailServiceMock = new Mock<IEmailService>();
            _configurationMock = new Mock<IConfiguration>();

            _service = new TrainingSessionService(
                _trainingSessionRepositoryMock.Object,
                _courtRepositoryMock.Object,
                _mailTemplateRepositoryMock.Object,
                _userRepositoryMock.Object,
                _httpContextAccessorMock.Object,
                _configurationMock.Object,
                _emailServiceMock.Object
            );

            SetupHttpContext("user123");
        }

        private void SetupHttpContext(string userId)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);
            var context = new DefaultHttpContext { User = principal };
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(context);
        }

        private List<TrainingSession> GetFakeTrainingSessions()
        {
            return new List<TrainingSession>
            {
                new TrainingSession
                {
                    TrainingSessionId = "session123",
                    ScheduledDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                    StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2)),
                    EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(3)),
                    Team = new Team { TeamName = "Team A" },
                    Court = new Court { CourtName = "Court A" }
                }
            };
        }

        [Theory]
        [InlineData(RoleCodeConstant.PlayerCode)]
        [InlineData(RoleCodeConstant.ManagerCode)]
        [InlineData(RoleCodeConstant.CoachCode)]
        [InlineData(RoleCodeConstant.ParentCode)]
        [InlineData(RoleCodeConstant.PresidentCode)]
        public async Task GetTrainingSessionsAsync_ShouldReturnSessions_WhenRoleValid(string role)
        {
            // Arrange
            _trainingSessionRepositoryMock.Setup(x => x.GetUserRoleAsync(It.IsAny<string>()))
                .ReturnsAsync(role);

            switch (role)
            {
                case RoleCodeConstant.PlayerCode:
                    _trainingSessionRepositoryMock.Setup(x => x.GetTrainingSessionsByPlayerAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                        .ReturnsAsync(GetFakeTrainingSessions());
                    break;
                case RoleCodeConstant.ManagerCode:
                    _trainingSessionRepositoryMock.Setup(x => x.GetTrainingSessionsByManagerAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                        .ReturnsAsync(GetFakeTrainingSessions());
                    break;
                case RoleCodeConstant.CoachCode:
                    _trainingSessionRepositoryMock.Setup(x => x.GetTrainingSessionsByCoachAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>()))
                        .ReturnsAsync(GetFakeTrainingSessions());
                    break;
                case RoleCodeConstant.ParentCode:
                    _trainingSessionRepositoryMock.Setup(x => x.GetTrainingSessionsByParentAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                        .ReturnsAsync(GetFakeTrainingSessions());
                    break;
                case RoleCodeConstant.PresidentCode:
                    _trainingSessionRepositoryMock.Setup(x => x.GetAllTrainingSessionsAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                        .ReturnsAsync(GetFakeTrainingSessions());
                    break;
            }

            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(7);

            // Act
            var result = await _service.GetTrainingSessionsAsync(startDate, endDate, null, null);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            Assert.Equal("Success", result.Status);
        }

        [Fact]
        public async Task GetTrainingSessionsAsync_ShouldReturnEmpty_WhenRoleInvalid()
        {
            // Arrange
            _trainingSessionRepositoryMock.Setup(x => x.GetUserRoleAsync(It.IsAny<string>()))
                .ReturnsAsync("UnknownRole");

            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(7);

            // Act
            var result = await _service.GetTrainingSessionsAsync(startDate, endDate, null, null);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data);
            Assert.Equal("Success", result.Status);
        }

        [Fact]
        public async Task GetTrainingSessionsAsync_ShouldReturnEmpty_WhenRepoReturnsNull()
        {
            // Arrange
            _trainingSessionRepositoryMock.Setup(x => x.GetUserRoleAsync(It.IsAny<string>()))
                .ReturnsAsync(RoleCodeConstant.PlayerCode);

            _trainingSessionRepositoryMock.Setup(x => x.GetTrainingSessionsByPlayerAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync((List<TrainingSession>)null);

            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(7);

            // Act
            var result = await _service.GetTrainingSessionsAsync(startDate, endDate, null, null);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data);
            Assert.Equal("Success", result.Status);
        }
    }
}
