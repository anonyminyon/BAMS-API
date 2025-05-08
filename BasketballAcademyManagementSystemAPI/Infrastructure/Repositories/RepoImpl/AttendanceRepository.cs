using BasketballAcademyManagementSystemAPI.Application.DTOs.Attendance;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class AttendanceRepository : IAttendanceRepository
	{
		private readonly BamsDbContext _context;

		public AttendanceRepository(BamsDbContext context)
		{
			_context = context;
		}
		public async Task<Attendance> GetAttendanceByIdAsync(string attendanceId)
		{
			return await _context.Attendances.FindAsync(attendanceId);
		}

		// Thêm hoặc cập nhật điểm danh cho một danh sách học viên
		public async Task<bool> TakeAttendanceAsync(List<Attendance> attendances)
		{
			foreach (var attendance in attendances)
			{
					
					// Thêm mới điểm danh nếu chưa có
					await _context.Attendances.AddAsync(attendance);
			}

			return await _context.SaveChangesAsync() > 0;
		}

		//kiểm tra xem có tồn tại điểm danh của user trong 1 training session hay chưa
		public async Task<bool> IsUserAttendanceInTrainingSession(string sessionId, string userId)
		{
			var attendaceOfUser = await _context.Attendances
				.AnyAsync(a => a.UserId == userId && a.TrainingSessionId == sessionId);
			return attendaceOfUser;
		}

		public async Task<Attendance> GetAttendanceBySessionAndUserAsync(string sessionId, string userId)
		{
			var attendaceOfUser = await _context.Attendances
				.FirstOrDefaultAsync(a => a.UserId == userId && a.TrainingSessionId == sessionId);
			return attendaceOfUser;
		}
		public async Task<bool> UpdateAttendanceListAsync(List<Attendance> attendances)
		{
			_context.Attendances.UpdateRange(attendances);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<bool> AddAttendanceListAsync(List<Attendance> attendances)
		{
			await _context.Attendances.AddRangeAsync(attendances);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<Attendance?> GetAttendanceByUserAndSessionAsync(string userId, string sessionId)
		{
			return await _context.Attendances
				.FirstOrDefaultAsync(a => a.UserId == userId && a.TrainingSessionId == sessionId);
		}
		//Update attendance
		public async Task UpdateAttendanceAsync(Attendance attendance)
		{
			var existingAttendance = await _context.Attendances
				.FirstOrDefaultAsync(a => a.AttendanceId == attendance.AttendanceId);

			if (existingAttendance != null)
			{
				// Cập nhật các trường cần thiết
				existingAttendance.Status = attendance.Status;
				existingAttendance.Note = attendance.Note;

				// Lưu thay đổi vào DB
				await _context.SaveChangesAsync();
			}
			else
			{
				throw new Exception("Attendance record not found.");
			}
		}

		// Phương thức để lấy danh sách Attendance theo TrainingSessionId
		public async Task<List<Attendance>> GetAttendancesByTrainingSessionAsync(string trainingSessionId)
		{
			return await _context.Attendances
		   .Include(a => a.TrainingSession).ThenInclude(x=>x.Team).ThenInclude(x=>x.Coaches).ThenInclude(x=>x.User)
		   .Include(u => u.User)
		   .Include(m => m.Manager).ThenInclude(x=>x.User)
		   .Where(a => a.TrainingSessionId == trainingSessionId)
		   .ToListAsync();
		}

		public async Task<User> GetUserById(string userId)
		{
			return await _context.Users
				.FirstOrDefaultAsync(u => u.UserId == userId);
		}

		// Lấy ra danh sách những học viên cần điểm danh trong buổi học đó
		public async Task<List<PlayerAttendanceDto>> GetPlayersByTrainingSessionIdAsync(string trainingSessionId)
		{
			var players = await _context.TrainingSessions
			.Where(ts => ts.TrainingSessionId == trainingSessionId)
			.Include(ts => ts.Team)
				.ThenInclude(team => team.Players!)
					.ThenInclude(p => p.User)
			.SelectMany(ts => ts.Team.Players!
				.Where(p => p.User.IsEnable)
				.Select(p => new PlayerAttendanceDto
				{
					UserId = p.User.UserId,
					FullName = p.User.Fullname,
					Username = p.User.Username,
					Status = _context.Attendances
						.Where(a => a.UserId == p.UserId && a.TrainingSessionId == trainingSessionId)
						.Select(a => (int?)a.Status)
						.FirstOrDefault() ?? null,
					Note = _context.Attendances
						.Where(a => a.UserId == p.UserId && a.TrainingSessionId == trainingSessionId)
						.Select(a => a.Note)
						.FirstOrDefault() ?? null
				}))
			.ToListAsync();


			return players;
		}
		// Lấy ra danh sách những hlv cần điểm danh trong buổi học đó
		public async Task<List<CoachAttendanceDto>> GetCoachesByTrainingSessionIdAsync(string trainingSessionId)
		{
			var session = await _context.TrainingSessions
				.Where(ts => ts.TrainingSessionId == trainingSessionId)
				.Include(ts => ts.Team)
				.ThenInclude(t => t.Coaches)
				.ThenInclude(c => c.User)
				.FirstOrDefaultAsync();

			if (session?.Team == null || session.Team.Coaches == null)
				return new List<CoachAttendanceDto>();

			var result = session.Team.Coaches
				.Where(c => c.User.IsEnable)
				.Select(c => new CoachAttendanceDto
				{
					UserId = c.User.UserId,
					FullName = c.User.Fullname,
					Username = c.User.Username,
					Status = _context.Attendances
						.Where(a => a.UserId == c.User.UserId && a.TrainingSessionId == trainingSessionId)
						.Select(a => (int?)a.Status)
						.FirstOrDefault(),
					Note = _context.Attendances
						.Where(a => a.UserId == c.User.UserId && a.TrainingSessionId == trainingSessionId)
						.Select(a => a.Note)
						.FirstOrDefault()
				})
				.ToList();

			return result;
		}

		//=========Sumary attendance ==========
		public async Task<List<UserTeamHistory>> GetUserTeamHistoriesByUserIdAsync(string userId, DateTime startDate, DateTime endDate)
		{
			return await _context.UserTeamHistories
				.Where(uth => uth.UserId == userId &&
							  uth.JoinDate <= endDate &&  // So sánh trực tiếp với DateTime
							  (uth.LeftDate == null || uth.LeftDate >= startDate)) // So sánh trực tiếp với DateTime
				.OrderByDescending(uth => uth.JoinDate)
				.ToListAsync();
		}

		public async Task<List<TrainingSession>> GetTrainingSessionByTeamIdInMonth(DateTime startDate, DateTime endDate)
		{
			// Chuyển startDate và endDate thành DateOnly
			var startDateOnly = DateOnly.FromDateTime(startDate);
			var endDateOnly = DateOnly.FromDateTime(endDate);

			return await _context.TrainingSessions
				.Where(ts =>
					// Kiểm tra nếu ScheduledDate nằm trong khoảng từ startDate đến endDate
					ts.ScheduledDate >= startDateOnly &&  // So sánh với DateOnly
					ts.ScheduledDate <= endDateOnly &&
					ts.Status == TrainingSessionConstant.Status.ACTIVE)      // So sánh với DateOnly
				.OrderByDescending(ts => ts.ScheduledDate)
				.ToListAsync();
		}

		//Hàm lấy ra email phụ huynh cho học sinh dưới 18 tuổi, để gửi email
		public async Task<string> GetParentEmailForUnderagePlayer(string playerId)
		{
			// Tìm kiếm học sinh (Player) theo PlayerId
			var player = await _context.Players
				.Include(p => p.Parent).Include(x=>x.User) // Bao gồm thông tin phụ huynh
				.FirstOrDefaultAsync(p => p.UserId == playerId);

			// Kiểm tra nếu không tìm thấy học sinh
			if (player == null)
			{
				return null; // Hoặc throw exception tùy nhu cầu
			}

			var today = DateOnly.FromDateTime(DateTime.Now); // Lấy ngày hiện tại dưới dạng DateOnly
			dynamic age = 0;
			// Kiểm tra nếu DateOfBirth có giá trị và tính toán tuổi
			if (player.User.DateOfBirth.HasValue)
			{
			    age = today.Year - player.User.DateOfBirth.Value.Year;

				// Kiểm tra nếu sinh nhật của học sinh chưa qua trong năm nay
				if (today < player.User.DateOfBirth.Value.AddYears(age))
				{
					age--;  // Nếu sinh nhật chưa qua trong năm nay thì giảm 1 tuổi
				}
			}
			else
			{
				// Xử lý nếu DateOfBirth không có giá trị
				throw new InvalidOperationException("DateOfBirth không có giá trị.");
			}


			// Kiểm tra nếu học sinh dưới 18 tuổi và có phụ huynh
			if (age < 18 && player.ParentId != null)
			{
				// Tìm thông tin phụ huynh dựa trên ParentId
				var parent = await _context.Parents
					.FirstOrDefaultAsync(p => p.UserId == player.ParentId);

				// Kiểm tra nếu tìm thấy phụ huynh và có email
				if (parent != null)
				{
					return parent.User.Email; // Trả về email của phụ huynh
				}
			}

			// Nếu không có phụ huynh hoặc học sinh trên 18 tuổi, trả về null
			return null;
		}


	}
}
