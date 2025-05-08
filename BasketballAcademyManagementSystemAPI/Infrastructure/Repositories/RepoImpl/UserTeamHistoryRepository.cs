using BasketballAcademyManagementSystemAPI.Application.DTOs.PlayerManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TeamFund;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Infrastructure;
using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Application.Repositories
{
	public class UserTeamHistoryRepository : IUserTeamHistoryRepository
	{
		private readonly BamsDbContext _dbcontext;

		public UserTeamHistoryRepository(BamsDbContext context)
		{
			_dbcontext = context;
		}

		public async Task<UserTeamHistory> GetMostRecentUserTeamHistoryAsync(string userId)
		{
			return await _dbcontext.UserTeamHistories
				.Where(uth => uth.UserId == userId)
				.OrderByDescending(uth => uth.JoinDate)
				.FirstOrDefaultAsync();
		}

		public async Task UpdateUserTeamHistoryAsync(UserTeamHistory userTeamHistory)
		{
			_dbcontext.UserTeamHistories.Update(userTeamHistory);
			await _dbcontext.SaveChangesAsync();
		}

		public async Task AddUserTeamHistoryAsync(UserTeamHistory userTeamHistory)
		{
			await _dbcontext.UserTeamHistories.AddAsync(userTeamHistory);
			await _dbcontext.SaveChangesAsync();
		}

		//lấy ra danh sách user 1 team theo ngày cụ thể 
		public async Task<List<PlayerExpenditureResponseDto>> GetUserIdsByTeamAndDateAsync(string teamId, DateTime targetDate)
		{
			return await _dbcontext.UserTeamHistories
				.Where(h =>
					h.TeamId == teamId &&
					h.JoinDate <= targetDate &&
					(h.LeftDate == null || targetDate <= h.LeftDate))
				.Join(_dbcontext.Users,
					history => history.UserId,
					user => user.UserId,
					(history, user) => new { history, user })
				.Where(x => x.user.RoleCode == RoleCodeConstant.PlayerCode)
				.Select(x => new PlayerExpenditureResponseDto
				{
					UserId = x.user.UserId,
					Fullname = x.user.Fullname,
					Phone = x.user.Phone,
					Email = x.user.Email,
				})
				.Distinct()
				.ToListAsync();
		}


	}
}
