using AutoMapper;
using Azure.Core;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.PlayerManagement;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{

    public class PlayerRepository : IPlayerRepository
	{
		private readonly BamsDbContext _dbContext;
		private readonly IMapper _mapper;
		public PlayerRepository(BamsDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}
		//xóa phụ huynh khỏi con
		public async Task<bool> RemoveParentFromPlayerAsync(string playerId)
		{
			var player = await _dbContext.Players.FindAsync(playerId);
			if (player == null) return false;

			player.ParentId = null;
			await SaveChangesAsync();
			return true;
		}

		public async Task<User?> GetPlayerByIdAsync(string playerId)
		{
			return await _dbContext.Users
			 .Include(p => p.Player)
				 .ThenInclude(p => p.Team) // Bao gồm cả Team của player
			 .FirstOrDefaultAsync(c => c.UserId == playerId && c.RoleCode == RoleCodeConstant.PlayerCode); ;
		}
		//cái này dùng cho lấy list player theo list id của use by user id trong expenditure
        public async Task<List<User>> GetPlayersByIdsAsync(IEnumerable<string> userIds)
        {
            return await _dbContext.Users
                .Where(u => userIds.Contains(u.UserId) && u.RoleCode == RoleCodeConstant.PlayerCode)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
		{
			await _dbContext.SaveChangesAsync();
		}

		public async Task<bool> DisablePlayer(string userId)
		{
			if (userId == null)
			{
				return false;
			}
			var user = await _dbContext.Users.FindAsync(userId);
			if (user == null) return false;
			user.IsEnable = false;
			try
			{
				int rowsAffected = await _dbContext.SaveChangesAsync();
				return true; // Nếu có ít nhất 1 dòng được update → Trả về true
			}
			catch (Exception ex)
			{
				Console.WriteLine($" Lỗi khi Disable User: {ex.Message}");
				return false; // Có lỗi khi lưu vào DB
			}

		}
		public async Task<Player?> GetPlayerByParentIdAsync(string parentId)
		{
			return await _dbContext.Players.FirstOrDefaultAsync(u => u.ParentId != null && u.ParentId == parentId);
		}

        public async Task<List<string>> GetPlayerUserIdsByParentIdAsync(string parentId)
        {
            if (string.IsNullOrWhiteSpace(parentId))
            {
                throw new ArgumentException("ParentId cannot be null or empty.", nameof(parentId));
            }

            var playerUserIds = await _dbContext.Players
                .Where(p => p.ParentId == parentId)
                .Select(p => p.UserId.ToString())
                .ToListAsync();

            return playerUserIds;
        }

        public async Task<PagedResponseDto<PlayerResponse>> GetFilteredPlayersAsync(PlayerFilterDto request)
        {
            var query = _dbContext.Players
                .Include(p => p.User)
                .Include(p => p.Team).ThenInclude(t => t.UserTeamHistories)
                .AsQueryable();

            // Áp dụng các filter
            if (!string.IsNullOrEmpty(request.Fullname))
                query = query.Where(p => p.User.Fullname.Contains(request.Fullname));

            if (!string.IsNullOrEmpty(request.Email))
                query = query.Where(p => p.User.Email.Contains(request.Email));

            if (!string.IsNullOrEmpty(request.Phone))
                query = query.Where(p => p.User.Phone.Contains(request.Phone));

            if (request.IsEnable.HasValue)
                query = query.Where(p => p.User.IsEnable == request.IsEnable.Value);

            if (!string.IsNullOrEmpty(request.TeamId))
                query = query.Where(p => p.TeamId == request.TeamId);

            if (request.OnlyNoTeam.HasValue)
            {
                if (request.OnlyNoTeam.Value)
                    query = query.Where(p => p.TeamId == null);
                else
                    query = query.Where(p => p.TeamId != null);
            }

            if (request.Gender.HasValue)
                query = query.Where(p => p.User.Gender == request.Gender);

            // Đếm tổng số bản ghi
            var totalRecords = await query.CountAsync();

			// Phân trang và map sang DTO
			var items = await query
				.Skip((request.PageIndex - 1) * request.PageSize)
				.Take(request.PageSize)
				.Select(p => new PlayerResponse
				{
					UserId = p.User.UserId,
					Fullname = p.User.Fullname,
					Email = p.User.Email,
					Phone = p.User.Phone,
					Address = p.User.Address,
					DateOfBirth = p.User.DateOfBirth,
					Gender = p.User.Gender,
					RoleCode = p.User.RoleCode,
					IsEnable = p.User.IsEnable,
					ParentId = p.ParentId,
					TeamId = p.TeamId,
					TeamName = p.Team != null ? p.Team.TeamName : null,
					RelationshipWithParent = p.RelationshipWithParent,
					Weight = p.Weight,
					Height = p.Height,
					Position = p.Position,
					ShirtNumber = p.ShirtNumber,
					ClubJoinDate = DateOnly.FromDateTime(p.Team.UserTeamHistories.Where(th => th.TeamId == p.TeamId)
						.Select(th => th.JoinDate)
						.FirstOrDefault())
				})
                .ToListAsync();

            return new PagedResponseDto<PlayerResponse>
            {
                Items = items,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / request.PageSize),
                CurrentPage = request.PageIndex,
                PageSize = request.PageSize
            };
        }


        public async Task<List<PlayerResponse>> GetPlayersWithoutTeamByGenderAsync(string teamId)
		{
			// Truy vấn tên đội từ TeamId
			var teamName = await _dbContext.Teams
				.Where(t => t.TeamId == teamId)
				.Select(t => t.TeamName)
				.FirstOrDefaultAsync();

			// Nếu không tìm thấy tên đội, trả về lỗi hoặc danh sách trống
			if (teamName == null)
			{
				throw new Exception("Team not found.");
			}

			// Xác định giới tính dựa trên tên đội
			bool? genderFilter = null;
			if (teamName.ToLower().Contains("nam"))
			{
				genderFilter = true;  // Nam
			}
			else if (teamName.ToLower().Contains("nữ"))
			{
				genderFilter = false; // Nữ
			}

			// Truy vấn các player chưa có team và có giới tính phù hợp
			var players = await _dbContext.Players
				.Join(_dbContext.Users, player => player.UserId, user => user.UserId, (player, user) => new { player, user })
				.Where(x => x.player.TeamId == null && x.user.Gender == genderFilter)
				.Select(x => new PlayerResponse
				{
					UserId = x.user.UserId,
					Fullname = x.user.Fullname,
					Email = x.user.Email,
					Phone = x.user.Phone,
					Address = x.user.Address,
					DateOfBirth = x.user.DateOfBirth,
					Gender = x.user.Gender,
					RoleCode = x.user.RoleCode,
					IsEnable = x.user.IsEnable,
					ParentId = x.player.ParentId,
					TeamId = x.player.TeamId,
					RelationshipWithParent = x.player.RelationshipWithParent,
					Weight = x.player.Weight,
					Height = x.player.Height,
					Position = x.player.Position,
					ShirtNumber = x.player.ShirtNumber,
					ClubJoinDate = x.player.ClubJoinDate
				})
				.ToListAsync();

			return players;
		}

	}

}
