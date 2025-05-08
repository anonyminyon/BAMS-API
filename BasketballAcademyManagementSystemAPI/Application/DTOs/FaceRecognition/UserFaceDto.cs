namespace BasketballAcademyManagementSystemAPI.Application.DTOs.FaceRecognition
{
    public class UserFaceDto
    {
        public int UserFaceId { get; set; }

        public string UserId { get; set; } = null!;

        public string RegisteredFaceId { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public DateTime RegisteredAt { get; set; }
    }
}
