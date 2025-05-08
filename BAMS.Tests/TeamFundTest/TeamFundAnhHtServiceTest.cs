using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TeamFund;

namespace BAMS.Tests.TeamFundTest
{
	public class TeamFundAnhHtServiceTest
	{
		[Fact]
		public async Task ApproveTeamFundAsync_TeamFundNotFoundOrAlreadyApproved_ReturnsFailedResult()
		{
			// Arrange
			var teamFundId = "teamFund123";
			var mockTeamFundRepository = new Mock<ITeamFundRepository>();

			// Giả lập trường hợp không tìm thấy TeamFund
			mockTeamFundRepository.Setup(repo => repo.GetTeamFundByIdAsync(teamFundId)).ReturnsAsync((TeamFund)null);

			var teamFundService = new TeamFundService(
				mockTeamFundRepository.Object,
				Mock.Of<IGeneratePaymentService>(),
				Mock.Of<IParentRepository>(),
				Mock.Of<ISendMailService>(),
				Mock.Of<IAuthService>(),
				Mock.Of<IMapper>(),
				Mock.Of<IPaymentRepository>()
			);

			// Act
			var result = await teamFundService.ApproveTeamFundAsync(teamFundId);

			// Assert
			Assert.Equal("TeamFund không tồn tại hoặc đã được duyệt", result.Message);
		}

		#region Approve Team Fund
		[Fact]
		public async Task ApproveTeamFundAsync_TeamFundAlreadyApproved_ReturnsFailedResult()
		{
			// Arrange
			var teamFundId = "teamFund123";
			var teamFund = new TeamFund { TeamFundId = teamFundId, Status = TeamFundStatusConstant.APPROVED }; // Quỹ đã được duyệt
			var mockTeamFundRepository = new Mock<ITeamFundRepository>();
			mockTeamFundRepository.Setup(repo => repo.GetTeamFundByIdAsync(teamFundId)).ReturnsAsync(teamFund);

			var teamFundService = new TeamFundService(
				mockTeamFundRepository.Object,
				Mock.Of<IGeneratePaymentService>(),
				Mock.Of<IParentRepository>(),
				Mock.Of<ISendMailService>(),
				Mock.Of<IAuthService>(),
				Mock.Of<IMapper>(),
				Mock.Of<IPaymentRepository>()
			);

			// Act
			var result = await teamFundService.ApproveTeamFundAsync(teamFundId);

			// Assert
			Assert.Equal("TeamFund không tồn tại hoặc đã được duyệt", result.Message);
		}
	
		
		[Fact]
		public async Task ApproveTeamFundAsync_TeamFundIdIsNullOrEmpty_ReturnsFailedResult()
		{
			// Arrange
			var teamFundId = "";  // teamFundId rỗng
			var mockTeamFundRepository = new Mock<ITeamFundRepository>();

			var teamFundService = new TeamFundService(
				mockTeamFundRepository.Object,
				Mock.Of<IGeneratePaymentService>(),
				Mock.Of<IParentRepository>(),
				Mock.Of<ISendMailService>(),
				Mock.Of<IAuthService>(),
				Mock.Of<IMapper>(),
				Mock.Of<IPaymentRepository>()
			);

			// Act
			var result = await teamFundService.ApproveTeamFundAsync(teamFundId);

			// Assert
			Assert.Equal("TeamFund không tồn tại hoặc đã được duyệt", result.Message);
		}
		

		[Fact]
		public async Task ApproveTeamFundAsync_TeamFundNotFound_ReturnsFailedResult()
		{
			// Arrange
			var teamFundId = "nonExistentTeamFundId";
			var mockTeamFundRepository = new Mock<ITeamFundRepository>();

			// Giả lập không tìm thấy TeamFund trong cơ sở dữ liệu
			mockTeamFundRepository.Setup(repo => repo.GetTeamFundByIdAsync(teamFundId)).ReturnsAsync((TeamFund)null);

			var teamFundService = new TeamFundService(
				mockTeamFundRepository.Object,
				Mock.Of<IGeneratePaymentService>(),
				Mock.Of<IParentRepository>(),
				Mock.Of<ISendMailService>(),
				Mock.Of<IAuthService>(),
				Mock.Of<IMapper>(),
				Mock.Of<IPaymentRepository>()
			);

			// Act
			var result = await teamFundService.ApproveTeamFundAsync(teamFundId);

			// Assert
			Assert.Equal("TeamFund không tồn tại hoặc đã được duyệt", result.Message);
		}
		#endregion
		#region Test GetTeamFundsAsync

		// Test case 1: Kiểm tra khi không có quỹ đội nào (empty list)
		[Fact]
		public async Task GetTeamFundsAsync_NoTeamFunds_ReturnsFailedResult()
		{
			// Arrange
			var filter = new TeamFundFilterDto();  // Giả lập filter
			var mockTeamFundRepository = new Mock<ITeamFundRepository>();

			// Giả lập không có dữ liệu quỹ đội
			mockTeamFundRepository.Setup(repo => repo.GetTeamFundsAsync(It.IsAny<TeamFundFilterDto>()))
				.ReturnsAsync(new List<TeamFundListDto>());  // Trả về danh sách rỗng

			var teamFundService = new TeamFundService(
				mockTeamFundRepository.Object,
				Mock.Of<IGeneratePaymentService>(),
				Mock.Of<IParentRepository>(),
				Mock.Of<ISendMailService>(),
				Mock.Of<IAuthService>(),
				Mock.Of<IMapper>(),
				Mock.Of<IPaymentRepository>()
			);

			// Act
			var result = await teamFundService.GetTeamFundsAsync(filter);

			// Assert
			Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
			Assert.Equal("Không tìm thấy quỹ đội nào phù hợp", result.Message);
			Assert.Empty(result.Data);  // Kiểm tra dữ liệu trả về là danh sách rỗng
		}

		#region Test GetTeamFundsAsync when valid data is returned

		// Test case 2: Kiểm tra khi trả về dữ liệu thành công
		[Fact]
		public async Task GetTeamFundsAsync_Success_ReturnsTeamFunds()
		{
			// Arrange
			var filter = new TeamFundFilterDto();  // Giả lập filter
			var mockTeamFundRepository = new Mock<ITeamFundRepository>();

			// Giả lập trả về dữ liệu quỹ đội
			var teamFunds = new List<TeamFundListDto>
			{
				new TeamFundListDto { TeamFundId = "teamFund123", TeamId = "team1" },
				new TeamFundListDto { TeamFundId = "teamFund124", TeamId = "team2" }
			};

			mockTeamFundRepository.Setup(repo => repo.GetTeamFundsAsync(It.IsAny<TeamFundFilterDto>()))
				.ReturnsAsync(teamFunds);

			var teamFundService = new TeamFundService(
				mockTeamFundRepository.Object,
				Mock.Of<IGeneratePaymentService>(),
				Mock.Of<IParentRepository>(),
				Mock.Of<ISendMailService>(),
				Mock.Of<IAuthService>(),
				Mock.Of<IMapper>(),
				Mock.Of<IPaymentRepository>()
			);

			// Act
			var result = await teamFundService.GetTeamFundsAsync(filter);

			// Assert
			Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
			Assert.Equal("Lấy danh sách quỹ đội thành công", result.Message);
			Assert.Equal(2, result.Data.Count);  // Kiểm tra số lượng quỹ đội trả về
			Assert.Equal("teamFund123", result.Data[0].TeamFundId);  // Kiểm tra ID của quỹ đội
		}

		#endregion

		#region Test GetTeamFundsAsync when error occurs

		// Test case 3: Kiểm tra khi có lỗi khi lấy dữ liệu từ repository
		[Fact]
		public async Task GetTeamFundsAsync_ErrorGettingData_ReturnsFailedResult()
		{
			// Arrange
			var filter = new TeamFundFilterDto();  // Giả lập filter
			var mockTeamFundRepository = new Mock<ITeamFundRepository>();

			// Giả lập lỗi khi gọi repository
			mockTeamFundRepository.Setup(repo => repo.GetTeamFundsAsync(It.IsAny<TeamFundFilterDto>()))
				.ThrowsAsync(new Exception("Database error"));

			var teamFundService = new TeamFundService(
				mockTeamFundRepository.Object,
				Mock.Of<IGeneratePaymentService>(),
				Mock.Of<IParentRepository>(),
				Mock.Of<ISendMailService>(),
				Mock.Of<IAuthService>(),
				Mock.Of<IMapper>(),
				Mock.Of<IPaymentRepository>()
			);

			// Act
			var result = await teamFundService.GetTeamFundsAsync(filter);

			// Assert
			Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
			Assert.Contains("Database error", result.Errors["exception"]);  // Kiểm tra lỗi trong Errors
		}


		#endregion

		#endregion

	}
}
