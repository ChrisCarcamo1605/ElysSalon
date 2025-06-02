using Core.Interfaces.Services;
using Infrastructure.Service;
using Microsoft.Extensions.Logging;
using static System.Formats.Asn1.AsnWriter;

namespace ReportWorkerService
{
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
            {
                try
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var reportService = scope.ServiceProvider.GetRequiredService<IReportInfraService>();
                        _logger.LogError("Mensaje de prueba {Fecha}", DateTime.Now);

                        var operation =    await reportService.GenerateDailyReportAsync();

                        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                        if (operation.Success)
                        {
                            _logger.LogInformation("Report generated successfully at: {time}", DateTimeOffset.Now);
                        }
                        else
                        {
                            _logger.LogError("Failed to generate report: {error}", operation.Message);
                        }
                    }

                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken); 

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
               
            }
        }
    }
}
