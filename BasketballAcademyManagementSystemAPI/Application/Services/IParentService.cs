using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Parent;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Team;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
	public interface IParentService
	{
		Task<ApiMessageModelV2<object>> AddParentForPlayerAsync(string playerId, string parentId);
		Task<ApiMessageModelV2<object>> GetParentDetailsAsync(string userId);
		Task<ApiMessageModelV2<List<PlayerDto>>> GetPlayersByParentIdAsync(string parentId);
		Task<ApiMessageModelV2<object>> CreateParentAccountAsync(CreateParentRequestDto dto);
		Task<ApiMessageModelV2<List<ParentDto>>> FilterParentsAsync(ParentFilterDto filter);
	}
}
