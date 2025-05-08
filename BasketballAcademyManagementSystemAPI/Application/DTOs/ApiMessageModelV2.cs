using BasketballAcademyManagementSystemAPI.Common.Constants;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs
{
    public class ApiMessageModelV2<T>
    {
        public string Status { get; set; } = ApiResponseStatusConstant.FailedStatus;
        public string? Message { get; set; }
        public T? Data { get; set; }
        public Dictionary<string, string>? Errors { get; set; }
    }
}
