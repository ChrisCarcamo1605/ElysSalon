using System.Collections.ObjectModel;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Globalization;
using Application.DTOs.Request.SalesData;
using Application.Enums;
using Application.Services;
using LiveChartsCore;
using LiveChartsCore.Defaults;


namespace ElysSalon2._0.Factories;

public class ChartAxisFactory
{
    public static Axis[] CreateXAxis(RangeFilter filter)
    {
        return filter switch
        {
            RangeFilter.LastSevenDays => CreateLastSevenDaysXAxis(),
            RangeFilter.LastMonth => CreateLastMonthXAxis(),
            RangeFilter.LastThreeMonths => CreateLastThreeMonthsXAxis(),
            _ => CreateDefaultXAxis()
        };
    }

    public static Axis[] CreateYAxis()
    {
        return new Axis[]
        {
            new()
            {
                Name = "Total",
                Position = AxisPosition.Start,
                ShowSeparatorLines = true,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200), 0.5f),
                SeparatorsAtCenter = true,
                SubseparatorsCount = 2
            }
        };
    }


    public static Axis[] CreateGeneralXAxis()
    {
        return CreateLastThreeMonthsXAxis();
    }

    public static ISeries[] CreateChartSeries(
        ObservableCollection<DTOSalesData> salesCollection,
        ObservableCollection<DTOSalesData> expensesCollection,
        RangeFilter filter)
    {
        var salesFiltered = FilterByRangeService.FilterByDateRange(salesCollection, filter);
        var expensesFiltered = FilterByRangeService.FilterByDateRange(expensesCollection, filter);

        var salePoints = new ObservableCollection<ObservablePoint>();
        var expensePoints = new ObservableCollection<ObservablePoint>();

        foreach (var venta in salesFiltered)
            salePoints.Add(new ObservablePoint(venta.Date.ToOADate(), (double)venta.TotalAmount));
        foreach (var expense in expensesFiltered)
            expensePoints.Add(new ObservablePoint(expense.Date.ToOADate(), (double)expense.TotalAmount));

        return filter switch
        {
            RangeFilter.LastSevenDays => CreateColumnSeries(salePoints, expensePoints, 4),
            RangeFilter.LastMonth => CreateColumnSeries(salePoints, expensePoints, 1),
            RangeFilter.LastThreeMonths => CreateLineSeries(salePoints, expensePoints),
            _ => CreateDefaultSeries(salePoints, expensePoints)
        };
    }

    public static ISeries[] CreateGeneralLineSeries(
        ObservableCollection<DTOSalesData> salesCollection,
        ObservableCollection<DTOSalesData> expensesCollection)
    {
        var allSales = salesCollection.OrderBy(x => x.Date).ToList();
        var allExpenses = expensesCollection.OrderBy(x => x.Date).ToList();

        var salePoints = new ObservableCollection<ObservablePoint>();
        var expensePoints = new ObservableCollection<ObservablePoint>();

        foreach (var venta in allSales)
            salePoints.Add(new ObservablePoint(venta.Date.ToOADate(), (double)venta.TotalAmount));
        foreach (var expense in allExpenses)
            expensePoints.Add(new ObservablePoint(expense.Date.ToOADate(), (double)expense.TotalAmount));

        return new ISeries[]
        {
            new LineSeries<ObservablePoint>
            {
                Name = "Ventas",
                Values = salePoints,
                GeometrySize = 3,
                Stroke = new SolidColorPaint(new SKColor(255, 0, 190), 1),
                GeometryFill = new SolidColorPaint(new SKColor(255, 0, 190), 0),
                Fill = null,
                LineSmoothness = 0,
                GeometryStroke = new SolidColorPaint(new SKColor(255, 0, 190), 0)
            },
            new LineSeries<ObservablePoint>
            {
                Name = "Gastos",
                Values = expensePoints,
                GeometrySize = 3,
                Stroke = new SolidColorPaint(new SKColor(0, 0, 0), 1),
                GeometryFill = new SolidColorPaint(new SKColor(0, 0, 0), 0),
                Fill = null,
                LineSmoothness = 0,
                GeometryStroke = new SolidColorPaint(new SKColor(0, 0, 0), 0)
            }
        };
    }

    private static ISeries[] CreateColumnSeries(
        ObservableCollection<ObservablePoint> salePoints,
        ObservableCollection<ObservablePoint> expensePoints,
        int padding)
    {
        return new ISeries[]
        {
            new ColumnSeries<ObservablePoint>
            {
                Name = "Ventas",
                Values = salePoints,
                Padding = padding,
                Fill = new SolidColorPaint(new SKColor(255, 0, 190), 1)
            },
            new ColumnSeries<ObservablePoint>
            {
                Name = "Gastos",
                Values = expensePoints,
                Padding = padding,
                Fill = new SolidColorPaint(new SKColor(0, 0, 0), 1)
            }
        };
    }

    private static ISeries[] CreateLineSeries(
        ObservableCollection<ObservablePoint> salePoints,
        ObservableCollection<ObservablePoint> expensePoints)
    {
        return new ISeries[]
        {
            new LineSeries<ObservablePoint>
            {
                Name = "Ventas",
                Values = salePoints,
                GeometrySize = 5,
                Stroke = new SolidColorPaint(new SKColor(255, 0, 190), 1),
                GeometryFill = new SolidColorPaint(new SKColor(255, 0, 190), 0),
                Fill = null,
                GeometryStroke = new SolidColorPaint(new SKColor(255, 0, 190), 0)
            },
            new LineSeries<ObservablePoint>
            {
                Name = "Gastos",
                Values = expensePoints,
                GeometrySize = 5,
                Stroke = new SolidColorPaint(new SKColor(0, 0, 0), 1),
                GeometryFill = new SolidColorPaint(new SKColor(0, 0, 0), 0),
                Fill = null,
                GeometryStroke = new SolidColorPaint(new SKColor(0, 0, 0), 0)
            }
        };
    }

    private static ISeries[] CreateDefaultSeries(
        ObservableCollection<ObservablePoint> salePoints,
        ObservableCollection<ObservablePoint> expensePoints)
    {
        return CreateColumnSeries(salePoints, expensePoints, 4); // Default to column series
    }

    private static Axis[] CreateLastSevenDaysXAxis()
    {
        return new Axis[]
        {
            new()
            {
                Labeler = value =>
                {
                    try
                    {
                        var date = DateTime.FromOADate(value);
                        return date.ToString("dddd", new CultureInfo("es-ES"));
                    }
                    catch
                    {
                        return "";
                    }
                },
                UnitWidth = TimeSpan.FromDays(1).TotalDays,
                MinStep = TimeSpan.FromDays(1).TotalDays,
                TextSize = 14,
                LabelsRotation = 0,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                SeparatorsAtCenter = false,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                ForceStepToMin = true,
                TicksAtCenter = true,
                Position = AxisPosition.Start
            }
        };
    }

    private static Axis[] CreateLastMonthXAxis()
    {
        return new Axis[]
        {
            new()
            {
                Labeler = value => DateTime.FromOADate(value).ToString("dd-ddd-MMM"),
                UnitWidth = TimeSpan.FromDays(1).TotalDays,
                MinStep = TimeSpan.FromDays(7).TotalDays,
                TicksPaint = new SolidColorPaint(new SKColor(115, 115, 115)),
                LabelsRotation = 90,
                TextSize = 12,
                ShowSeparatorLines = true,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200), 0.9f),
                SeparatorsAtCenter = false,
                TicksAtCenter = true,
                SubseparatorsCount = 2
            }
        };
    }

    private static Axis[] CreateLastThreeMonthsXAxis()
    {
        return new Axis[]
        {
            new()
            {
                Labeler = value => DateTime.FromOADate(value).ToString("dd-ddd-MMM"),
                UnitWidth = TimeSpan.FromDays(1).TotalDays,
                MinStep = TimeSpan.FromDays(7).TotalDays,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                LabelsRotation = 90,
                TextSize = 12,
                ShowSeparatorLines = true,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200), 0.5f),
                SeparatorsAtCenter = true,
                SubseparatorsCount = 2
            }
        };
    }

    private static Axis[] CreateDefaultXAxis()
    {
        return CreateLastSevenDaysXAxis(); // Default to last seven days
    }
}