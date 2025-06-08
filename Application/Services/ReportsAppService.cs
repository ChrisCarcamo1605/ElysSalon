using System.Collections.ObjectModel;
using System.Globalization;
using Application.DTOs.Request.Reports;
using Application.DTOs.Request.SalesData;
using Application.DTOs.Request.TicketsDetails;
using Application.DTOs.Response.Expenses;
using Application.DTOs.Response.SalesData;
using Application.DTOs.Response.TicketDetails;
using Application.Interfaces;
using Application.Utils;
using Core.Common;
using Core.Interfaces;
using Core.Interfaces.Services;

namespace Application.Services;

public class ReportsAppService : IReportsService
{
    private readonly IFilePathProvider _fileFileDialog;

    private readonly ISaleDataAppService _saleService;

    public ReportsAppService(IFilePathProvider fileFileDialog,
        ISaleDataAppService saleService)
    {
        _fileFileDialog = fileFileDialog ?? throw new ArgumentNullException(nameof(fileFileDialog));
        _saleService = saleService;
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
                return ResultFromService.Failed("Reporte Cancelado o ruta de archivo no válida.");

            CollectionUtil.LoadEmptyDates(salesFiltered, dateSelector, totalSelector);
            CollectionUtil.LoadEmptyDates(expensesFiltered, dateSelector, totalSelector);

            if (salesFiltered.Count == 0 && expensesFiltered.Count == 0)
                return ResultFromService.Failed("No hay datos para generar el reporte en el rango de fechas seleccionado.");

            ReportsGeneratorUtil.GenerateReport(
                fromDate, untilDate, salesFiltered, expensesFiltered,
                dateSelector, totalSelector, filePath);

            return ResultFromService.SuccessResult("Reporte Generado Exitosamente");
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

            var ticketDetails = ticketDetOperation.Data as IEnumerable<DTOGetTicketDetails> ??
                                Enumerable.Empty<DTOGetTicketDetails>();
            var expenses = expensesOperation.Data as IEnumerable<DTOGetExpense> ?? Enumerable.Empty<DTOGetExpense>();

            var expensesList = expenses
                .Where(x => x.Date.Date == today)
                .ToList();

            var ticketDetailsList = ticketDetails
                .Where(x => x.Ticket.EmissionDateTime.Date == today)
                .Select(x => new DTOSetTicketDetailsReport(x.TicketDetailsId, x.Article.Name, x.Quantity, x.date,
                    x.PriceBuy, x.TotalPrice))
                .ToList();

            return await Task.Run(() =>
                ReportsGeneratorUtil.GenerateDailyReport(ticketDetailsList, expensesList, path));
        }
        catch (Exception e)
        {
            return ResultFromService.Failed($"Error al generar el reporte diario: {e.Message}");
        }
    }

    public async Task<ResultFromService> GenerateAnualReport(string path)
    {
        var salesOperation = await _saleService.GetAllOf<DTOGetSales>();
        var expensesOperation = await _saleService.GetAllOf<DTOGetExpense>();

        if (!salesOperation.Success || !expensesOperation.Success)
        {
            var errors = new List<string>();
            if (!salesOperation.Success)
                errors.Add($"Error obteniendo ventas: {salesOperation.Message}");
            if (!expensesOperation.Success)
                errors.Add($"Error obteniendo gastos: {expensesOperation.Message}");
            return ResultFromService.Failed(string.Join("; ", errors));
        }

        var expenses = (ObservableCollection<DTOSalesData>)salesOperation.Data;
        var sales = (ObservableCollection<DTOGetExpense>)expensesOperation.Data;

        try
        {
            if (sales == null) throw new ArgumentNullException(nameof(sales));
            if (expenses == null) throw new ArgumentNullException(nameof(expenses));

            await Task.Run(() =>
            {
                var salesData = GenerateAnualDataInternal(sales, s => s.Date, s => s.Amount);
                var expensesData =
                    GenerateAnualDataInternal(expenses, e => e.Date, e => e.TotalAmount);

                ReportsGeneratorUtil.GenerateAnualReport(salesData, expensesData);
            });

            return ResultFromService.SuccessResult("Reporte anual generado exitosamente.");
        }
        catch (Exception e)
        {
            return ResultFromService.Failed($"Error al generar el reporte anual: {e.Message}");
        }
    }

    public async Task<ResultFromService> GenerateMonthReport(string path)
    {
        try
        {
            var salesOperation = await _saleService.GetAllOf<DTOGetSales>();
            var expensesOperation = await _saleService.GetAllOf<DTOGetExpense>();

            if (!salesOperation.Success || !expensesOperation.Success)
            {
                var errors = new List<string>();
                if (!salesOperation.Success)
                    errors.Add($"Error obteniendo ventas: {salesOperation.Message}");
                if (!expensesOperation.Success)
                    errors.Add($"Error obteniendo gastos: {expensesOperation.Message}");
                return ResultFromService.Failed(string.Join("; ", errors));
            }

            var salesCollection = (ObservableCollection<DTOSalesData>)salesOperation.Data;
            var expensesCollection = (ObservableCollection<DTOGetExpense>)expensesOperation.Data;


            if (salesCollection == null) throw new ArgumentNullException(nameof(salesCollection));
            if (expensesCollection == null) throw new ArgumentNullException(nameof(expensesCollection));

            var now = DateTime.Now;
            var weekRanges = new List<WeekDateRange>
            {
                new(now.AddDays(-28), now.AddDays(-21)),
                new(now.AddDays(-21), now.AddDays(-14)),
                new(now.AddDays(-14), now.AddDays(-7)),
                new(now.AddDays(-7), now)
            }.AsReadOnly();

            await Task.Run(() =>
            {
                var salesData = GenerateMonthDataInternal(salesCollection, s => s.Date, s => s.TotalAmount,
                    weekRanges);
                var expensesData = GenerateMonthDataInternal(expensesCollection, e => e.Date,
                    e => e.Amount, weekRanges);

                ReportsGeneratorUtil.GenerateMonthReport(salesData, expensesData);
            });

            return ResultFromService.SuccessResult("Reporte mensual generado exitosamente.");
        }
        catch (Exception e)
        {
            return ResultFromService.Failed($"Error al generar el reporte mensual: {e.Message}");
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

        var totalsByMonth = new decimal[12];
        for (var i = 0; i < 12; i++) totalsByMonth[i] = monthlyTotals.TryGetValue(i + 1, out var total) ? total : 0m;

        return new DTOAddAnualData(currentYear,
            totalsByMonth[0], totalsByMonth[1], totalsByMonth[2], totalsByMonth[3],
            totalsByMonth[4], totalsByMonth[5], totalsByMonth[6], totalsByMonth[7],
            totalsByMonth[8], totalsByMonth[9], totalsByMonth[10], totalsByMonth[11]
        );
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

        var w1 = weeklyTotals.Count > 0 ? weeklyTotals[0] : 0m;
        var w2 = weeklyTotals.Count > 1 ? weeklyTotals[1] : 0m;
        var w3 = weeklyTotals.Count > 2 ? weeklyTotals[2] : 0m;
        var w4 = weeklyTotals.Count > 3 ? weeklyTotals[3] : 0m;

        return new DtoMonthFinancialData(monthName, w1, w2, w3, w4);
    }
}

public class WeekDateRange
{
    public WeekDateRange(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public DateTime Start { get; }
    public DateTime End { get; }
}