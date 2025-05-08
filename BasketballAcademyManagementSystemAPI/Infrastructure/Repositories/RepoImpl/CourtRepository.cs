using BasketballAcademyManagementSystemAPI.Application.DTOs.CourtManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.InkML;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class CourtRepository : ICourtRepository
    {
        private readonly BamsDbContext _dbContext;

        public CourtRepository(BamsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResponseDto<Court>> GetAllCourts(CourtFilterDto filter)
        {
            var query = _dbContext.Courts.AsQueryable();

            // Áp dụng các filter
            if (!string.IsNullOrEmpty(filter.CourtName))
                query = query.Where(c => c.CourtName.Contains(filter.CourtName));
            if (!string.IsNullOrEmpty(filter.Address))
                query = query.Where(c => c.Address.Contains(filter.Address));
            if (!string.IsNullOrEmpty(filter.Contact))
                query = query.Where(c => c.Contact.Contains(filter.Contact));
            if (filter.Status.HasValue)
                query = query.Where(c => c.Status == filter.Status);
            else
                query = query.Where(c => c.Status == CourtConstant.Status.ACTIVE);
            if (!string.IsNullOrEmpty(filter.Type))
                query = query.Where(c => c.Type == filter.Type);
            if (!string.IsNullOrEmpty(filter.Kind))
                query = query.Where(c => c.Kind == filter.Kind);
            if (filter.UsagePurpose.HasValue)
            {
                var purposeValue = (int)filter.UsagePurpose.Value;

                switch (purposeValue)
                {
                    case CourtConstant.UsagePurpose.COMPETE_AND_TRAINING:
                        query = query.Where(c => c.UsagePurpose == CourtConstant.UsagePurpose.COMPETE_AND_TRAINING ||
                                               c.UsagePurpose == CourtConstant.UsagePurpose.COMPETE ||
                                               c.UsagePurpose == CourtConstant.UsagePurpose.TRAINING);
                        break;

                    case CourtConstant.UsagePurpose.COMPETE:
                        query = query.Where(c => c.UsagePurpose == CourtConstant.UsagePurpose.COMPETE ||
                                               c.UsagePurpose == CourtConstant.UsagePurpose.COMPETE_AND_TRAINING);
                        break;

                    case CourtConstant.UsagePurpose.TRAINING:
                        query = query.Where(c => c.UsagePurpose == CourtConstant.UsagePurpose.TRAINING ||
                                               c.UsagePurpose == CourtConstant.UsagePurpose.COMPETE_AND_TRAINING);
                        break;
                }
            }
            if (filter.MinRentPricePerHour.HasValue)
                query = query.Where(c => c.RentPricePerHour >= filter.MinRentPricePerHour);
            if (filter.MaxRentPricePerHour.HasValue)
                query = query.Where(c => c.RentPricePerHour <= filter.MaxRentPricePerHour);

            // Đếm tổng số bản ghi
            int totalRecords = await query.CountAsync();

            // Lấy dữ liệu phân trang
            var courts = await query
                .OrderBy(c => c.Status)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            // Trả về kết quả phân trang
            return new PagedResponseDto<Court>
            {
                Items = courts,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)filter.PageSize),
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }

        public async Task<Court?> GetCourtById(string courtId)
        {
            return await _dbContext.Courts.FindAsync(courtId);
        }

        public async Task<Court> CreateCourt(Court court)
        {
            _dbContext.Courts.Add(court);
            await _dbContext.SaveChangesAsync();
            return court;
        }

        public async Task<Court?> UpdateCourt(Court court)
        {
            var existingCourt = await _dbContext.Courts.FindAsync(court.CourtId);
            if (existingCourt == null) return null;

            _dbContext.Entry(existingCourt).CurrentValues.SetValues(court);
            await _dbContext.SaveChangesAsync();
            return existingCourt;
        }

        public async Task<bool> DisableCourt(string courtId)
        {
            var court = await _dbContext.Courts.FindAsync(courtId);
            if (court == null) return false;

            court.Status = CourtConstant.Status.DELETED;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Court> GetCourtByNameExceptCourtID(string courtName, string? courtId)
        {
            return await _dbContext.Courts.FirstOrDefaultAsync(c => 
            c.CourtName == courtName 
            && (courtId == null || c.CourtId != courtId) 
            && !c.Status.Equals(CourtConstant.Status.DELETED));//check if they get court already delete
        }

        public async Task<bool> CourtExistsAsync(string courtId)
        {
            return await _dbContext.Courts.AnyAsync(c => c.CourtId == courtId);
        }
    }
}
