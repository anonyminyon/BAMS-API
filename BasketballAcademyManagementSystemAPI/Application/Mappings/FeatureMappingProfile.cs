using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs.ManagerManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Team;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Mappings
{
    public class FeatureMappingProfile : Profile
    {
        // Replace Feature as you want
        public FeatureMappingProfile()
        {
            // Mapping giữa Manager và ManagerDto
            CreateMap<Manager, DTOs.ManagerManagement.ManagerDto>().ReverseMap();

            //Map PlayerRegistration
            CreateMap<PlayerRegistrationDto, PlayerRegistration>();
           

            //Map ManagerRegistration
            CreateMap<ManagerRegistrationDto, ManagerRegistration>()
            .ForMember(dest => dest.MemberRegistrationSession, opt => opt.Ignore());//ignore MemberRegistration
        }
    }
}
