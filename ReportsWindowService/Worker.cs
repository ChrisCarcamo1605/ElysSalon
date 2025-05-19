namespace ReportsWindowService;

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
            var horaActual = DateTime.Now.TimeOfDay;
            var horaObjetivo = new TimeSpan(23, 0, 0); // Ejemplo: 11:00 PM

            if (horaActual.Hours == horaObjetivo.Hours && horaActual.Minutes == horaObjetivo.Minutes)
            {
                await GenerarReporteAsync(); // Tu lógica de reporte
                await Task.Delay(TimeSpan.FromMinutes(1),
                    stoppingToken); // Esperar 1 min para evitar ejecuciones dobles
            }
            else
            {
                await Task.Delay(10000, stoppingToken); // Verificar cada 10 segundos
            }
        }
    }

    public async Task GenerarReporteAsync()
    {
        _logger.LogInformation("Generando reporte a las: {time}", DateTimeOffset.Now);
    }
}