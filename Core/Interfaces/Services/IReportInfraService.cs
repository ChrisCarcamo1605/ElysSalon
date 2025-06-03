using Core.Common;

namespace Core.Interfaces.Services;

public interface IReportInfraService
{
    Task<ResultFromService> GenerateDailyReportAsync(string path);
    Task GenerateMonthlyReportAsync(string path);
    Task GenerateAnnualReportAsync(string path);
}