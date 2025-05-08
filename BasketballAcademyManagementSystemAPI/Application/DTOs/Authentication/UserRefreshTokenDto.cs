using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Authentication
{
    public partial class UserRefreshTokenDto
    {
        public int UserRefreshTokenId { get; set; }

        public string UserId { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;

        public DateTime ExpiresAt { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime? RevokedAt { get; set; }

    }
}
