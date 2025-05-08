namespace BasketballAcademyManagementSystemAPI.Application.DTOs.Authentication
{
    public class SetNewPasswordRequestDto
    {
        public string ForgotPasswordToken { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
