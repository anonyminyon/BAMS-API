using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs.MemberRegistrationSession;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Mappings
{
    public class MemberRegistrationSessionProfile : Profile
    {
        public MemberRegistrationSessionProfile()
        {
            CreateMap<MemberRegistrationSession, MemberRegistrationSessionResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MemberRegistrationSessionId))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss")))
            .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToString("dd/MM/yyyy HH:mm:ss") : null));
        }
    }
}
