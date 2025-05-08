using System.Security.Claims;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Match;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BAMS.Tests.MatchServiceTests
{
    public class CreateMatchAsyncTests
    {
        private readonly Mock<IMatchRepository> _matchRepoMock = new();
        private readonly Mock<ITeamRepository> _teamRepoMock = new();
        private readonly Mock<ICourtRepository> _courtRepoMock = new();
        private readonly Mock<IMatchLineupRepository> _matchLineupRepoMock = new();
        private readonly Mock<ICoachRepository> _coachRepoMock = new();
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock = new();
        private readonly Mock<IConfiguration> _configurationMock = new();
        private readonly MatchService _service;

        public CreateMatchAsyncTests()
        {
            _service = new MatchService(
                _matchRepoMock.Object,
                _teamRepoMock.Object,
                _courtRepoMock.Object,
                _matchLineupRepoMock.Object,
                _coachRepoMock.Object,
                _httpContextAccessorMock.Object,
                _configurationMock.Object);
        }

        private void SetupValidContext(string userId = "user-123")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);
            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(principal);
        }

        private void SetupConfiguration(double minimumAdvanceScheduling = 24, double minimumHourDuration = 1)
        {
            var dict = new Dictionary<string, string>
            {
                { "MatchSettings:MinimumAdvanceScheduling", minimumAdvanceScheduling.ToString() },
                { "MatchSettings:MinimumHourDurationOfAMatch", minimumHourDuration.ToString() },
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(dict)
                .Build();

            _configurationMock.Setup(x => x.GetSection(It.IsAny<string>())).Returns(config.GetSection("MatchSettings"));
        }

        private CreateMatchRequest GetValidRequest()
        {
            return new CreateMatchRequest
            {
                MatchName = "Friendly Match",
                MatchDate = DateTime.Now.AddHours(25),
                HomeTeamId = "team-1",
                AwayTeamId = "team-2",
                CourtId = "court-1",
                OpponentTeamName = "Opponent"
            };
        }

        private void SetupCommonMocks()
        {
            _courtRepoMock.Setup(x => x.CourtExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _matchRepoMock.Setup(x => x.IsCourtAvailableAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(true);
            _teamRepoMock.Setup(x => x.TeamExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _teamRepoMock.Setup(x => x.GetTeamByIdAsync(It.IsAny<string>())).ReturnsAsync(new Team { Status = TeamStatusConstant.ACTIVE });
            _matchRepoMock.Setup(x => x.IsTeamAvailableAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(true);
            _coachRepoMock.Setup(x => x.GetACoachByUserIdAsync(It.IsAny<string>())).ReturnsAsync(new Coach { TeamId = "team-1" });
        }

        [Fact]
        public async Task CreateMatchAsync_WithValidRequest_ShouldReturnSuccess()
        {
            SetupValidContext();
            SetupConfiguration();
            SetupCommonMocks();

            var request = GetValidRequest();

            _matchRepoMock.Setup(x => x.AddMatchAsync(It.IsAny<BasketballAcademyManagementSystemAPI.Models.Match>())).Returns(Task.CompletedTask);

            var result = await _service.CreateMatchAsync(request);

            Assert.NotNull(result);
            Assert.Equal("Success", result.Status);
            Assert.Equal(MatchMessage.Success.MatchCreated, result.Message);
            Assert.Equal(request, result.Data);
        }

        [Fact]
        public async Task CreateMatchAsync_WithInvalidRequest_ShouldReturnValidationErrors()
        {
            SetupValidContext();
            SetupConfiguration();
            SetupCommonMocks();

            var request = GetValidRequest();
            request.MatchName = "";

            var result = await _service.CreateMatchAsync(request);

            Assert.NotNull(result);
            Assert.Equal("Failed", result.Status);
            Assert.NotNull(result.Errors);
        }

        [Fact]
        public async Task CreateMatchAsync_WithMinimalValidData_ShouldPass()
        {
            SetupValidContext();
            SetupConfiguration();

            var request = new CreateMatchRequest
            {
                MatchName = "Friendly Match",
                MatchDate = DateTime.Now.AddHours(26),
                HomeTeamId = "team-1",
                CourtId = "court-1",
                OpponentTeamName = "Opponent"
            };

            _courtRepoMock.Setup(x => x.CourtExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _matchRepoMock.Setup(x => x.IsCourtAvailableAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(true);
            _teamRepoMock.Setup(x => x.TeamExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _teamRepoMock.Setup(x => x.GetTeamByIdAsync(It.IsAny<string>())).ReturnsAsync(new Team { Status = TeamStatusConstant.ACTIVE });
            _matchRepoMock.Setup(x => x.IsTeamAvailableAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(true);
            _coachRepoMock.Setup(x => x.GetACoachByUserIdAsync(It.IsAny<string>())).ReturnsAsync(new Coach { TeamId = "team-1" });
            _matchRepoMock.Setup(x => x.AddMatchAsync(It.IsAny<BasketballAcademyManagementSystemAPI.Models.Match>())).Returns(Task.CompletedTask);

            var result = await _service.CreateMatchAsync(request);

            Assert.NotNull(result);
            Assert.Equal("Success", result.Status);
            Assert.Equal(request, result.Data);
        }
    }

}
