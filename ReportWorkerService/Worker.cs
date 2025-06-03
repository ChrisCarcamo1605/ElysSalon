using Core.Interfaces;
using Core.Interfaces.Services;

namespace ReportWorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider serviceProvider;

    public Worker(ILogger<Worker> logger, IServiceProvider service)
    {
        _logger = logger;
        serviceProvider = service;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var reportService = scope.ServiceProvider.GetRequiredService<IReportInfraService>();
                    var filePathProvider = scope.ServiceProvider.GetRequiredService<IFilePathProvider>();

                    var operation =
                        await reportService.GenerateDailyReportAsync(filePathProvider.GetReportsDirectory());

                    if (operation.Success)
                        _logger.LogInformation(
                            "Report generated successfully at: {time} path: " + filePathProvider.GetReportsDirectory(),
                            DateTimeOffset.Now);
                    else
                        _logger.LogError("Failed to generate report: {error}", operation.Message);
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unhandled exception in Worker");
                throw;
            }
    }
}