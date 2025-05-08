namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Authentication
{
    public class AuthTokensResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
