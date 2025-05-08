using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface ISendMailService
    {
        Task SendMailAssignToTeamAsync(User user, Team team, string mailTemplateId);
        Task SendMailByMailTemplateIdAsync(string mailTemplateId, string email, dynamic data);
    }
}
