using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TeamFund;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Mappings
{
    public class TeamFundMappingProfile : Profile
    {
        public TeamFundMappingProfile()
        {
            CreateMap<CreateExpenditureDto, Expenditure>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount));

            CreateMap<UpdateExpenditureDto, Expenditure>()
                .ForMember(dest => dest.ExpenditureId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount));
          

            CreateMap<Expenditure, ExpenditureDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExpenditureId))
                .ForMember(dest => dest.TeamFundId, opt => opt.MapFrom(src => src.TeamFundId))
                .ForMember(dest => dest.ByManagerId, opt => opt.MapFrom(src => src.ByManagerId))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToDateTime(TimeOnly.MinValue)));
        }
    }
}
