using Azure.Core;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Authentication;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static BasketballAcademyManagementSystemAPI.Common.Messages.AuthenticationMessage;

namespace BasketballAcademyManagementSystemAPI.Common.Helpers
{
    public class TokenHelper
    {
        public static AuthTokensResponseDto GenerateJwtTokens(User user, IConfiguration _configuration)
        {
            // Tạo Access Token
            var accessTokenString = GenerateAccessTokenString(user, _configuration);

            // Tạo Refresh Token
            var refreshTokenString = GenerateRefreshTokenString();

            return new AuthTokensResponseDto { AccessToken = accessTokenString, RefreshToken = refreshTokenString };
        }

        public static string GenerateAccessTokenString(User user, IConfiguration _configuration)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            // Kiểm tra Key hợp lệ
            var keyString = jwtSettings["Key"];
            if (string.IsNullOrEmpty(keyString) || keyString.Length < 32)
            {
                throw new Exception("JWT Key is missing or too short (must be at least 32 characters).");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Kiểm tra Subject
            var subject = jwtSettings["Subject"];
            if (string.IsNullOrEmpty(subject))
            {
                throw new Exception("JwtSettings:Subject is missing in configuration.");
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim("Username", user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.UserId),
                new Claim(ClaimTypes.Role, user.RoleCode)
            };

            // Tạo Access Token
            var accessToken = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["AccessTokenExpiryMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        private static string GenerateRefreshTokenString()
        {
            var refreshTokenBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(refreshTokenBytes);
            }

            var refreshTokenString = Convert.ToBase64String(refreshTokenBytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .TrimEnd('=');

            return refreshTokenString;
        }

        public static void SetTokenToCookie(IHttpContextAccessor httpContextAccessor, string cookieName, string token, DateTime expiresTime)
        {
            httpContextAccessor.HttpContext?.Response.Cookies.Append(cookieName, token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = expiresTime
            });
        }

        public static string? GetTokenFromCookie(IHttpContextAccessor httpContextAccessor,string cookieName)
        {
            var context = httpContextAccessor.HttpContext;
            return context?.Request.Cookies[cookieName];
        }

        public static void RemoveTokenFromCookie(IHttpContextAccessor httpContextAccessor, string cookieName)
        {
            if (httpContextAccessor?.HttpContext?.Response?.Cookies == null)
            {
                return;
            }

            httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, "", new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });
        }

        public static string GenerateForgotPasswordToken(IConfiguration _configuration)
        {
            var forgotPasswordSettingsSection = _configuration.GetSection("ForgotPasswordSettings");
            int tokenLength = Int32.Parse(forgotPasswordSettingsSection["ForgotPasswordTokenLength"] ?? "64");
            string chars = forgotPasswordSettingsSection["GeneratingChars"] ?? "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder token = new StringBuilder();
            byte[] data = new byte[tokenLength];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(data);
            }

            foreach (var byteValue in data)
            {
                token.Append(chars[byteValue % chars.Length]);
            }

            return token.ToString();
        }

        public static string GetCurrentUserId(IHttpContextAccessor _httpContextAccessor)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity?.IsAuthenticated == true)
            {
                throw new UnauthorizedAccessException(AuthenticationErrorMessage.AuthenticatedRequired);
            }

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
             ?? user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception(AuthenticationErrorMessage.UserNotFound);
            }

            return userId;
        }

    }
}
