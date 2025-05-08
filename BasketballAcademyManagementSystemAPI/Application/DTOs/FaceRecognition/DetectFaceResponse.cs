namespace BasketballAcademyManagementSystemAPI.Application.DTOs.FaceRecognition
{
    public class DetectFaceResponse
    {
        public List<DetectedFaceInformation> DetectedFaces { get; set; } = new List<DetectedFaceInformation>();
        public string OriginalImageUrl { get; set; } = string.Empty;
        public string ProcessedImageUrl { get; set; } = string.Empty;
    }

    public class DetectedFaceInformation
    {
        public string Username { get; set; }
        public string UserId { get; set; }
        public string FaceId { get; set; }
        public double Confidence { get; set; }
        public FaceBoundingBox BoundingBox { get; set; } = new FaceBoundingBox();
    }
}
