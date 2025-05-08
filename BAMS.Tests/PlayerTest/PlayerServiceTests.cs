using BasketballAcademyManagementSystemAPI.Application.DTOs.PlayerManagement;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Moq;
namespace BAMS.Tests.PlayerServiceTest;

public class PlayerServiceTests
{
	private readonly Mock<IPlayerRepository> _mockPlayerRepository;
	private readonly Mock<IUserTeamHistoryService> _mockUserTeamHistoryService;
	private readonly Mock<ITeamRepository> _mockTeamRepository;
	private readonly PlayerService _playerService;

	public PlayerServiceTests()
	{
		_mockPlayerRepository = new Mock<IPlayerRepository>();
		_mockUserTeamHistoryService = new Mock<IUserTeamHistoryService>();
		_mockTeamRepository = new Mock<ITeamRepository>();

		_playerService = new PlayerService(
			_mockPlayerRepository.Object,
			_mockUserTeamHistoryService.Object,
			Mock.Of<ISendMailService>(),  // Nếu không cần thiết, bạn có thể bỏ qua việc mock SendMailService
			_mockTeamRepository.Object
		);
	}

	// Test case 1: Kiểm tra khi không tìm thấy team
	[Fact]
	public async Task AssignPlayersToTeamAsync_TeamNotFound_ReturnsFailedResult()
	{
		// Arrange
		var playerIds = new List<string> { "player1" };
		var teamId = "team1";
		_mockTeamRepository.Setup(repo => repo.GetTeamByIdAsync(teamId)).ReturnsAsync((Team)null); // Không tìm thấy team

		// Act
		var result = await _playerService.AssignPlayersToTeamAsync(playerIds, teamId);

		// Assert
		Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
		Assert.Contains("teamNotFound", result.Errors.Keys);
	}

	// Test case 2: Kiểm tra khi không tìm thấy player
	[Fact]
	public async Task AssignPlayersToTeamAsync_PlayerNotFound_ReturnsFailedResult()
	{
		// Arrange
		var playerIds = new List<string> { "player1" };
		var teamId = "team1";
		var team = new Team { TeamId = teamId };
		_mockTeamRepository.Setup(repo => repo.GetTeamByIdAsync(teamId)).ReturnsAsync(team); // Tìm thấy team
		_mockPlayerRepository.Setup(repo => repo.GetPlayerByIdAsync("player1")).ReturnsAsync((User)null); // Không tìm thấy player

		// Act
		var result = await _playerService.AssignPlayersToTeamAsync(playerIds, teamId);

		// Assert
		var resultList = result.Data as List<PlayerAssignResultDto>;
		var playerResult = resultList.First();
		Assert.False(playerResult.Success);
		Assert.Equal("Không tìm thấy cầu thủ.", playerResult.Message);
	}

	// Test case 3: Kiểm tra khi cầu thủ đã ở trong đội
	[Fact]
	public async Task AssignPlayersToTeamAsync_PlayerAlreadyInTeam_ReturnsFailedResult()
	{
		// Arrange
		var playerIds = new List<string> { "player1" };
		var teamId = "team1";
		var team = new Team { TeamId = teamId };
		var player = new User { UserId = "player1", Player = new Player { UserId = "player1", TeamId = teamId } }; // Player đã ở trong đội

		_mockTeamRepository.Setup(repo => repo.GetTeamByIdAsync(teamId)).ReturnsAsync(team); // Tìm thấy team
		_mockPlayerRepository.Setup(repo => repo.GetPlayerByIdAsync("player1")).ReturnsAsync(player); // Tìm thấy player

		// Act
		var result = await _playerService.AssignPlayersToTeamAsync(playerIds, teamId);

		// Assert
		var resultList = result.Data as List<PlayerAssignResultDto>;
		var playerResult = resultList.First();
		Assert.False(playerResult.Success);
		Assert.Equal("Cầu thủ đã ở trong đội này trước đó.", playerResult.Message);
	}

	//// Test case 4: Kiểm tra khi gán player vào đội thành công
	[Fact]
	public async Task AssignPlayersToTeamAsync_PlayerAssignedToTeam_ReturnsSuccessResult()
	{
		// Arrange
		var playerIds = new List<string> { "player1" };
		var teamId = "team1";
		var team = new Team { TeamId = teamId };
		var player = new User { UserId = "player1", Player = new Player { UserId = "player1", TeamId = "oldTeam" } }; // Player chưa thuộc đội

		_mockTeamRepository.Setup(repo => repo.GetTeamByIdAsync(teamId)).ReturnsAsync(team); // Tìm thấy team
		_mockPlayerRepository.Setup(repo => repo.GetPlayerByIdAsync("player1")).ReturnsAsync(player); // Tìm thấy player
		_mockPlayerRepository.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask); // Giả lập lưu vào database

		_mockPlayerRepository.Setup(repo => repo.SaveChangesAsync()).Returns(Task.FromResult(1));

		// Act
		var result = await _playerService.AssignPlayersToTeamAsync(playerIds, teamId);

		// Assert
		var resultList = result.Data as List<PlayerAssignResultDto>;
		var playerResult = resultList.First();
		Assert.True(playerResult.Success);
		Assert.Equal("Gán đội thành công.", playerResult.Message);
	}

	// Test case 5: Lỗi khi ghi lịch sử đội
	[Fact]
	public async Task AssignPlayersToTeamAsync_ErrorInSavingHistory_ReturnsFailedResult()
	{
		// Arrange
		var playerIds = new List<string> { "player1" };
		var teamId = "team1";
		var team = new Team { TeamId = teamId };
		var player = new User { UserId = "player1", Player = new Player { UserId = "player1", TeamId = "oldTeam" } };

		_mockTeamRepository.Setup(repo => repo.GetTeamByIdAsync(teamId)).ReturnsAsync(team); // Tìm thấy team
		_mockPlayerRepository.Setup(repo => repo.GetPlayerByIdAsync("player1")).ReturnsAsync(player); // Tìm thấy player
		_mockPlayerRepository.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask); // Giả lập lưu vào database

		_mockUserTeamHistoryService.Setup(service => service.UserAssignToNewTeamHistory(It.IsAny<string>(), It.IsAny<string>()))
			.ThrowsAsync(new InvalidOperationException("Database error")); // Giả lập lỗi khi ghi lịch sử

		// Act
		var result = await _playerService.AssignPlayersToTeamAsync(playerIds, teamId);

		// Assert
		var resultList = result.Data as List<PlayerAssignResultDto>;
		var playerResult = resultList.First();
		Assert.False(playerResult.Success);
		Assert.Equal("Lỗi ghi lịch sử đội: Database error", playerResult.Message);
	}

	[Fact]
	public async Task AssignPlayersToTeamAsync_EmptyPlayerIds_ReturnsFailedResult()
	{
		// Arrange
		var playerIds = new List<string>();  // Danh sách playerIds rỗng
		var teamId = "team1";
		var team = new Team { TeamId = teamId };

		// Giả lập trả về team hợp lệ
		_mockTeamRepository.Setup(repo => repo.GetTeamByIdAsync(teamId)).ReturnsAsync(team);

		// Act
		var result = await _playerService.AssignPlayersToTeamAsync(playerIds, teamId);

		// Assert
		Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
		Assert.Contains("playerIdsEmpty", result.Errors.Keys);  // Kiểm tra lỗi về danh sách playerIds rỗng
	}


	[Fact]
	public async Task AssignPlayersToTeamAsync_ErrorSavingPlayer_ReturnsFailedResult()
	{
		// Arrange
		var playerIds = new List<string> { "player1" };
		var teamId = "team1";
		var team = new Team { TeamId = teamId };
		var player = new User { UserId = "player1", Player = new Player { UserId = "player1", TeamId = "oldTeam" } };

		// Giả lập trả về team và player hợp lệ
		_mockTeamRepository.Setup(repo => repo.GetTeamByIdAsync(teamId)).ReturnsAsync(team);
		_mockPlayerRepository.Setup(repo => repo.GetPlayerByIdAsync("player1")).ReturnsAsync(player);

		// Giả lập lỗi khi lưu player vào database
		_mockPlayerRepository.Setup(repo => repo.SaveChangesAsync()).ThrowsAsync(new Exception("Error saving player"));

		// Act
		var result = await _playerService.AssignPlayersToTeamAsync(playerIds, teamId);

		// Assert
		var resultList = result.Data as List<PlayerAssignResultDto>;
		var playerResult = resultList.First();
		Assert.False(playerResult.Success);
		Assert.Equal("Lỗi xử lý cầu thủ: Error saving player", playerResult.Message);
	}


}
