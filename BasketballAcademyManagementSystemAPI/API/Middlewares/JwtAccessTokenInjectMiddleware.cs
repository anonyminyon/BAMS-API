using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using static BasketballAcademyManagementSystemAPI.Common.Messages.AuthenticationMessage;

namespace BasketballAcademyManagementSystemAPI.API.Middlewares
{
    // Middleware dùng để lấy access token từ HTTP-Only cookie và gán vào header của request
    public class JwtAccessTokenInjectMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public JwtAccessTokenInjectMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (context.Request.Cookies.TryGetValue(JwtConstant.AccessTokenCookieName, out var accessToken))
                {
                    if (!context.Request.Headers.ContainsKey("Authorization"))
                    {
                        context.Request.Headers.Authorization = $"Bearer {accessToken}";
                    }

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
                        var userId = GetUserIdFromToken(accessToken);
                        if (!await authService.IsUserValidAsync(userId))
                        {
                            await WriteErrorResponse(context, StatusCodes.Status401Unauthorized, AuthenticationErrorMessage.AccountDisabled);
                            return;
                        }
                    }
                }

                await _next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                await WriteErrorResponse(context, StatusCodes.Status401Unauthorized, ex.Message);
            }
            catch (Exception)
            {
                await WriteErrorResponse(context, StatusCodes.Status500InternalServerError, ApiResponseMessage.ApiResponseErrorMessage.ApiFailedMessage);
            }
        }


        public static string GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value
                         ?? jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception(AuthenticationErrorMessage.UserNotFound);
            }

            return userId;
        }

        private void ClearInvalidAccessToken(HttpContext context)
        {
            // Xóa cookie refresh token không hợp lệ
            context.Response.Cookies.Delete(JwtConstant.AccessTokenCookieName, new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.None
            });
        }

        private async Task WriteErrorResponse(HttpContext context, int statusCode, string message)
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
