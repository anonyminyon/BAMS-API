using System.Security.Claims;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BAMS.Tests.TrainingSessionServiceTests
{
    public class CreateAddtitionalTrainingSessionAsyncTests
    {
        private readonly Mock<ITrainingSessionRepository> _trainingSessionRepositoryMock;
        private readonly Mock<ICourtRepository> _courtRepositoryMock;
        private readonly Mock<IMailTemplateRepository> _mailTemplateRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly TrainingSessionService _service;

        public CreateAddtitionalTrainingSessionAsyncTests()
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
        }

        private void SetupHttpContext(string userId)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);
            var context = new DefaultHttpContext { User = principal };
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(context);
        }

        #region CreateAddtitionalTrainingSessionAsync Tests
        [Fact]
        public async Task CreateAddtitionalTrainingSessionAsync_ShouldReturnValidationError_WhenValidationFails()
        {
            // Arrange
            var request = new CreateTrainingSessionRequest();
            SetupHttpContext("user123");

            // Fake validation fail: Team invalid
            _trainingSessionRepositoryMock.Setup(x => x.IsValidTeamAsync(It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _service.CreateAddtitionalTrainingSessionAsync(request);

            // Assert
            Assert.Null(result.Data);
            Assert.Equal("Tạo buổi tập luyện thất bại.", result.Message);
            Assert.Equal("Failed", result.Status);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public async Task CreateAddtitionalTrainingSessionAsync_ShouldReturnError_WhenAddTrainingSessionFails()
        {
            // Arrange
            var request = new CreateTrainingSessionRequest
            {
                TeamId = "team123",
                CourtId = "court123",
                ScheduledDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
                StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(26)),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(28))
            };

            SetupHttpContext("user123");

            _trainingSessionRepositoryMock.Setup(x => x.IsValidTeamAsync(It.IsAny<string>())).ReturnsAsync(true);
            _trainingSessionRepositoryMock.Setup(x => x.IsValidCourtAsync(It.IsAny<string>())).ReturnsAsync(true);
            _trainingSessionRepositoryMock.Setup(x => x.IsCourtAvailableAsync(It.IsAny<string>(), It.IsAny<DateOnly>(), It.IsAny<TimeOnly>(), It.IsAny<TimeOnly>())).ReturnsAsync(true);
            _trainingSessionRepositoryMock.Setup(x => x.IsUserCoachOfTeamAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            _trainingSessionRepositoryMock.Setup(x => x.IsTeamAvailableAsync(It.IsAny<string>(), It.IsAny<DateOnly>(), It.IsAny<TimeOnly>(), It.IsAny<TimeOnly>())).ReturnsAsync(true);

            _trainingSessionRepositoryMock.Setup(x => x.AddTrainingSessionAsync(It.IsAny<TrainingSession>())).ReturnsAsync(false);

            // Act
            var result = await _service.CreateAddtitionalTrainingSessionAsync(request);

            // Assert
            Assert.Null(result.Data);
            Assert.Equal("Tạo buổi tập luyện thất bại.", result.Message);
            Assert.NotNull(result.Status);
        }

        [Fact]
        public async Task CreateAddtitionalTrainingSessionAsync_ShouldReturnSuccess_WhenAllValidationsPass()
        {
            // Arrange
            var request = new CreateTrainingSessionRequest
            {
                TeamId = "team123",
                CourtId = "court123",
                ScheduledDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
                StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(26)),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(28))
            };

            SetupHttpContext("user123");

            _trainingSessionRepositoryMock.Setup(x => x.IsValidTeamAsync(It.IsAny<string>())).ReturnsAsync(true);
            _trainingSessionRepositoryMock.Setup(x => x.IsValidCourtAsync(It.IsAny<string>())).ReturnsAsync(true);
            _trainingSessionRepositoryMock.Setup(x => x.IsCourtAvailableAsync(It.IsAny<string>(), It.IsAny<DateOnly>(), It.IsAny<TimeOnly>(), It.IsAny<TimeOnly>())).ReturnsAsync(true);
            _trainingSessionRepositoryMock.Setup(x => x.IsUserCoachOfTeamAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            _trainingSessionRepositoryMock.Setup(x => x.IsTeamAvailableAsync(It.IsAny<string>(), It.IsAny<DateOnly>(), It.IsAny<TimeOnly>(), It.IsAny<TimeOnly>())).ReturnsAsync(true);

            _trainingSessionRepositoryMock.Setup(x => x.AddTrainingSessionAsync(It.IsAny<TrainingSession>())).ReturnsAsync(true);
            _trainingSessionRepositoryMock.Setup(x => x.GetTeamManagerEmailsAsync(It.IsAny<string>())).ReturnsAsync(new List<string> { "manager@example.com" });
            _userRepositoryMock.Setup(x => x.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(new User { Username = "TestUser" });
            _trainingSessionRepositoryMock.Setup(x => x.GetTrainingSessionDetailAsync(It.IsAny<string>())).ReturnsAsync(new TrainingSession { Team = new Team(), Court = new Court() });

            // Act
            var result = await _service.CreateAddtitionalTrainingSessionAsync(request);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal("Success", result.Status);
            Assert.Equal("Tạo buổi tập luyện thành công.", result.Message);
        }
        #endregion
    }
}
