using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TeamFund;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Moq;
using Xunit;

namespace BAMS.Tests.TeamFundTest
{
    public class TeamFundServiceTest
    {
        private readonly Mock<ITeamFundRepository> _teamFundRepositoryMock;
        private readonly Mock<IGeneratePaymentService> _generatePaymentServiceMock;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<IParentRepository> _parentRepositoryMock;
        private readonly Mock<ISendMailService> _sendMailServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
        private readonly TeamFundService _service;

        public TeamFundServiceTest()
        {
            _teamFundRepositoryMock = new Mock<ITeamFundRepository>();
            _generatePaymentServiceMock = new Mock<IGeneratePaymentService>();
            _authServiceMock = new Mock<IAuthService>();
            _parentRepositoryMock = new Mock<IParentRepository>();
            _sendMailServiceMock = new Mock<ISendMailService>();
            _mapperMock = new Mock<IMapper>();
            _paymentRepositoryMock = new Mock<IPaymentRepository>();

            _service = new TeamFundService(
                _teamFundRepositoryMock.Object,
                _generatePaymentServiceMock.Object,
                _parentRepositoryMock.Object,
                _sendMailServiceMock.Object,
                _authServiceMock.Object,
                _mapperMock.Object,
                _paymentRepositoryMock.Object
            );
        }

        [Fact]
        public async Task AddExpendituresAsync_WhenExceptionThrown_ShouldReturnServerError()
        {
            // Arrange
            var expenditures = new List<CreateExpenditureDto>
            {
                new CreateExpenditureDto { Name = "Test Exception", Amount = 100, PayoutDate = DateTime.Now }
            };
            _authServiceMock.Setup(x => x.GetCurrentLoggedInUserInformationAsync())
                .ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _service.AddExpendituresAsync(expenditures, "1");

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Contains("Đã xảy ra lỗi hệ thống", result.Message);
        }

        

        [Fact]
        public async Task GetExpendituresAsync_WithNullTeamFundId_ShouldReturnFailed()
        {
            // Act
            var result = await _service.GetExpendituresAsync(null, 1, 10);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Equal("Team Id không được null hoặc bỏ trống", result.Message);
        }

        [Fact]
        public async Task DeleteExpendituresAsync_WithExistingExpenditure_ShouldReturnSuccess()
        {
            // Arrange
            _teamFundRepositoryMock.Setup(x => x.DeleteExpenditureByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteExpendituresAsync("expenditureId123");

            // Assert
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.Equal("Xóa khoản chi thành công.", result.Message);
            _teamFundRepositoryMock.Verify(x => x.DeleteExpenditureByIdAsync("expenditureId123"), Times.Once);
        }

        [Fact]
        public async Task DeleteExpendituresAsync_WithNonExistingExpenditure_ShouldReturnFailed()
        {
            // Arrange
            _teamFundRepositoryMock.Setup(x => x.DeleteExpenditureByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var result = await _service.DeleteExpendituresAsync("nonExistingId");

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Contains("Không tìm thấy khoản chi", result.Message);
        }


    }
}
