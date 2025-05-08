namespace BasketballAcademyManagementSystemAPI.Application.DTOs.FaceRecognition
{
    public class RegisterFacesRequest
    {
        public string UserId { get; set; }
        public IFormFile Image { get; set; }
    }
}
