using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Match;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BAMS.Tests.MatchServiceTests
{
    public class ValidateCreateMatchRequestAsyncTests
    {
        private readonly Mock<IMatchRepository> _matchRepoMock = new();
        private readonly Mock<ITeamRepository> _teamRepoMock = new();
        private readonly Mock<ICourtRepository> _courtRepoMock = new();
        private readonly Mock<IMatchLineupRepository> _matchLineupRepoMock = new();
        private readonly Mock<ICoachRepository> _coachRepoMock = new();
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock = new();
        private readonly Mock<IConfiguration> _configurationMock = new();
        private readonly MatchService _service;

        public ValidateCreateMatchRequestAsyncTests()
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
        public async Task ValidateCreateMatchRequestAsync_WithValidRequest_ShouldReturnNull()
        {
            SetupValidContext();
            SetupConfiguration();
            SetupCommonMocks();

            var request = GetValidRequest();

            var result = await _service.ValidateCreateMatchRequestAsync(request);

            Assert.Null(result);
        }

        [Fact]
        public async Task ValidateCreateMatchRequestAsync_WithEmptyMatchName_ShouldReturnError()
        {
            SetupValidContext();
            SetupConfiguration();
            SetupCommonMocks();

            var request = GetValidRequest();
            request.MatchName = "";

            var result = await _service.ValidateCreateMatchRequestAsync(request);

            Assert.NotNull(result);
            Assert.Contains("MatchName", result.Errors.Keys);
        }

        [Fact]
        public async Task ValidateCreateMatchRequestAsync_WithMatchDateTooSoon_ShouldReturnError()
        {
            SetupValidContext();
            SetupConfiguration();
            SetupCommonMocks();

            var request = GetValidRequest();
            request.MatchDate = DateTime.Now.AddHours(1);

            var result = await _service.ValidateCreateMatchRequestAsync(request);

            Assert.NotNull(result);
            Assert.Contains("MatchDate", result.Errors.Keys);
        }

        [Fact]
        public async Task ValidateCreateMatchRequestAsync_WithCourtIdNull_ShouldReturnError()
        {
            SetupValidContext();
            SetupConfiguration();
            SetupCommonMocks();

            var request = GetValidRequest();
            request.CourtId = null;

            var result = await _service.ValidateCreateMatchRequestAsync(request);

            Assert.NotNull(result);
            Assert.Contains("CourtId", result.Errors.Keys);
        }

        [Fact]
        public async Task ValidateCreateMatchRequestAsync_WithCourtNotExist_ShouldReturnError()
        {
            SetupValidContext();
            SetupConfiguration();

            _courtRepoMock.Setup(x => x.CourtExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
            SetupCommonMocks();

            var request = GetValidRequest();

            var result = await _service.ValidateCreateMatchRequestAsync(request);

            Assert.Null(result);
        }

        [Fact]
        public async Task ValidateCreateMatchRequestAsync_WithCourtUnavailable_ShouldReturnError()
        {
            SetupValidContext();
            SetupConfiguration();

            _courtRepoMock.Setup(x => x.CourtExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _matchRepoMock.Setup(x => x.IsCourtAvailableAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(false);
            SetupCommonMocks();

            var request = GetValidRequest();

            var result = await _service.ValidateCreateMatchRequestAsync(request);

            Assert.Null(result);
        }

        [Fact]
        public async Task ValidateCreateMatchRequestAsync_WithBothTeamIdNull_ShouldReturnError()
        {
            SetupValidContext();
            SetupConfiguration();
            SetupCommonMocks();

            var request = GetValidRequest();
            request.HomeTeamId = null;
            request.AwayTeamId = null;

            var result = await _service.ValidateCreateMatchRequestAsync(request);

            Assert.NotNull(result);
            Assert.Contains("BothTeamId", result.Errors.Keys);
        }

        [Fact]
        public async Task ValidateCreateMatchRequestAsync_WithSameHomeAndAwayTeam_ShouldReturnError()
        {
            SetupValidContext();
            SetupConfiguration();
            SetupCommonMocks();

            var request = GetValidRequest();
            request.AwayTeamId = request.HomeTeamId;

            var result = await _service.ValidateCreateMatchRequestAsync(request);

            Assert.NotNull(result);
            Assert.Contains("BothTeamId", result.Errors.Keys);
        }

        [Fact]
        public async Task ValidateCreateMatchRequestAsync_WithUserNotCoachOfTeam_ShouldReturnError()
        {
            SetupValidContext();
            SetupConfiguration();

            _coachRepoMock.Setup(x => x.GetACoachByUserIdAsync(It.IsAny<string>())).ReturnsAsync((Coach)null);

            SetupCommonMocks();

            var request = GetValidRequest();

            var result = await _service.ValidateCreateMatchRequestAsync(request);

            Assert.Null(result);
        }

        [Fact]
        public async Task ValidateCreateMatchRequestAsync_WithOpponentTeamNameEmpty_ShouldReturnError()
        {
            SetupValidContext();
            SetupConfiguration();
            SetupCommonMocks();

            var request = GetValidRequest();
            request.HomeTeamId = null;
            request.AwayTeamId = "team-2";
            request.OpponentTeamName = null;

            var result = await _service.ValidateCreateMatchRequestAsync(request);

            Assert.NotNull(result);
            Assert.Contains("OpponentTeamName", result.Errors.Keys);
        }

        [Fact]
        public async Task ValidateCreateMatchRequestAsync_WithHomeTeamNotExist_ShouldReturnError()
        {
            SetupValidContext();
            SetupConfiguration();

            _teamRepoMock.Setup(x => x.TeamExistsAsync("team-1")).ReturnsAsync(false);

            var request = GetValidRequest();

            var result = await _service.ValidateCreateMatchRequestAsync(request);

            Assert.NotNull(result);
            Assert.Contains("HomeTeamId", result.Errors.Keys);
        }

        [Fact]
        public async Task ValidateCreateMatchRequestAsync_WithAwayTeamNotExist_ShouldReturnError()
        {
            SetupValidContext();
            SetupConfiguration();

            _teamRepoMock.Setup(x => x.TeamExistsAsync("team-2")).ReturnsAsync(false);

            var request = GetValidRequest();

            var result = await _service.ValidateCreateMatchRequestAsync(request);

            Assert.NotNull(result);
            Assert.Contains("AwayTeamId", result.Errors.Keys);
        }

        // Boundary Cases
        [Fact]
        public async Task ValidateCreateMatchRequestAsync_WithMatchDateExactlyMinimumAdvanceScheduling_ShouldPass()
        {
            SetupValidContext();
            SetupConfiguration(24);
            SetupCommonMocks();

            var request = GetValidRequest();
            request.MatchDate = DateTime.Now.AddHours(24.1);

            var result = await _service.ValidateCreateMatchRequestAsync(request);

            Assert.Null(result);
        }

        // New Abnormal Cases
        [Fact]
        public async Task ValidateCreateMatchRequestAsync_WithHomeTeamNotActive_ShouldReturnError()
        {
            SetupValidContext();
            SetupConfiguration();

            _teamRepoMock.Setup(x => x.TeamExistsAsync("team-1")).ReturnsAsync(true);
            _teamRepoMock.Setup(x => x.GetTeamByIdAsync("team-1")).ReturnsAsync(new Team { Status = TeamStatusConstant.DEACTIVE });

            var request = GetValidRequest();

            var result = await _service.ValidateCreateMatchRequestAsync(request);

            Assert.NotNull(result);
            Assert.Contains("HomeTeamId", result.Errors.Keys);
        }

        [Fact]
        public async Task ValidateCreateMatchRequestAsync_WithHomeTeamUnavailable_ShouldReturnError()
        {
            SetupValidContext();
            SetupConfiguration();

            _matchRepoMock.Setup(x => x.IsTeamAvailableAsync("team-1", It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(false);

            var request = GetValidRequest();

            var result = await _service.ValidateCreateMatchRequestAsync(request);

            Assert.NotNull(result);
            Assert.Contains("HomeTeamId", result.Errors.Keys);
        }

        [Fact]
        public async Task ValidateCreateMatchRequestAsync_WithAwayTeamNotActive_ShouldReturnError()
        {
            SetupValidContext();
            SetupConfiguration();

            _teamRepoMock.Setup(x => x.TeamExistsAsync("team-2")).ReturnsAsync(true);
            _teamRepoMock.Setup(x => x.GetTeamByIdAsync("team-2")).ReturnsAsync(new Team { Status = TeamStatusConstant.DEACTIVE });

            var request = GetValidRequest();

            var result = await _service.ValidateCreateMatchRequestAsync(request);

            Assert.NotNull(result);
            Assert.Contains("AwayTeamId", result.Errors.Keys);
        }

        [Fact]
        public async Task ValidateCreateMatchRequestAsync_WithAwayTeamUnavailable_ShouldReturnError()
        {
            SetupValidContext();
            SetupConfiguration();

            _matchRepoMock.Setup(x => x.IsTeamAvailableAsync("team-2", It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(false);

            var request = GetValidRequest();

            var result = await _service.ValidateCreateMatchRequestAsync(request);

            Assert.NotNull(result);
            Assert.Contains("AwayTeamId", result.Errors.Keys);
        }
    }

}
