using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Mappings
{
    public class ManagerRegistrationProfile : Profile
    {
        public ManagerRegistrationProfile()
        {
            // Map từ Model -> DTO
            CreateMap<ManagerRegistration, ManagerRegistrationDto>()
                .ForMember(dest => dest.SubmitedDate, opt => opt.MapFrom(src => src.SubmitedDate.ToString("yyyy-MM-dd HH:mm:ss")));

            // Map từ DTO -> Model
            CreateMap<ManagerRegistrationDto, ManagerRegistration>()
                .ForMember(dest => dest.SubmitedDate, opt => opt.MapFrom(src => DateTime.Parse(src.SubmitedDate)));
        }
    }
}
