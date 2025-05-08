namespace BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount
{
    public class UserAccountDto<T> where T : class?
    {
        public string UserId { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Fullname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? ProfileImage { get; set; }

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string DateOfBirth { get; set; }

        public string RoleCode { get; set; } = null!;

        public string CreatedAt { get; set; }

        public string? UpdatedAt { get; set; }

        public bool IsEnable { get; set; }

        public T? RoleInformation { get; set; } 
    }
}
