using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TeamFund
{
    public class CreateExpenditureDto
    {
        public string Name { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime? PayoutDate { get; set; } // Thêm thuộc tính mới
    }
    public class CreateExpenditureHasListUserIdDto
    {
        public string Name { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime? PayoutDate { get; set; } // Thêm thuộc tính mới
        public List<string>? UserIds { get; set; }
        public override string ToString()
        {
            var payout = PayoutDate?.ToString("yyyy-MM-dd") ?? "null";
            var users = UserIds != null && UserIds.Any()
                ? string.Join(", ", UserIds)
                : "[]";

            return $"Name: {Name}, Amount: {Amount}, PayoutDate: {payout}, UserIds: [{users}]";
        }
    }
}
