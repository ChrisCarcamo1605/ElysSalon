using System.Collections.ObjectModel;
using Core.Common;
using Core.Domain.Entities;

namespace Core.Interfaces.Services;

public interface IReportsService
{
    Task<ResultFromService> GenerateReport<T>(DateTime fromDate, DateTime untilDate,
        ObservableCollection<T> salesCollection, ObservableCollection<T> expensesCollection,
        Func<T, DateTime> dateSelector, Func<T, decimal> totalSelector)
        where T : class;

    Task<ResultFromService> GenerateAnualReport<T>(ObservableCollection<T> collection,
        ObservableCollection<T> expensesCollection,
        Func<T, DateTime> dateSelector, Func<T, decimal> totalSelector) where T : class;

    Task<ResultFromService> GenerateMonthRepor<T>(ObservableCollection<T> salesCollection,
        ObservableCollection<T> expensesCollection, Func<T, DateTime> dateSelector,
        Func<T, decimal> totalSelector) where T : class;

    Task<ResultFromService> GenerateDailyReport(string path);
}