using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserRegistration;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Mappings
{
	public class PlayerRegistrationProfile:Profile
	{
		public PlayerRegistrationProfile()
		{
			// Ánh xạ từ PlayerRegistrationDto sang PlayerRegistration
			CreateMap<PlayerRegistrationDto, PlayerRegistration>()
				.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
				.ForMember(dest => dest.GenerationAndSchoolName, opt => opt.MapFrom(src => src.GenerationAndSchoolName))
				.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
				.ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
				.ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
				.ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.Weight))
				.ForMember(dest => dest.FacebookProfileUrl, opt => opt.MapFrom(src => src.FacebookProfileURL))
				.ForMember(dest => dest.KnowledgeAboutAcademy, opt => opt.MapFrom(src => src.KnowledgeAboutAcademy))
				.ForMember(dest => dest.ReasonToChooseUs, opt => opt.MapFrom(src => src.ReasonToChooseUs))
				.ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
				.ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experience))
				.ForMember(dest => dest.Achievement, opt => opt.MapFrom(src => src.Achievement))
				.ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.ParentName))
				.ForMember(dest => dest.ParentPhoneNumber, opt => opt.MapFrom(src => src.ParentPhoneNumber))
				.ForMember(dest => dest.ParentEmail, opt => opt.MapFrom(src => src.ParentEmail))
				.ForMember(dest => dest.RelationshipWithParent, opt => opt.MapFrom(src => src.RelationshipWithParent))
				.ForMember(dest => dest.ParentCitizenId, opt => opt.MapFrom(src => src.ParentCitizenId))
				.ForMember(dest => dest.CandidateNumber, opt => opt.MapFrom(_ => (int?)null)) // Set CandidateNumber = null
				.ForMember(dest => dest.TryOutNote, opt => opt.MapFrom(src => src.TryOutNote))
				 .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => RegistrationStatusConstant.PENDING)) // Gán giá trị mặc định cho Status
				.ForMember(dest => dest.SubmitedDate, opt => opt.MapFrom(_ => DateTime.Now)); // Gán giá trị mặc định cho SubmitedDate

	
		}
	}
}
