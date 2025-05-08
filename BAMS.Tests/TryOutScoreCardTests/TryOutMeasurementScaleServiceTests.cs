using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Moq;
using static BasketballAcademyManagementSystemAPI.Common.Messages.ApiResponseMessage;

namespace BAMS.Tests.TryOutScoreCardTests
{
    public class TryOutMeasurementScaleServiceTests
    {
        private readonly Mock<ITryOutMeasurementScaleRepository> _repositoryMock;
        private readonly TryOutMeasurementScaleService _service;

        public TryOutMeasurementScaleServiceTests()
        {
            _repositoryMock = new Mock<ITryOutMeasurementScaleRepository>();
            _service = new TryOutMeasurementScaleService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetSkillTreeAsync_ShouldReturnRootSkills_WhenRepositoryReturnsData()
        {
            // Arrange
            var skills = new List<TryOutMeasurementScale>
            {
                new TryOutMeasurementScale { MeasurementScaleCode = "1", MeasurementName = "Skill 1", ParentMeasurementScaleCode = null, SortOrder = 2 },
                new TryOutMeasurementScale { MeasurementScaleCode = "2", MeasurementName = "Skill 2", ParentMeasurementScaleCode = null, SortOrder = 1 },
                new TryOutMeasurementScale { MeasurementScaleCode = "3", MeasurementName = "Skill 3", ParentMeasurementScaleCode = "1", SortOrder = 3 }
            };
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(skills);

            // Act
            var result = await _service.GetSkillTreeAsync();

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count); // Only root skills
            Assert.Equal("2", result.Data[0].MeasurementScaleCode); // Sorted by SortOrder
            Assert.Equal("1", result.Data[1].MeasurementScaleCode);
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.Equal(ApiResponseSuccessMessage.ApiSuccessMessage, result.Message);
        }

        [Fact]
        public async Task GetSkillTreeAsync_ShouldHandleException_WhenRepositoryThrowsException()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Something went wrong"));

            // Act
            var result = await _service.GetSkillTreeAsync();

            // Assert
            Assert.Null(result.Data);
            Assert.NotNull(result.Errors);
            Assert.Contains("Something went wrong", result.Errors);
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Equal(ApiResponseErrorMessage.ApiFailedMessage, result.Message);
        }

        [Fact]
        public async Task GetSkillTreeAsync_ShouldReturnEmptyList_WhenRepositoryReturnsEmptyList()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<TryOutMeasurementScale>());

            // Act
            var result = await _service.GetSkillTreeAsync();

            // Assert
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data);
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.Equal(ApiResponseSuccessMessage.ApiSuccessMessage, result.Message);
        }
    }
}
