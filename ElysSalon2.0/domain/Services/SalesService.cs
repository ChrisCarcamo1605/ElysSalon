using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using ElysSalon2._0.aplication.DTOs.DTOSales;
using ElysSalon2._0.aplication.Interfaces.Repositories;
using ElysSalon2._0.aplication.Interfaces.Services;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.domain.Entities;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace ElysSalon2._0.domain.Services;

public class SalesService : ISalesService
{
    private ISalesRepository _salesRepo;
    private ITicketRepository _ticketRepo;
    private ReportsConfiguration _reportConfig;

    public SalesService(ISalesRepository salesRepo, ITicketRepository ticketRepo, ReportsConfiguration reportsConfig)
    {
        _reportConfig = reportsConfig;
        _salesRepo = salesRepo;
        _ticketRepo = ticketRepo;
    }

    public async Task<ServiceResult> GenerateReport<T>(DateTime fromDate, DateTime untilDate,
        ObservableCollection<T> collection, Func<T, DateTime> dateSelector, Func<T, decimal> totalSelector)
        where T : class
    {
        var collectionFilter =
            collection.Where(x => dateSelector(x) >= fromDate && dateSelector(x) <= untilDate).ToList();

        var fromDateFormated = fromDate.ToString("ddMMMM", new CultureInfo("es-ES"));
        var untilDateFormated = untilDate.ToString("ddMMMM", new CultureInfo("es-ES"));

        var result =
            ReportsGeneratorUtil.GenerateReport(fromDate, untilDate, collectionFilter, dateSelector, totalSelector);
        return result;
    }

    public async Task GenerateAnualReport(ObservableCollection<Sales> collection)
    {
        var year = DateTime.Now.Year;
        var jenuary = collection
            .Where(x => x.SaleDate.Month == 1)
            .Aggregate(0m, (acumulador, n) => acumulador + n.Total);

        var february = collection
            .Where(x => x.SaleDate.Month == 2)
            .Aggregate(0m, (acumulador, n) => acumulador + n.Total);
        var march = collection
            .Where(x => x.SaleDate.Month == 3)
            .Aggregate(0m, (acumulador, n) => acumulador + n.Total);

        var april = collection
            .Where(x => x.SaleDate.Month == 4)
            .Aggregate(0m, (acumulador, n) => acumulador + n.Total);

        var may = collection
            .Where(x => x.SaleDate.Month == 5)
            .Aggregate(0m, (acumulador, n) => acumulador + n.Total);
        var june = collection
            .Where(x => x.SaleDate.Month == 6)
            .Aggregate(0m, (acumulador, n) => acumulador + n.Total);

        var july = collection
            .Where(x => x.SaleDate.Month == 7)
            .Aggregate(0m, (acumulador, n) => acumulador + n.Total);
        var august = collection
            .Where(x => x.SaleDate.Month == 8)
            .Aggregate(0m, (acumulador, n) => acumulador + n.Total);
        var september = collection
            .Where(x => x.SaleDate.Month == 9)
            .Aggregate(0m, (acumulador, n) => acumulador + n.Total);
        var october = collection
            .Where(x => x.SaleDate.Month == 10)
            .Aggregate(0m, (acumulador, n) => acumulador + n.Total);

        var november = collection
            .Where(x => x.SaleDate.Month == 11)
            .Aggregate(0m, (acumulador, n) => acumulador + n.Total);

        var december = collection
            .Where(x => x.SaleDate.Month == 12)
            .Aggregate(0m, (acumulador, n) => acumulador + n.Total);

        ReportsGeneratorUtil.GenerateAnualReport(new DtoAnualData(year, jenuary, february, march, april, may, june,
            july,
            august, september, october, november, december
        ));
    }

    public async Task GenerateAnualReport(ObservableCollection<Ticket> collection)
    {
        var year = DateTime.Now.Year;

        var jenuary = collection
            .Where(x => x.EmissionDateTime.Month == 1)
            .Aggregate(0m, (acumulador, n) => acumulador + n.TotalAmount);

        var february = collection
            .Where(x => x.EmissionDateTime.Month == 2)
            .Aggregate(0m, (acumulador, n) => acumulador + n.TotalAmount);
        var march = collection
            .Where(x => x.EmissionDateTime.Month == 3)
            .Aggregate(0m, (acumulador, n) => acumulador + n.TotalAmount);

        var april = collection
            .Where(x => x.EmissionDateTime.Month == 4)
            .Aggregate(0m, (acumulador, n) => acumulador + n.TotalAmount);

        var may = collection
            .Where(x => x.EmissionDateTime.Month == 5)
            .Aggregate(0m, (acumulador, n) => acumulador + n.TotalAmount);
        var june = collection
            .Where(x => x.EmissionDateTime.Month == 6)
            .Aggregate(0m, (acumulador, n) => acumulador + n.TotalAmount);

        var july = collection
            .Where(x => x.EmissionDateTime.Month == 7)
            .Aggregate(0m, (acumulador, n) => acumulador + n.TotalAmount);
        var august = collection
            .Where(x => x.EmissionDateTime.Month == 8)
            .Aggregate(0m, (acumulador, n) => acumulador + n.TotalAmount);
        var september = collection
            .Where(x => x.EmissionDateTime.Month == 9)
            .Aggregate(0m, (acumulador, n) => acumulador + n.TotalAmount);
        var october = collection
            .Where(x => x.EmissionDateTime.Month == 10)
            .Aggregate(0m, (acumulador, n) => acumulador + n.TotalAmount);

        var november = collection
            .Where(x => x.EmissionDateTime.Month == 11)
            .Aggregate(0m, (acumulador, n) => acumulador + n.TotalAmount);

        var december = collection
            .Where(x => x.EmissionDateTime.Month == 12)
            .Aggregate(0m, (acumulador, n) => acumulador + n.TotalAmount);

        ReportsGeneratorUtil.GenerateAnualReport(new DtoAnualData(year, jenuary, february, march, april, may, june,
            july,
            august, september, october, november, december
        ));
    }

    public async Task GenerateMonthReport(ObservableCollection<Sales> collection)
    {
        var dto = _reportConfig.GetWeeksRanges(collection);

        var week1 = collection
            .Where(x => x.SaleDate > dto.week1Start && x.SaleDate < dto.week1End)
            .Aggregate(0m, (acumulador, n) => acumulador + n.Total);
        var week2 = collection
            .Where(x => x.SaleDate > dto.week2Start && x.SaleDate < dto.week3End)
            .Aggregate(0m, (acumulador, n) => acumulador + n.Total);
        var week3 = collection
            .Where(x => x.SaleDate > dto.week3Start && x.SaleDate < dto.week3End)
            .Aggregate(0m, (acumulador, n) => acumulador + n.Total);
        var week4 = collection.Where(x => x.SaleDate > dto.week4Start && x.SaleDate < dto.week4End)
            .Aggregate(0m, (acumulador, n) => acumulador + n.Total);

        var now = DateTime.Now;
        var month = now.ToString("MMMM", new CultureInfo("es-ES")).ToUpper();
        ReportsGeneratorUtil.GenerateMonthReport(new DtoMonthFinancialData(month, week1, week2, week3, week4));

        //MessageBox.Show(
        //    $"DiasPorSemana{daysPerWeek} week2= {daysPerWeek * 2} week4= {daysPerWeek * 3}" +
        //    $" week4= {daysPerWeek * 4}  TOTAL DE DIAS DEL MES= {totalsDaysInMonth} MES={firstDayMonth.Month}");
    }

    public async Task SaveSale(Sales sale)
    {
        _salesRepo.SavesSale(sale);
    }

    public async Task<ObservableCollection<Sales>> GetSales()
    {
        return await _salesRepo.GetSalesAsync();
    }

    public async Task GenerateMonthReport(ObservableCollection<Ticket> collection)
    {
        var dto = _reportConfig.GetWeeksRanges(collection);

        var week1 = collection
            .Where(x => x.EmissionDateTime > DateTime.Now.AddDays(-28) &&
                        x.EmissionDateTime < DateTime.Now.AddDays(-21))
            .Aggregate(0m, (acumulador, n) => acumulador + n.TotalAmount);
        var week2 = collection
            .Where(x => x.EmissionDateTime > DateTime.Now.AddDays(-21) &&
                        x.EmissionDateTime < DateTime.Now.AddDays(-14))
            .Aggregate(0m, (acumulador, n) => acumulador + n.TotalAmount);
        var week3 = collection
            .Where(x => x.EmissionDateTime > DateTime.Now.AddDays(-14) && x.EmissionDateTime < DateTime.Now.AddDays(-7))
            .Aggregate(0m, (acumulador, n) => acumulador + n.TotalAmount);
        var week4 = collection
            .Where(x => x.EmissionDateTime > DateTime.Now.AddDays(-7) && x.EmissionDateTime < DateTime.Now)
            .Aggregate(0m, (acumulador, n) => acumulador + n.TotalAmount);

        var month = DateTime.Now.ToString("MMMM", new CultureInfo("es-ES")).ToUpperInvariant();
        ReportsGeneratorUtil.GenerateMonthReport(new DtoMonthFinancialData(month, week1, week2, week3, week4));
    }
}