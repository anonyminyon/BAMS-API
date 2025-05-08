using BasketballAcademyManagementSystemAPI.API.Controllers;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.CallToTryOut;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IPlayerRegistrationService
	{
		//đăng kí mới đơn
		Task<ApiMessageModelV2<object>> RegisterPlayerAsync(PlayerRegistrationDto playerDto);
		//Trong trường hợp tồn tại email ở bảng player register thì update
		Task<ApiMessageModelV2<object>> UpdatePlayerRegisterFormAsync(PlayerRegistrationDto playerDto);
		//Hàm filter
		Task<ApiMessageModelV2<object>> GetPlayers(
			int? memberRegistrationSessionId,
			string? email,
            DateTime? startDate,
			DateTime? endDate,
			int? minAge,
			int? maxAge,
			bool? gender,
			string? status);

		//Approved player registration
		Task<ApiMessageModelV2<object>> ApproveRegistrationAsync(int registrationId);

		//Reject player registration 
		Task<ApiMessageModelV2<object>> RejectRegistrationAsync(int registrationId);

		//Call to Try out
		Task<ApiMessageModelV2<object>> CallToTryoutList(
		List<int> playerRegistIds,
		string? location,
		DateTime? tryOutDateTime);

		//Add try out note
		Task<bool> AddTryOutNote(string playerRegisterId, string tryOutNote);

		//Check input mail when send otp 
		Task<ApiMessageModelV2<object>> ValidateEmailRegistrationAsync(string email, string memberSessionId);
		Task<ApiMessageModelV2<object>> UpdateStatusForm(int registrationId, string status);
	}
}
