using System.Globalization;
using AutoMapper;
using BasketballAcademyManagementSystemAPI.Application.DTOs.UserAccount;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Mappings
{
    public class UserAccountProfile : Profile
    {
        public UserAccountProfile()
        {
            // ======================= Entity to DTO =========================
            CreateMap<User, UserAccountDto<object>>()
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.HasValue ? src.DateOfBirth.Value.ToString("dd/MM/yyyy") : null))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy HH:mm")))
            .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToString("dd/MM/yyyy HH:mm") : null));

            // Mapping từng Role sang RoleAccountDto
            CreateMap<Coach, CoachAccountDto>();
            CreateMap<Manager, ManagerAccountDto>();
            CreateMap<Parent, ParentAccountDto>();
            CreateMap<Player, PlayerAccountDto>();
            CreateMap<President, PresidentAccountDto>();

            // Mapping User sang UserAccountDto với từng role cụ thể
            CreateMap<User, UserAccountDto<CoachAccountDto>>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.HasValue ? src.DateOfBirth.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToString("dd/MM/yyyy HH:mm") : null))
                .ForMember(dest => dest.RoleInformation, opt => opt.MapFrom(src => src.Coach))
                .ForMember(dest => dest.RoleInformation, opt => opt.MapFrom(src =>
                    src.Coach != null
                        ? new CoachAccountDto
                        {
                            UserId = src.Coach.UserId,
                            TeamId = src.Coach.Team != null ? src.Coach.Team.TeamId : null,
                            CreatedByPresidentId = src.Coach.CreatedByPresidentId,
                            CreatedByPresident = src.Coach.CreatedByPresident.User.Fullname,
                            Bio = src.Coach.Bio ?? "Chưa có",
                            ContractStartDate = src.Coach.ContractStartDate.ToString(),
                            ContractEndDate = src.Coach.ContractEndDate.ToString(),
                            TeamName = src.Coach.Team != null ? src.Coach.Team.TeamName : "Chưa có đội",
                        }
                        : new CoachAccountDto
                        {
                            TeamName = "Chưa có đội",
                        }));

            CreateMap<User, UserAccountDto<ManagerAccountDto>>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.HasValue ? src.DateOfBirth.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToString("dd/MM/yyyy HH:mm") : null))
                .ForMember(dest => dest.RoleInformation, opt => opt.MapFrom(src => src.Manager))
                .ForMember(dest => dest.RoleInformation, opt => opt.MapFrom(src =>
                    src.Manager != null
                        ? new ManagerAccountDto
                        {
                            UserId = src.Manager.UserId,
                            TeamId = src.Manager.Team != null ? src.Manager.Team.TeamId : "Chưa có đội",
                            TeamName = src.Manager.Team != null ? src.Manager.Team.TeamName : "Chưa có đội",
                            BankAccountNumber = src.Manager.BankAccountNumber ?? "Chưa có",
                            BankName = src.Manager.BankName ?? "Chưa có",
                            PaymentMethod = src.Manager.PaymentMethod ?? -1,
                            BankBinId = src.Manager.BankBinId ?? "Chưa có",
                        }
                        : new ManagerAccountDto
                        {
                            TeamName = "Chưa có đội",
                        }));

            CreateMap<User, UserAccountDto<PlayerAccountDto>>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.HasValue ? src.DateOfBirth.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToString("dd/MM/yyyy HH:mm") : null))
                .ForMember(dest => dest.RoleInformation, opt => opt.MapFrom(src =>
                    src.Player != null
                        ? new PlayerAccountDto
                        {
                            UserId = src.Player.UserId,
                            TeamId = src.Player.Team != null ? src.Player.Team.TeamId : null,
                            TeamName = src.Player.Team != null ? src.Player.Team.TeamName : "Chưa có đội",
                            Height = src.Player.Height ?? 0,
                            Weight = src.Player.Weight ?? 0,
                            Position = src.Player.Position ?? "Chưa xác định",
                            ShirtNumber = src.Player.ShirtNumber ?? -1,
                            JoinDate = src.Player.ClubJoinDate.ToString("dd/MM/yyyy"),
                            ParentId = src.Player.Parent != null ? src.Player.Parent.UserId : null,
                            RelationshipWithParent = src.Player.RelationshipWithParent,
                            ParentName = src.Player.Parent != null ? src.Player.Parent.User.Fullname : "N/A"
                        }
                        : new PlayerAccountDto
                        {
                            TeamName = "Chưa có đội",
                            ParentName = "N/A"
                        }));


            CreateMap<User, UserAccountDto<PresidentAccountDto>>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.HasValue ? src.DateOfBirth.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToString("dd/MM/yyyy HH:mm") : null))
                .ForMember(dest => dest.RoleInformation, opt => opt.MapFrom(src => src.President));

            CreateMap<User, UserAccountDto<ParentAccountDto>>()
                .ForMember(dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth.HasValue ? src.DateOfBirth.Value.ToString("dd/MM/yyyy") : null))
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToString("dd/MM/yyyy HH:mm") : null))
                .ForMember(dest => dest.RoleInformation,
                    opt => opt.MapFrom(src => src.Parent));

            // ===============================================================
        }
    }
}
