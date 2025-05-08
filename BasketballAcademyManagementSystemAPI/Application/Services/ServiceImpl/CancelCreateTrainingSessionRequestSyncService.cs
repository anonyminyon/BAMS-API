
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class CancelCreateTrainingSessionRequestSyncService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CancelCreateTrainingSessionRequestSyncService> _logger;

        public CancelCreateTrainingSessionRequestSyncService(IServiceProvider serviceProvider
            , ILogger<CancelCreateTrainingSessionRequestSyncService> logger)
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
                    _logger.LogInformation("CancelCreateTrainingSessionRequestSyncService running at: {time}", DateTimeOffset.Now);

                    using var scope = _serviceProvider.CreateScope();
                    var schedulerService = scope.ServiceProvider.GetRequiredService<ICancelCreateTrainingSessionRequestSchedulerService>();
                    await schedulerService.SyncPendingRequestsFromDatabaseAsync();

                    await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while execute CancelCreateTrainingSessionRequestSyncService: {message}", ex.Message);
            }

            
        }
    }
}
