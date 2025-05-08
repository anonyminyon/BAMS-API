using BasketballAcademyManagementSystemAPI.Application.DTOs.TeamFund;
using BasketballAcademyManagementSystemAPI.Models;
using BasketballAcademyManagementSystemAPI.Application.DTOs;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
	public interface ITeamFundRepository
	{
		Task<List<Team>> GetAllTeamsAsync();
		Task<bool> TeamFundExistsAsync(string teamId, DateOnly startDate, DateOnly endDate);
		Task AddTeamFundAsync(TeamFund teamFund);
		Task SaveChangesAsync();
		Task<Payment> GetPaymentByIdAsync(string paymentId);
		Task<bool> AnyPaymentById(string paymentId);
		Task<List<TeamFundListDto>> GetTeamFundsAsync(TeamFundFilterDto filter);
		Task<bool> UpdatePaymentStatusAsync(string paymentId, int? newStatus = null, string? newNote = null);
		Task<Manager> GetManagerPaymentByPaymentId(string paymentId);
		Task<TeamFund> GetTeamFundByIdAsync(string teamFundId);
		Task<List<Expenditure>> GetExpendituresByTeamFundIdAsync(string teamFundId);
		Task<List<Expenditure>> GetExpendituresByTeamFundAndDateRangeAsync(string teamFundId, DateOnly startDate, DateOnly endDate);
		Task<List<Expenditure>> GetExpendituresByTeamFundAndDateRangeStartWithOneAsync(string teamFundId, DateOnly startDate, DateOnly endDate);
		Task<List<UserTeamHistory>> GetUserTeamHistoriesByTeamIdAsync(string teamId, DateOnly startDate, DateOnly endDate);
		Task SaveUsersPaidForExpendituresAsync(Dictionary<string, List<string>> calculatePaymentList);
		Task<List<User>> GetPlayersByTeamIdAsync(string teamId);
		Task<string> GetManagerEmailByTeamFundId(string teamFundId);

		//phần này của hiếu
		Task AddExpendituresAsync(IEnumerable<Expenditure> expenditures);
		Task UpdateExpendituresAsync(IEnumerable<Expenditure> expenditures);
        Task<PagedResponseDto<Expenditure>> GetExpendituresAsync(string teamFundId, int pageNumber, int pageSize);
		Task<bool> DeleteExpenditureByIdAsync(string expenditureId);
		Task AddExpenditureCourtForTeamFund();
		Task<Expenditure?> GetExpenditureByIdAsync(string expenditureId);
		Task UpdateExpenditureAsync(Expenditure expenditure);


        //Test export expenditure
        Task AddPaymentAsync(Payment payment);
		Task AddRangeAsync(IEnumerable<PaymentItem> paymentItems);
		Task<List<Payment>> GetPaymentsByTeamFundIdAsync(string teamFundId);
		Task<string?> GetPaymentIdAsync(string userId, string teamFundId);
		Task<bool> DeletePaymentsByTeamFundIdAsync(string teamFundId);
		Task<bool> IsPayoutDateValidForAnyUserInTeamFundAsync(DateTime payoutDate, string teamFundId);
		Task AddPaymentItemsByPaymentsAsync(List<Payment> payments);
		Task<ICollection<PaymentItem>?> GetPaymentItemsByPaymentIdAsync(string paymentId);
		Task UpdatePaymentWithItemsAsync(Payment payment);
		Task CreatePaymentWithItemsAsync(Payment payment);
	}
}
