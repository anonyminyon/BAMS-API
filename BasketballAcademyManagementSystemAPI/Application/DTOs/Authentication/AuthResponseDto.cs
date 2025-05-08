using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Authentication
{
    public class AuthResponseDto
    {
        public string Message { get; set; } = null!;

        [JsonPropertyName("user")]
        public UserDataAuthResponse UserDataAuthResponse { get; set; } = null!;
    }

    public class UserDataAuthResponse
    {
        public string UserId { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string RoleCode { get; set; } = null!;
    }
}
