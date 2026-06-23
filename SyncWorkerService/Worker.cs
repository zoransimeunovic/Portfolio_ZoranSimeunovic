using SyncWorkerService.Services;

namespace SyncWorkerService;

public class Worker(
    IServiceScopeFactory scopeFactory,
    IConfiguration config,
    ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var intervalMinutes = config.GetValue<int>("SyncWorker:IntervalMinutes", 10);
        logger.LogInformation("SyncWorkerService started. Interval: {Minutes} min", intervalMinutes);

        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Starting lead scan at {Time}", DateTimeOffset.UtcNow);

            try
            {
                using var scope = scopeFactory.CreateScope();
                var scanner = scope.ServiceProvider.GetRequiredService<LeadScannerService>();
                await scanner.ScanAndProcessAsync(stoppingToken);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                logger.LogError(ex, "Unhandled error during lead scan");
            }

            logger.LogInformation("Lead scan complete. Next run in {Minutes} min", intervalMinutes);
            await Task.Delay(TimeSpan.FromMinutes(intervalMinutes), stoppingToken);
        }
    }
}
