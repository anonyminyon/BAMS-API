using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Moq;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Team;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
namespace BAMS.Tests.TeamServiceTest;

public class TeamServiceTest
{
	private readonly Mock<ITeamRepository> _teamRepositoryMock;
	private readonly Mock<IMapper> _mapperMock;
	private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock; // Mock IHttpContextAccessor
	private readonly TeamService _service;

	public TeamServiceTest()
	{
		// Mock the IHttpContextAccessor
		_httpContextAccessorMock = new Mock<IHttpContextAccessor>();

		// Create a fake ClaimsPrincipal
		var fakeUser = new ClaimsPrincipal(new ClaimsIdentity(new[]
		{
			new Claim(ClaimTypes.NameIdentifier, "user123"),
			new Claim(JwtRegisteredClaimNames.Sub, "user123")
		}));

		// Set up the HttpContext to return the fake user
		_httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(fakeUser);

		// Now we can create the GetLoginUserHelper with the mock IHttpContextAccessor
		var getLoginUserHelper = new GetLoginUserHelper(_httpContextAccessorMock.Object);

		// Initialize your service with the mocked dependencies
		_teamRepositoryMock = new Mock<ITeamRepository>();
		_mapperMock = new Mock<IMapper>();
		_service = new TeamService(
			_teamRepositoryMock.Object,
			_mapperMock.Object,
			getLoginUserHelper, // Pass the mocked GetLoginUserHelper
			new Mock<IUserTeamHistoryService>().Object,
			new Mock<IManagerRepository>().Object
		);
	}
	#region CreateTeamAsync

	// Test case 1: Team name is valid and doesn't already exist
	[Fact]
	public async Task CreateTeamAsync_ShouldReturnSuccess_WhenValidTeamName()
	{
		// Arrange
		var teamName = "New Team";
		var status = 1;

		// Mock the repository method to simulate that the team name does not exist
		// Adjusting mock setup to include excludeTeamId
		_teamRepositoryMock.Setup(repo => repo.IsTeamNameExistsAsync(It.IsAny<string>(), It.IsAny<string>()))
			.ReturnsAsync((string teamName, string excludeTeamId) =>
			{
				// Example logic to simulate the "excludeTeamId" behavior
				if (teamName == "Existing Team" && excludeTeamId != "1") // Exclude team with ID "1"
				{
					return true; // The team name exists
				}
				return false; // The team name does not exist or is excluded
			});

		_mapperMock.Setup(mapper => mapper.Map<Team>(It.IsAny<TeamDto>())).Returns(new Team());
		_teamRepositoryMock.Setup(repo => repo.AddTeamAsync(It.IsAny<Team>()))
			.ReturnsAsync(new Team()); // Returns a Task<T> with a new Team object
									   // Act
		var result = await _service.CreateTeamAsync(teamName, status);

		// Assert
		Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
		Assert.Equal("Tạo đội thành công", result.Message);
		Assert.NotNull(result.Data);
		Assert.Equal(teamName, result.Data.TeamName);
	}

	// Test case 2: Team name is empty
	[Fact]
	public async Task CreateTeamAsync_ShouldReturnError_WhenTeamNameIsEmpty()
	{
		// Arrange
		var teamName = "";
		var status = 1;
		var errors = new Dictionary<string, string>
		{
			{ "teamEmpty", TeamMessage.Errors.TeamNameEmpty }
		};

		// Act
		var result = await _service.CreateTeamAsync(teamName, status);

		// Assert
		Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
		Assert.Equal(errors, result.Errors);
		Assert.NotNull(result.Data);
	}

	// Test case 3: Team name is too long
	[Fact]
	public async Task CreateTeamAsync_ShouldReturnError_WhenTeamNameIsTooLong()
	{
		// Arrange
		var teamName = new string('A', 51); // Team name with length > 50
		var status = 1;
		var errors = new Dictionary<string, string>
		{
			{ "largerTeamNameLength", TeamMessage.Errors.TeamNameEmpty }
		};

		// Act
		var result = await _service.CreateTeamAsync(teamName, status);

		// Assert
		Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
		Assert.Equal(errors, result.Errors);
		Assert.NotNull(result.Data);
	}

	// Test case 4: Team name already exists
	[Fact]
	public async Task CreateTeamAsync_ShouldReturnError_WhenTeamNameExists()
	{
		// Arrange
		var teamName = "Existing Team";
		var status = 1;
		var errors = new Dictionary<string, string>
		{
			{ "existTeamName", TeamMessage.Errors.ExistTeamName }
		};

		// Adjusting mock setup to include excludeTeamId
		_teamRepositoryMock.Setup(repo => repo.IsTeamNameExistsAsync(It.IsAny<string>(), It.IsAny<string>()))
			.ReturnsAsync((string teamName, string excludeTeamId) =>
			{
				// Example logic to simulate the "excludeTeamId" behavior
				if (teamName == "Existing Team" && excludeTeamId != "1") // Exclude team with ID "1"
				{
					return true; // The team name exists
				}
				return false; // The team name does not exist or is excluded
			});


		// Act
		var result = await _service.CreateTeamAsync(teamName, status);

		// Assert
		Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
		Assert.Equal(errors, result.Errors);
		Assert.NotNull(result.Data);
	}

	[Fact]
	public async Task CreateTeamAsync_ShouldReturnError_WhenExceptionOccurs()
	{
		// Arrange
		var teamName = "New Team";
		var status = -2;
		var errors = new Dictionary<string, string>
	{
		{ "exception", "Some error message" }
	};

		// Mock setup to simulate that no team exists
		_teamRepositoryMock.Setup(repo => repo.IsTeamNameExistsAsync(It.IsAny<string>(), It.IsAny<string>()))
			.ReturnsAsync(false); // Simulate no team exists

		// Mock the mapping to return a Team object
		_mapperMock.Setup(mapper => mapper.Map<Team>(It.IsAny<TeamDto>())).Returns(new Team());

		// Simulate an exception when adding the team
		_teamRepositoryMock.Setup(repo => repo.AddTeamAsync(It.IsAny<Team>())).ThrowsAsync(new Exception("Some error message"));

		// Act
		var result = await _service.CreateTeamAsync(teamName, status);

		// Assert
		// Ensure that the status is Failed because of the exception
		Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);  // This should be Failed, not Success
		Assert.Equal("Đã xảy ra lỗi hệ thống.", result.Message);  // Ensure the error message is returned
		Assert.Equal(errors, result.Errors);  // Ensure that the exception message is in the Errors dictionary
	}

	[Fact]
	public async Task CreateTeamAsync_ShouldTrimTeamName_WhenInputHasSpaces()
	{
		// Arrange
		var teamName = "  New Team  ";
		var status = 1;

		_teamRepositoryMock.Setup(repo => repo.IsTeamNameExistsAsync(It.IsAny<string>(), It.IsAny<string>()))
			.ReturnsAsync(false);
		_mapperMock.Setup(mapper => mapper.Map<Team>(It.IsAny<TeamDto>())).Returns(new Team());
		_teamRepositoryMock.Setup(repo => repo.AddTeamAsync(It.IsAny<Team>())).ReturnsAsync(new Team());

		// Act
		var result = await _service.CreateTeamAsync(teamName, status);

		// Assert
		Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
		Assert.Equal("New Team", result.Data.TeamName.Trim()); // Check after trim
	}
	[Fact]
	public async Task CreateTeamAsync_ShouldReturnSuccess_WhenStatusIsNull()
	{
		// Arrange
		var teamName = "New Team";
		int? status = null; // Status is null

		_teamRepositoryMock.Setup(repo => repo.IsTeamNameExistsAsync(It.IsAny<string>(), It.IsAny<string>()))
			.ReturnsAsync(false);
		_mapperMock.Setup(mapper => mapper.Map<Team>(It.IsAny<TeamDto>())).Returns(new Team());
		_teamRepositoryMock.Setup(repo => repo.AddTeamAsync(It.IsAny<Team>())).ReturnsAsync(new Team());

		// Act
		var result = await _service.CreateTeamAsync(teamName, status);

		// Assert
		Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
		Assert.Equal(1, result.Data.Status); // Default to 1
	}

	[Fact]
	public async Task CreateTeamAsync_ShouldReturnSuccess_WhenTeamNameHasSpecialCharacters()
	{
		// Arrange
		var teamName = "Team #1$%!";
		var status = 1;

		_teamRepositoryMock.Setup(repo => repo.IsTeamNameExistsAsync(It.IsAny<string>(), It.IsAny<string>()))
			.ReturnsAsync(false);
		_mapperMock.Setup(mapper => mapper.Map<Team>(It.IsAny<TeamDto>())).Returns(new Team());
		_teamRepositoryMock.Setup(repo => repo.AddTeamAsync(It.IsAny<Team>())).ReturnsAsync(new Team());

		// Act
		var result = await _service.CreateTeamAsync(teamName, status);

		// Assert
		Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
		Assert.Equal(teamName, result.Data.TeamName);
	}
	[Fact]
	public async Task CreateTeamAsync_ShouldReturnPartialData_WhenValidationFails()
	{
		// Arrange
		var teamName = ""; // Invalid input
		var status = 5;

		// Act
		var result = await _service.CreateTeamAsync(teamName, status);

		// Assert
		Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
		Assert.NotNull(result.Data);
		Assert.Equal(status, result.Data.Status);
		Assert.Equal(teamName, result.Data.TeamName); // Vẫn giữ tên cũ để FE có thể show lại cho user chỉnh sửa
	}

	#endregion


}
