namespace BasketballAcademyManagementSystemAPI.Application.Services
{
    public interface IClaudeService
    {
        Task<string> AskAsync(string prompt);
    }
}
