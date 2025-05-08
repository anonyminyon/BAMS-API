using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Team;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class TeamRepository:ITeamRepository
	{
		private readonly BamsDbContext _context;

		public TeamRepository(BamsDbContext context)
		{
			_context = context;
		}

		public async Task<Team> AddTeamAsync(Team team)
		{
			await _context.Teams.AddAsync(team);
			await _context.SaveChangesAsync();
			return team;
		}

		public async Task<bool> IsTeamNameExistsAsync(string teamName, string? excludeTeamId = null)
		{
			return await _context.Teams
				.AnyAsync(t => t.TeamName.ToLower() == teamName.ToLower() && (excludeTeamId == null || t.TeamId != excludeTeamId));
		}

		public async Task<TeamDetailsDto?> GetTeamDtoByIdAsync(string teamId)
		{
				return await _context.Teams.Include(x=>x.FundManager).Include(X=>X.UserTeamHistories)
					.Where(t => t.TeamId == teamId)
					.Select(t => new TeamDetailsDto
					{
						TeamId = t.TeamId,
						TeamName = t.TeamName,
						Status = t.Status,
						CreateAt = t.CreateAt,
						FundManagerId = t.FundManagerId,
						FundManagerName = t.FundManager.Fullname,
						Players = t.Players.Where(p => p.User.IsEnable == true) //  Lọc chỉ lấy Players có isEnable = true
							.Select(p => new PlayerDto
							{
								UserId = p.UserId,
								TeamId = p.TeamId,
								Fullname = p.User.Fullname,  // 🔹 Lấy Fullname từ bảng User
								Position = p.Position ?? "N/A",
								Email = p.User.Email,
								Phone = p.User.Phone,
								TeamName = p.Team == null ? "N/A" : p.Team.TeamName,
								DateOfBirth = p.User.DateOfBirth,
								Weight = p.Weight,
								Height = p.Height,
								ShirtNumber = p.ShirtNumber,
								ClubJoinDate = DateOnly.FromDateTime(p.Team.UserTeamHistories.Where(th => th.TeamId == p.TeamId)
                        .Select(th => th.JoinDate)
                        .FirstOrDefault()),
								RelationshipWithParent = p.RelationshipWithParent
							}).ToList(),
						Coaches = t.Coaches.Where(p => p.User.IsEnable == true) //  Lọc chỉ lấy Coaches có isEnable = true
							.Select(c => new CoachDto
							{
								UserId = c.UserId,
								TeamId = c.TeamId,
								CreatedByPresidentId = c.CreatedByPresidentId,
								CoachName = c.User.Fullname,
								CoachEmail = c.User.Email,
								CoachPhone = c.User.Phone,
								Bio = c.Bio ?? "",
								ContractStartDate = c.ContractStartDate,
								ContractEndDate = c.ContractEndDate
							}).ToList(),
						Managers = t.Managers.Where(p => p.User.IsEnable == true)  //  Lọc chỉ lấy Managers có isEnable = true
							.Select(m => new ManagerDto
							{
								UserId = m.UserId,
								TeamId = m.TeamId,
								ManagerName = m.User.Fullname,
								BankAccountNumber = m.BankAccountNumber,
								ManagerEmail = m.User.Email,
								ManagerPhone = m.User.Phone,
								BankName = m.BankName ?? ""
							}).ToList()
					})
					.FirstOrDefaultAsync();
		}


		//Update team
		public async Task<Team> GetTeamByIdAsync(string teamId)
		{
			return await _context.Teams.FindAsync(teamId);
		}
		public async Task UpdateTeamAsync(Team team)
		{
			 _context.Teams.Update(team);
			await _context.SaveChangesAsync();
        }

		//lấy thông tin danh sách những cầu thủ theo list playerID của 1 team
		public async Task<List<Player>> GetPlayersByTeamAsync(string teamId, List<string> playerIds)
		{
			return await _context.Players
				.Where(p => p.TeamId == teamId && playerIds.Contains(p.UserId))
				.ToListAsync();
		}
		//lấy thông tin danh sách những hlv theo list playerID của 1 team

		public async Task<List<Coach>> GetCoachesByTeamAsync(string teamId, List<string> coachIds)
		{
			return await _context.Coaches
				.Where(c => c.TeamId == teamId && coachIds.Contains(c.UserId))
				.ToListAsync();
		}

		//lấy thông tin danh sách những quản lý theo list playerID của 1 team

		public async Task<List<Manager>> GetManagersByTeamAsync(string teamId, List<string> managerIds)
		{
			return await _context.Managers
				.Where(m => m.TeamId == teamId && managerIds.Contains(m.UserId))
				.ToListAsync();
		}


		//Disband team
		public async Task<List<Player>> GetAllPlayersByTeamAsync(string teamId)
		{
			return await _context.Players.Where(p => p.TeamId == teamId).Include(p => p.User).ToListAsync();
		}

		public async Task<List<Coach>> GetAllCoachesByTeamAsync(string teamId)
		{
			return await _context.Coaches.Where(c => c.TeamId == teamId).ToListAsync();
		}

		public async Task<List<Manager>> GetAllManagersByTeamAsync(string teamId)
		{
			return await _context.Managers.Where(m => m.TeamId == teamId).ToListAsync();
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}

		//Lấy danh sách đội 
		public async Task<PagedResponseDto<TeamDto>> GetTeamsAsync(TeamFilterDto filter)
		{
			IQueryable<Team> query = _context.Teams;

			// Áp dụng bộ lọc nếu có giá trị
			if (!string.IsNullOrEmpty(filter.TeamName))
			{
				query = query.Where(t => t.TeamName.Contains(filter.TeamName));
			}

			if (filter.Status.HasValue)
			{
				query = query.Where(t => t.Status == filter.Status.Value);
			}

			// Tính tổng số bản ghi trước khi phân trang
			int totalRecords = await query.CountAsync();

			// Tính tổng số trang
			int totalPages = (int)Math.Ceiling(totalRecords / (double)filter.PageSize);

			// Áp dụng phân trang và ánh xạ sang DTO
			var teams = await query
				.Skip((filter.PageNumber - 1) * filter.PageSize)
				.Take(filter.PageSize)
				.Select(t => new TeamDto
				{
					TeamId = t.TeamId,
					TeamName = t.TeamName,
					Status = t.Status
				})
				.ToListAsync();

			// Trả về dữ liệu với thông tin phân trang
			return new PagedResponseDto<TeamDto>
			{
				Items = teams,
				TotalRecords = totalRecords,
				TotalPages = totalPages,
				CurrentPage = filter.PageNumber,
				PageSize = filter.PageSize
			};
		}

		// Thêm lịch sử team sau khi giải tán
		public async Task AddUserTeamHistoryAsync(UserTeamHistory userTeamHistory)
		{
			await _context.UserTeamHistories.AddAsync(userTeamHistory);
		}

        // Kiểm tra team tồn tại
        public async Task<bool> TeamExistsAsync(string teamId)
        {
            return await _context.Teams.AnyAsync(t => t.TeamId == teamId);
        }

		//Cập nhật lại người quản lí nhận tiền trong 1 team
		public async Task<bool> UpdateFundManagerIdAsync(string teamId, string fundManagerId)
		{
			var team = await _context.Teams.FindAsync(teamId);
			if (team == null) return false;

			team.FundManagerId = fundManagerId;
			_context.Teams.Update(team);
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
