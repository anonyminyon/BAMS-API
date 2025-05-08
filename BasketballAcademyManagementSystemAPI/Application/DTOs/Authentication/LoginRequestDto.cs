using System.ComponentModel.DataAnnotations;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Authentication
{
    public class LoginRequestDto
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
