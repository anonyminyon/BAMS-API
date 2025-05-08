using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.ManagerManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly BamsDbContext _dbContext;
        public ManagerRepository(BamsDbContext dbContext) { _dbContext = dbContext; }

        public async Task AddManagerAsync(User user, Manager manager)
        {
            _dbContext.Users.Add(user);
            _dbContext.Managers.Add(manager);
            await _dbContext.SaveChangesAsync(); // Đảm bảo có dòng này
        }


        //using for assign manager to team just update in manager table
        public async Task<ManagerDto?> UpdateManagerByUserIdAsync(ManagerDto managerDto)
        {
            var manager = await _dbContext.Managers.FindAsync(managerDto.UserId);
            if (manager == null) return null;

            _dbContext.Entry(manager).CurrentValues.SetValues(managerDto);
            await _dbContext.SaveChangesAsync();
            return managerDto;
        }

        public async Task<User?> GetUserWithManagerDetailAsync(string userId)
        {
            return await _dbContext.Users
                .Include(u => u.Manager) // Include Manager data
                .FirstOrDefaultAsync(u => u.UserId == userId && u.RoleCode == RoleCodeConstant.ManagerCode);
        }

        public async Task<bool> ChangeStatusManagerAsync(string userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null || user.RoleCode != RoleCodeConstant.ManagerCode)
            {
                return false;
            }

            user.IsEnable = !user.IsEnable;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        //=========================Update-user-manager-=========================
        public async Task<Manager> GetManagerByUserIdAsync(string userId)
        {
            return await _dbContext.Managers.FindAsync(userId);
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<bool> UpdateManagerAsync(Manager manager)
        {
            _dbContext.Managers.Update(manager);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _dbContext.Users.Update(user);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        //=======================================================================

        public async Task<bool> IsEmailFullNameExists(UserAccountDto<ManagerDto> user)
        {
            return await _dbContext.Users
                .Where(u => u.UserId != user.UserId)
                .Where(u=>u.Fullname.Equals(user.Fullname) || u.Email.Equals(user.Email) || u.Username.Equals(user.Username)).AnyAsync();
        }

        public async Task<PagedResponseDto<Manager>> GetFilteredPagedManagersAsync(ManagerFilterDto filter)
        {
            var query = _dbContext.Managers
                .Include(m => m.User) // Include thông tin User
                .AsQueryable();

            //  Lọc theo tên
            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(m => m.User.Fullname.Contains(filter.Name));

            //  Lọc theo email
            if (!string.IsNullOrWhiteSpace(filter.Email))
                query = query.Where(m => m.User.Email.Contains(filter.Email));

            //  Lọc theo trạng thái (Enable/Disable)
            if (filter.IsEnable.HasValue)
                query = query.Where(m => m.User.IsEnable == filter.IsEnable.Value);

            //  Lọc theo TeamId (nếu TeamId là số, cần kiểm tra kiểu dữ liệu)
            if (!string.IsNullOrEmpty(filter.TeamId))
                query = query.Where(m => m.TeamId == filter.TeamId);

            //  Tổng số bản ghi
            int totalRecords = await query.CountAsync();

            //  Kiểm tra xem `SortBy` có hợp lệ không
            var validSortProperties = new HashSet<string> { "Fullname", "Email", "IsEnable", "TeamId" };
            bool isValidSort = !string.IsNullOrEmpty(filter.SortBy) && validSortProperties.Contains(filter.SortBy);

            //  Sắp xếp kết quả
            if (isValidSort)
            {
                query = filter.IsDescending
                    ? query.OrderByDescending(e => EF.Property<object>(e.User, filter.SortBy))
                    : query.OrderBy(e => EF.Property<object>(e.User, filter.SortBy));
            }
            else
            {
                // Sắp xếp mặc định theo Fullname
                query = query.OrderBy(m => m.User.Fullname);
            }

            //  Phân trang
            var managers = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            //  Trả về kết quả dạng phân trang
            return new PagedResponseDto<Manager>
            {
                Items = managers,
                TotalRecords = totalRecords,
                PageSize = filter.PageSize,
                CurrentPage = filter.Page,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)filter.PageSize)
            };
        }

    }
}
