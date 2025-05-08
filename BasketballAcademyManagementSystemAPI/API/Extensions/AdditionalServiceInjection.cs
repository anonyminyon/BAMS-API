using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories.RepoImpl;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Common.Helpers;

namespace BasketballAcademyManagementSystemAPI.API.Extensions
{
    public static class AdditionalServiceInjection
    {
        public static IServiceCollection AddAdditionalApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<EmailHelper>();

			return services;
        }
    }
}
