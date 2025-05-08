using static BasketballAcademyManagementSystemAPI.Common.Messages.AuthenticationMessage;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace BasketballAcademyManagementSystemAPI.Common.Helpers
{
	public class GetLoginUserHelper
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		public GetLoginUserHelper(IHttpContextAccessor httpContextAccessor) { 
			_httpContextAccessor = httpContextAccessor;
		}

		public string GetUserIdLoggedIn()
		{
			var user = _httpContextAccessor.HttpContext?.User;

			// Kiểm tra nếu người dùng chưa được xác thực
			if (user == null || !user.Identity?.IsAuthenticated == true)
			{
				throw new UnauthorizedAccessException("Authentication is required");
			}

			// Kiểm tra userId có phải null hay không
			var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
						 ?? user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

			if (string.IsNullOrEmpty(userId))
			{
				throw new Exception("UserId not found");
			}

			return userId;
		}
	}
}
