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

    public async Task<ResultFromService> GenerateDailyReportAsync(string path)
    {
        try
        {
            return await _reportsService.GenerateDailyReport(path);
        }
        catch (Exception e)
        {
            return ResultFromService.Failed(e.Message);
            throw;
        }
    }

    public Task GenerateMonthlyReportAsync(string path)
    {
        throw new NotImplementedException();
    }

    public Task GenerateAnnualReportAsync(string path)
    {
        throw new NotImplementedException();
    }
}