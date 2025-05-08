using System.Text.Json.Serialization;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs.ManagerManagement
{
    public class ManagerDto
    {
        public string UserId { get; set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TeamId { get; set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BankName { get; set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BankAccountNumber { get; set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? PaymentMethod { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BankBinId { get; set; }
    }
}
