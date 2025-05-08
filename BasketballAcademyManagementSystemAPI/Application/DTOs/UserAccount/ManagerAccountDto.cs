namespace BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount
{
    public class ManagerAccountDto
    {
        public string UserId { get; set; } = null!;

        public string TeamId { get; set; } = null!;

        public string TeamName { get; set; } = null!;

        public string BankName { get; set; } = null!;

        public string BankAccountNumber { get; set; } = null!;

        public int PaymentMethod { get; set; }

        public string BankBinId { get; set; } = null!;
    }
}
