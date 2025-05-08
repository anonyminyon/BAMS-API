namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class CancelTrainingSessionStatusChangeRequestSyncService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CancelTrainingSessionStatusChangeRequestSyncService> _logger;
        public CancelTrainingSessionStatusChangeRequestSyncService(IServiceProvider serviceProvider
            , ILogger<CancelTrainingSessionStatusChangeRequestSyncService> logger)
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
                    _logger.LogInformation("CancelTrainingSessionStatusChangeRequestSyncService running at: {time}", DateTimeOffset.Now);
                    using var scope = _serviceProvider.CreateScope();
                    var schedulerService = scope.ServiceProvider.GetRequiredService<ICancelTrainingSessionStatusChangeRequestSchedulerService>();
                    await schedulerService.SyncPendingRequestsFromDatabaseAsync();
                    await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while execute CancelTrainingSessionStatusChangeRequestSyncService: {message}", ex.Message);
            }
        }
    }
}
