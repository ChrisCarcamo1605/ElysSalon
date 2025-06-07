using System.Collections.ObjectModel;
using Core.Common;

namespace Core.Interfaces.Services;

public interface IReportsService
{
    Task<ResultFromService> GenerateReport<T>(DateTime fromDate, DateTime untilDate,
        ObservableCollection<T> salesCollection, ObservableCollection<T> expensesCollection,
        Func<T, DateTime> dateSelector, Func<T, decimal> totalSelector)
        where T : class;

    Task<ResultFromService> GenerateAnualReport(string path);

    Task<ResultFromService> GenerateMonthReport(string path);

    Task<ResultFromService> GenerateDailyReport(string path);
}