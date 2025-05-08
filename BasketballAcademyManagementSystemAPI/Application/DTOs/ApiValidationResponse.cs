using BasketballAcademyManagementSystemAPI.Common.Constants;

namespace BasketballAcademyManagementSystemAPI.Application.DTOs
{
	public class ApiValidationResponse<T>
	{
		public string Status { get; set; } = ApiResponseStatusConstant.FailedStatus;
		public string? Message { get; set; }
		public T? Data { get; set; }
		public List<ValidationError>? Errors { get; set; }
	}

	public class ValidationError
	{
		public string FieldName { get; set; }
		public string ErrorMessage { get; set; }
	}
}
