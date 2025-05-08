using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs.MemberRegistrationSession;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Team;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Mappings
{
	public class TeamMappingProfile:Profile
	{
		public TeamMappingProfile()
		{
			// Ánh xạ từ Team sang TeamRequestModel và ngược lại
			CreateMap<Team, TeamRequestModel>()
				.ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.TeamName))
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
				.ReverseMap(); // Tự động ánh xạ ngược lại

			// Ánh xạ từ Team sang TeamRequestModel và ngược lại
			CreateMap<Team, TeamDto>()
				.ForMember(dest => dest.TeamId, opt => opt.MapFrom(src => src.TeamId))
				.ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.TeamName))
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
				.ReverseMap(); // Tự động ánh xạ ngược lại
		}
	}
}
