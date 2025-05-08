using System.Text.Json.Serialization;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs.ManagerManagement
{
    public class ManagerResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ManagerDto? Manager { get; set; } = null!; 
        public bool IsSuccess { get; set; } // Cho biết đăng ký thành công hay thất bại
        public string Message { get; set; } = string.Empty; // Lưu thông báo lỗi hoặc thành công
    }
}
