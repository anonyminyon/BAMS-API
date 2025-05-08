using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Parent;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Mappings
{
	public class ParentProfile:Profile
	{
		public ParentProfile()
		{
			CreateMap<Parent, ParentDto>()
			.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
			.ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.User.Fullname))
			.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
			.ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.Phone))
			.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.User.Address))
			.ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.User.DateOfBirth));
		}
	}
}
