using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.CoachManagement;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class CoachRepository : ICoachRepository
    {
        private readonly BamsDbContext _dbContext;

        public CoachRepository(BamsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCoachAsync(User user, Coach coach)
        {
            _dbContext.Users.Add(user);
            _dbContext.Coaches.Add(coach);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetCoachByUserIdAsync(string userId)
        {
            return await _dbContext.Users
                .Include(c => c.Coach)
                    .ThenInclude(coach => coach.Team) // Bao gồm cả Team của Coach
                .Include(c => c.Coach)
                    .ThenInclude(coach => coach.CreatedByPresident) // Bao gồm cả President của Coach
                .ThenInclude(president => president.User) // Bao gồm cả User của President
                .FirstOrDefaultAsync(c => c.UserId == userId && c.RoleCode == RoleCodeConstant.CoachCode);
        }

        public async Task<bool> ChangeStatusCoachAsync(string userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null || user.RoleCode != RoleCodeConstant.CoachCode)
            {
                return false;
            }

            user.IsEnable = !user.IsEnable;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCoachAsync(Coach coach)
        {
            _dbContext.Coaches.Update(coach);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<PagedResponseDto<Coach>> GetFilteredPagedCoachesAsync(CoachFilterDto filter)
        {
            var query = _dbContext.Coaches
                .Include(c => c.User)
                .Include(c => c.Team) // Bao gồm cả Team của Coach
                .AsQueryable();

            // Lọc theo tên
            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(c => c.User.Fullname.Contains(filter.Name));

            // Lọc theo email
            if (!string.IsNullOrWhiteSpace(filter.Email))
                query = query.Where(c => c.User.Email.Contains(filter.Email));

            // Lọc theo trạng thái (Enable/Disable)
            if (filter.IsEnable.HasValue)
                query = query.Where(c => c.User.IsEnable == filter.IsEnable.Value);

            // Lọc theo TeamId
            if (!string.IsNullOrEmpty(filter.TeamId))
                query = query.Where(c => c.TeamId == filter.TeamId);

            // Tổng số bản ghi
            int totalRecords = await query.CountAsync();

            // Sắp xếp kết quả
            query = filter.IsDescending
                ? query.OrderByDescending(c => c.User.Fullname)
                : query.OrderBy(c => c.User.Fullname);

            // Phân trang
            var coaches = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            // Trả về kết quả dạng phân trang
            return new PagedResponseDto<Coach>
            {
                Items = coaches,
                TotalRecords = totalRecords,
                PageSize = filter.PageSize,
                CurrentPage = filter.Page,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)filter.PageSize)
            };
        }

        public async Task<Dictionary<string, string>> CheckDuplicateCoachAsync(string phone, string email, string username, string? userId = null)
        {
            var errors = new Dictionary<string, string>();

            var phoneExists = await _dbContext.Users.AnyAsync(c => c.Phone == phone && (c.UserId == null || c.UserId != userId));
            if (phoneExists)
            {
                errors.Add(CoachMessage.Key.ErrorPhoneNum, CoachMessage.Error.PhoneAlreadyRegistered);
            }

            var emailExists = await _dbContext.Users.AnyAsync(c => c.Email == email && (c.UserId == null || c.UserId != userId));
            if (emailExists)
            {
                errors.Add(CoachMessage.Key.ErrorEmail, CoachMessage.Error.EmailAlreadyRegistered);
            }

            var usernameExists = await _dbContext.Users.AnyAsync(c => c.Username == username && (c.UserId == null || c.UserId != userId));
            if (usernameExists)
            {
                errors.Add(CoachMessage.Key.ErrorUsername, CoachMessage.Error.UsernameAlreadyRegistered);
            }

            return errors;
        }

        public async Task<Dictionary<string, string>> CheckDuplicateCoachAsync(string phone, string email)
        {
            var errors = new Dictionary<string, string>();

            var phoneExists = await _dbContext.Users.AnyAsync(c => c.Phone == phone);
            if (phoneExists)
            {
                errors.Add(CoachMessage.Key.ErrorPhoneNum, CoachMessage.Error.PhoneAlreadyRegistered);
            }

            var emailExists = await _dbContext.Users.AnyAsync(c => c.Email == email);
            if (emailExists)
            {
                errors.Add(CoachMessage.Key.ErrorEmail, CoachMessage.Error.EmailAlreadyRegistered);
            }
            return errors;
        }

        public async Task<Coach?> GetACoachByUserIdAsync(string userId)
        {
            return await _dbContext.Coaches
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

    }
}