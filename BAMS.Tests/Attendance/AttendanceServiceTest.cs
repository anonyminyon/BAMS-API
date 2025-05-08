using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Attendance;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace BAMS.Tests.AttendanceTest;

public class AttendanceServiceTest
{
	private readonly Mock<IAttendanceRepository> _attendanceRepositoryMock;
	private readonly Mock<ITrainingSessionRepository> _trainingSessionRepositoryMock;
	private readonly Mock<IMapper> _mapperMock;
	private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock; // Mock IHttpContextAccessor
	private readonly Mock<GetLoginUserHelper> _getUserLoggedInMock; // Mock GetLoginUserHelper
	private readonly AttendanceService _service;

	public AttendanceServiceTest()
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
		_getUserLoggedInMock = new Mock<GetLoginUserHelper>(_httpContextAccessorMock.Object); // Pass the mocked IHttpContextAccessor

		// Initialize your service with the mocked dependencies
		_attendanceRepositoryMock = new Mock<IAttendanceRepository>();
		_trainingSessionRepositoryMock = new Mock<ITrainingSessionRepository>();
		_mapperMock = new Mock<IMapper>();
		_service = new AttendanceService(
			_attendanceRepositoryMock.Object,
			_mapperMock.Object,
			_getUserLoggedInMock.Object,
			_trainingSessionRepositoryMock.Object
		);
	}

	#region TakeAttendanceAsync
	[Fact]
	public async Task TakeAttendanceAsync_ShouldReturnError_WhenNoAttendanceData()
	{
		// Arrange
		var attendances = new List<TakeAttendanceDTO>(); // Empty list

		// Act
		var result = await _service.TakeAttendanceAsync(attendances);

		// Assert
		Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);  // Expect Failed status
		Assert.Equal("Không có dữ liệu điểm danh.", result.Message);  // Expect error message
		Assert.Null(result.Errors);  // No errors
	}


	// Test case 2: When `TrainingSessionId` is null
	[Fact]
	public async Task TakeAttendanceAsync_ShouldReturnError_WhenTrainingSessionIdIsNull()
	{
		// Arrange
		var attendances = new List<TakeAttendanceDTO>
		{
			new TakeAttendanceDTO { UserId = "user1", TrainingSessionId = null, Status = 1 }
		};

		// Act
		var result = await _service.TakeAttendanceAsync(attendances);

		// Assert
		Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
		Assert.Equal("Không tồn tại buổi tập.", result.Message);
		Assert.Null(result.Errors);
	}

	// Test case 3: When attendance already exists
	[Fact]
	public async Task TakeAttendanceAsync_ShouldUpdateAttendance_WhenExists()
	{
		// Arrange
		var teamId = "team123";
		var attendances = new List<TakeAttendanceDTO>
		{
			new TakeAttendanceDTO { UserId = "user1", TrainingSessionId = teamId, Status = 1, Note = "Test" }
		};

		var existingAttendance = new BasketballAcademyManagementSystemAPI.Models.Attendance
		{
			UserId = "user1",
			TrainingSessionId = teamId,
			Status = 0,
			Note = "Old Note"
		};

		_attendanceRepositoryMock.Setup(repo => repo.GetAttendanceByUserAndSessionAsync(It.IsAny<string>(), It.IsAny<string>()))
			.ReturnsAsync(existingAttendance);

		_getUserLoggedInMock.Setup(helper => helper.GetUserIdLoggedIn()).Returns("manager1");

		// Act
		var result = await _service.TakeAttendanceAsync(attendances);

		// Assert
		Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
		Assert.Equal("Điểm danh thành công.", result.Message);
		Assert.Null(result.Errors);
		Assert.Equal(1, existingAttendance.Status);  // Status should be updated
		Assert.Equal("Test", existingAttendance.Note); // Note should be updated
	}

	// Test case 4: When attendance does not exist for a user in the session
	[Fact]
	public async Task TakeAttendanceAsync_ShouldAddNewAttendance_WhenNotExist()
	{
		// Arrange
		var teamId = "team123";
		var attendances = new List<TakeAttendanceDTO>
		{
			new TakeAttendanceDTO { UserId = "user1", TrainingSessionId = teamId, Status = 1, Note = "Test" }
		};

		_attendanceRepositoryMock.Setup(repo => repo.GetAttendanceByUserAndSessionAsync(It.IsAny<string>(), It.IsAny<string>()))
			.ReturnsAsync((BasketballAcademyManagementSystemAPI.Models.Attendance)null); // No existing attendance

		_getUserLoggedInMock.Setup(helper => helper.GetUserIdLoggedIn()).Returns("manager1");

		// Act
		var result = await _service.TakeAttendanceAsync(attendances);

		// Assert
		Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
		Assert.Equal("Điểm danh thành công.", result.Message);
		Assert.Null(result.Errors);
		_attendanceRepositoryMock.Verify(repo => repo.AddAttendanceListAsync(It.IsAny<List<BasketballAcademyManagementSystemAPI.Models.Attendance>>()), Times.Once); // Ensure the attendance is added
	}

	#endregion

	#region GetAttendanceByTrainingSessionOrUser

	// Test case 1: When trainingSessionId or userId is null or empty
	[Fact]
	public async Task GetAttendanceByTrainingSessionOrUser_ShouldReturnError_WhenTrainingSessionIdOrUserIdIsEmpty()
	{
		// Arrange
		string trainingSessionId = "";  // Empty trainingSessionId
		string userId = "user123";      // Valid userId

		// Act
		var result = await _service.GetAttendanceByTrainingSessionOrUser(trainingSessionId, userId);

		// Assert
		Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
		Assert.Equal("Không tìm thấy lịch học hoặc người dùng", result.Message);
	}

	// Test case 2: When no attendance is found for the given session and user
	[Fact]
	public async Task GetAttendanceByTrainingSessionOrUser_ShouldReturnError_WhenAttendanceNotFound()
	{
		// Arrange
		string trainingSessionId = "session123";
		string userId = "user123";
		_attendanceRepositoryMock.Setup(repo => repo.GetAttendanceBySessionAndUserAsync(trainingSessionId, userId))
			.ReturnsAsync((Attendance)null);  // Simulating no attendance found

		// Act
		var result = await _service.GetAttendanceByTrainingSessionOrUser(trainingSessionId, userId);

		// Assert
		Assert.Equal(ApiResponseStatusConstant.FailedStatus, result.Status);
		Assert.Equal("Không tìm thấy thông tin điểm danh", result.Errors["notFoundAttendance"]);
	}

	// Test case 3: When attendance is found for the given session and user
	[Fact]
	public async Task GetAttendanceByTrainingSessionOrUser_ShouldReturnSuccess_WhenAttendanceFound()
	{
		// Arrange
		string trainingSessionId = "session123";
		string userId = "user123";

		var attendance = new Attendance
		{
			AttendanceId = "attendance123",
			UserId = userId,
			ManagerId = "manager123",
			TrainingSessionId = trainingSessionId,
			Status = 1,
			Note = "Present"
		};

		_attendanceRepositoryMock.Setup(repo => repo.GetAttendanceBySessionAndUserAsync(trainingSessionId, userId))
			.ReturnsAsync(attendance);  // Simulating that attendance is found

		var attendanceDto = new UserAttendance
		{
			AttendanceId = attendance.AttendanceId,
			UserId = attendance.UserId,
			ManagerId = attendance.ManagerId,
			TrainingSessionId = attendance.TrainingSessionId,
			Status = attendance.Status,
			Note = attendance.Note
		};

		_mapperMock.Setup(mapper => mapper.Map<UserAttendance>(attendance)).Returns(attendanceDto);

		// Act
		var result = await _service.GetAttendanceByTrainingSessionOrUser(trainingSessionId, userId);

		// Assert
		Assert.Equal(ApiResponseStatusConstant.SuccessStatus, result.Status);
		Assert.NotNull(result.Data);
		Assert.Equal(attendanceDto.AttendanceId, ((UserAttendance)result.Data).AttendanceId);
		Assert.Equal("Present", ((UserAttendance)result.Data).Note);  // Check that the note is returned correctly
	}

	


	#endregion
}
