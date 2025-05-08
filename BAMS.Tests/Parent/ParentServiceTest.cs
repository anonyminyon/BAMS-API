using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballAcademyManagementSystemAPI.Models; // Thêm dòng này để sử dụng class Parent từ đúng namespace

namespace BAMS.Tests.ParentTest
{
	public class ParentServiceTest
	{
		private readonly Mock<IParentRepository> _mockParentRepository;
		private readonly ParentService _parentService;

		public ParentServiceTest()
		{
			_mockParentRepository = new Mock<IParentRepository>();
			_parentService = new ParentService(_mockParentRepository.Object);
		}


		#region Assign Parent to Player
		// Test case 1: Kiểm tra khi cầu thủ không tồn tại
		[Fact]
		public async Task AddParentForPlayerAsync_PlayerNotFound_ReturnsFailedResult()
		{
			// Arrange
			var playerId = "player1";
			var parentId = "parent1";

			// Giả lập cầu thủ không tồn tại
			_mockParentRepository.Setup(repo => repo.GetPlayerByIdAsync(playerId)).ReturnsAsync((Player)null);

			// Act
			var result = await _parentService.AddParentForPlayerAsync(playerId, parentId);

			// Assert
			Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
			Assert.Contains("playerNotFound", result.Errors.Keys);
		}

		// Test case 2: Kiểm tra khi phụ huynh không tồn tại
		[Fact]
		public async Task AddParentForPlayerAsync_ParentNotFound_ReturnsFailedResult()
		{
			// Arrange
			var playerId = "player1";
			var parentId = "parent1";
			var player = new Player { UserId = playerId };

			// Giả lập cầu thủ tồn tại
			_mockParentRepository.Setup(repo => repo.GetPlayerByIdAsync(playerId)).ReturnsAsync(player);
			// Giả lập phụ huynh không tồn tại
			_mockParentRepository.Setup(repo => repo.GetParentByIdAsync(parentId)).ReturnsAsync((Parent)null);

			// Act
			var result = await _parentService.AddParentForPlayerAsync(playerId, parentId);

			// Assert
			Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
			Assert.Contains("parentNotFound", result.Errors.Keys);
		}

		// Test case 3: Kiểm tra khi không thể cập nhật phụ huynh cho cầu thủ
		[Fact]
		public async Task AddParentForPlayerAsync_FailToUpdate_ReturnsFailedResult()
		{
			// Arrange
			var playerId = "player1";
			var parentId = "parent1";
			var player = new Player { UserId = playerId };
			var parent = new Parent { UserId = parentId };

			// Giả lập cầu thủ và phụ huynh tồn tại
			_mockParentRepository.Setup(repo => repo.GetPlayerByIdAsync(playerId)).ReturnsAsync(player);
			_mockParentRepository.Setup(repo => repo.GetParentByIdAsync(parentId)).ReturnsAsync(parent);
			// Giả lập thất bại khi cập nhật
			_mockParentRepository.Setup(repo => repo.AddParentForPlayerAsync(playerId, parentId)).ReturnsAsync(false);

			// Act
			var result = await _parentService.AddParentForPlayerAsync(playerId, parentId);

			// Assert
			Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
			Assert.Contains("updateFailed", result.Errors.Keys);
		}

		// Test case 4: Kiểm tra khi cập nhật phụ huynh thành công
		[Fact]
		public async Task AddParentForPlayerAsync_Success_ReturnsSuccessResult()
		{
			// Arrange
			var playerId = "player1";
			var parentId = "parent1";
			var player = new Player { UserId = playerId };
			var parent = new Parent { UserId = parentId };

			// Giả lập cầu thủ và phụ huynh tồn tại
			_mockParentRepository.Setup(repo => repo.GetPlayerByIdAsync(playerId)).ReturnsAsync(player);
			_mockParentRepository.Setup(repo => repo.GetParentByIdAsync(parentId)).ReturnsAsync(parent);
			// Giả lập cập nhật thành công
			_mockParentRepository.Setup(repo => repo.AddParentForPlayerAsync(playerId, parentId)).ReturnsAsync(true);

			// Act
			var result = await _parentService.AddParentForPlayerAsync(playerId, parentId);

			// Assert
			Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
			Assert.Equal("Đã thêm phụ huynh cho cầu thủ thành công.", result.Message);
		}
		[Fact]
		public async Task AddParentForPlayerAsync_PlayerIdOrParentIdEmpty_ReturnsFailedResult()
		{
			// Arrange
			var playerId = "";  // PlayerId rỗng
			var parentId = "parent1";

			// Act
			var result = await _parentService.AddParentForPlayerAsync(playerId, parentId);

			// Assert
			Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
			Assert.Contains("playerNotFound", result.Errors.Keys); // Có thể kiểm tra nếu playerId là rỗng.
		}
		[Fact]
		public async Task AddParentForPlayerAsync_ParentIdEmpty_ReturnsFailedResult()
		{
			// Arrange
			var playerId = "player1";
			var parentId = "";  // ParentId rỗng

			// Giả lập cầu thủ tồn tại
			_mockParentRepository.Setup(repo => repo.GetPlayerByIdAsync(playerId)).ReturnsAsync(new Player { UserId = playerId });

			// Act
			var result = await _parentService.AddParentForPlayerAsync(playerId, parentId);

			// Assert
			Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
			Assert.Contains("parentNotFound", result.Errors.Keys); // Nếu parentId là rỗng.
		}

		[Fact]
		public async Task AddParentForPlayerAsync_UnhandledError_ReturnsFailedResult()
		{
			// Arrange
			var playerId = "player1";
			var parentId = "parent1";
			var player = new Player { UserId = playerId };
			var parent = new Parent { UserId = parentId };

			// Giả lập cầu thủ và phụ huynh tồn tại
			_mockParentRepository.Setup(repo => repo.GetPlayerByIdAsync(playerId)).ReturnsAsync(player);
			_mockParentRepository.Setup(repo => repo.GetParentByIdAsync(parentId)).ReturnsAsync(parent);
			// Giả lập lỗi không xác định
			_mockParentRepository.Setup(repo => repo.AddParentForPlayerAsync(playerId, parentId)).ThrowsAsync(new Exception("Unhandled error"));

			// Act
			var result = await _parentService.AddParentForPlayerAsync(playerId, parentId);

			// Assert
			Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
			Assert.Contains("exception", result.Errors.Keys);
			Assert.Equal("Unhandled error", result.Errors["exception"]);
		}
		[Fact]
		public async Task AddParentForPlayerAsync_ParentNotLinkedToPlayer_ReturnsFailedResult()
		{
			// Arrange
			var playerId = "player1";
			var parentId = "parent1";
			var player = new Player { UserId = playerId, ParentId = null };  // Cầu thủ chưa có phụ huynh
			var parent = new Parent { UserId = parentId };

			// Giả lập cầu thủ và phụ huynh tồn tại nhưng không có liên kết
			_mockParentRepository.Setup(repo => repo.GetPlayerByIdAsync(playerId)).ReturnsAsync(player);
			_mockParentRepository.Setup(repo => repo.GetParentByIdAsync(parentId)).ReturnsAsync(parent);

			// Giả lập không thể thêm phụ huynh cho cầu thủ
			_mockParentRepository.Setup(repo => repo.AddParentForPlayerAsync(playerId, parentId)).ReturnsAsync(false);

			// Act
			var result = await _parentService.AddParentForPlayerAsync(playerId, parentId);

			// Assert
			Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
			Assert.Contains("updateFailed", result.Errors.Keys);  // Thêm phụ huynh không thành công
		}
		[Fact]
		public async Task AddParentForPlayerAsync_PlayerNotFound_ParentValid_ReturnsFailedResult()
		{
			// Arrange
			var playerId = "invalidPlayerId";  // PlayerId không tồn tại
			var parentId = "parent1";  // ParentId hợp lệ

			// Giả lập cầu thủ không tồn tại
			_mockParentRepository.Setup(repo => repo.GetPlayerByIdAsync(playerId)).ReturnsAsync((Player)null);
			_mockParentRepository.Setup(repo => repo.GetParentByIdAsync(parentId)).ReturnsAsync(new Parent { UserId = parentId });

			// Act
			var result = await _parentService.AddParentForPlayerAsync(playerId, parentId);

			// Assert
			Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
			Assert.Contains("playerNotFound", result.Errors.Keys);  // Kiểm tra lỗi cầu thủ không tồn tại
		}
		[Fact]
		public async Task AddParentForPlayerAsync_ParentNotFound_PlayerValid_ReturnsFailedResult()
		{
			// Arrange
			var playerId = "player1";  // PlayerId hợp lệ
			var parentId = "invalidParentId";  // ParentId không tồn tại

			// Giả lập cầu thủ tồn tại
			_mockParentRepository.Setup(repo => repo.GetPlayerByIdAsync(playerId)).ReturnsAsync(new Player { UserId = playerId });
			_mockParentRepository.Setup(repo => repo.GetParentByIdAsync(parentId)).ReturnsAsync((Parent)null);  // Không tìm thấy phụ huynh

			// Act
			var result = await _parentService.AddParentForPlayerAsync(playerId, parentId);

			// Assert
			Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
			Assert.Contains("parentNotFound", result.Errors.Keys);  // Kiểm tra lỗi phụ huynh không tồn tại
		}
		[Fact]
		public async Task AddParentForPlayerAsync_SuccessButRepositoryReturnsError_ReturnsFailedResult()
		{
			// Arrange
			var playerId = "player1";
			var parentId = "parent1";
			var player = new Player { UserId = playerId };
			var parent = new Parent { UserId = parentId };

			// Giả lập cầu thủ và phụ huynh tồn tại
			_mockParentRepository.Setup(repo => repo.GetPlayerByIdAsync(playerId)).ReturnsAsync(player);
			_mockParentRepository.Setup(repo => repo.GetParentByIdAsync(parentId)).ReturnsAsync(parent);

			// Giả lập cập nhật thành công nhưng repository gặp sự cố
			_mockParentRepository.Setup(repo => repo.AddParentForPlayerAsync(playerId, parentId)).ThrowsAsync(new Exception("Repository error"));

			// Act
			var result = await _parentService.AddParentForPlayerAsync(playerId, parentId);

			// Assert
			Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
			Assert.Contains("exception", result.Errors.Keys);  // Kiểm tra lỗi repository
			Assert.Equal("Repository error", result.Errors["exception"]);
		}
		[Fact]
		public async Task AddParentForPlayerAsync_ParentAlreadyAssignedToAnotherPlayer_ReturnsFailedResult()
		{
			// Arrange
			var playerId = "player1";
			var parentId = "parent1";
			var player = new Player { UserId = playerId };
			var parent = new Parent { UserId = parentId };

			// Giả lập cầu thủ và phụ huynh
			_mockParentRepository.Setup(repo => repo.GetPlayerByIdAsync(playerId)).ReturnsAsync(player);
			_mockParentRepository.Setup(repo => repo.GetParentByIdAsync(parentId)).ReturnsAsync(parent);

			// Giả lập phụ huynh đã được gán cho một cầu thủ khác
			_mockParentRepository.Setup(repo => repo.AddParentForPlayerAsync(playerId, parentId)).ReturnsAsync(false);

			// Act
			var result = await _parentService.AddParentForPlayerAsync(playerId, parentId);

			// Assert
			Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
			Assert.Contains("updateFailed", result.Errors.Keys);  // Kiểm tra lỗi phụ huynh đã được gán cho cầu thủ khác
		}
		#endregion

	}
}
