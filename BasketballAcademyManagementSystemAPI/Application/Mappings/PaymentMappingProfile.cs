using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Payment;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Payment.PaymentItems;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Mappings
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
			//ko hiểu sao mapp kiểu này nhiều trường quá lại không đọc được
			//CreateMap<Payment, PaymentDto>()
			//    .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src =>
			//        src.TeamFund != null && src.TeamFund.Team != null
			//            ? src.TeamFund.Team.TeamName
			//            : null));
			CreateMap<AddPaymentItemsDto, PaymentItem>()
	.ForMember(dest => dest.PaymentItemId, opt => opt.Ignore()); // Bỏ qua IDENTITY column

		}
	}
}