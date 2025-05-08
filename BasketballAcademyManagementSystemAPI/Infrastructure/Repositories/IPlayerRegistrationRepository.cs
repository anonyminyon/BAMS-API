using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration;
using BasketballAcademyManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BasketballAcademyManagementSystemAPI.Infrastructure.Repositories
{
	public interface IPlayerRegistrationRepository
	{
		
		//Lưu player register vào db
		Task<PlayerRegistration> RegisterPlayerAsync(PlayerRegistration player);
		Task UpdatePlayerRegistrationAsync(PlayerRegistration registration);

		//Danh sách Đơn đăng kí
		Task<List<PlayerSubmittedFormDto>> GetPlayers(
		int? memberRegistrationSessionId,
		string? email,
        DateTime? startDate,
		DateTime? endDate,
		int? minAge,
		int? maxAge,
		bool? gender,
		string? status);
		Task<PagedResponseDto<PlayerSubmittedFormDto>> GetPlayersOldForm(
		int? memberRegistrationSessionId,
		string? email,
		int pageNumber = 1,
		int pageSize = 10);
		Task<PlayerRegistration> GetPlayerRegisterFormByEmailAsync(string email);
		//Approved Player Registration
		Task<PlayerRegistration> GetRegistrationByIdAsync(int id);
		Task<User> GetUserByEmailAsync(string email);
		Task<User> GetParentByEmailAsync(string email);
		Task AddUserAsync(User user);
		Task AddParentAsync(Parent parent);
		Task AddPlayerAsync(Player player);
		void Update(PlayerRegistration registration);
		Task<bool> DeleteByEmailAndSessionIdAsync(int sessionId,string email);
		Task SaveAsync();

		//Try out note
		Task<bool> AddTryOutNote(string playerRegisterId, string tryOutNote);

		//Call To TryOut
		Task<int> GetMaxCandidateNumber(int memberRegistrationSessionId);

		Task<PlayerRegistration?> GetPlayerRegistrationByIdAsync(int playerRegistrationId);
        Task<PlayerRegistration?> GetPlayerRegistrationAsync(int sessionId);

        Task DeleteRangeAsync(ICollection<PlayerRegistration> playerRegistrations);

        //Kiểm tra xem email có tồn tại trong bảng user hay chưa
        Task<bool> IsEmailExistsInUser(string email);

        //Kiểm tra xem email có tồn tại trong bảng player registration hay ko 
        Task<bool> IsEmailExistsInPlayerRegistration(string email, string memberSessionId);
		//Kiểm tra xem email có tồn tại trong bảng player registration hay ko 
		Task<bool> IsEmailExistsInManagerRegistration(string email, string memberRegistrationSessionId);
		Task<bool> IsExistedParentByCitizen(string citizenId);

		Task<bool> IsEmailExistsAndPendingAsync( string? email);
		Task<bool> IsExistedManagerById(string userId);
		Task<bool> UpdateRegistrationStatusAsync(int registrationId, string newStatus, string? reviewerId = null);
	}
}
