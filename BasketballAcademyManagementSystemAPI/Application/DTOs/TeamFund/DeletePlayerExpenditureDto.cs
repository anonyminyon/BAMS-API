namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TeamFund
{
    public class DeletePlayerExpenditureDto
    {
        public string? ExpenditureId { get; set; }
        public string? UserId { get; set; }
    }
    public class AddPlayerExpenditureDto
    {
        public string? ExpenditureId { get; set; }
        public List<string> UserIds { get; set; } = new();
    }

}
