namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TeamFund
{
    public class UpdateExpenditureDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime? PayoutDate { get; set; } 
    }
    public class UpdateExpenditureHasListUserIdDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime? PayoutDate { get; set; }
        public List<string>? UserIds { get; set; }

    }
}
