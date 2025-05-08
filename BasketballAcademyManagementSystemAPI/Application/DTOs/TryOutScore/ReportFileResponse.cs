namespace BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore
{
    public class ReportFileResponse
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
    }
}
