using BasketballAcademyManagementSystemAPI.Application.DTOs.ClubContact;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using BasketballAcademyManagementSystemAPI.Common.Constants;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class ClubContactService : IClubContactService
    {
        private readonly IClubContactRepository _clubContactRepository;
        public ClubContactService(IClubContactRepository clubContactRepository)
        {
            _clubContactRepository = clubContactRepository;
        }

        public async Task<IEnumerable<ClubContact>> GetClubContactMethodsAsync()
        {
            return await _clubContactRepository.GetClubContactMethodsAsync();
        }

        public async Task<ApiMessageModelV2<DetailClubContactDto>> EditClubContactAsync(UpdateClubContactDto updateClubContactDto)
        {
            var clubContact = await _clubContactRepository.GetClubContactByIdAsync(updateClubContactDto.ContactMethodId);
            if (clubContact == null)
            {
                return new ApiMessageModelV2<DetailClubContactDto>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ClubContactMessage.Error.ClubContactNotFound
                };
            }

            clubContact.ContactMethodName = updateClubContactDto.ContactMethodName;
            clubContact.MethodValue = updateClubContactDto.MethodValue;
            clubContact.IconUrl = updateClubContactDto.IconUrl;
            clubContact.UpdatedAt = DateTime.UtcNow;

            await _clubContactRepository.UpdateClubContactAsync(clubContact);

            var detailClubContactDto = new DetailClubContactDto
            {
                ContactMethodId = clubContact.ContactMethodId,
                ContactMethodName = clubContact.ContactMethodName,
                MethodValue = clubContact.MethodValue,
                IconUrl = clubContact.IconUrl,
                CreatedAt = clubContact.CreatedAt,
                UpdatedAt = clubContact.UpdatedAt
            };

            return new ApiMessageModelV2<DetailClubContactDto>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Message = ClubContactMessage.Success.ClubContactUpdated,
                Data = detailClubContactDto
            };
        }
    }
}
