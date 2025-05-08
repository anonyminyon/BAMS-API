namespace BasketballAcademyManagementSystemAPI.Application.DTOs.FaceRecognition
{
    public class UserRegisteredFacesDto
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public List<UserFaceDto> UserFaces { get; set; } = new List<UserFaceDto>();
    }
}
