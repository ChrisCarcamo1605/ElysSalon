using System; // Asegúrate de que System esté importado para ArgumentNullException, DateTime, etc.
using System.Collections.Generic; // Para List<T>
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Application.Configurations;
using Application.DTOs.Request.Reports;
using Application.DTOs.Request.SalesData;
using Application.DTOs.Request.TicketsDetails;
using Application.DTOs.Response.Expenses;
using Application.DTOs.Response.SalesData;
using Application.DTOs.Response.TicketDetails;
using Application.Utils;
using Core.Common;
using Core.Domain.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using ElysSalon2._0.Utils;

namespace Application.Services;

public class ReportsAppService : IReportsService
{
    private readonly IFilePathProvider _fileFileDialog;
    private readonly ReportsConfiguration _reportConfig;
    private readonly SaleDataAppService _saleService;

    public ReportsAppService(ReportsConfiguration reportsConfig, IFilePathProvider fileFileDialog,
        SaleDataAppService saleService)
    {
        _reportConfig = reportsConfig ?? throw new ArgumentNullException(nameof(reportsConfig));
        _fileFileDialog = fileFileDialog ?? throw new ArgumentNullException(nameof(fileFileDialog));
        _saleService = saleService ?? throw new ArgumentNullException(nameof(saleService));
    }

    public async Task<ResultFromService> GenerateReport<T>(DateTime fromDate, DateTime untilDate,
        ObservableCollection<T> salesCollection, ObservableCollection<T> expensesCollection,
        Func<T, DateTime> dateSelector, Func<T, decimal> totalSelector)
        where T : class
    {
        try
        {
            if (salesCollection == null) return ResultFromService.Failed("La colección de ventas no puede ser nula.");
            if (expensesCollection == null)
                return ResultFromService.Failed("La colección de gastos no puede ser nula.");
            if (dateSelector == null) return ResultFromService.Failed("El selector de fecha no puede ser nulo.");
            if (totalSelector == null) return ResultFromService.Failed("El selector de total no puede ser nulo.");
            if (fromDate > untilDate)
                return ResultFromService.Failed("La fecha 'desde' no puede ser posterior a la fecha 'hasta'.");


            var salesFiltered = salesCollection
                .Where(x =>
                {
                    var date = dateSelector(x).Date;
                    return date >= fromDate.Date && date <= untilDate.Date;
                })
                .ToList();

            var expensesFiltered = expensesCollection
                .Where(x =>
                {
                    var date = dateSelector(x).Date;
                    return date >= fromDate.Date && date <= untilDate.Date;
                })
                .ToList();


            var filePath = await _fileFileDialog.ShowSaveFileDialogAsync(fromDate, untilDate);
            if (string.IsNullOrEmpty(filePath))
            {
                return ResultFromService.Failed("Reporte Cancelado o ruta de archivo no válida.");
            }

            CollectionUtil.LoadEmptyDates( salesFiltered, dateSelector, totalSelector);
            CollectionUtil.LoadEmptyDates( expensesFiltered, dateSelector, totalSelector);
            var result =  ReportsGeneratorUtil.GenerateReport(
                fromDate, untilDate, salesFiltered, expensesFiltered,
                dateSelector, totalSelector, filePath);

            return result;
        }
        catch (Exception e)
        {
            return ResultFromService.Failed($"Error al generar el reporte: {e.Message}");
        }
    }


    public async Task<ResultFromService> GenerateDailyReport(string path)
    {
        try
        {
            var today = DateTime.Now.Date;

            var ticketDetOperation = await _saleService.GetAllOf<DTOGetTicketDetails>();
            var expensesOperation = await _saleService.GetAllOf<DTOGetExpense>();

            if (!ticketDetOperation.Success || !expensesOperation.Success)
            {
                var errors = new List<string>();
                if (!ticketDetOperation.Success)
                    errors.Add($"Error obteniendo detalles de tickets: {ticketDetOperation.Message}");
                if (!expensesOperation.Success) errors.Add($"Error obteniendo gastos: {expensesOperation.Message}");
                return ResultFromService.Failed(string.Join("; ", errors));
            }

            var ticketDetails = (ticketDetOperation.Data as IEnumerable<DTOGetTicketDetails>) ??
                                Enumerable.Empty<DTOGetTicketDetails>();
            var expenses = (expensesOperation.Data as IEnumerable<DTOGetExpense>) ?? Enumerable.Empty<DTOGetExpense>();

            var expensesList = expenses
                .Where(x => x.Date.Date == today)
                .ToList();

            var ticketDetailsList = ticketDetails
                .Where(x => x.Ticket.EmissionDateTime.Date == today)
                .Select(x => new DTOSetTicketDetailsReport(x.TicketDetailsId, x.Article.Name, x.Quantity, x.date,
                    x.PriceBuy, x.TotalPrice))
                .ToList();

            return await Task.Run(() => ReportsGeneratorUtil.GenerateDailyReport(ticketDetailsList, expensesList, path));
        }
        catch (Exception e)
        {
            return ResultFromService.Failed($"Error al generar el reporte diario: {e.Message}");
        }
    }

    private DTOAddAnualData GenerateAnualDataInternal<T>(
        IEnumerable<T> collection,
        Func<T, DateTime> dateSelector,
        Func<T, decimal> totalSelector)
    {
        var currentYear = DateTime.Now.Year;

        var monthlyTotals = collection
            .Where(x => dateSelector(x).Year == currentYear)
            .GroupBy(x => dateSelector(x).Month)
            .ToDictionary(g => g.Key, g => g.Sum(totalSelector));

        decimal[] totalsByMonth = new decimal[12];
        for (int i = 0; i < 12; i++)
        {
            totalsByMonth[i] = monthlyTotals.TryGetValue(i + 1, out var total) ? total : 0m;
        }

        return new DTOAddAnualData(currentYear,
            totalsByMonth[0], totalsByMonth[1], totalsByMonth[2], totalsByMonth[3],
            totalsByMonth[4], totalsByMonth[5], totalsByMonth[6], totalsByMonth[7],
            totalsByMonth[8], totalsByMonth[9], totalsByMonth[10], totalsByMonth[11]
        );
    }

    // Método público para generar reporte anual con Sales y Expenses
    public async Task<ResultFromService> GenerateAnualReport<T>(ObservableCollection<T> salesCollection,
        ObservableCollection<T> expensesCollection, Func<T, DateTime> dateSelector,
        Func<T, decimal> totalSelector) where T : class
    {
        try
        {
            if (salesCollection == null) throw new ArgumentNullException(nameof(salesCollection));
            if (expensesCollection == null) throw new ArgumentNullException(nameof(expensesCollection));

            await Task.Run(() =>
            {
                var salesData = GenerateAnualDataInternal(salesCollection, s => dateSelector(s), s => totalSelector(s));
                var expensesData =
                    GenerateAnualDataInternal(expensesCollection, e => dateSelector(e), e => totalSelector(e));

                ReportsGeneratorUtil.GenerateAnualReport(salesData, expensesData);
            });

            return ResultFromService.SuccessResult("Reporte anual generado exitosamente.");
        }
        catch (Exception e)
        {
            return ResultFromService.Failed($"Error al generar el reporte anual: {e.Message}");
        }
    }

    private DtoMonthFinancialData GenerateMonthDataInternal<T>(
        IEnumerable<T> collection,
        Func<T, DateTime> dateSelector,
        Func<T, decimal> totalSelector,
        IReadOnlyList<WeekDateRange> weekRanges)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (weekRanges == null || !weekRanges.Any())
            throw new ArgumentException("Se requieren rangos de semanas.", nameof(weekRanges));

        var weeklyTotals = new List<decimal>();
        foreach (var range in weekRanges)
        {
            var weekTotal = collection
                .Where(x =>
                {
                    var date = dateSelector(x);
                    return date > range.Start && date < range.End;
                })
                .Sum(totalSelector);
            weeklyTotals.Add(weekTotal);
        }

        var now = DateTime.Now;
        var monthName = now.ToString("MMMM", CultureInfo.GetCultureInfo("es-ES")).ToUpperInvariant();

        decimal w1 = weeklyTotals.Count > 0 ? weeklyTotals[0] : 0m;
        decimal w2 = weeklyTotals.Count > 1 ? weeklyTotals[1] : 0m;
        decimal w3 = weeklyTotals.Count > 2 ? weeklyTotals[2] : 0m;
        decimal w4 = weeklyTotals.Count > 3 ? weeklyTotals[3] : 0m;

        return new DtoMonthFinancialData(monthName, w1, w2, w3, w4);
    }

    public async Task<ResultFromService> GenerateMonthRepor<T>(ObservableCollection<T> salesCollection,
        ObservableCollection<T> expensesCollection, Func<T, DateTime> dateSelector,
        Func<T, decimal> totalSelector) where T : class
    {
        try
        {
            if (salesCollection == null) throw new ArgumentNullException(nameof(salesCollection));
            if (expensesCollection == null) throw new ArgumentNullException(nameof(expensesCollection));

            var now = DateTime.Now;
            var weekRanges = new List<WeekDateRange>
            {
                new WeekDateRange(now.AddDays(-28), now.AddDays(-21)),
                new WeekDateRange(now.AddDays(-21), now.AddDays(-14)),
                new WeekDateRange(now.AddDays(-14), now.AddDays(-7)),
                new WeekDateRange(now.AddDays(-7), now)
            }.AsReadOnly();

            await Task.Run(() =>
            {
                var salesData = GenerateMonthDataInternal(salesCollection, s => dateSelector(s), s => totalSelector(s),
                    weekRanges);
                var expensesData = GenerateMonthDataInternal(expensesCollection, e => dateSelector(e),
                    e => totalSelector(e), weekRanges);

                ReportsGeneratorUtil.GenerateMonthReport(salesData, expensesData);
            });

            return ResultFromService.SuccessResult("Reporte mensual generado exitosamente.");
        }
        catch (Exception e)
        {
            return ResultFromService.Failed($"Error al generar el reporte mensual: {e.Message}");
        }
    }
}

public class WeekDateRange
{
    public DateTime Start { get; }
    public DateTime End { get; }

    public WeekDateRange(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }
}