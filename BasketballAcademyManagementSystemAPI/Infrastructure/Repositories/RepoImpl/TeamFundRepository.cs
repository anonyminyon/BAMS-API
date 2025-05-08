using BasketballAcademyManagementSystemAPI.Application.DTOs.TeamFund;
using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.EntityFrameworkCore;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using log4net;
using BasketballAcademyManagementSystemAPI.Application.Services;


namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl
{
	public class TeamFundRepository : ITeamFundRepository
	{
		private readonly BamsDbContext _context;
        private static readonly ILog _logger = LogManager.GetLogger(LogConstant.LogName.TeamFundManagementFeature);

        public TeamFundRepository(BamsDbContext context)
		{
			_context = context;
		}
		public Task<List<Team>> GetAllTeamsAsync()
		{
			return _context.Teams.ToListAsync();
		}

		public Task<bool> TeamFundExistsAsync(string teamId, DateOnly startDate, DateOnly endDate)
		{
			return _context.TeamFunds.AnyAsync(tf =>
				tf.TeamId == teamId &&
				tf.StartDate == startDate &&
				tf.EndDate == endDate);
		}

		public async Task AddTeamFundAsync(TeamFund teamFund)
		{
			await _context.TeamFunds.AddAsync(teamFund);
		}

		public Task SaveChangesAsync()
		{
			return _context.SaveChangesAsync();
		}
		public async Task<List<TeamFundListDto>> GetTeamFundsAsync(TeamFundFilterDto filter)
		{
			var query = _context.TeamFunds
				.Include(tf => tf.Team).ThenInclude(x => x.FundManager)
				.Include(tf => tf.Team.Managers) // Ea>ger load the Manager data
				.AsQueryable();

			// Lọc theo TeamFundId
			if (!string.IsNullOrEmpty(filter.TeamFundId))
				query = query.Where(tf => tf.TeamFundId == filter.TeamFundId);

			// Lọc theo TeamId
			if (!string.IsNullOrEmpty(filter.TeamId))
				query = query.Where(tf => tf.TeamId == filter.TeamId);

			// Lọc theo Status
			if (filter.Status.HasValue)
				query = query.Where(tf => tf.Status == filter.Status.Value);

			// Lọc theo StartDate (>=)
			if (filter.StartDate.HasValue)
				query = query.Where(tf => tf.StartDate >= filter.StartDate.Value);

			// Lọc theo EndDate (<=)
			if (filter.EndDate.HasValue)
				query = query.Where(tf => tf.EndDate <= filter.EndDate.Value);

			// Lọc theo Description
			if (!string.IsNullOrEmpty(filter.Description))
				query = query.Where(tf => tf.Description != null && tf.Description.Contains(filter.Description));

			return await query
				.OrderByDescending(tf => tf.StartDate)
				.Select(tf => new TeamFundListDto
				{
					TeamFundId = tf.TeamFundId,
					TeamId = tf.TeamId,
					TeamName = tf.Team.TeamName,
					StartDate = tf.StartDate,
					EndDate = tf.EndDate,
					Status = tf.Status,
					TotalExpenditure = _context.Expenditures
						.Where(e => e.TeamFundId == tf.TeamFundId)
						.Sum(e => e.Amount).ToString(),  // Tính tổng chi phí theo TeamFundId
					Description = tf.Description,
					ApproveAt = tf.ApprovedAt,
					ManagerId = tf.Team.FundManagerId,  // Manager's ID
					ManagerName = tf.Team.FundManager.Fullname // Manager's Name
				})
				.ToListAsync();
		}

		public async Task<string> GetManagerEmailByTeamFundId(string teamFundId)
		{
			return await _context.TeamFunds
				.Where(tf => tf.TeamFundId == teamFundId)
				.Select(tf => tf.Team.FundManager.Email)
				.FirstOrDefaultAsync();  // Returns the email or null if not found
		}


		public async Task<Payment> GetPaymentByIdAsync(string paymentId)
		{
			return await _context.Payments
				.Include(x => x.PaymentItems)
				.FirstOrDefaultAsync(x => x.PaymentId == paymentId);
		}

		//kiểm tra tồn tại payment hay ko dựa vào paymentId
		public async Task<bool> UpdatePaymentStatusAsync(string paymentId, int? newStatus = null, string? newNote = null)
		{
			var payment = await _context.Payments.FirstOrDefaultAsync(p => p.PaymentId == paymentId);

			if (payment == null)
				return false;

			// Chỉ update nếu có giá trị truyền vào
			if (newStatus.HasValue)
			{
				payment.Status = newStatus.Value;
				payment.PaidDate = DateTime.Now;
				payment.PaymentMethod = 1;
			}

			if (!string.IsNullOrEmpty(newNote))
			{
				payment.Note = newNote;
			}

			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> AnyPaymentById(string paymentId)
		{
			return await _context.Payments
				.AnyAsync(x => x.PaymentId == paymentId);
		}

		public async Task<TeamFund> GetTeamFundByIdAsync(string teamFundId)
		{
			return await _context.TeamFunds.FirstOrDefaultAsync(tf => tf.TeamFundId == teamFundId);
		}

		public async Task<List<Expenditure>> GetExpendituresByTeamFundIdAsync(string teamFundId)
		{
			return await _context.Expenditures.Where(e => e.TeamFundId == teamFundId).ToListAsync();
		}

	
		public async Task<List<User>> GetPlayersByTeamIdAsync(string teamId)
		{
			return await _context.Users
				.Include(x => x.Player)
				.Where(p => p.Player.TeamId == teamId).ToListAsync();
		}

        //============================================phần này của hiếu============================================
        public async Task AddExpendituresAsync(IEnumerable<Expenditure> expenditures)
		{
			await _context.Expenditures.AddRangeAsync(expenditures);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateExpendituresAsync(IEnumerable<Expenditure> expenditures)
		{
			foreach (var expenditure in expenditures)
			{
				var entity = await _context.Expenditures.FindAsync(expenditure.ExpenditureId);
				if (entity != null)
				{
					//entity.TeamFundId = expenditure.TeamFundId;
					entity.Name = expenditure.Name;
					entity.ByManagerId = expenditure.ByManagerId;
					entity.Amount = expenditure.Amount;
					entity.Date = expenditure.Date;
					entity.PayoutDate = expenditure.PayoutDate?.Date.AddDays(1).AddSeconds(-1);
                }
            }

			await _context.SaveChangesAsync();
		}

        public async Task<PagedResponseDto<Expenditure>> GetExpendituresAsync(string teamFundId, int pageNumber, int pageSize)
        {
            var query = _context.Expenditures
                .Where(e => e.TeamFundId == teamFundId);

            var totalRecords = await query.CountAsync();

            var expenditures = await query
                .OrderBy(e => e.Date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponseDto<Expenditure>
            {
                Items = expenditures,
                TotalRecords = totalRecords,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }


        public async Task<bool> DeleteExpenditureByIdAsync(string expenditureId)
        {
            var expenditure = await _context.Expenditures.FindAsync(expenditureId);
            if (expenditure == null)
            {
                return false; // Expenditure not found
            }

            _context.Expenditures.Remove(expenditure);
            await _context.SaveChangesAsync();
            return true; // Successfully deleted
        }

        public async Task AddExpenditureCourtForTeamFund()
        {
            // Get all active team funds
            var teamFunds = await _context.TeamFunds
                //.Where(tf => tf.Status == TeamFundStatusConstant.APPROVED) // Assuming you have an enum
                .Include(tf => tf.Team)
                .ToListAsync();

            foreach (var teamFund in teamFunds)
            {
                await ProcessCourtFeesForTeamFund(teamFund);
            }
        }

        private async Task ProcessCourtFeesForTeamFund(TeamFund teamFund)
        {
			Console.WriteLine($"\nProcessing court fees for team fund: {teamFund.TeamFundId}\n");
            // Get all training sessions for this team during the fund period
            var sessions = await _context.TrainingSessions
                .Where(ts => ts.TeamId == teamFund.TeamId &&
                            ts.ScheduledDate >= teamFund.StartDate &&
                            ts.ScheduledDate <= teamFund.EndDate &&
                            ts.Status == 1) // Only completed sessions
				.Include(ts => ts.Court)
                .ToListAsync();

            foreach (var session in sessions)
            {
                // no court price
                if (!session.CourtPrice.HasValue)
                {
					Console.WriteLine($"\nSession {session.TrainingSessionId} has court price.\n");
                    continue;
                }

                // Check if expenditure already exists for this session
                var existingExpenditure = await _context.Expenditures
                    .FirstOrDefaultAsync(e => e.Name.Contains($"Tiền thuê sân {session.Court.CourtName} cho buổi tập từ lúc {session.StartTime}-{session.EndTime}"));

                if (existingExpenditure != null)
                {
                    Console.WriteLine($"Khoản chi này đã tồn tại ở session {session.TrainingSessionId} so continue");
                    continue;
                }

                // Get a manager associated with the team (could be the session creator)
                var manager = await _context.Managers
                    .FirstOrDefaultAsync(m => m.TeamId == teamFund.TeamId)
                    ?? session.CreatedDecisionByManager; // Fallback to session approver

                if (manager == null)
                {
                    _logger.Warn($"No manager found for team {teamFund.TeamId} to create expenditure");
                    continue;
                }

                // Create new expenditure
                var expenditure = new Expenditure
                {
                    ExpenditureId = Guid.NewGuid().ToString(),
                    TeamFundId = teamFund.TeamFundId,
                    Name = $"Tiền thuê sân {session.Court.CourtName} cho buổi tập từ lúc {session.StartTime}-{session.EndTime}",
                    Amount = session.CourtPrice.Value,
                    Date = session.ScheduledDate,
                    ByManagerId = manager.UserId,
                    PayoutDate = session.ScheduledDate.ToDateTime(TimeOnly.MinValue),
                    UsedByUserId = "1()"
                };

                _context.Expenditures.Add(expenditure);
            }

            await _context.SaveChangesAsync();
        }

        //  Lấy 1 expenditure theo ID
        public async Task<Expenditure?> GetExpenditureByIdAsync(string expenditureId)
        {
            return await _context.Expenditures
                .FirstOrDefaultAsync(e => e.ExpenditureId == expenditureId);
        }

        //  Cập nhật expenditure
        public async Task UpdateExpenditureAsync(Expenditure expenditure)
        {
            _context.Expenditures.Update(expenditure);
            await _context.SaveChangesAsync();
        }
        //============================================================================================================
        public async Task<Manager> GetManagerPaymentByPaymentId(string paymentId)
		{
			var paymentInfo = await _context.Payments
				.Where(p => p.PaymentId == paymentId)
				.Select(p => new
				{
					FundManagerId = p.TeamFund.Team.FundManagerId ?? string.Empty,
					TeamId = p.TeamFund.Team.TeamId ?? string.Empty
				})
				.FirstOrDefaultAsync();

			if (paymentInfo == null)
				return null;

			var manager = await _context.Managers
				.Where(m => m.UserId == paymentInfo.FundManagerId && m.TeamId == paymentInfo.TeamId)
				.FirstOrDefaultAsync();

			return manager;
		}

		// Lấy ra expenditure trong 1 tháng dựa vào start date, end date của team fund
		public async Task<List<Expenditure>> GetExpendituresByTeamFundAndDateRangeAsync(string teamFundId, DateOnly startDate, DateOnly endDate)
		{
			return await _context.Expenditures
		.Where(e => e.TeamFundId == teamFundId &&
					e.Date >= startDate &&
					e.Date <= endDate &&
					e.UsedByUserId != null) // bắt đầu bằng "0(" và kết thúc bằng ")"
		.ToListAsync();
		}

		// Lấy ra expenditure trong 1 tháng dựa vào start date, end date của team fund bắt đầu bằng số 1( để có thể lọc theo userTeamHistory
		public async Task<List<Expenditure>> GetExpendituresByTeamFundAndDateRangeStartWithOneAsync(string teamFundId, DateOnly startDate, DateOnly endDate)
		{
			return await _context.Expenditures
		.Where(e => e.TeamFundId == teamFundId &&
					e.Date >= startDate &&
					e.Date <= endDate &&
					e.UsedByUserId != null &&
					e.UsedByUserId.StartsWith("1(") && e.UsedByUserId.EndsWith(")")) // bắt đầu bằng "0(" và kết thúc bằng ")"


		.ToListAsync();
		}


		public async Task<List<UserTeamHistory>> GetUserTeamHistoriesByTeamIdAsync(string teamId, DateOnly startDate, DateOnly endDate)
		{
			return await _context.UserTeamHistories
		.Where(uth => uth.TeamId == teamId &&
					 DateOnly.FromDateTime(uth.JoinDate) <= endDate &&
					 (uth.LeftDate == null || DateOnly.FromDateTime(uth.LeftDate.Value) >= startDate))
		.OrderByDescending(uth => uth.JoinDate)
		.ToListAsync();
		}

		public async Task SaveUsersPaidForExpendituresAsync(Dictionary<string, List<string>> calculatePaymentList)
		{
			var expenditureIds = calculatePaymentList.Keys.ToList();

			// Lấy toàn bộ expenditure cần update chỉ trong 1 truy vấn
			var expenditures = await _context.Expenditures
				.Where(e => expenditureIds.Contains(e.ExpenditureId))
				.ToListAsync();

			foreach (var expenditure in expenditures)
			{
				if (calculatePaymentList.TryGetValue(expenditure.ExpenditureId, out var userIds))
				{
					// Gộp các userId thành chuỗi phân tách bởi dấu ","
					expenditure.UsedByUserId = "1("+ string.Join(",", userIds.Distinct())+")";
				}
			}

			await _context.SaveChangesAsync();
		}

		public async Task AddPaymentAsync(Payment payment)
		{
			await _context.Payments.AddAsync(payment);
		}

		public async Task AddRangeAsync(IEnumerable<PaymentItem> paymentItems)
		{
			await _context.PaymentItems.AddRangeAsync(paymentItems);
		}

		public async Task<List<Payment>> GetPaymentsByTeamFundIdAsync(string teamFundId)
		{
			return await _context.Payments
				.Include(p => p.PaymentItems) // nếu bạn muốn lấy cả PaymentItems
				.Where(p => p.TeamFundId == teamFundId)
				.ToListAsync();
		}

		public async Task<string?> GetPaymentIdAsync(string userId, string teamFundId)
		{
			var payment = await _context.Payments
				.FirstOrDefaultAsync(p => p.UserId == userId && p.TeamFundId == teamFundId);

			return payment?.PaymentId;
		}

		public async Task AddPaymentItemsByPaymentsAsync(List<Payment> payments)
		{
			var allItems = payments.SelectMany(p => p.PaymentItems).ToList();
			await _context.PaymentItems.AddRangeAsync(allItems);
		}

		public async Task<ICollection<PaymentItem>?> GetPaymentItemsByPaymentIdAsync(string paymentId)
		{
			if (string.IsNullOrEmpty(paymentId))
				throw new ArgumentNullException(nameof(paymentId));

			return await _context.PaymentItems
				.Where(pi => pi.PaymentId == paymentId)
				.ToListAsync();
		}

		public async Task UpdatePaymentWithItemsAsync(Payment updated)
		{
			var existing = await _context.Payments
			.Include(p => p.PaymentItems)
					.FirstOrDefaultAsync(p => p.PaymentId == updated.PaymentId);

			if (existing == null) throw new Exception("Payment not found");

			foreach (var item in updated.PaymentItems)
			{
				bool exists = existing.PaymentItems.Any(pi =>
					pi.PaidItemName == item.PaidItemName &&
					 decimal.Abs(pi.Amount - item.Amount) < 0.01m);

				if (!exists)
				{
					item.PaymentId = existing.PaymentId;
					existing.PaymentItems.Add(item);
				}
			}

			await _context.SaveChangesAsync();
		}

		public async Task CreatePaymentWithItemsAsync(Payment payment)
		{
			_context.Payments.Add(payment); // EF sẽ tự thêm các PaymentItems
			await _context.SaveChangesAsync();
		}

		public async Task<bool> IsPayoutDateValidForAnyUserInTeamFundAsync(DateTime payoutDate, string teamFundId)
		{
			// Lấy TeamId từ teamFundId
			var teamFund = await _context.TeamFunds
				.Where(tf => tf.TeamFundId == teamFundId)
				.Select(tf => tf.TeamId)
				.FirstOrDefaultAsync();

			if (string.IsNullOrEmpty(teamFund))
				return false;

			// Kiểm tra có User nào trong Team đó còn hoạt động tại thời điểm payoutDate
			return await _context.UserTeamHistories
				.AnyAsync(h =>
					h.TeamId == teamFund &&
					h.JoinDate <= payoutDate &&
					(h.LeftDate == null || payoutDate <= h.LeftDate)
				);
		}

		//xóa tất cả payment và paymentItems theo payment bằng teamfundId
		public async Task<bool> DeletePaymentsByTeamFundIdAsync(string teamFundId)
		{
			var payments = await _context.Payments
				.Where(p => p.TeamFundId == teamFundId)
				.ToListAsync();

			if (payments == null || !payments.Any())
				return false;

			var paymentIds = payments.Select(p => p.PaymentId).ToList();

			var paymentItems = await _context.PaymentItems
				.Where(pi => paymentIds.Contains(pi.PaymentId))
				.ToListAsync();

			_context.PaymentItems.RemoveRange(paymentItems);
			_context.Payments.RemoveRange(payments);

			await _context.SaveChangesAsync();
			return true;
		}

	}
}
