using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using Microsoft.Extensions.Configuration;

namespace BAMS.Tests.TrainingSessionServiceTests
{
    public class ValidateCreateTrainingSessionRequestAsyncTests
    {
        private readonly Mock<ITrainingSessionRepository> _trainingSessionRepositoryMock;
        private readonly Mock<ICourtRepository> _courtRepositoryMock;
        private readonly Mock<IMailTemplateRepository> _mailTemplateRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly TrainingSessionService _service;

        public ValidateCreateTrainingSessionRequestAsyncTests()
        {
            _trainingSessionRepositoryMock = new Mock<ITrainingSessionRepository>();
            _courtRepositoryMock = new Mock<ICourtRepository>();
            _mailTemplateRepositoryMock = new Mock<IMailTemplateRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _emailServiceMock = new Mock<IEmailService>();
            _configurationMock = new Mock<IConfiguration>();

            var settings = new Dictionary<string, string>
            {
                {"TrainingSessionSettings:MinimumDurationOfASession", "1"},
                {"TrainingSessionSettings:MinimumAdvanceScheduling", "24"}
            };
            _configurationMock.Setup(x => x.GetSection("TrainingSessionSettings")["MinimumDurationOfASession"]).Returns(settings["TrainingSessionSettings:MinimumDurationOfASession"]);
            _configurationMock.Setup(x => x.GetSection("TrainingSessionSettings")["MinimumAdvanceScheduling"]).Returns(settings["TrainingSessionSettings:MinimumAdvanceScheduling"]);

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
        public async Task ValidateCreateTrainingSessionRequestAsync_ShouldReturnError_WhenEndTimeTooShort()
        {
            // Arrange
            var request = new CreateTrainingSessionRequest
            {
                StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2)),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2.5)),
                ScheduledDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2))
            };

            // Act
            var result = await InvokePrivateValidateCreateTrainingSessionRequestAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(nameof(request.EndTime), result.Errors.Keys);
        }

        [Fact]
        public async Task ValidateCreateTrainingSessionRequestAsync_ShouldReturnError_WhenScheduledDateTooSoon()
        {
            // Arrange
            var request = new CreateTrainingSessionRequest
            {
                StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(2)),
                ScheduledDate = DateOnly.FromDateTime(DateTime.Now) // today
            };

            // Act
            var result = await InvokePrivateValidateCreateTrainingSessionRequestAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(nameof(request.ScheduledDate), result.Errors.Keys);
        }

        [Fact]
        public async Task ValidateCreateTrainingSessionRequestAsync_ShouldReturnError_WhenTeamInvalid()
        {
            // Arrange
            var request = ValidRequest();
            _trainingSessionRepositoryMock.Setup(x => x.IsValidTeamAsync(It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await InvokePrivateValidateCreateTrainingSessionRequestAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(nameof(request.TeamId), result.Errors.Keys);
        }

        [Fact]
        public async Task ValidateCreateTrainingSessionRequestAsync_ShouldReturnError_WhenCourtInvalid()
        {
            // Arrange
            var request = ValidRequest();
            _trainingSessionRepositoryMock.Setup(x => x.IsValidTeamAsync(It.IsAny<string>())).ReturnsAsync(true);
            _trainingSessionRepositoryMock.Setup(x => x.IsValidCourtAsync(It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await InvokePrivateValidateCreateTrainingSessionRequestAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(nameof(request.CourtId), result.Errors.Keys);
        }

        [Fact]
        public async Task ValidateCreateTrainingSessionRequestAsync_ShouldReturnError_WhenCourtUnavailable()
        {
            // Arrange
            var request = ValidRequest();
            _trainingSessionRepositoryMock.Setup(x => x.IsValidTeamAsync(It.IsAny<string>())).ReturnsAsync(true);
            _trainingSessionRepositoryMock.Setup(x => x.IsValidCourtAsync(It.IsAny<string>())).ReturnsAsync(true);
            _trainingSessionRepositoryMock.Setup(x => x.IsCourtAvailableAsync(It.IsAny<string>(), It.IsAny<DateOnly>(), It.IsAny<TimeOnly>(), It.IsAny<TimeOnly>())).ReturnsAsync(false);

            // Act
            var result = await InvokePrivateValidateCreateTrainingSessionRequestAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Contains("isCourtAvailable", result.Errors.Keys);
        }

        [Fact]
        public async Task ValidateCreateTrainingSessionRequestAsync_ShouldReturnSuccess_WhenRequestValid()
        {
            // Arrange
            var request = ValidRequest();
            _trainingSessionRepositoryMock.Setup(x => x.IsValidTeamAsync(It.IsAny<string>())).ReturnsAsync(true);
            _trainingSessionRepositoryMock.Setup(x => x.IsValidCourtAsync(It.IsAny<string>())).ReturnsAsync(true);
            _trainingSessionRepositoryMock.Setup(x => x.IsCourtAvailableAsync(It.IsAny<string>(), It.IsAny<DateOnly>(), It.IsAny<TimeOnly>(), It.IsAny<TimeOnly>())).ReturnsAsync(true);
            _trainingSessionRepositoryMock.Setup(x => x.IsUserCoachOfTeamAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            _trainingSessionRepositoryMock.Setup(x => x.IsTeamAvailableAsync(It.IsAny<string>(), It.IsAny<DateOnly>(), It.IsAny<TimeOnly>(), It.IsAny<TimeOnly>())).ReturnsAsync(true);

            // Act
            var result = await InvokePrivateValidateCreateTrainingSessionRequestAsync(request);

            // Assert
            Assert.Null(result);
        }

        private CreateTrainingSessionRequest ValidRequest()
        {
            return new CreateTrainingSessionRequest
            {
                TeamId = "team123",
                CourtId = "court123",
                StartTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(26)),
                EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(28)),
                ScheduledDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2))
            };
        }

        private async Task<ApiMessageModelV2<TrainingSession>?> InvokePrivateValidateCreateTrainingSessionRequestAsync(CreateTrainingSessionRequest request)
        {
            var method = _service.GetType().GetMethod("ValidateCreateTrainingSessionRequestAsync", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var task = (Task<ApiMessageModelV2<TrainingSession>?>)method.Invoke(_service, new object[] { request });

            return await task;
        }
    }
}
