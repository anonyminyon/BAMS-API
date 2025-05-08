//using System.Text.Json.Serialization;

//namespace BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration
//{
//    public class ManagerRegistrationResponse
//    {
//        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
//        public int? ManagerRegistrationId { get; set; } = null!; // ID của bản ghi nếu đăng ký thành công
//        public bool IsSuccess { get; set; } // Cho biết đăng ký thành công hay thất bại
//        public string Message { get; set; } = string.Empty; // Lưu thông báo lỗi hoặc thành công
//        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
//        public object? Data { get; set; }     
//    }
//}
