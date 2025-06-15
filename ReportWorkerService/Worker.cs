using Core.Interfaces;
using Core.Interfaces.Services;

namespace ReportWorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider serviceProvider;
    private IEmailService _emailService;

    public Worker(ILogger<Worker> logger, IServiceProvider service)
    {
        _logger = logger;
        serviceProvider = service;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
                try
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var reportService = scope.ServiceProvider.GetRequiredService<IReportInfraService>();
                        var filePathProvider = scope.ServiceProvider.GetRequiredService<IFilePathProvider>();
                        _emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                        var operation =
                            await reportService.GenerateAnnualReportAsync(filePathProvider.GetReportsDirectory());

                        if (operation.Success)
                            _logger.LogInformation(
                                "Report generated successfully at: {time} path: " +
                                filePathProvider.GetReportsDirectory(),
                                DateTimeOffset.Now);
                        else
                            _logger.LogError("Failed to generate report: {error}", operation.Message);

                        var fileBytes = File.ReadAllBytes((string)operation.Data);

                        await _emailService.SendEmailWithAttachmentAsync("mojicachris27@gmail.com", "Daily Report",
                            "<h1>Esto es una prueba</h1><p>Funciona correctamente!</p>", fileBytes,
                            $"Reporte Diario {DateTime.Now.ToString()}.xlsx");

                        _logger.LogInformation(
                            "CORREO ENVIADOOO");
                    }
                    
                    await Task.Delay(TimeSpan.FromSeconds(200), stoppingToken);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Unhandled exception in Worker");
                    throw;
                }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unhandled exception in Worker");
            throw;
        }
    }
}