using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs.CoachManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;
using BasketballAcademyManagementSystemAPI.Models;

public class CoachMappingProfile : Profile
{
    public CoachMappingProfile()
    {
        // Mapping User sang UserAccountDto với từng role cụ thể
        CreateMap<User, UserAccountDto<CoachDetailDto>>()
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.HasValue ? src.DateOfBirth.Value.ToString("dd/MM/yyyy") : null))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss")))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToString("dd/MM/yyyy HH:mm:ss") : null))
            .ForMember(dest => dest.RoleInformation, opt => opt.MapFrom(src => src.Coach));

        // Mapping Coach sang CoachDetailDto
        CreateMap<Coach, CoachDetailDto>()
            .ForMember(dest => dest.ContractStartDate, opt => opt.MapFrom(src => src.ContractStartDate.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.ContractEndDate, opt => opt.MapFrom(src => src.ContractEndDate.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team != null ? src.Team.TeamName : null))
            .ForMember(dest => dest.CreatedByPresident, opt => opt.MapFrom(src => src.CreatedByPresident.User.Fullname));
    }
}

