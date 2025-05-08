using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Attendance;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl;
using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.Vml.Spreadsheet;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
	public class AttendanceService : IAttendanceService
	{
		private readonly IAttendanceRepository _attendanceRepository;
		private readonly ITrainingSessionRepository _trainingSessionRepository;
		private readonly IMapper _mapper;
		private readonly ISendMailService _sendMailService;
		private readonly GetLoginUserHelper _getUserLoggedIn;

		public AttendanceService(IAttendanceRepository repository, IMapper mapper,
			GetLoginUserHelper getUserLoggedIn,
			ITrainingSessionRepository trainingSessionRepository,
			ISendMailService sendMailService)
		{
			_attendanceRepository = repository;
			_mapper = mapper;
			_getUserLoggedIn = getUserLoggedIn;
			_trainingSessionRepository = trainingSessionRepository;
			_sendMailService = sendMailService;
		}

		// Hàm kiểm tra trạng thái điểm danh, nếu chưa thì cập nhật trạng thái, rồi thì update ,
		// khi điểm danh thì gửi email, khi cập nhật thì gửi email đính chính tới phụ huynh
		public async Task<ApiMessageModelV2<object>> TakeAttendanceAsync(List<TakeAttendanceDTO> attendances)
		{
			var errors = new Dictionary<string, string>();
			var attendanceToAdd = new List<Attendance>();
			var attendanceToUpdate = new List<Attendance>();

			try
			{
				if (!attendances.Any())
				{
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Message = "Không có dữ liệu điểm danh.",
						Errors = null
					};
				}

				// Kiểm tra TrainingSessionId có tồn tại không
				var trainingSessionId = attendances.First().TrainingSessionId;

				if (trainingSessionId == null)
				{
					return new ApiMessageModelV2<object>
					{
						Status = ApiResponseStatusConstant.FailedStatus,
						Message = $"Không tồn tại buổi tập.",
						Errors = null
					};
				}

				foreach (var att in attendances)
				{
					var existing = await _attendanceRepository
						.GetAttendanceByUserAndSessionAsync(att.UserId, att.TrainingSessionId);

					//Cập nhật trạng thái điểm danh
					if (existing != null)
					{
						// Nếu đã điểm danh thì cập nhật trạng thái và note
						existing.Status = att.Status;
						existing.Note = att.Note;
						existing.ManagerId = _getUserLoggedIn.GetUserIdLoggedIn();
						attendanceToUpdate.Add(existing);

						//gửi email đính chính trạng thái điểm danh tới phụ huynh, 0 là gửi mail thông báo, 1 là gửi mail đính chính
						await SendMailAttendanceToParent(att, 1);
					}
					else
					{
						// Nếu chưa điểm danh thì thêm mới
						var newAttendance = new Attendance
						{
							AttendanceId = Guid.NewGuid().ToString(),
							UserId = att.UserId,
							TrainingSessionId = att.TrainingSessionId,
							ManagerId = _getUserLoggedIn.GetUserIdLoggedIn(),
							Status = att.Status,
							Note = att.Note
						};

						attendanceToAdd.Add(newAttendance);
						//Gửi email tới phụ huynh về tình trạng điểm danh của con
						await SendMailAttendanceToParent(att, 0);
					}
				}

				var isSuccessAdd = true;
				var isSuccessUpdate = true;

				if (attendanceToAdd.Any())
					isSuccessAdd = await _attendanceRepository.AddAttendanceListAsync(attendanceToAdd);

				if (attendanceToUpdate.Any())
					isSuccessUpdate = await _attendanceRepository.UpdateAttendanceListAsync(attendanceToUpdate);

				bool overallSuccess = isSuccessAdd && isSuccessUpdate;

				return new ApiMessageModelV2<object>
				{
					Status = overallSuccess ? ApiResponseStatusConstant.SuccessStatus : ApiResponseStatusConstant.FailedStatus,
					Message = overallSuccess ? "Điểm danh thành công." : "Có lỗi xảy ra khi lưu điểm danh.",
					Errors = errors.Any() ? errors : null
				};
			}
			catch (Exception ex)
			{
				errors["Exception"] = ex.Message;
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Message = "Đã xảy ra lỗi hệ thống.",
					Errors = errors
				};
			}
		}

		private async Task SendMailAttendanceToParent(TakeAttendanceDTO attendance, int typeEmailForParentAttendance)
		{

			// Phần gửi email tới phụ huynh
			var emailParent = await _attendanceRepository.GetParentEmailForUnderagePlayer(attendance.UserId);
			var trainingSession = await _trainingSessionRepository.GetTrainingSessionForParentEmailAsync(attendance.TrainingSessionId);
			if (emailParent != null)
			{
				// loại mail là thông báo điểm danh tới phụ huynh
				if (typeEmailForParentAttendance == 0)
				{
					await _sendMailService.SendMailByMailTemplateIdAsync(MailTemplateConstant.ReportAttendanceToParent, emailParent, new
					{
						PlayerName = await _attendanceRepository.GetUserById(attendance.UserId),
						TrainingSessionInfo = trainingSession,
						Status = attendance.Status

					});
				}
				else// loại mail là đính chính
				{
					await _sendMailService.SendMailByMailTemplateIdAsync(MailTemplateConstant.CorrectionAttendanceToParent, emailParent, new
					{
						PlayerName = await _attendanceRepository.GetUserById(attendance.UserId),
						TrainingSessionInfo = trainingSession,
						Status = attendance.Status
					});
				}
			}

		}

		//sửa lại attendace
		public async Task<ApiMessageModelV2<object>> EditAttendanceAsync(EditAttendanceDto attendanceDto)
		{
			var errors = new Dictionary<string, string>();

			// Kiểm tra xem AttendanceId có hợp lệ không
			if (string.IsNullOrEmpty(attendanceDto.AttendanceId))
			{
				errors.Add("attendanceIdMissing", "AttendanceId is required.");
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors
				};
			}

			// Chuyển đổi DTO sang entity
			var attendance = _mapper.Map<Attendance>(attendanceDto);

			try
			{
				// Gọi repository để cập nhật bản ghi attendance
				await _attendanceRepository.UpdateAttendanceAsync(attendance);

				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Message = "Điểm danh học viên đã được cập nhật"
				};
			}
			catch (Exception ex)
			{
				errors.Add("Exception", ex.Message);
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors
				};
			}
		}

		//Lấy attandence toàn bộ thành viên theo training session
		public async Task<ApiMessageModelV2<List<AttendanceDTO>>> GetAttendancesByTrainingSessionAsync(string trainingSessionId)
		{
			try
			{
				// Lấy danh sách Attendance từ Repository
				var attendances = await _attendanceRepository.GetAttendancesByTrainingSessionAsync(trainingSessionId);

				// Chuyển đổi sang DTO
				var attendanceDTOs = attendances.Select(a => new AttendanceDTO
				{
					AttendanceId = a.AttendanceId,
					UserId = a.UserId,
					UserFullName = a.User?.Fullname,
					ManagerId = a.ManagerId,
					ManagerFullName = a.Manager.User.Fullname,
					TeamId = a.TrainingSession.TeamId,
					TrainingSessionId = a.TrainingSessionId,
					TeamName = a.TrainingSession.Team.TeamName,
					Status = a.Status,
					Note = a.Note,
					CreateByUserId = a.TrainingSession.CreatedByUserId,
					CreateByUserName = a.TrainingSession.CreatedByUser.User.Fullname
				}).ToList();

				// Trả về kết quả với ApiMessageModelV2
				return new ApiMessageModelV2<List<AttendanceDTO>>
				{
					Status = ApiResponseStatusConstant.SuccessStatus,
					Data = attendanceDTOs
				};
			}
			catch (Exception ex)
			{
				// Nếu có lỗi, trả về thông báo lỗi
				return new ApiMessageModelV2<List<AttendanceDTO>>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = new Dictionary<string, string> { { "Error", ex.Message } }
				};
			}
		}

		//api lấy thông tin điểm danh của 1 người (userId) theo buổi học ( training session) 
		public async Task<ApiMessageModelV2<object>> GetAttendanceByTrainingSessionOrUser(string trainingSessionId, string userId)
		{
			var errors = new Dictionary<string, string>();


			// Kiểm tra điều kiện đầu vào
			if (string.IsNullOrEmpty(trainingSessionId) || string.IsNullOrEmpty(userId))
			{
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Message = "Không tìm thấy lịch học hoặc người dùng"
				};
			}

			// Gọi phương thức bất đồng bộ để lấy thông tin điểm danh
			var attendance = await _attendanceRepository.GetAttendanceBySessionAndUserAsync(trainingSessionId, userId);

			// Kiểm tra nếu không tìm thấy thông tin điểm danh
			if (attendance == null)
			{
				errors.Add("notFoundAttendance", "Không tìm thấy thông tin điểm danh");
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Errors = errors
				};
			}

			// Nếu tìm thấy, trả về thông tin điểm danh
			var attendanceDto = new UserAttendance
			{
				AttendanceId = attendance.AttendanceId,
				UserId = attendance.UserId,
				ManagerId = attendance.ManagerId,
				TrainingSessionId = attendance.TrainingSessionId,
				Status = attendance.Status,
				Note = attendance.Note
			};

			return new ApiMessageModelV2<object>
			{
				Status = ApiResponseStatusConstant.SuccessStatus,
				Data = attendanceDto
			};
		}

		// Lấy ra danh sách player cần điểm danh cho 1 training session
		public async Task<ApiMessageModelV2<object>> GetPlayersForAttendanceAsync(string trainingSessionId)
		{
			if (string.IsNullOrEmpty(trainingSessionId))
			{
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Message = "TrainingSessionId không được để trống."
				};
			}

			var players = await _attendanceRepository.GetPlayersByTrainingSessionIdAsync(trainingSessionId);

			return new ApiMessageModelV2<object>
			{
				Status = ApiResponseStatusConstant.SuccessStatus,
				Message = "Lấy danh sách học viên thành công.",
				Data = players
			};
		}
		public async Task<ApiMessageModelV2<object>> GetCoachesForAttendanceAsync(string trainingSessionId)
		{
			if (string.IsNullOrEmpty(trainingSessionId))
			{
				return new ApiMessageModelV2<object>
				{
					Status = ApiResponseStatusConstant.FailedStatus,
					Message = "TrainingSessionId không được để trống."
				};
			}

			var coaches = await _attendanceRepository.GetCoachesByTrainingSessionIdAsync(trainingSessionId);

			return new ApiMessageModelV2<object>
			{
				Status = ApiResponseStatusConstant.SuccessStatus,
				Message = "Lấy danh sách huấn luyện viên thành công.",
				Data = coaches
			};
		}

		//===========Sumary attendance ================
		public async Task<ApiMessageModelV2<object>> GetUserAttendanceSumary(string userId, DateTime startDate, DateTime endDate)
		{
			// Khai báo list chứa training sessions mà user thuộc về
			var listTrainingSessionId = new List<TrainingSession>();
			var listAttendanceInfo = new List<UserAttendance>();

			// Lấy user team history trong khoảng thời gian
			var userTeamHistoryInRangeTime = await _attendanceRepository.GetUserTeamHistoriesByUserIdAsync(userId, startDate, endDate);

			// Lấy tất cả các training sessions trong khoảng thời gian
			var trainingSession = await _attendanceRepository.GetTrainingSessionByTeamIdInMonth(startDate, endDate);

			// Tạo danh sách các training sessions mà user đã tham gia trong khoảng thời gian
			for (int i = 0; i < userTeamHistoryInRangeTime.Count; i++)
			{
				for (int j = 0; j < trainingSession.Count; j++)
				{
					var userTeamHistoryJoinDate = userTeamHistoryInRangeTime[i].JoinDate;

					// Nếu userTeamHistoryJoinDate < startDate, set thành ngày đầu tiên của tháng trong startDate
					if (userTeamHistoryJoinDate < startDate)
					{
						userTeamHistoryJoinDate = new DateTime(startDate.Year, startDate.Month, 1); // Ngày đầu tiên của tháng
					}

					// Chuyển ngày LeftDate sang DateTime hoặc gán null
					DateTime? userTeamHistoryLeftDate = userTeamHistoryInRangeTime[i].LeftDate.HasValue
						? userTeamHistoryInRangeTime[i].LeftDate.Value
						: (DateTime?)null;

					// Nếu userTeamHistoryLeftDate == null, set thành ngày cuối cùng của tháng trong startDate
					if (userTeamHistoryLeftDate == null)
					{
						userTeamHistoryLeftDate = new DateTime(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month)); // Ngày cuối cùng của tháng
					}

					// Chuyển ScheduledDate sang DateTime
					var trainingSessionScheduled = trainingSession[j].ScheduledDate.ToDateTime(TimeOnly.MinValue);

					// Nếu trainingSession nằm trong khoảng thời gian user ở team thì thêm vào list
					if (trainingSessionScheduled >= userTeamHistoryJoinDate &&
						(trainingSessionScheduled < userTeamHistoryLeftDate))
					{
						listTrainingSessionId.Add(trainingSession[j]);
					}
				}
			}

			// Lấy thông tin điểm danh cho từng training session
			foreach (var session in listTrainingSessionId)
			{
				var attendance = await GetAttendanceByTrainingSessionOrUser(session.TrainingSessionId, userId);

				if (attendance.Status == ApiResponseStatusConstant.SuccessStatus && attendance.Data != null)
				{
					listAttendanceInfo.Add((UserAttendance)attendance.Data); // Thêm vào danh sách thông tin điểm danh
				}
			}

			// Trả về kết quả với danh sách thông tin điểm danh
			return new ApiMessageModelV2<object>
			{
				Status = ApiResponseStatusConstant.SuccessStatus,
				Message = "Lấy danh sách điểm danh thành công.",
				Data = listAttendanceInfo
			};
		}
	}


}
