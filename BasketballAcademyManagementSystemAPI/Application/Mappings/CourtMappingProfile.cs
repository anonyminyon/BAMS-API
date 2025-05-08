using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs.CourtManagement;
using BasketballAcademyManagementSystemAPI.Models;
using System.Linq.Expressions;

namespace BasketballAcademyManagementSystemAPI.Application.Mappings
{
    public class CourtMappingProfile : Profile
    {
        public CourtMappingProfile()
        {
            // Tạo mapping hai chiều giữa Court và CourtDto
            CreateMap<Court, CourtDto>().ReverseMap();
        }
    }
}