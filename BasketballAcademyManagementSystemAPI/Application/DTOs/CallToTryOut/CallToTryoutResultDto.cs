namespace BasketballAcademyManagementSystemAPI.Application.DTOs.CallToTryOut
{
    public class CallToTryoutResultDto
    {
        public int PlayerRegistId { get; set; }
        public string Status { get; set; } // Success / Failed
        public string Message { get; set; }
    }
}
