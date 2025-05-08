using System.Globalization;
using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.MemberRegistrationSession;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using log4net;
using Moq;
using Xunit;
using static BasketballAcademyManagementSystemAPI.Common.Messages.MemberRegistrationSessionMessage;

namespace BAMS.Tests.MemberRegistrationSessionTest
{
    public class MemberRegistrationSessionTest
    {
        private readonly Mock<IMemberRegistrationSessionRepository> _memberRegistrationSessionRepositoryMock;
        private readonly Mock<IManagerRegistrationRepository> _managerRegistrationRepositoryMock;
        private readonly Mock<IPlayerRegistrationRepository> _playerRegistrationRepositoryMock;
        private readonly Mock<ITryOutScorecardRepository> _tryOutScorecardRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly MemberRegistrationSessionService _service;

        public MemberRegistrationSessionTest()
        {
            _memberRegistrationSessionRepositoryMock = new Mock<IMemberRegistrationSessionRepository>();
            _managerRegistrationRepositoryMock = new Mock<IManagerRegistrationRepository>();
            _playerRegistrationRepositoryMock = new Mock<IPlayerRegistrationRepository>();
            _tryOutScorecardRepositoryMock = new Mock<ITryOutScorecardRepository>();
            _mapperMock = new Mock<IMapper>();

            _service = new MemberRegistrationSessionService(
                _memberRegistrationSessionRepositoryMock.Object,
                _managerRegistrationRepositoryMock.Object,
                _playerRegistrationRepositoryMock.Object,
                _tryOutScorecardRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        #region CreateAsync

        // UTCID01
        [Fact]
        public async Task CreateAsync_ShouldReturnSuccess_WhenValidDto()
        {
            // Arrange
            var dto = new CreateMemberRegistrationSessionRequestDto
            {
                RegistrationName = "Test Session",
                Description = "Test Description",
                StartDate = "21/02/2025 23:59:59",
                EndDate = "24/02/2025 23:59:59",
                IsAllowPlayerRecruit = true,
                IsAllowManagerRecruit = true,
                IsEnable = true
            };

            var session = new MemberRegistrationSession
            {
                MemberRegistrationSessionId = 1,
                RegistrationName = dto.RegistrationName,
                Description = dto.Description,
                StartDate = DateTime.ParseExact(dto.StartDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact(dto.EndDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                IsAllowPlayerRecruit = dto.IsAllowPlayerRecruit,
                IsAllowManagerRecruit = dto.IsAllowManagerRecruit,
                IsEnable = dto.IsEnable,
                CreatedAt = DateTime.Now
            };

            _memberRegistrationSessionRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<MemberRegistrationSession>())).Returns(Task.CompletedTask);
            _mapperMock.Setup(mapper => mapper
                .Map<MemberRegistrationSessionResponseDto>(It.IsAny<MemberRegistrationSession>()))
                .Returns(new MemberRegistrationSessionResponseDto());

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.Equal(MemberRegistrationSessionSuccessMessage.CreateSessionSuccessfully, result.Message);
            Assert.NotNull(result.Data);
        }

        // UTCID02
        [Fact]
        public async Task CreateAsync_ShouldReturnFailed_WhenRegistrationNameIsBlank()
        {
            // Arrange
            var dto = new CreateMemberRegistrationSessionRequestDto
            {
                RegistrationName = "",
                Description = "Test Description",
                StartDate = "21/02/2025 23:59:59",
                EndDate = "24/02/2025 23:59:59",
                IsAllowPlayerRecruit = true,
                IsAllowManagerRecruit = true,
                IsEnable = false
            };

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Equal(MemberRegistrationSessionErrorMessage.CreateSessionFailed, result.Message);
            Assert.Contains(MemberRegistrationSessionErrorMessage.RegistrationNameCannotBeEmpty, result.Errors);
            Assert.Null(result.Data);
        }

        // UTCID03
        [Fact]
        public async Task CreateAsync_ShouldReturnFailed_WhenRegistrationNameTooLong()
        {
            // Arrange
            var dto = new CreateMemberRegistrationSessionRequestDto
            {
                RegistrationName = new string('A', 256),
                Description = "Test Description",
                StartDate = "21/02/2025 23:59:59",
                EndDate = "24/02/2025 23:59:59",
                IsAllowPlayerRecruit = true,
                IsAllowManagerRecruit = true,
                IsEnable = true
            };

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Equal(MemberRegistrationSessionErrorMessage.CreateSessionFailed, result.Message);
            Assert.Contains(MemberRegistrationSessionErrorMessage.RegistrationNameTooLong, result.Errors);
            Assert.Null(result.Data);
        }

        // UTCID04
        [Fact]
        public async Task CreateAsync_ShouldReturnFailed_WhenInvalidDateFormat()
        {
            // Arrange
            var dto = new CreateMemberRegistrationSessionRequestDto
            {
                RegistrationName = "Test Session",
                Description = "Test Description",
                StartDate = "31-02-2025 23:59:59",
                EndDate = "24/02/2025 23:59:59",
                IsAllowPlayerRecruit = true,
                IsAllowManagerRecruit = true,
                IsEnable = false
            };

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Equal(MemberRegistrationSessionErrorMessage.CreateSessionFailed, result.Message);
            Assert.Contains(MemberRegistrationSessionErrorMessage.InvalidDateTimeFormat, result.Errors);
            Assert.Null(result.Data);
        }

        // UTCID05
        [Fact]
        public async Task CreateAsync_ShouldReturnFailed_WhenEndDateLessThanMinimumSessionOpenHours()
        {
            // Arrange
            var dto = new CreateMemberRegistrationSessionRequestDto
            {
                RegistrationName = "Test Session",
                Description = "Test Description",
                StartDate = "21/02/2025 23:59:59",
                EndDate = "22/02/2025 23:59:59",
                IsAllowPlayerRecruit = true,
                IsAllowManagerRecruit = true,
                IsEnable = true
            };

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Equal(MemberRegistrationSessionErrorMessage.CreateSessionFailed, result.Message);
            Assert.Contains(MemberRegistrationSessionErrorMessage.InvalidMinimumSessionOpenHours, result.Errors);
            Assert.Null(result.Data);
        }

        // UTCID06
        [Fact]
        public async Task CreateAsync_ShouldReturnFailed_WhenStartDateGreaterThanEndDate()
        {
            // Arrange
            var dto = new CreateMemberRegistrationSessionRequestDto
            {
                RegistrationName = "Test Session",
                Description = "Test Description",
                StartDate = "24/02/2025 23:59:59",
                EndDate = "21/02/2025 23:59:59",
                IsAllowPlayerRecruit = true,
                IsAllowManagerRecruit = true,
                IsEnable = false
            };

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Equal(MemberRegistrationSessionErrorMessage.CreateSessionFailed, result.Message);
            Assert.Contains(MemberRegistrationSessionErrorMessage.InvalidMinimumSessionOpenHours, result.Errors);
            Assert.Null(result.Data);
        }

        // UTCID07
        [Fact]
        public async Task CreateAsync_ShouldReturnFailed_WhenInvalidTypeOfRecruits()
        {
            // Arrange
            var dto = new CreateMemberRegistrationSessionRequestDto
            {
                RegistrationName = "Test Session",
                Description = "Test Description",
                StartDate = "21/02/2025 23:59:59",
                EndDate = "24/02/2025 23:59:59",
                IsAllowPlayerRecruit = false,
                IsAllowManagerRecruit = false,
                IsEnable = true
            };

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Equal(MemberRegistrationSessionErrorMessage.CreateSessionFailed, result.Message);
            Assert.Contains(MemberRegistrationSessionErrorMessage.InvalidTypeOfRecruits, result.Errors);
            Assert.Null(result.Data);
        }
        #endregion

        #region GetDetailsAsync
        [Fact]
        public async Task GetDetailsAsync_ShouldReturnSessionDetails_WhenSessionExists()
        {
            // Arrange
            var sessionId = 1;
            var session = new MemberRegistrationSession
            {
                MemberRegistrationSessionId = sessionId,
                RegistrationName = "Test Session"
            };

            _memberRegistrationSessionRepositoryMock.Setup(repo => repo.GetByIdAsync(sessionId)).ReturnsAsync(session);
            _mapperMock.Setup(mapper => mapper.Map<MemberRegistrationSessionResponseDto>(session)).Returns(new MemberRegistrationSessionResponseDto());

            // Act
            var result = await _service.GetDetailsAsync(sessionId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetDetailsAsync_ShouldReturnNull_WhenSessionDoesNotExist()
        {
            // Arrange
            var sessionId = -1;
            _memberRegistrationSessionRepositoryMock.Setup(repo => repo.GetByIdAsync(sessionId)).ReturnsAsync((MemberRegistrationSession)null);

            // Act
            var result = await _service.GetDetailsAsync(sessionId);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region UpdateAsync

        // UTCID01
        [Fact]
        public async Task UpdateAsync_ShouldReturnSuccess_WhenValidData()
        {
            // Arrange
            var dto = new UpdateMemberRegistrationSessionRequestDto
            {
                RegistrationName = "Valid Name",
                Description = "Description",
                StartDate = "21/02/2025 10:00:00",
                EndDate = "24/02/2025 10:00:00",
                IsAllowPlayerRecruit = true,
                IsAllowManagerRecruit = true,
                IsEnable = true
            };

            var session = new MemberRegistrationSession
            {
                MemberRegistrationSessionId = 1,
                RegistrationName = "Old Name",
                Description = "Old Description",
                StartDate = DateTime.ParseExact("21/02/2025 08:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("24/02/2025 08:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                IsAllowPlayerRecruit = true,
                IsAllowManagerRecruit = true,
                IsEnable = true
            };

            _memberRegistrationSessionRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(session);
            _mapperMock.Setup(m => m.Map<MemberRegistrationSessionResponseDto>(It.IsAny<MemberRegistrationSession>())).Returns(new MemberRegistrationSessionResponseDto());

            // Act
            var result = await _service.UpdateAsync(1, dto);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.Equal(MemberRegistrationSessionSuccessMessage.UpdateSessionSuccessfully, result.Message);
            Assert.NotNull(result.Data);
        }

        // UTCID02
        [Fact]
        public async Task UpdateAsync_ShouldReturnError_WhenIdNotFound()
        {
            // Arrange
            var dto = new UpdateMemberRegistrationSessionRequestDto
            {
                RegistrationName = "Valid Name",
                Description = "Description",
                StartDate = "21/02/2025 10:00:00",
                EndDate = "24/02/2025 10:00:00",
                IsAllowPlayerRecruit = true,
                IsAllowManagerRecruit = true,
                IsEnable = true
            };

            _memberRegistrationSessionRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((MemberRegistrationSession)null);

            // Act
            var result = await _service.UpdateAsync(-1, dto);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Contains(MemberRegistrationSessionErrorMessage.NotFoundRegistrationSession, result.Errors);
        }

        // UTCID03
        [Fact]
        public async Task UpdateAsync_ShouldReturnError_WhenRegistrationNameIsEmpty()
        {
            // Arrange
            var dto = new UpdateMemberRegistrationSessionRequestDto
            {
                RegistrationName = "",
                Description = "Description",
                StartDate = "21/02/2025 10:00:00",
                EndDate = "24/02/2025 10:00:00",
                IsAllowPlayerRecruit = true,
                IsAllowManagerRecruit = true,
                IsEnable = true
            };

            // Act
            var result = await _service.UpdateAsync(1, dto);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Contains(MemberRegistrationSessionErrorMessage.RegistrationNameCannotBeEmpty, result.Errors);
        }

        // UTCID04
        [Fact]
        public async Task UpdateAsync_ShouldReturnError_WhenRegistrationNameIsTooLong()
        {
            // Arrange
            var dto = new UpdateMemberRegistrationSessionRequestDto
            {
                RegistrationName = new string('A', 256),
                Description = "Description",
                StartDate = "21/02/2025 10:00:00",
                EndDate = "24/02/2025 10:00:00",
                IsAllowPlayerRecruit = true,
                IsAllowManagerRecruit = true,
                IsEnable = true
            };

            // Act
            var result = await _service.UpdateAsync(1, dto);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Contains(MemberRegistrationSessionErrorMessage.RegistrationNameTooLong, result.Errors);
        }

        // UTCID05
        [Fact]
        public async Task UpdateAsync_ShouldReturnError_WhenInvalidTypeOfRecruits()
        {
            // Arrange
            var dto = new UpdateMemberRegistrationSessionRequestDto
            {
                RegistrationName = "Valid Name",
                Description = "Description",
                StartDate = "21/02/2025 10:00:00",
                EndDate = "24/02/2025 10:00:00",
                IsAllowPlayerRecruit = false,
                IsAllowManagerRecruit = false,
                IsEnable = true
            };

            // Act
            var result = await _service.UpdateAsync(1, dto);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Contains(MemberRegistrationSessionErrorMessage.InvalidTypeOfRecruits, result.Errors);
        }

        // UTCID06
        [Fact]
        public async Task UpdateAsync_ShouldReturnError_WhenInvalidDateTimeFormat()
        {
            // Arrange
            var dto = new UpdateMemberRegistrationSessionRequestDto
            {
                RegistrationName = "Valid Name",
                Description = "Description",
                StartDate = "Invalid Date",
                EndDate = "24/02/2025 10:00:00",
                IsAllowPlayerRecruit = true,
                IsAllowManagerRecruit = true,
                IsEnable = true
            };

            // Act
            var result = await _service.UpdateAsync(1, dto);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Contains(MemberRegistrationSessionErrorMessage.InvalidDateTimeFormat, result.Errors);
        }

        // UTCID07
        [Fact]
        public async Task UpdateAsync_ShouldReturnError_WhenEndDateIsLessThanMinimumSessionOpenHours()
        {
            // Arrange
            var dto = new UpdateMemberRegistrationSessionRequestDto
            {
                RegistrationName = "Valid Name",
                Description = "Description",
                StartDate = "21/02/2025 10:00:00",
                EndDate = "24/02/2025 09:59:59",
                IsAllowPlayerRecruit = true,
                IsAllowManagerRecruit = true,
                IsEnable = true
            };

            // Act
            var result = await _service.UpdateAsync(1, dto);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Contains(MemberRegistrationSessionErrorMessage.InvalidMinimumSessionOpenHours, result.Errors);
        }

        // UTCID08
        [Fact]
        public async Task UpdateAsync_ShouldReturnError_WhenStartDateGreaterThanEndDate()
        {
            // Arrange
            var dto = new UpdateMemberRegistrationSessionRequestDto
            {
                RegistrationName = "Valid Name",
                Description = "Description",
                StartDate = "21/02/2025 10:00:00",
                EndDate = "21/02/2025 09:59:59",
                IsAllowPlayerRecruit = true,
                IsAllowManagerRecruit = true,
                IsEnable = true
            };

            // Act
            var result = await _service.UpdateAsync(1, dto);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Contains(MemberRegistrationSessionErrorMessage.InvalidMinimumSessionOpenHours, result.Errors);
        }
        #endregion

        #region ToggleMemberRegistratrionSessionStatusAsync
        [Fact]
        public async Task ToggleMemberRegistratrionSessionStatusAsync_SessionNotFound_ReturnsErrorResponse()
        {
            // Arrange
            int sessionId = 1;
            _memberRegistrationSessionRepositoryMock.Setup(repo => repo.GetByIdAsync(sessionId))
                .ReturnsAsync((MemberRegistrationSession)null);

            // Act
            var result = await _service.ToggleMemberRegistratrionSessionStatusAsync(sessionId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Contains(MemberRegistrationSessionErrorMessage.NotFoundRegistrationSession, result.Errors);
        }

        [Fact]
        public async Task ToggleMemberRegistratrionSessionStatusAsync_SessionFound_ReturnsSuccessResponse()
        {
            // Arrange
            int sessionId = 1;
            var session = new MemberRegistrationSession
            {
                MemberRegistrationSessionId = sessionId,
                RegistrationName = "Test Session",
                IsEnable = true,
                EndDate = DateTime.Now.AddDays(1)
            };
            _memberRegistrationSessionRepositoryMock.Setup(repo => repo.GetByIdAsync(sessionId))
                .ReturnsAsync(session);
            _memberRegistrationSessionRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<MemberRegistrationSession>()))
                .Returns(Task.CompletedTask);
            _mapperMock.Setup(mapper => mapper.Map<MemberRegistrationSessionResponseDto>(It.IsAny<MemberRegistrationSession>()))
                .Returns(new MemberRegistrationSessionResponseDto());

            // Act
            var result = await _service.ToggleMemberRegistratrionSessionStatusAsync(sessionId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.Equal(MemberRegistrationSessionSuccessMessage.DisableSessionSuccessfully, result.Message);
        }

        [Fact]
        public async Task ToggleMemberRegistratrionSessionStatusAsync_SessionEndDatePassed_ReturnsErrorResponse()
        {
            // Arrange
            int sessionId = 2;
            var session = new MemberRegistrationSession
            {
                MemberRegistrationSessionId = sessionId,
                RegistrationName = "Test Session",
                IsEnable = true,
                EndDate = DateTime.Now.AddDays(-1)
            };
            _memberRegistrationSessionRepositoryMock.Setup(repo => repo.GetByIdAsync(sessionId))
                .ReturnsAsync(session);

            // Act
            var result = await _service.ToggleMemberRegistratrionSessionStatusAsync(sessionId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Contains(MemberRegistrationSessionErrorMessage.CanNotToggleStatusOfOutDateSession, result.Errors);
        }
        #endregion

        #region GetFilteredPagedAsync

        [Fact]
        public async Task GetFilteredPagedAsync_ShouldReturnAll_WhenNoFilterProvided()
        {
            // Arrange
            var filter = new MemberRegistrationSessionFilterDto();

            var pagedResponse = new PagedResponseDto<MemberRegistrationSession>
            {
                Items = new List<MemberRegistrationSession>
                {
                    new MemberRegistrationSession { MemberRegistrationSessionId = 1, RegistrationName = "Session 1" },
                    new MemberRegistrationSession { MemberRegistrationSessionId = 2, RegistrationName = "Session 2" }
                },
                TotalRecords = 2,
                PageSize = 10,
                CurrentPage = 1,
                TotalPages = 1
            };

            _memberRegistrationSessionRepositoryMock
                .Setup(repo => repo.GetFilteredPagedAsync(filter))
                .ReturnsAsync(pagedResponse);

            // Act
            var result = await _service.GetFilteredPagedAsync(filter);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.TotalRecords);
            Assert.Equal(2, result.Data.Items.Count());
        }

        [Fact]
        public async Task GetFilteredPagedAsync_ShouldReturnFilteredByName()
        {
            // Arrange
            var filter = new MemberRegistrationSessionFilterDto { Name = "Session 1" };

            var pagedResponse = new PagedResponseDto<MemberRegistrationSession>
            {
                Items = new List<MemberRegistrationSession>
                {
                    new MemberRegistrationSession { MemberRegistrationSessionId = 1, RegistrationName = "Session 1" }
                },
                TotalRecords = 1,
                PageSize = 10,
                CurrentPage = 1,
                TotalPages = 1
            };

            _memberRegistrationSessionRepositoryMock
                .Setup(repo => repo.GetFilteredPagedAsync(filter))
                .ReturnsAsync(pagedResponse);

            // Act
            var result = await _service.GetFilteredPagedAsync(filter);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.TotalRecords);
            Assert.Single(result.Data.Items);
        }

        [Fact]
        public async Task GetFilteredPagedAsync_ShouldReturnFilteredByDateRange()
        {
            // Arrange
            var filter = new MemberRegistrationSessionFilterDto
            {
                StartDate = new DateTime(2025, 1, 1),
                EndDate = new DateTime(2025, 12, 31)
            };

            var pagedResponse = new PagedResponseDto<MemberRegistrationSession>
            {
                Items = new List<MemberRegistrationSession>
                {
                    new MemberRegistrationSession { MemberRegistrationSessionId = 1, RegistrationName = "Session 1", StartDate = new DateTime(2025, 6, 1), EndDate = new DateTime(2025, 6, 30) }
                },
                TotalRecords = 1,
                PageSize = 10,
                CurrentPage = 1,
                TotalPages = 1
            };

            _memberRegistrationSessionRepositoryMock
                .Setup(repo => repo.GetFilteredPagedAsync(filter))
                .ReturnsAsync(pagedResponse);

            // Act
            var result = await _service.GetFilteredPagedAsync(filter);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.TotalRecords);
            Assert.Single(result.Data.Items);
        }

        [Fact]
        public async Task GetFilteredPagedAsync_ShouldReturnFilteredByStartDate()
        {
            // Arrange
            var filter = new MemberRegistrationSessionFilterDto
            {
                StartDate = new DateTime(2025, 1, 1)
            };

            var pagedResponse = new PagedResponseDto<MemberRegistrationSession>
            {
                Items = new List<MemberRegistrationSession>
                {
                    new MemberRegistrationSession { MemberRegistrationSessionId = 1, RegistrationName = "Session 1", StartDate = new DateTime(2025, 6, 1), EndDate = new DateTime(2025, 6, 30) }
                },
                TotalRecords = 1,
                PageSize = 10,
                CurrentPage = 1,
                TotalPages = 1
            };

            _memberRegistrationSessionRepositoryMock
                .Setup(repo => repo.GetFilteredPagedAsync(filter))
                .ReturnsAsync(pagedResponse);

            // Act
            var result = await _service.GetFilteredPagedAsync(filter);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.TotalRecords);
            Assert.Single(result.Data.Items);
        }

        [Fact]
        public async Task GetFilteredPagedAsync_ShouldReturnFilteredByEndDate()
        {
            // Arrange
            var filter = new MemberRegistrationSessionFilterDto
            {
                EndDate = new DateTime(2025, 12, 31)
            };

            var pagedResponse = new PagedResponseDto<MemberRegistrationSession>
            {
                Items = new List<MemberRegistrationSession>
                {
                    new MemberRegistrationSession { MemberRegistrationSessionId = 1, RegistrationName = "Session 1", StartDate = new DateTime(2025, 6, 1), EndDate = new DateTime(2025, 6, 30) }
                },
                TotalRecords = 1,
                PageSize = 10,
                CurrentPage = 1,
                TotalPages = 1
            };

            _memberRegistrationSessionRepositoryMock
                .Setup(repo => repo.GetFilteredPagedAsync(filter))
                .ReturnsAsync(pagedResponse);

            // Act
            var result = await _service.GetFilteredPagedAsync(filter);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.TotalRecords);
            Assert.Single(result.Data.Items);
        }

        [Fact]
        public async Task GetFilteredPagedAsync_ShouldReturnFilteredByIsEnableTrue()
        {
            // Arrange
            var filter = new MemberRegistrationSessionFilterDto
            {
                IsEnable = true
            };

            var pagedResponse = new PagedResponseDto<MemberRegistrationSession>
            {
                Items = new List<MemberRegistrationSession>
                {
                    new MemberRegistrationSession { MemberRegistrationSessionId = 1, RegistrationName = "Session 1", IsEnable = true }
                },
                TotalRecords = 1,
                PageSize = 10,
                CurrentPage = 1,
                TotalPages = 1
            };

            _memberRegistrationSessionRepositoryMock
                .Setup(repo => repo.GetFilteredPagedAsync(filter))
                .ReturnsAsync(pagedResponse);

            // Act
            var result = await _service.GetFilteredPagedAsync(filter);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.TotalRecords);
            Assert.Single(result.Data.Items);
        }

        [Fact]
        public async Task GetFilteredPagedAsync_ShouldReturnFilteredByIsEnableFalse()
        {
            // Arrange
            var filter = new MemberRegistrationSessionFilterDto
            {
                IsEnable = false
            };

            var pagedResponse = new PagedResponseDto<MemberRegistrationSession>
            {
                Items = new List<MemberRegistrationSession>
                {
                    new MemberRegistrationSession { MemberRegistrationSessionId = 1, RegistrationName = "Session 1", IsEnable = true }
                },
                TotalRecords = 0,
                PageSize = 10,
                CurrentPage = 1,
                TotalPages = 1
            };

            _memberRegistrationSessionRepositoryMock
                .Setup(repo => repo.GetFilteredPagedAsync(filter))
                .ReturnsAsync(pagedResponse);

            // Act
            var result = await _service.GetFilteredPagedAsync(filter);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.NotNull(result.Data);
            Assert.Equal(0, result.Data.TotalRecords);
        }

        [Fact]
        public async Task GetFilteredPagedAsync_ShouldReturnSortedByNameAscending()
        {
            // Arrange
            var filter = new MemberRegistrationSessionFilterDto
            {
                SortBy = "RegistrationName",
                IsDescending = false
            };

            var pagedResponse = new PagedResponseDto<MemberRegistrationSession>
            {
                Items = new List<MemberRegistrationSession>
                {
                    new MemberRegistrationSession { MemberRegistrationSessionId = 1, RegistrationName = "A Session" },
                    new MemberRegistrationSession { MemberRegistrationSessionId = 2, RegistrationName = "B Session" }
                },
                TotalRecords = 2,
                PageSize = 10,
                CurrentPage = 1,
                TotalPages = 1
            };

            _memberRegistrationSessionRepositoryMock
                .Setup(repo => repo.GetFilteredPagedAsync(filter))
                .ReturnsAsync(pagedResponse);

            // Act
            var result = await _service.GetFilteredPagedAsync(filter);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.TotalRecords);
            Assert.Equal("A Session", result.Data.Items.First().RegistrationName);
        }

        [Fact]
        public async Task GetFilteredPagedAsync_ShouldReturnSortedByStartDateDescending()
        {
            // Arrange
            var filter = new MemberRegistrationSessionFilterDto
            {
                SortBy = "StartDate",
                IsDescending = true
            };

            var pagedResponse = new PagedResponseDto<MemberRegistrationSession>
            {
                Items = new List<MemberRegistrationSession>
                {
                    new MemberRegistrationSession { MemberRegistrationSessionId = 1, RegistrationName = "B Session", StartDate = new DateTime(2023, 6, 1) },
                    new MemberRegistrationSession { MemberRegistrationSessionId = 2, RegistrationName = "A Session", StartDate = new DateTime(2023, 5, 1) }
                },
                TotalRecords = 2,
                PageSize = 10,
                CurrentPage = 1,
                TotalPages = 1
            };

            _memberRegistrationSessionRepositoryMock
                .Setup(repo => repo.GetFilteredPagedAsync(filter))
                .ReturnsAsync(pagedResponse);

            // Act
            var result = await _service.GetFilteredPagedAsync(filter);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.TotalRecords);
            Assert.Equal(new DateTime(2023, 6, 1), result.Data.Items.First().StartDate);
        }

        [Fact]
        public async Task GetFilteredPagedAsync_ShouldReturnError_WhenPageSizeIsZero()
        {
            // Arrange
            var filter = new MemberRegistrationSessionFilterDto
            {
                PageSize = 0
            };

            // Act
            var result = await _service.GetFilteredPagedAsync(filter);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Contains(MemberRegistrationSessionErrorMessage.InvalidPageSize, result.Errors);
        }

        [Fact]
        public async Task GetFilteredPagedAsync_ShouldReturnError_WhenPageNumberIsZeroOrLess()
        {
            // Arrange
            var filter = new MemberRegistrationSessionFilterDto
            {
                PageNumber = 0
            };

            // Act
            var result = await _service.GetFilteredPagedAsync(filter);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Contains(MemberRegistrationSessionErrorMessage.InvalidPageNumber, result.Errors);
        }

        [Fact]
        public async Task GetFilteredPagedAsync_ShouldReturnFilteredAndSortedResults()
        {
            // Arrange
            var filter = new MemberRegistrationSessionFilterDto
            {
                Name = "Session",
                StartDate = new DateTime(2025, 1, 1),
                EndDate = new DateTime(2025, 12, 31),
                SortBy = "RegistrationName",
                IsDescending = false
            };

            var pagedResponse = new PagedResponseDto<MemberRegistrationSession>
            {
                Items = new List<MemberRegistrationSession>
                {
                    new MemberRegistrationSession { MemberRegistrationSessionId = 1, RegistrationName = "A Session", StartDate = new DateTime(2025, 6, 1), EndDate = new DateTime(2025, 6, 30) },
                    new MemberRegistrationSession { MemberRegistrationSessionId = 2, RegistrationName = "B Session", StartDate = new DateTime(2025, 7, 1), EndDate = new DateTime(2025, 7, 31) }
                },
                TotalRecords = 2,
                PageSize = 10,
                CurrentPage = 1,
                TotalPages = 1
            };

            _memberRegistrationSessionRepositoryMock
                .Setup(repo => repo.GetFilteredPagedAsync(filter))
                .ReturnsAsync(pagedResponse);

            // Act
            var result = await _service.GetFilteredPagedAsync(filter);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.TotalRecords);
            Assert.Equal("A Session", result.Data.Items.First().RegistrationName);
        }

        #endregion

        #region DeleteAsync

        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccessResponse_WhenSessionCanBeDeleted()
        {
            // Arrange
            var sessionId = 1;
            var session = new MemberRegistrationSession
            {
                MemberRegistrationSessionId = sessionId,
                RegistrationName = "Test Session",
                EndDate = DateTime.Now.AddDays(-1),
                IsEnable = false,
                ManagerRegistrations = new List<ManagerRegistration>(),
                PlayerRegistrations = new List<PlayerRegistration>()
            };

            _memberRegistrationSessionRepositoryMock.Setup(repo => repo.GetToDeleteByIdAsync(sessionId)).ReturnsAsync(session);
            _memberRegistrationSessionRepositoryMock.Setup(repo => repo.DeleteAsync(session)).ReturnsAsync(1);

            // Act
            var result = await _service.DeleteAsync(sessionId);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
            Assert.True(result.Data);
            Assert.Equal(MemberRegistrationSessionSuccessMessage.DeleteSessionSuccessfully, result.Message);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnErrorResponse_WhenSessionNotFound()
        {
            // Arrange
            var sessionId = -1;
            _memberRegistrationSessionRepositoryMock.Setup(repo => repo.GetToDeleteByIdAsync(sessionId)).ReturnsAsync((MemberRegistrationSession)null);

            // Act
            var result = await _service.DeleteAsync(sessionId);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Contains(MemberRegistrationSessionErrorMessage.DeleteSessionFailed, result.Message);
            Assert.Contains(MemberRegistrationSessionErrorMessage.NotFoundRegistrationSession, result.Errors);
            Assert.False(result.Data);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnErrorResponse_WhenSessionIsStillOpen()
        {
            // Arrange
            var sessionId = 2;
            var session = new MemberRegistrationSession
            {
                MemberRegistrationSessionId = sessionId,
                RegistrationName = "Test Session",
                EndDate = DateTime.Now.AddDays(1),
                IsEnable = true,
                ManagerRegistrations = new List<ManagerRegistration>(),
                PlayerRegistrations = new List<PlayerRegistration>()
            };

            _memberRegistrationSessionRepositoryMock.Setup(repo => repo.GetToDeleteByIdAsync(sessionId)).ReturnsAsync(session);

            // Act
            var result = await _service.DeleteAsync(sessionId);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Contains(MemberRegistrationSessionErrorMessage.DeleteSessionFailed, result.Message);
            Assert.Contains(MemberRegistrationSessionErrorMessage.CanNotDeleteOpeningSession, result.Errors);
            Assert.False(result.Data);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnErrorResponse_WhenSessionHasPendingManagerRegistrations()
        {
            // Arrange
            var sessionId = 3;
            var session = new MemberRegistrationSession
            {
                MemberRegistrationSessionId = sessionId,
                RegistrationName = "Test Session",
                EndDate = DateTime.Now.AddDays(-1),
                IsEnable = false,
                ManagerRegistrations = new List<ManagerRegistration>
                {
                    new ManagerRegistration { Status = "Pending" }
                },
                PlayerRegistrations = new List<PlayerRegistration>()
            };

            _memberRegistrationSessionRepositoryMock.Setup(repo => repo.GetToDeleteByIdAsync(sessionId)).ReturnsAsync(session);

            // Act
            var result = await _service.DeleteAsync(sessionId);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Contains(MemberRegistrationSessionErrorMessage.DeleteSessionFailed, result.Message);
            Assert.Contains(MemberRegistrationSessionErrorMessage.CanNotDeleteSessionWithPendingRegistrations, result.Errors);
            Assert.False(result.Data);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnErrorResponse_WhenSessionHasPendingPlayerRegistrations()
        {
            // Arrange
            var sessionId = 4;
            var session = new MemberRegistrationSession
            {
                MemberRegistrationSessionId = sessionId,
                RegistrationName = "Test Session",
                EndDate = DateTime.Now.AddDays(-1),
                IsEnable = false,
                ManagerRegistrations = new List<ManagerRegistration>(),
                PlayerRegistrations = new List<PlayerRegistration>
                {
                    new PlayerRegistration { Status = "Pending" }
                }
            };

            _memberRegistrationSessionRepositoryMock.Setup(repo => repo.GetToDeleteByIdAsync(sessionId)).ReturnsAsync(session);

            // Act
            var result = await _service.DeleteAsync(sessionId);

            // Assert
            Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
            Assert.Contains(MemberRegistrationSessionErrorMessage.DeleteSessionFailed, result.Message);
            Assert.Contains(MemberRegistrationSessionErrorMessage.CanNotDeleteSessionWithPendingRegistrations, result.Errors);
            Assert.False(result.Data);
        }
        #endregion
    }
}
