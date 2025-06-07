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

    public async Task<ResultFromService> GenerateMonthlyReportAsync(string path)
    {
        try
        {
            return await _reportsService.GenerateMonthReport(path);
        }
        catch (Exception e)
        {
            return ResultFromService.Failed(e.Message);
        }
    }

    public async Task<ResultFromService> GenerateAnnualReportAsync(string path)
    {
        try
        {
            return await _reportsService.GenerateAnualReport(path);
        }
        catch (Exception e)
        {
            return ResultFromService.Failed(e.Message);
        }
    }
}