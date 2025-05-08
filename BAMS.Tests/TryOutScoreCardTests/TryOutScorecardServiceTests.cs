using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Moq;
namespace BAMS.Tests.TryOutScoreCardTests
{
    public class TryOutScorecardServiceTests
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<ITryOutScorecardRepository> _tryOutScorecardRepositoryMock;
        private readonly Mock<IPlayerRegistrationRepository> _playerRegistrationRepositoryMock;
        private readonly TryOutScorecardService _service;

        public TryOutScorecardServiceTests()
        {
            _playerRegistrationRepositoryMock = new Mock<IPlayerRegistrationRepository>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _tryOutScorecardRepositoryMock = new Mock<ITryOutScorecardRepository>();

            _service = new TryOutScorecardService(
                _httpContextAccessorMock.Object,
                _tryOutScorecardRepositoryMock.Object,
                Mock.Of<ITryOutMeasurementScaleRepository>(),
                _playerRegistrationRepositoryMock.Object,
                Mock.Of<IMemberRegistrationSessionRepository>()
            );
        }

        [Fact]
        public async Task AddOrUpdateScoresAsync_TC01_AllScoresValid()
        {
            // Arrange
            var userId = "test-user";
            SetupHttpContextWithUser(userId);

            var input = new BulkPlayerScoreInputDto
            {
                Players = new List<PlayerScoreInputDto>
                {
                    new PlayerScoreInputDto
                    {
                        PlayerRegistrationId = 1,
                        Scores = new List<AddTryOutScorecardDto>
                        {
                            new AddTryOutScorecardDto { SkillCode = "Dribble", Score = "T" },
                            new AddTryOutScorecardDto { SkillCode = "Passing", Score = "K" }
                        }
                    }
                }
            };

            _tryOutScorecardRepositoryMock
                .Setup(repo => repo.AddOrUpdateAsync(It.IsAny<TryOutScorecard>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.AddOrUpdateScoresAsync(input);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.Equal(TryOutScoreMessage.Success.SaveAllScoresSuccess, result.Message);
            Assert.Empty(result.Errors);

            _tryOutScorecardRepositoryMock.Verify(repo => repo.AddOrUpdateAsync(It.IsAny<TryOutScorecard>()), Times.Exactly(2));
        }

        [Fact]
        public async Task AddOrUpdateScoresAsync_TC02_SomeScoresInvalid()
        {
            // Arrange
            var userId = "test-user";
            SetupHttpContextWithUser(userId);

            var input = new BulkPlayerScoreInputDto
            {
                Players = new List<PlayerScoreInputDto>
                {
                    new PlayerScoreInputDto
                    {
                        PlayerRegistrationId = 1,
                        Scores = new List<AddTryOutScorecardDto>
                        {
                            new AddTryOutScorecardDto { SkillCode = "Dribble", Score = "T" },
                            new AddTryOutScorecardDto { SkillCode = "Skill1", Score = "Invalid" }
                        }
                    }
                }
            };

            _tryOutScorecardRepositoryMock
                .Setup(repo => repo.AddOrUpdateAsync(It.IsAny<TryOutScorecard>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.AddOrUpdateScoresAsync(input);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.Equal(TryOutScoreMessage.Success.SaveSomeScoresSuccess, result.Message);
            Assert.Single(result.Errors);

            _tryOutScorecardRepositoryMock.Verify(repo => repo.AddOrUpdateAsync(It.IsAny<TryOutScorecard>()), Times.Once);
        }

        [Fact]
        public async Task AddOrUpdateScoresAsync_TC03_AllScoresInvalid()
        {
            // Arrange
            var userId = "test-user";
            SetupHttpContextWithUser(userId);

            var input = new BulkPlayerScoreInputDto
            {
                Players = new List<PlayerScoreInputDto>
                {
                    new PlayerScoreInputDto
                    {
                        PlayerRegistrationId = 1,
                        Scores = new List<AddTryOutScorecardDto>
                        {
                            new AddTryOutScorecardDto { SkillCode = "Skill1", Score = "Invalid" },
                            new AddTryOutScorecardDto { SkillCode = "Skill2", Score = "Invalid" }
                        }
                    }
                }
            };

            // Act
            var result = await _service.AddOrUpdateScoresAsync(input);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Equal(TryOutScoreMessage.Error.SaveAllScoresFailed, result.Message);
            Assert.Equal(2, result.Errors.Count);

            _tryOutScorecardRepositoryMock.Verify(repo => repo.AddOrUpdateAsync(It.IsAny<TryOutScorecard>()), Times.Never);
        }

        [Fact]
        public async Task AddOrUpdateScoresAsync_TC04_NoUserLoggedIn()
        {
            // Arrange
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext());

            var input = new BulkPlayerScoreInputDto
            {
                Players = new List<PlayerScoreInputDto>()
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.AddOrUpdateScoresAsync(input));
            Assert.Equal(AuthenticationMessage.AuthenticationErrorMessage.PleaseLogin, exception.Message);
        }

        [Fact]
        public async Task AddOrUpdateScoresAsync_TC05_NoPlayersInInput()
        {
            // Arrange
            var userId = "test-user";
            SetupHttpContextWithUser(userId);

            var input = new BulkPlayerScoreInputDto
            {
                Players = new List<PlayerScoreInputDto>()
            };

            // Act
            var result = await _service.AddOrUpdateScoresAsync(input);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Equal(TryOutScoreMessage.Error.SaveAllScoresFailed, result.Message);
            Assert.Empty(result.Errors);

            _tryOutScorecardRepositoryMock.Verify(repo => repo.AddOrUpdateAsync(It.IsAny<TryOutScorecard>()), Times.Never);
        }

        private void SetupHttpContextWithUser(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);
        }

        [Fact]
        public async Task GetScoresByPlayerRegistrationIdAsync_PlayerExistsWithScores_ReturnsSuccess()
        {
            // Arrange (N1)
            int playerId = 1;
            var player = new PlayerRegistration
            {
                PlayerRegistrationId = playerId,
                FullName = "John Doe",
                Gender = true,
                DateOfBirth = new DateOnly(2000, 1, 1)
            };
            var scores = new List<TryOutScorecard>
            {
                new TryOutScorecard
                {
                    TryOutScorecardId = 1,
                    PlayerRegistrationId = playerId,
                    MeasurementScaleCode = "Passing",
                    Score = "5",
                    ScoredBy = "Admin",
                    ScoredAt = DateTime.Now,
                    MeasurementScaleCodeNavigation = new TryOutMeasurementScale
                    {
                        MeasurementScaleCode = "Passing",
                        MeasurementName = "Passing Skill"
                    }
                }
            };

            _playerRegistrationRepositoryMock
                .Setup(repo => repo.GetPlayerRegistrationByIdAsync(playerId))
                .ReturnsAsync(player);

            _tryOutScorecardRepositoryMock
                .Setup(repo => repo.GetScoresByPlayerIdAsync(playerId))
                .ReturnsAsync(scores);

            // Act
            var result = await _service.GetScoresByPlayerRegistrationIdAsync(playerId);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.NotNull(result.Data);
            Assert.Equal(playerId, result.Data.PlayerRegistrationId);
            Assert.Single(result.Data.Scores);
        }

        [Fact]
        public async Task GetScoresByPlayerRegistrationIdAsync_PlayerExistsWithoutScores_ReturnsSuccessWithError()
        {
            // Arrange (N2)
            int playerId = 1;
            var player = new PlayerRegistration
            {
                PlayerRegistrationId = playerId,
                FullName = "John Doe",
                Gender = true,
                DateOfBirth = new DateOnly(2000, 1, 1)
            };

            _playerRegistrationRepositoryMock
                .Setup(repo => repo.GetPlayerRegistrationByIdAsync(playerId))
                .ReturnsAsync(player);

            _tryOutScorecardRepositoryMock
                .Setup(repo => repo.GetScoresByPlayerIdAsync(playerId))
                .ReturnsAsync(new List<TryOutScorecard>());

            // Act
            var result = await _service.GetScoresByPlayerRegistrationIdAsync(playerId);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.NotNull(result.Errors);
            Assert.Contains(TryOutScoreMessage.Error.SessionRegistrationPlayerScoreDoesNotExist, result.Errors);
        }

        [Fact]
        public async Task GetScoresByPlayerRegistrationIdAsync_PlayerDoesNotExist_ReturnsError()
        {
            // Arrange (A1)
            int playerId = 1;

            _playerRegistrationRepositoryMock
                .Setup(repo => repo.GetPlayerRegistrationByIdAsync(playerId))
                .ReturnsAsync((PlayerRegistration)null);

            // Act
            var result = await _service.GetScoresByPlayerRegistrationIdAsync(playerId);

            // Assert
            Assert.Null(result.Data);
            Assert.Contains(TryOutScoreMessage.Error.SessionRegistrationPlayerDoesNotExist, result.Errors);
        }

        [Theory]
        [InlineData(0)]  // B1
        [InlineData(-1)] // B2
        public async Task GetScoresByPlayerRegistrationIdAsync_InvalidPlayerId_ReturnsError(int playerId)
        {
            // Arrange
            _playerRegistrationRepositoryMock
                .Setup(repo => repo.GetPlayerRegistrationByIdAsync(playerId))
                .ReturnsAsync((PlayerRegistration)null);

            // Act
            var result = await _service.GetScoresByPlayerRegistrationIdAsync(playerId);

            // Assert
            Assert.Null(result.Data);
            Assert.Contains(TryOutScoreMessage.Error.SessionRegistrationPlayerDoesNotExist, result.Errors);
        }

    }
}
