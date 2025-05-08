using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.Extensions.Configuration;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Common.Constants;

namespace BAMS.Tests.TrainingSessionServiceTests
{
    public class RejectTrainingSessionAsyncTests
    {
        private readonly Mock<ITrainingSessionRepository> _trainingSessionRepositoryMock;
        private readonly Mock<ICourtRepository> _courtRepositoryMock;
        private readonly Mock<IMailTemplateRepository> _mailTemplateRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly TrainingSessionService _service;

        public RejectTrainingSessionAsyncTests()
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
        public async Task RejectTrainingSessionAsync_ShouldReturnError_WhenSessionNotFound()
        {
            // Arrange
            _trainingSessionRepositoryMock.Setup(x => x.GetTrainingSessionDetailAsync(It.IsAny<string>()))
                .ReturnsAsync((TrainingSession)null);

            var request = new CancelTrainingSessionRequest { TrainingSessionId = "session123", Reason = "No time" };

            // Act
            var result = await _service.RejectTrainingSessionAsync(request);

            // Assert
            Assert.Null(result.Data);
            Assert.Equal(TrainingSessionMessage.Error.TrainingSessionNotFound, result.Message);
        }

        [Fact]
        public async Task RejectTrainingSessionAsync_ShouldReturnError_WhenSessionNotPending()
        {
            // Arrange
            _trainingSessionRepositoryMock.Setup(x => x.GetTrainingSessionDetailAsync(It.IsAny<string>()))
                .ReturnsAsync(new TrainingSession
                {
                    TrainingSessionId = "session123",
                    Status = TrainingSessionConstant.Status.CANCELED,
                    TeamId = "team123"
                });

            var request = new CancelTrainingSessionRequest { TrainingSessionId = "session123", Reason = "No time" };

            // Act
            var result = await _service.RejectTrainingSessionAsync(request);

            // Assert
            Assert.Null(result.Data);
            Assert.Equal(TrainingSessionMessage.Error.TrainingSessionNotPending, result.Message);
        }

        [Fact]
        public async Task RejectTrainingSessionAsync_ShouldReturnError_WhenUserNotManager()
        {
            // Arrange
            _trainingSessionRepositoryMock.Setup(x => x.GetTrainingSessionDetailAsync(It.IsAny<string>()))
                .ReturnsAsync(new TrainingSession
                {
                    TrainingSessionId = "session123",
                    Status = TrainingSessionConstant.Status.PENDING,
                    TeamId = "team123"
                });

            _trainingSessionRepositoryMock.Setup(x => x.IsUserManagerOfTeamAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var request = new CancelTrainingSessionRequest { TrainingSessionId = "session123", Reason = "No time" };

            // Act
            var result = await _service.RejectTrainingSessionAsync(request);

            // Assert
            Assert.Null(result.Data);
            Assert.Equal(TrainingSessionMessage.Error.OnlyManagerOfTeamCanRejectTrainingSession, result.Message);
        }

        [Fact]
        public async Task RejectTrainingSessionAsync_ShouldReturnSuccess_WhenAllValid()
        {
            // Arrange
            var trainingSession = new TrainingSession
            {
                TrainingSessionId = "session123",
                Status = TrainingSessionConstant.Status.PENDING,
                TeamId = "team123",
                Court = new Court { CourtName = "CourtA" },
                Team = new Team { TeamName = "TeamA" }
            };

            _trainingSessionRepositoryMock.Setup(x => x.GetTrainingSessionDetailAsync(It.IsAny<string>()))
                .ReturnsAsync(trainingSession);

            _trainingSessionRepositoryMock.Setup(x => x.IsUserManagerOfTeamAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            _trainingSessionRepositoryMock.Setup(x => x.UpdateTrainingSessionAsync(It.IsAny<TrainingSession>()))
                .ReturnsAsync(true);

            _userRepositoryMock.Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new User { Username = "RejectedUser" });

            var request = new CancelTrainingSessionRequest
            {
                TrainingSessionId = "session123",
                Reason = "No time"
            };

            // Act
            var result = await _service.RejectTrainingSessionAsync(request);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(TrainingSessionMessage.Success.TrainingSessionRejectedSuccessfully, result.Message);
            Assert.Equal("Success", result.Status);
        }
    }
}
