using System.Collections.ObjectModel;
using Core.Common;
using Core.Domain.Entities;

namespace Application.Interfaces;

public interface ISalesReportsService
{
    Task<ResultFromService> GenerateReport<T>(DateTime fromDate, DateTime untilDate,
        ObservableCollection<T> collection, Func<T, DateTime> dateSelector, Func<T, decimal> totalSelector)
        where T : class;

    Task GenerateAnualReport(ObservableCollection<Sales> collection);
    Task GenerateAnualReport(ObservableCollection<Ticket> collection);
    Task GenerateMonthReport(ObservableCollection<Sales> collection);
}