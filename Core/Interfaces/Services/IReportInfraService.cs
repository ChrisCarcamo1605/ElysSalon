using Core.Common;

namespace Core.Interfaces.Services;

public interface IReportInfraService
{
    Task<ResultFromService> GenerateDailyReportAsync(string path);
    Task<ResultFromService> GenerateMonthlyReportAsync(string path);
    Task<ResultFromService> GenerateAnnualReportAsync(string path);
}