namespace BasketballAcademyManagementSystemAPI.Application.DTOs.ManagerManagement
{
    public class ManagerFilterDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public bool? IsEnable { get; set; } // Trạng thái Active/Inactive thay vì int Status
        public string? TeamId { get; set; } // Lọc theo Team nếu cần
        public string? SortBy { get; set; } = "Fullname"; // Sắp xếp mặc định theo Fullname
        public bool IsDescending { get; set; } = false; // True: Giảm dần, False: Tăng dần
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
