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

    Task GenerateAnualReport(ObservableCollection<Sales> collection);
    Task GenerateAnualReport(ObservableCollection<Ticket> collection);
    Task GenerateMonthReport(ObservableCollection<Sales> collection);

    Task<ResultFromService> GenerateDailyReport();
}