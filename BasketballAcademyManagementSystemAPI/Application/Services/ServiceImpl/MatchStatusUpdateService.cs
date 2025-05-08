using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Infrastructure;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class MatchStatusUpdateService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MatchStatusUpdateService> _logger;

        public MatchStatusUpdateService(IServiceProvider serviceProvider, ILogger<MatchStatusUpdateService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("MatchStatusUpdateService running at: {time}", DateTimeOffset.Now);

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var matchRepository = scope.ServiceProvider.GetRequiredService<IMatchRepository>();

                        var matches = await matchRepository.GetNonCanceledMatchesAsync(stoppingToken);

                        foreach (var match in matches)
                        {
                            if (match.Status == MatchConstant.Status.UPCOMING && match.MatchDate <= DateTime.Now)
                            {
                                match.Status = MatchConstant.Status.ONGOING;
                            }

                            if ((match.Status == MatchConstant.Status.ONGOING || match.Status == MatchConstant.Status.UPCOMING)
                                && match.MatchDate.AddHours(1) <= DateTime.Now)
                            {
                                match.Status = MatchConstant.Status.FINISHED;
                            }
                        }

                        await matchRepository.SaveChangesAsync(stoppingToken);
                    }

                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating match statuses: {message}", ex.Message);
            }
            finally
            {
                _logger.LogInformation("MatchStatusUpdateService stopped at: {time}", DateTimeOffset.Now);
            }
        }
    }
}
