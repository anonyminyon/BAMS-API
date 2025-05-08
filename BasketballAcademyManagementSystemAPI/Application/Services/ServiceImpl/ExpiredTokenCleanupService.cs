using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class ExpiredTokenCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ExpiredTokenCleanupService> _logger;

        public ExpiredTokenCleanupService(IServiceProvider serviceProvider, ILogger<ExpiredTokenCleanupService> logger)
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
                    _logger.LogInformation("ExpiredTokenCleanupService running at: {time}", DateTimeOffset.Now);

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var userRefreshTokenRepository = scope.ServiceProvider.GetRequiredService<IUserRefreshTokenRepository>();

                        var expiredTokens = await userRefreshTokenRepository.GetAllAsync();
                        var tokensToDelete = expiredTokens.Where(t => t.ExpiresAt < DateTime.Now).ToList();

                        foreach (var token in tokensToDelete)
                        {
                            await userRefreshTokenRepository.DeleteAsync(token.UserRefreshTokenId);
                        }
                    }

                    await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while cleaning up expired tokens: {message}", ex.Message);
            }
        }
    }
}
