using System.Collections.ObjectModel;
using ElysSalon2._0.Core.aplication.DTOs.DTOSales;
using ElysSalon2._0.Core.domain.Entities;
using ElysSalon2._0.Core.domain.Services;

namespace ElysSalon2._0.Core.aplication.Ports.Services;

public interface ISalesService
{
    Task<ServiceResult> GenerateReport<T>(DateTime fromDate, DateTime untilDate,
        ObservableCollection<T> collection, Func<T, DateTime> dateSelector, Func<T, decimal> totalSelector)
        where T : class;

    Task GenerateAnualReport(ObservableCollection<Sales> collection);
    Task GenerateAnualReport(ObservableCollection<Ticket> collection);
    Task GenerateMonthReport(ObservableCollection<Sales> collection);

    Task SaveSale(Sales sale);
    Task<ObservableCollection<Sales>> GetSales();
}