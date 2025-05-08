using BasketballAcademyManagementSystemAPI.Common.Constants;
using static BasketballAcademyManagementSystemAPI.Common.Messages.ApiResponseMessage;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs
{
    public class ApiResponseModel<T>
    {
        public string Status { get; set; } = ApiResponseStatusConstant.FailedStatus;
        public string? Message { get; set; } = ApiResponseErrorMessage.ApiFailedMessage;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
    }
}
