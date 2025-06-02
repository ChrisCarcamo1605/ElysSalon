using Core.Common;
using Core.Interfaces.Services;

namespace Infrastructure.Service;

public class ReportInfraService : IReportInfraService
{
    private readonly IReportsService _reportsService;
    public ReportInfraService(IReportsService reportsService)
    {
        _reportsService = reportsService;
    }

    public async Task<ResultFromService> GenerateDailyReportAsync()
    {
       return  await _reportsService.GenerateDailyReport();
    }

    public Task GenerateMonthlyReportAsync()
    {
        throw new NotImplementedException();
    }

    public Task GenerateAnnualReportAsync()
    {
        throw new NotImplementedException();
    }
}