using Core.Common;

namespace Core.Interfaces.Services;

public interface IReportInfraService
{
    Task<ResultFromService> GenerateDailyReportAsync();
    Task GenerateMonthlyReportAsync();
    Task GenerateAnnualReportAsync();
}