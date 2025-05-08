using System.IO;
using System.Net;
using System.Text.Json;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using Microsoft.AspNetCore.Authorization;

namespace BasketballAcademyManagementSystemAPI.API.Middlewares
{
    public class JwtAutoTokenRefreshMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JwtAutoTokenRefreshMiddleware> _logger;

        public JwtAutoTokenRefreshMiddleware(
            RequestDelegate next,
            ILogger<JwtAutoTokenRefreshMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IAuthService authService)
        {
            try
            {
                var accessToken = context.Request.Headers["Authorization"]
                    .ToString()
                    .Replace("Bearer ", "");

                var refreshToken = context.Request.Cookies[JwtConstant.RefreshTokenCookieName];

                // Chỉ xử lý khi không có access token nhưng có refresh token
                if (string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(refreshToken))
                {
                    var newTokens = await authService.AutoRefreshTokensAsync();

                    if (newTokens != null)
                    {
                        // Cập nhật access token mới vào header
                        context.Request.Headers["Authorization"] = $"Bearer {newTokens.AccessToken}";
                    }
                    else
                    {
                        // Refresh thất bại: Xóa cookie và dừng request
                        await WriteErrorResponse(context, 401, "Invalid refresh token");
                        return;
                    }
                }

                await _next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Auth failure during token refresh");
                await WriteErrorResponse(context, 401, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Critical error during token refresh");
                await WriteErrorResponse(context, 500, "Internal server error");
            }
        }

        private async Task WriteErrorResponse(
            HttpContext context,
            int statusCode,
            string message)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                var errorResponse = new ApiResponseModel<string>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = message,
                    Errors = new List<string> { message }
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
        }
    }
}
