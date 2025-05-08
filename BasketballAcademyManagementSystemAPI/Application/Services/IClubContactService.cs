using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.ClubContact;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IClubContactService 
    {
        Task<ApiMessageModelV2<DetailClubContactDto>> EditClubContactAsync(UpdateClubContactDto updateClubContactDto);
        Task<IEnumerable<ClubContact>> GetClubContactMethodsAsync();
    }
}
