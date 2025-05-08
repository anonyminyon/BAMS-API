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
using static BasketballAcademyManagementSystemAPI.Common.Messages.AuthenticationMessage;

namespace BAMS.Tests.TrainingSessionServiceTests
{
    public class GetPendingTrainingSessionTests
    {
        private readonly Mock<ITrainingSessionRepository> _trainingSessionRepositoryMock;
        private readonly Mock<ICourtRepository> _courtRepositoryMock;
        private readonly Mock<IMailTemplateRepository> _mailTemplateRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly TrainingSessionService _service;

        public GetPendingTrainingSessionTests()
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

        [Fact]
        public async Task GetPendingTrainingSession_ShouldReturnError_WhenUserNotFound()
        {
            // Arrange
            _userRepositoryMock.Setup(x => x.GetUserWithRoleByIdAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            // Act
            var result = await _service.GetPendingTrainingSession();

            // Assert
            Assert.Null(result.Data);
            Assert.Equal(AuthenticationErrorMessage.PleaseLogin, result.Message);
        }

        [Fact]
        public async Task GetPendingTrainingSession_ShouldReturnEmpty_WhenUserHasNoTeam()
        {
            // Arrange
            _userRepositoryMock.Setup(x => x.GetUserWithRoleByIdAsync(It.IsAny<string>())).ReturnsAsync(new User
            {
                Manager = null,
                Coach = null
            });

            // Act
            var result = await _service.GetPendingTrainingSession();

            // Assert
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data);
            Assert.Equal("Success", result.Status);
        }

        [Fact]
        public async Task GetPendingTrainingSession_ShouldReturnPendingSessions_WhenUserHasTeam()
        {
            // Arrange
            var teamId = "team123";

            _userRepositoryMock.Setup(x => x.GetUserWithRoleByIdAsync(It.IsAny<string>())).ReturnsAsync(new User
            {
                Manager = new Manager { TeamId = teamId }
            });

            _trainingSessionRepositoryMock.Setup(x => x.GetPendingTrainingSessionOfATeamAsync(teamId)).ReturnsAsync(new List<TrainingSession>
            {
                new TrainingSession
                {
                    TrainingSessionId = "session1",
                    TeamId = teamId,
                    ScheduledDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                    StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2)),
                    EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(3)),
                    CourtId = "court123",
                    Team = new Team { TeamName = "Team A" },
                    Court = new Court { CourtName = "Court A", Address = "123 Street", Contact = "0123456789", RentPricePerHour = 100 }
                }
            });

            // Act
            var result = await _service.GetPendingTrainingSession();

            // Assert
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            Assert.Equal("Success", result.Status);
            Assert.Equal("session1", result.Data[0].TrainingSessionId);
            Assert.Equal("Team A", result.Data[0].TeamName);
        }

        [Fact]
        public async Task GetPendingTrainingSession_ShouldReturnEmpty_WhenNoPendingSession()
        {
            // Arrange
            var teamId = "team123";

            _userRepositoryMock.Setup(x => x.GetUserWithRoleByIdAsync(It.IsAny<string>())).ReturnsAsync(new User
            {
                Manager = new Manager { TeamId = teamId }
            });

            _trainingSessionRepositoryMock.Setup(x => x.GetPendingTrainingSessionOfATeamAsync(teamId)).ReturnsAsync(new List<TrainingSession>());

            // Act
            var result = await _service.GetPendingTrainingSession();

            // Assert
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data);
            Assert.Equal("Success", result.Status);
        }
    }
}
