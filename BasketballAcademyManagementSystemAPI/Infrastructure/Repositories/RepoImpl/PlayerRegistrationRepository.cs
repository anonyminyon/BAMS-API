using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class PlayerRegistrationRepository : IPlayerRegistrationRepository
	{
		private readonly BamsDbContext _dbContext;
		private readonly IMapper _mapper;
		public PlayerRegistrationRepository(BamsDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}
		

		public async Task<PlayerRegistration> RegisterPlayerAsync(PlayerRegistration player)
		{
			_dbContext.PlayerRegistrations.Add(player);
			await _dbContext.SaveChangesAsync();
			return player;
		}

		public async Task<PlayerRegistration> GetPlayerRegisterFormByEmailAsync(string email)
		{
			return await _dbContext.PlayerRegistrations
				.FirstOrDefaultAsync(p => p.Email.ToLower().Equals(email.ToLower()));
		}

		public async Task<List<PlayerSubmittedFormDto>> GetPlayers(
		int? memberRegistrationSessionId,
		string? email,
        DateTime? startDate,
		DateTime? endDate,
		int? minAge,
		int? maxAge,
		bool? gender,
		string? status)
		{
			var query = _dbContext.PlayerRegistrations
				.Include(p => p.MemberRegistrationSession)
				.AsQueryable();

			// Lọc theo memberRegistrationSessionId
			if (memberRegistrationSessionId.HasValue)
			{
				query = query.Where(p => p.MemberRegistrationSessionId == memberRegistrationSessionId.Value);
			}
			// Lọc theo email
			if (!string.IsNullOrEmpty(email))
			{
				query = query.Where(p => p.Email == email);
			}

			// Lọc theo ngày bắt đầu
			if (startDate.HasValue)
			{
				query = query.Where(p => p.MemberRegistrationSession.StartDate >= startDate.Value);
			}

			// Lọc theo ngày kết thúc
			if (endDate.HasValue)
			{
				query = query.Where(p => p.MemberRegistrationSession.EndDate <= endDate.Value);
			}

			// Lọc theo status
			if (!string.IsNullOrEmpty(status))
			{
				query = query.Where(p => p.Status == status);
			}

			// Lọc theo độ tuổi
			if (minAge.HasValue || maxAge.HasValue)
			{
				var today = DateOnly.FromDateTime(DateTime.Today);

				if (minAge.HasValue)
				{
					var minBirthDate = today.AddYears(-minAge.Value);
					query = query.Where(p => p.DateOfBirth <= minBirthDate);
				}

				if (maxAge.HasValue)
				{
					var maxBirthDate = today.AddYears(-maxAge.Value);
					query = query.Where(p => p.DateOfBirth >= maxBirthDate);
				}
			}

			// Lọc theo giới tính
			if (gender.HasValue)
			{
				query = query.Where(p => p.Gender == gender.Value);
			}

			// Trả toàn bộ danh sách (không phân trang)
			var players = await query
				.OrderBy(p => p.PlayerRegistrationId)
				.Select(p => new PlayerSubmittedFormDto
				{
					PlayerRegistrationId = p.PlayerRegistrationId,
					FullName = p.FullName,
					GenerationAndSchoolName = p.GenerationAndSchoolName,
					PhoneNumber = p.PhoneNumber,
					Email = p.Email,
					Gender = p.Gender,
					DateOfBirth = p.DateOfBirth,
					Height = p.Height,
					Weight = p.Weight,
					FacebookProfileUrl = p.FacebookProfileUrl,
					KnowledgeAboutAcademy = p.KnowledgeAboutAcademy,
					ReasonToChooseUs = p.ReasonToChooseUs,
					Position = p.Position,
					Experience = p.Experience,
					Achievement = p.Achievement,
					CandidateNumber = p.CandidateNumber,
					ParentName = p.ParentName,
					ParentPhoneNumber = p.ParentPhoneNumber,
					ParentEmail = p.ParentEmail,
					RelationshipWithParent = p.RelationshipWithParent,
					ParentCitizenId = p.ParentCitizenId,
					TryOutNote = p.TryOutNote,
					SubmitedDate = p.SubmitedDate,
					Status = p.Status,
					RegistrationName = p.MemberRegistrationSession.RegistrationName,
					StartDate = p.MemberRegistrationSession.StartDate,
					EndDate = p.MemberRegistrationSession.EndDate,
					TryOutDate = p.TryOutDate,
					TryOutLocation = p.TryOutLocation
				})
				.ToListAsync();

			return players;
		}



		public async Task<bool> AddTryOutNote(string playerRegisterId, string tryOutNote)
		{
			var player = await _dbContext.PlayerRegistrations.FindAsync(playerRegisterId);
			if (player == null)
				return false;

			player.TryOutNote = tryOutNote;
			await _dbContext.SaveChangesAsync();
			return true;
		}
		public async Task<PlayerRegistration> GetRegistrationByIdAsync(int id)
		{
			return await _dbContext.PlayerRegistrations.FindAsync(id);
		}

		public async Task<User> GetUserByEmailAsync(string email)
		{
			return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
		}

		public async Task<User> GetParentByEmailAsync(string email)
		{
			return await _dbContext.Users.Include(x=>x.Parent).FirstOrDefaultAsync(p => p.Email == email);
		}

		public async Task AddUserAsync(User user)
		{
			await _dbContext.Users.AddAsync(user);
		}

		public async Task AddParentAsync(Parent parent)
		{
			await _dbContext.Parents.AddAsync(parent);
		}

		public async Task AddPlayerAsync(Player player)
		{
			await _dbContext.Players.AddAsync(player);
		}

		public void Update(PlayerRegistration registration)
		{
			_dbContext.PlayerRegistrations.Update(registration);
		}

		
		public async Task SaveAsync()
		{
			await _dbContext.SaveChangesAsync();
		}

		public async Task<int> GetMaxCandidateNumber(int memberRegistrationSessionId)
		{
			var maxCandidate = await _dbContext.PlayerRegistrations
					   .Where(p => p.MemberRegistrationSessionId == memberRegistrationSessionId)
					   .MaxAsync(p => (int?)p.CandidateNumber) ?? 1;

			return maxCandidate;
		}

		public async Task UpdatePlayerRegistrationAsync(PlayerRegistration registration)
		{
			_dbContext.PlayerRegistrations.Attach(registration);
			_dbContext.Entry(registration).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();
		}

        public async Task<PlayerRegistration?> GetPlayerRegistrationByIdAsync(int playerRegistrationId)
        {
            return await _dbContext.PlayerRegistrations
                .FirstOrDefaultAsync(p => p.PlayerRegistrationId == playerRegistrationId);
        }

        public async Task<PlayerRegistration?> GetPlayerRegistrationAsync(int sessionId)
        {
            var player = await _dbContext.PlayerRegistrations
                .Where(p => p.PlayerRegistrationId == sessionId)
                .Include(p => p.TryOutScorecards).ThenInclude(s => s.MeasurementScaleCodeNavigation)
                .FirstOrDefaultAsync();

            return player;
        }

        public async Task DeleteRangeAsync(ICollection<PlayerRegistration> playerRegistrations)
        {
            _dbContext.PlayerRegistrations.RemoveRange(playerRegistrations);
            await _dbContext.SaveChangesAsync();
        }

        // Check email đã tồn tại trong bảng user hay chưa
        public async Task<bool> IsEmailExistsInUser(string email)
        {
            email = email.Trim().ToLower(); // Chuyển email về chữ thường trước khi so sánh

			var emailInUser = await _dbContext.Users
				.AnyAsync(u => u.Email != null && u.Email.ToLower() == email);

			return emailInUser; 
        }

        //kiểm tra email có tồn tại trong bảng manager Registation 
        public async Task<bool> IsEmailExistsInManagerRegistration(string email, string memberRegistrationSessionId)
        {

            email = email.ToLower(); // Giảm số lần gọi .ToLower()

            // Kiểm tra trong bảng ManagerRegistration 
            bool existsInManager = await _dbContext.ManagerRegistrations 
                .AnyAsync(m => m.Email != null && m.Email.ToLower() == email &&
				!m.Status.Equals(RegistrationStatusConstant.REJECTED));
            return existsInManager;
        }

		//kiểm tra email có tồn tại trong bảng player Registation
		public async Task<bool> IsEmailExistsInPlayerRegistration(string email, string memberRegistrationSessionId)
        {
            email = email.ToLower(); // Giảm số lần gọi .ToLower()

			return
			//cùng mùa, bị reject => return true ( tức là ko đăng kí đc)
			await _dbContext.PlayerRegistrations
			.AnyAsync(p => p.Email != null && p.Email.ToLower() == email
			&& p.MemberRegistrationSessionId == Int32.Parse(memberRegistrationSessionId)
			&& !p.Status.Equals(RegistrationStatusConstant.PENDING)) 
			||
			//khác mùa, pending => return true ( tức là ko đăng kí đc)
			await _dbContext.PlayerRegistrations
			.AnyAsync(p => p.Email != null && p.Email.ToLower() == email
			&& p.MemberRegistrationSessionId != Int32.Parse(memberRegistrationSessionId)
			&& p.Status.Equals(RegistrationStatusConstant.PENDING));
		}

		//kiểm tra email tồn tại ở trạng thái pending ko 
        public async Task<bool> IsEmailExistsAndPendingAsync( string? email)
        {
			var pr = await _dbContext.PlayerRegistrations
				.Where(pr => pr.Email == email 
				&& pr.Status == RegistrationStatusConstant.PENDING )
				.FirstOrDefaultAsync();

            if (pr != null)
			{
				return true;
			}
			return false;
        }

		//Kiểm tra xem có tồn tại manager với userId truyền vào ko 
		public async Task<bool> IsExistedManagerById(string userId)
		{
			return await _dbContext.Users.AnyAsync(x => x.UserId == userId && x.RoleCode == RoleCodeConstant.ManagerCode);
		}

		public async Task<bool> IsExistedParentByCitizen(string citizenId)
		{
			return await _dbContext.Parents.AnyAsync(x=>x.CitizenId == citizenId );
		}

		public async Task<bool> UpdateRegistrationStatusAsync(int registrationId, string newStatus, string? reviewerId = null)
		{
			var registration = await _dbContext.PlayerRegistrations
				.FirstOrDefaultAsync(r => r.PlayerRegistrationId == registrationId);

			if (registration == null)
				return false;

			registration.Status = newStatus;
			registration.ReviewedDate = DateTime.Now;

			if (!string.IsNullOrEmpty(reviewerId))
				registration.FormReviewedBy = reviewerId;

			await _dbContext.SaveChangesAsync();
			return true;
		}

		public async Task<PagedResponseDto<PlayerSubmittedFormDto>> GetPlayersOldForm(int? memberRegistrationSessionId, string? email, int pageNumber = 1, int pageSize = 10)
		{
			var query = _dbContext.PlayerRegistrations
				  .Include(p => p.MemberRegistrationSession)
				  .AsQueryable();

			// Lọc theo memberRegistrationSessionId
			if (memberRegistrationSessionId.HasValue)
			{
				query = query.Where(p => p.MemberRegistrationSessionId == memberRegistrationSessionId.Value);
			}

			// Lọc theo email
			if (!string.IsNullOrEmpty(email))
			{
				query = query.Where(p => p.Email == email);
			}

			// Tổng số bản ghi sau khi lọc
			var totalRecords = await query.CountAsync();

			// Áp dụng phân trang và lấy dữ liệu
			var players = await query
				.OrderBy(p => p.PlayerRegistrationId)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.Select(p => new PlayerSubmittedFormDto
				{
					PlayerRegistrationId = p.PlayerRegistrationId,
					FullName = p.FullName,
					GenerationAndSchoolName = p.GenerationAndSchoolName,
					PhoneNumber = p.PhoneNumber,
					Email = p.Email,
					Gender = p.Gender,
					DateOfBirth = p.DateOfBirth,
					Height = p.Height,
					Weight = p.Weight,
					FacebookProfileUrl = p.FacebookProfileUrl,
					KnowledgeAboutAcademy = p.KnowledgeAboutAcademy,
					ReasonToChooseUs = p.ReasonToChooseUs,
					Position = p.Position,
					Experience = p.Experience,
					Achievement = p.Achievement,
					CandidateNumber = p.CandidateNumber,
					ParentName = p.ParentName,
					ParentPhoneNumber = p.ParentPhoneNumber,
					ParentEmail = p.ParentEmail,
					RelationshipWithParent = p.RelationshipWithParent,
					ParentCitizenId = p.ParentCitizenId,
					TryOutNote = p.TryOutNote,
					SubmitedDate = p.SubmitedDate,
					Status = p.Status,
					RegistrationName = p.MemberRegistrationSession.RegistrationName,
					MemberRegistrationSessionId =p.MemberRegistrationSession.MemberRegistrationSessionId,
					StartDate = p.MemberRegistrationSession.StartDate,
					EndDate = p.MemberRegistrationSession.EndDate
				}).Where(x=>x.Status == RegistrationStatusConstant.PENDING)
				.ToListAsync();

			return new PagedResponseDto<PlayerSubmittedFormDto>
			{
				Items = players,
				TotalRecords = totalRecords,
				CurrentPage = pageNumber,
				PageSize = pageSize
			};
		}
		public async Task<bool> DeleteByEmailAndSessionIdAsync(int sessionId, string email)
		{
			var registration = await _dbContext.PlayerRegistrations
				.FirstOrDefaultAsync(p => p.Email == email && p.MemberRegistrationSessionId == sessionId);

			if (registration == null)
				return false;

			_dbContext.PlayerRegistrations.Remove(registration);
			await _dbContext.SaveChangesAsync();
			return true;
		}
	}
}
