using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Payment;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly BamsDbContext _dbContext;

        public PaymentRepository(BamsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> HasAccessToPaymentDetailAsync(string userId, string paymentId, string roleCode)
        {
            // First, get the payment with its related TeamFund and Player
            var payment = await _dbContext.Payments
                .Include(p => p.TeamFund)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId);

            if (payment == null)
            {
                return false; // Payment doesn't exist
            }

            if (roleCode == RoleCodeConstant.ManagerCode)
            {
                // Check if the payment has a TeamFund and if the manager is in that team
                if (payment.TeamFundId != null)
                {
                    return await _dbContext.Managers
                        .AnyAsync(m => m.UserId == userId && m.TeamId == payment.TeamFund.TeamId);
                }
                return false;
            }
            else if (roleCode == RoleCodeConstant.CoachCode)
            {
                // Check if the payment has a TeamFund and if the coach is in that team
                if (payment.TeamFundId != null)
                {
                    return await _dbContext.Coaches
                        .AnyAsync(c => c.UserId == userId && c.TeamId == payment.TeamFund.TeamId);
                }
                return false;
            }
            else if (roleCode == RoleCodeConstant.ParentCode)
            {
                // Check if the payment's UserId belongs to a player who has this parent
                return await _dbContext.Players
                    .AnyAsync(p => p.UserId == payment.UserId && p.ParentId == userId);
            }

            return false;
        }

        public async Task<PagedResponseDto<PaymentDto>> GetPaymentAsync(PaymentFilterDto filter)
        {
            var query = _dbContext.Payments
                .Include(p => p.PaymentItems)
                .Include(p => p.TeamFund).ThenInclude(tf => tf.Team)
                .Include(p => p.User).ThenInclude(u => u.User)
                .AsQueryable();

            // Áp dụng các filter
            if (!string.IsNullOrEmpty(filter.PaymentId))
            {
                query = query.Where(p => p.PaymentId == filter.PaymentId);
            }
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                query = query.Where(p => p.UserId == filter.UserId);
            }
            //lọc theo status
            if (filter.Status.HasValue)
            {
                if (filter.Status == PaymentStatusConstant.OVERDUE)
                {
                    // Lọc các đơn chưa thanh toán và quá hạn (nếu không có filter.Status)
                    query = query.Where(p => p.Status == PaymentStatusConstant.NOT_PAID && p.DueDate < DateTime.Now);
                }
                else 
                {
                    query = query.Where(p => p.Status == filter.Status);
                }
            }
            //lọc theo phương thức thanh toán
            if (filter.PaymentMethod.HasValue)
            {
                query = query.Where(p => p.PaymentMethod == filter.PaymentMethod);
            }
            //lọc theo ngày cả paid date lẫn due date
            if (filter.StartDate.HasValue)
            {
                query = query.Where(p => p.PaidDate >= filter.StartDate);
            }
            if (filter.EndDate.HasValue)
            {
                query = query.Where(p => p.PaidDate <= filter.EndDate);
            }
            if (filter.StartDate.HasValue)
            {
                query = query.Where(p => p.DueDate >= filter.StartDate);
            }
            if (filter.EndDate.HasValue)
            {
                query = query.Where(p => p.DueDate <= filter.EndDate);
            }
            //lọc theo note
            if (!string.IsNullOrEmpty(filter.Note))
            {
                query = query.Where(p => p.Note.Contains(filter.Note));
            }
            //lọc theo due date
            if (filter.DueDate.HasValue)
            {
                query = query.Where(p => p.DueDate == filter.DueDate);
            }
            //lọc theo due date
            if (filter.PaidDate.HasValue)
            {
                query = query.Where(p => p.DueDate == filter.DueDate);
            }

            var totalRecords = await query.CountAsync();
            var payments = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var paymentDtos = payments.Select(p => new PaymentDto
            {
                PaymentId = p.PaymentId,
                TeamFundId = p.TeamFundId,
                TeamFundDescription = p.TeamFund.Description,
                UserId = p.UserId,
                Status = p.Status,
                PaidDate = p.PaidDate,
                Note = p.Note,
                DueDate = p.DueDate,
                PaymentMethod = p.PaymentMethod,
                TeamName = p.TeamFund?.Team?.TeamName,
                Fullname = p.User?.User?.Fullname,
                TotalAmount = p.PaymentItems.Sum(pi => pi.Amount),
                ApprovedAt = p.TeamFund?.ApprovedAt != null ? p.TeamFund.ApprovedAt : null
            }).ToList();

            return new PagedResponseDto<PaymentDto>
            {
                Items = paymentDtos,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)filter.PageSize),
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }

        //hàm này dùng cho parent nhưng cũng có thể xem xét dùng cho manager và coach khi tối ưu
        public async Task<PagedResponseDto<PaymentDto>> GetPaymentByListUserIdAsync(PaymentFilterDto filter, List<string> playerIds)
        {
            // Kiểm tra danh sách playerIds
            if (playerIds == null || !playerIds.Any())
            {
                return new PagedResponseDto<PaymentDto>
                {
                    Items = new List<PaymentDto>(),
                    TotalRecords = 0,
                    TotalPages = 0,
                    CurrentPage = filter.PageNumber,
                    PageSize = filter.PageSize
                };
            }

            var query = _dbContext.Payments
                .Include(p => p.PaymentItems)
                .Include(p => p.TeamFund).ThenInclude(tf => tf.Team)
                .Include(p => p.User).ThenInclude(u => u.User)
                .Where(p => playerIds.Contains(p.UserId)) // Lọc theo playerIds trước
                .AsQueryable();

            // Áp dụng các filter
            if (!string.IsNullOrEmpty(filter.PaymentId))
            {
                query = query.Where(p => p.PaymentId == filter.PaymentId);
            }
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                query = query.Where(p => p.UserId == filter.UserId);
            }
            //lọc theo status
            if (filter.Status.HasValue)
            {
                if (filter.Status == PaymentStatusConstant.OVERDUE)
                {
                    // Lọc các đơn chưa thanh toán và quá hạn (nếu không có filter.Status)
                    query = query.Where(p => p.Status == PaymentStatusConstant.NOT_PAID && p.DueDate < DateTime.Now);
                }
                else
                {
                    query = query.Where(p => p.Status == filter.Status);
                }
            }
            //lọc theo phương thức thanh toán
            if (filter.PaymentMethod.HasValue)
            {
                query = query.Where(p => p.PaymentMethod == filter.PaymentMethod);
            }
            //lọc theo ngày cả paid date lẫn due date
            if (filter.StartDate.HasValue)
            {
                query = query.Where(p => p.PaidDate >= filter.StartDate);
            }
            if (filter.EndDate.HasValue)
            {
                query = query.Where(p => p.PaidDate <= filter.EndDate);
            }
            if (filter.StartDate.HasValue)
            {
                query = query.Where(p => p.DueDate >= filter.StartDate);
            }
            if (filter.EndDate.HasValue)
            {
                query = query.Where(p => p.DueDate <= filter.EndDate);
            }
            //lọc theo note
            if (!string.IsNullOrEmpty(filter.Note))
            {
                query = query.Where(p => p.Note.Contains(filter.Note));
            }
            //lọc theo due date
            if (filter.DueDate.HasValue)
            {
                query = query.Where(p => p.DueDate == filter.DueDate);
            }
            //lọc theo due date
            if (filter.PaidDate.HasValue)
            {
                query = query.Where(p => p.DueDate == filter.DueDate);
            }

            // Tính tổng số bản ghi
            var totalRecords = await query.CountAsync();

            // Lấy dữ liệu phân trang
            var payments = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            // Chuyển đổi sang DTO
            var paymentDtos = payments.Select(p => new PaymentDto
            {
                PaymentId = p.PaymentId,
                TeamFundId = p.TeamFundId,
                TeamFundDescription = p.TeamFund.Description,
                UserId = p.UserId,
                Status = p.Status,
                PaidDate = p.PaidDate,
                Note = p.Note,
                DueDate = p.DueDate,
                PaymentMethod = p.PaymentMethod,
                TeamName = p.TeamFund?.Team?.TeamName,
                Fullname = p.User?.User?.Fullname,
                TotalAmount = p.PaymentItems.Sum(pi => pi.Amount),
                ApprovedAt = p.TeamFund?.ApprovedAt != null ? p.TeamFund.ApprovedAt : null
            }).ToList();

            // Trả về kết quả phân trang
            return new PagedResponseDto<PaymentDto>
            {
                Items = paymentDtos,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)filter.PageSize),
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }

        //dùng cho coach với manager xem được payment thuộc team mình quản lý
        public async Task<PagedResponseDto<PaymentDto>> GetPaymentByTeamIdAsync(PaymentFilterDto filter, string teamId)
        {
            var query = _dbContext.Payments
                .Include(p => p.PaymentItems)
                .Include(p => p.TeamFund).ThenInclude(tf => tf.Team)
                .Include(p => p.User).ThenInclude(u => u.User)
                .Where(p => p.TeamFund != null && p.TeamFund.TeamId == teamId);

            // Áp dụng các filter
            if (!string.IsNullOrEmpty(filter.PaymentId))
            {
                query = query.Where(p => p.PaymentId == filter.PaymentId);
            }
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                query = query.Where(p => p.UserId == filter.UserId);
            }
            //lọc theo status
            if (filter.Status.HasValue)
            {
                if (filter.Status == PaymentStatusConstant.OVERDUE)
                {
                    // Lọc các đơn chưa thanh toán và quá hạn (nếu không có filter.Status)
                    query = query.Where(p => p.Status == PaymentStatusConstant.NOT_PAID && p.DueDate < DateTime.Now);
                }
                else
                {
                    query = query.Where(p => p.Status == filter.Status);
                }
            }
            //lọc theo phương thức thanh toán
            if (filter.PaymentMethod.HasValue)
            {
                query = query.Where(p => p.PaymentMethod == filter.PaymentMethod);
            }
            //lọc theo ngày cả paid date lẫn due date
            if (filter.StartDate.HasValue)
            {
                query = query.Where(p => p.PaidDate >= filter.StartDate);
            }
            if (filter.EndDate.HasValue)
            {
                query = query.Where(p => p.PaidDate <= filter.EndDate);
            }
            if (filter.StartDate.HasValue)
            {
                query = query.Where(p => p.DueDate >= filter.StartDate);
            }
            if (filter.EndDate.HasValue)
            {
                query = query.Where(p => p.DueDate <= filter.EndDate);
            }
            //lọc theo note
            if (!string.IsNullOrEmpty(filter.Note))
            {
                query = query.Where(p => p.Note.Contains(filter.Note));
            }
            //lọc theo due date
            if (filter.DueDate.HasValue)
            {
                query = query.Where(p => p.DueDate == filter.DueDate);
            }
            //lọc theo due date
            if (filter.PaidDate.HasValue)
            {
                query = query.Where(p => p.DueDate == filter.DueDate);
            }

            var totalRecords = await query.CountAsync();

            var payments = await query
                .OrderByDescending(p => p.PaidDate)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var paymentDtos = payments.Select(p => new PaymentDto
            {
                PaymentId = p.PaymentId,
                TeamFundId = p.TeamFundId,
                TeamFundDescription = p.TeamFund.Description,
                UserId = p.UserId,
                Status = p.Status,
                PaidDate = p.PaidDate,
                Note = p.Note,
                DueDate = p.DueDate,
                PaymentMethod = p.PaymentMethod,
                TeamName = p.TeamFund?.Team?.TeamName,
                Fullname = p.User?.User?.Fullname,
                TotalAmount = p.PaymentItems.Sum(pi => pi.Amount),
                ApprovedAt = p.TeamFund?.ApprovedAt != null ? p.TeamFund.ApprovedAt : null
            }).ToList();

            return new PagedResponseDto<PaymentDto>
            {
                Items = paymentDtos,
                TotalRecords = totalRecords,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)filter.PageSize)
            };
        }
        
        public async Task<PaymentDetailDto?> GetPaymentDetailAsync(MyDetailPaymentRequestDto request)
        {
            var p = await _dbContext.Payments
                .Include(p => p.PaymentItems)
                .Include(p => p.TeamFund).ThenInclude(tf => tf.Team)
                .Include(p => p.User).ThenInclude(u => u.User)
                .FirstOrDefaultAsync(p => p.PaymentId == request.PaymentId);

            if (p == null)
            {
                return null;
            }

            return new PaymentDetailDto
            {
                PaymentId = p.PaymentId,
                TeamFundId = p.TeamFundId,
                TeamFundDescription = p.TeamFund.Description ?? "không có dữ liệu",
                UserId = p.UserId,
                Status = p.Status,
                PaidDate = p.PaidDate,
                Note = p.Note,
                TeamName = p.TeamFund?.Team?.TeamName ?? "không có team",
                Fullname = p.User?.User?.Fullname,
                DueDate = p.DueDate,
                PaymentMethod = p.PaymentMethod,
                ApprovedAt = p.TeamFund?.ApprovedAt != null ? p.TeamFund.ApprovedAt : null,
                PaymentItems = p.PaymentItems.Select(pi => new PaymentItemDto
                {
                    PaymentItemId = pi.PaymentItemId,
                    PaymentId = pi.PaymentId,
                    PaidItemName = pi.PaidItemName,
                    Amount = pi.Amount,
                    Note = pi.Note
                }).ToList(),
                TotalAmount = p.PaymentItems.Sum(pi => pi.Amount) // Tính tổng Amount
            };
        }

        public async Task<List<Payment>> GetPaymentsDueOnDateAsync(DateTime dueDate)
       {
            return await _dbContext.Payments
                .Include(p => p.User)
                .ThenInclude(u => u.User)
                .Include(p => p.TeamFund)
                .Where(p => p.DueDate.HasValue &&
                            p.DueDate.Value.Date == dueDate.Date &&
                            (p.Status != PaymentStatusConstant.PAID_CONFIRMED
                            || p.Status != PaymentStatusConstant.PAID_UNCONFIRMED)) 
                .ToListAsync();
        }

        public async Task<bool> AnyPaymentById(string paymentId)
		{
            return await _dbContext.Payments.AnyAsync(x=>x.PaymentId == paymentId);
		}

		public async Task UpdatePaymentAsync(Payment payment)
		{
			_dbContext.Payments.Update(payment);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<Payment?> GetPaymentByIdAsync(string paymentId)
		{
			return await _dbContext.Payments.FirstOrDefaultAsync(p => p.PaymentId == paymentId);
		}
    }
}
