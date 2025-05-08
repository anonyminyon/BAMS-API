using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs.ManagerManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Mappings
{
    public class ManagerMappingProfile : Profile
    {
        public ManagerMappingProfile() {
            CreateMap<User, UserAccountDto<ManagerDto>>()
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.HasValue ? src.DateOfBirth.Value.ToString("dd-MM-yyyy") : null))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MM-yyyy")))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToString("dd-MM-yyyy") : null))
            .ForMember(dest => dest.RoleInformation, opt => opt.MapFrom(src => src.Manager));

        }
    }
}
