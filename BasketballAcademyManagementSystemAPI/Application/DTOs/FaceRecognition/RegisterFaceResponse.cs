namespace BasketballAcademyManagementSystemAPI.Application.DTOs.FaceRecognition
{
    public class RegisterFaceResponse
    {
        public string UserId { get; set; }
        public List<UserFaceDto> RegisteredFaces { get; set; } = new List<UserFaceDto>();
    }
}
