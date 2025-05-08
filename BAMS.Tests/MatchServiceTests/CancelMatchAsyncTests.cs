using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using Match = BasketballAcademyManagementSystemAPI.Models.Match;

namespace BAMS.Tests.MatchServiceTests
{
    public class CancelMatchAsyncTests
    {
        private readonly Mock<IMatchRepository> _matchRepoMock = new();
        private readonly Mock<ITeamRepository> _teamRepoMock = new();
        private readonly Mock<ICourtRepository> _courtRepoMock = new();
        private readonly Mock<IMatchLineupRepository> _matchLineupRepoMock = new();
        private readonly Mock<ICoachRepository> _coachRepoMock = new();
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock = new();
        private readonly Mock<IConfiguration> _configurationMock = new();
        private readonly MatchService _service;

        public CancelMatchAsyncTests()
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

        [Fact]
        public async Task CancelMatchAsync_WithValidRequest_ShouldCancelSuccessfully()
        {
            SetupValidContext();

            var match = new Match { MatchId = 1, Status = MatchConstant.Status.UPCOMING, HomeTeamId = "team-1" };

            _matchRepoMock.Setup(x => x.GetMatchByIdAsync(1)).ReturnsAsync(match);
            _coachRepoMock.Setup(x => x.GetACoachByUserIdAsync(It.IsAny<string>())).ReturnsAsync(new Coach { TeamId = "team-1" });
            _matchRepoMock.Setup(x => x.UpdateMatchAsync(It.IsAny<Match>())).ReturnsAsync(true);

            var result = await _service.CancelMatchAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Success", result.Status);
            Assert.True(result.Data);
        }

        [Fact]
        public async Task CancelMatchAsync_WithNonExistentMatch_ShouldReturnMatchNotFound()
        {
            SetupValidContext();

            _matchRepoMock.Setup(x => x.GetMatchByIdAsync(1)).ReturnsAsync((Match)null);

            var result = await _service.CancelMatchAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Failed", result.Status);
            Assert.Contains(MatchMessage.Error.MatchNotFound, result.Errors);
        }

        [Fact]
        public async Task CancelMatchAsync_WithAlreadyCanceledMatch_ShouldReturnMatchAlreadyCanceled()
        {
            SetupValidContext();

            var match = new Match { MatchId = 1, Status = MatchConstant.Status.CANCELED, HomeTeamId = "team-1" };

            _matchRepoMock.Setup(x => x.GetMatchByIdAsync(1)).ReturnsAsync(match);

            var result = await _service.CancelMatchAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Failed", result.Status);
            Assert.Contains(MatchMessage.Error.MatchAlreadyCanceled, result.Errors);
        }

        [Fact]
        public async Task CancelMatchAsync_WithUserNotCoachOfTeam_ShouldReturnOnlyCoachCanCancel()
        {
            SetupValidContext();

            var match = new Match { MatchId = 1, Status = MatchConstant.Status.UPCOMING, HomeTeamId = "team-1" };

            _matchRepoMock.Setup(x => x.GetMatchByIdAsync(1)).ReturnsAsync(match);
            _coachRepoMock.Setup(x => x.GetACoachByUserIdAsync(It.IsAny<string>())).ReturnsAsync(new Coach { TeamId = "other-team" });

            var result = await _service.CancelMatchAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Failed", result.Status);
            Assert.Contains(MatchMessage.Error.OnlyTeamCoachCanCancelMatch, result.Errors);
        }
    }

}
