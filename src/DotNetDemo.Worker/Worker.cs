namespace DotNetDemo.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(1000, stoppingToken);

            // Get pending ArticleComments, preferably without deleted, by calling /open;
            // Loop available comments;
            // When action needed, queue process task which calls /process.
            //
            // Aternatively, call /pendingprocess and then for each call /process.

            // In another process, purge deleted ArticleComments, by calling /pendingpurge (returns all ArticleComment from TableStorage With DeletionDate filled);
            // Loop comments to purge;
            // For each, queue purge task which calls /purge.
        }
    }
}
