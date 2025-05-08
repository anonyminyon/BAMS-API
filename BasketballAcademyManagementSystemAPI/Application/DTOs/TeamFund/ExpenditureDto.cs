namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TeamFund
{
    public class ExpenditureDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PayoutDate { get; set; } 
        public DateTime Date { get; set; }
        public string TeamFundId { get; set; }
        public string ByManagerId { get; set;}
        public string? UsedByUserId { get; set; } // Danh sách UserId của người dùng đã sử dụng khoản chi tiêu này
        public List<PlayerExpenditureDto>? PlayerExpenditures { get; set; } = null;// Thông tin cầu thủ nếu khoản chi tiêu này là cho cầu thủ
        public bool? AllowToEditPlayer { get; set; } = null; // Cho phép chỉnh sửa cầu thủ hay không
    }

    public class PlayerExpenditureDto
    {
        public string UserId { get; set; }
        public string Fullname { get; set; }
    }
}
