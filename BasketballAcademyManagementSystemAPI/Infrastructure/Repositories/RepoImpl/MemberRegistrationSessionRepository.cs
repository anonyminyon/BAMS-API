using BasketballAcademyManagementSystemAPI.Application.DTOs.MemberRegistrationSession;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class MemberRegistrationSessionRepository : IMemberRegistrationSessionRepository
    {
        private readonly BamsDbContext _dbContext;

        public MemberRegistrationSessionRepository(BamsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MemberRegistrationSession>> GetAllAsync()
        {
            return await _dbContext.MemberRegistrationSessions.ToListAsync();
        }

        public async Task<MemberRegistrationSession?> GetByIdAsync(int id)
        {
            return await _dbContext.MemberRegistrationSessions.FindAsync(id);
        }

        public async Task AddAsync(MemberRegistrationSession session)
        {
            await _dbContext.MemberRegistrationSessions.AddAsync(session);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(MemberRegistrationSession session)
        {
            _dbContext.MemberRegistrationSessions.Update(session);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var session = await _dbContext.MemberRegistrationSessions.FindAsync(id);
            if (session != null)
            {
                _dbContext.MemberRegistrationSessions.Remove(session);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteAsync(MemberRegistrationSession session)
        {
            _dbContext.MemberRegistrationSessions.Remove(session);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<MemberRegistrationSession?> GetToDeleteByIdAsync(int id)
        {
            return await _dbContext.MemberRegistrationSessions
                .Include(m => m.ManagerRegistrations)
                .Include(p => p.PlayerRegistrations).ThenInclude(p => p.TryOutScorecards)
                .FirstOrDefaultAsync(s => s.MemberRegistrationSessionId == id);
        }

        public async Task<(IEnumerable<MemberRegistrationSession>, int)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _dbContext.MemberRegistrationSessions.AsQueryable();
            int totalRecords = await query.CountAsync();
            var sessions = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (sessions, totalRecords);
        }

        public async Task<IEnumerable<MemberRegistrationSession>> FilterAsync(string? name, DateTime? startDate, DateTime? endDate)
        {
            var query = _dbContext.MemberRegistrationSessions.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(s => s.RegistrationName.Contains(name));
            }
            if (startDate.HasValue)
            {
                query = query.Where(s => s.StartDate >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                query = query.Where(s => s.EndDate <= endDate.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<PagedResponseDto<MemberRegistrationSession>> GetFilteredPagedAsync(MemberRegistrationSessionFilterDto filter)
        {
            var query = GetQueryableByFilter(filter);

            // Tổng số bản ghi
            int totalRecords = await query.CountAsync();

            // Phân trang
            var sessions = await query
                .Skip((int)((filter.PageNumber - 1) * filter.PageSize))
                .Take((int)filter.PageSize)
                .ToListAsync();

            return new PagedResponseDto<MemberRegistrationSession>
            {
                Items = sessions,
                TotalRecords = totalRecords,
                PageSize = (int)filter.PageSize,
                CurrentPage = (int)filter.PageNumber,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)filter.PageSize)
            };
        }

        private IQueryable<MemberRegistrationSession> GetQueryableByFilter(MemberRegistrationSessionFilterDto filter)
        {
            var query = _dbContext.MemberRegistrationSessions.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(s => s.RegistrationName.Contains(filter.Name));
            }
            if (filter.StartDate.HasValue)
            {
                var startOfDay = filter.StartDate.Value.Date;
                query = query.Where(s => s.StartDate.Date >= startOfDay);
            }
            if (filter.EndDate.HasValue)
            {
                var endOfDay = filter.EndDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(s => s.EndDate.Date <= endOfDay);
            }
            if (filter.IsEnable.HasValue)
            {
                if (filter.IsEnable == true)
                {
                    query = query.Where(s => s.IsEnable == true && s.EndDate >= DateTime.Now);
                }
                else if (filter.IsEnable == false)
                {
                    query = query.Where(s => s.IsEnable == false || s.EndDate < DateTime.Now);
                }
            }

            // Sắp xếp
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                query = filter.IsDescending
                    ? query.OrderByDescending(e => EF.Property<object>(e, filter.SortBy))
                    : query.OrderBy(e => EF.Property<object>(e, filter.SortBy));
            }
            else
            {
                query = query.OrderBy(s => s.MemberRegistrationSessionId);
            }

            return query;
        }
    }
}
