using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SkiaSharp;
using System.Windows;
using Wpf.Ui.Input;
using Window = System.Windows.Window;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using Windows.System.Update;
using ElysSalon2._0.adapters.InBound.views;
using ElysSalon2._0.adapters.OutBound.Repositories;
using ElysSalon2._0.aplication.DTOs.DTOSales;
using ElysSalon2._0.aplication.DTOs.DTOTicketDetails;
using ElysSalon2._0.aplication.Interfaces.Services;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.domain.Entities;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.Kernel.Sketches;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ElysSalon2._0.adapters.InBound.ViewModels;

public class ChartsViewModel : INotifyPropertyChanged
{
    private Window _window;
    private WindowsManager _winManager;
    private ITicketService _ticketService;
    public ICommand ExitCommand { get; }
    private List<string> Last7daysLabels;
    private ObservableCollection<DtoSalesList> _salesCollection;
    private ObservableCollection<DtoSalesList> _ticketCollection;
    private ObservableCollection<KeyValuePair<RangeFilter, int>> OrderBy;
    private ObservableCollection<DtoSalesList> _expensesCollection;
    private ObservableCollection<TicketDetails> _ticketDetailsCollection;

    private ObservableCollection<KeyValuePair<RangeFilter, string>>? _rangeOptions;

    public ObservableCollection<KeyValuePair<RangeFilter, string>>? RangeOptions
    {
        get => _rangeOptions;
        set
        {
            SetField(ref _rangeOptions, value);
            OnPropertyChanged();
        }
    }

    private KeyValuePair<RangeFilter, String>? _selectedFilter;

    public KeyValuePair<RangeFilter, String>? SelectedFilter
    {
        get => _selectedFilter;
        set
        {
            SetField(ref _selectedFilter, value);
            OnPropertyChanged();
            ApplyFilter();
        }
    }

    private ObservableCollection<DtoBestSellerTicketDetails> _bestArticlesSeller;

    public ObservableCollection<DtoBestSellerTicketDetails> BestArticlesSeller
    {
        get => _bestArticlesSeller;
        set
        {
            SetField(ref _bestArticlesSeller, value);
            OnPropertyChanged();
        }
    }

    private ObservableCollection<DtoBestSellerTicketDetails> _bestServicesSeller;

    public ObservableCollection<DtoBestSellerTicketDetails> BestServicesSeller
    {
        get => _bestServicesSeller;
        set
        {
            SetField(ref _bestServicesSeller, value);
            OnPropertyChanged();
        }
    }

    public ISeries[] _series;

    public ISeries[] Series
    {
        get => _series;
        set
        {
            SetField(ref _series, value);
            OnPropertyChanged();
        }
    }

    private Axis[] _generalYAxis;

    public Axis[] GeneralYAxis
    {
        get => _generalYAxis;
        set
        {
            SetField(ref _generalYAxis, value);
            OnPropertyChanged();
        }
    }

    private Axis[] _generalXAxis;

    public Axis[] GeneralXAxis
    {
        get => _generalXAxis;
        set
        {
            SetField(ref _generalXAxis, value);
            OnPropertyChanged();
        }
    }

    private ISeries[] _generalSeries;

    public ISeries[] GeneralSeries
    {
        get => _generalSeries;
        set
        {
            SetField(ref _generalSeries, value);
            OnPropertyChanged();
        }
    }

    private Axis[] _xLabels;

    public Axis[] XLabels
    {
        get => _xLabels;
        set
        {
            if (_xLabels != value)
            {
                _xLabels = value;
                OnPropertyChanged();
            }
        }
    }

    private Axis[] _yLabels;

    public Axis[] YLabels
    {
        get => _yLabels;
        set
        {
            if (_yLabels != value)
            {
                _yLabels = value;
                OnPropertyChanged();
            }
        }
    }

    public ISeries[] LastYearSeries { get; set; }
    public Axis[] LastYearXAxes { get; set; }
    public IEnumerable<ISeries> PieSeries { get; set; }


    public ChartsViewModel(Window window, WindowsManager winManager, ObservableCollection<DtoSalesList> salesCollection,
        ObservableCollection<DtoSalesList> ticketCollection, ObservableCollection<DtoSalesList> expensesCollection
        , ObservableCollection<TicketDetails> ticketDetailsCollection)
    {
        InitializeRangeOptions();
        _winManager = winManager;
        _window = window;

        _salesCollection = salesCollection;
        _ticketCollection = ticketCollection;
        _expensesCollection = expensesCollection;
        _ticketDetailsCollection = ticketDetailsCollection;

        _bestArticlesSeller = new ObservableCollection<DtoBestSellerTicketDetails>();
        _bestServicesSeller = new ObservableCollection<DtoBestSellerTicketDetails>();
        _ = GenerateTopArticleServices();

        LoadAllSales();
        _expensesCollection = LoadEmptyDates(_expensesCollection);
        _salesCollection = LoadEmptyDates(_salesCollection);
        ApplyFilter();

        ExitCommand = new RelayCommand(Exit);
    }


    private ISeries[] LoadLastMonth()
    {
        var expenses = new ObservableCollectionListSource<ObservablePoint>();
        var sales = new ObservableCollectionListSource<ObservablePoint>();


        var lastMonthSales = _salesCollection
            .Where(x => x.Date.Date >= DateTime.Now.AddDays(-30).Date)
            .GroupBy(x => x.Date.Date)
            .Select(x => new
            {
                Date = x.Key,
                TotalAmount = x.Sum(x => x.TotalAmount)
            })
            .OrderBy(x => x.Date)
            .ToList();

        var lastMonthExpenses = _expensesCollection
            .Where(x => x.Date.Date >= DateTime.Now.AddDays(-30).Date)
            .GroupBy(x => x.Date.Date)
            .Select(x => new
            {
                Date = x.Key,
                TotalAmount = x.Sum(x => x.TotalAmount)
            })
            .OrderBy(x => x.Date)
            .ToList();


        foreach (var venta in lastMonthSales)
        {
            sales.Add(new ObservablePoint(venta.Date.ToOADate(), (double)venta.TotalAmount));
        }

        foreach (var ticket in lastMonthExpenses)
        {
            expenses.Add(new ObservablePoint(ticket.Date.ToOADate(), (double)ticket.TotalAmount));
        }

        ISeries[] series =
        [
            new LineSeries<ObservablePoint>()
            {
                Values = sales,
                GeometrySize = 10,
                Name = "Ventas",
                Stroke = new SolidColorPaint(new SKColor(255, 0, 190), 1),
                GeometryFill = new SolidColorPaint(new SKColor(255, 0, 190)),
                GeometryStroke = new SolidColorPaint(new SKColor(255, 0, 190), 0),
                Fill = null,
                LineSmoothness = 0
            },
            new LineSeries<ObservablePoint>()
            {
                Values = expenses,
                GeometrySize = 10,
                Name = "Gastos",
                Stroke = new SolidColorPaint(new SKColor(0, 0, 0), 1),
                GeometryFill = new SolidColorPaint(new SKColor(0, 0, 0)),
                GeometryStroke = new SolidColorPaint(new SKColor(0, 0, 0), 0),
                Fill = null,
                LineSmoothness = 0
            }
        ];
        return series;
    }


    public void LoadAllSales()
    {
        var lastYearSales = _salesCollection
            .Where(x => x.Date > DateTime.Now.AddYears(-1))
            .OrderBy(x => x.Date)
            .ToList();

        var puntos = new ObservableCollection<ObservablePoint>();

        foreach (var venta in lastYearSales)
        {
            puntos.Add(new ObservablePoint(venta.Date.ToOADate(), (double)venta.TotalAmount));
        }


        _generalYAxis = new Axis[]
        {
            new Axis
            {
                Name = "Total",
                Position = AxisPosition.Start,
                ShowSeparatorLines = true,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200), strokeWidth: 0.5f),
                SeparatorsAtCenter = true,
                SubseparatorsCount = 2
            }
        };

        _generalXAxis = new Axis[]
        {
            new Axis
            {
                Labeler = value => DateTime.FromOADate(value).ToString("dd-MM"),
                UnitWidth = TimeSpan.FromDays(1).TotalDays,
                MinStep = TimeSpan.FromDays(7).TotalDays,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                LabelsRotation = 90,
                TextSize = 12, ShowSeparatorLines = true,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200), strokeWidth: 0.5f),
                // Opcional: Controla el estilo de las líneas de cuadrícula
                SeparatorsAtCenter = true,
                SubseparatorsCount = 2
            }
        };

        _generalSeries = new ISeries[]
        {
            new LineSeries<ObservablePoint>
            {
                Name = "Ventas",
                Values = puntos,
                GeometrySize = 10,
                Stroke = new SolidColorPaint(new SKColor(255, 0, 190), 1),
                GeometryFill = new SolidColorPaint(new SKColor(255, 0, 190), 0),
                Fill = null,

                GeometryStroke = new SolidColorPaint(new SKColor(255, 0, 190), 0),
            }
        };
    }


    private void LoadLastYear(ObservableCollection<DtoSalesList> sales,
        ObservableCollection<DtoSalesList> tickets)
    {
        var lastYearSales = new ObservableCollection<DtoSalesList>(
            sales.Where(x => x.Date > DateTime.Now.AddYears(-1)));
        var lastYearTickets = new ObservableCollection<DtoSalesList>(
            new ObservableCollection<DtoSalesList>(
                tickets.Where(x => x.Date > DateTime.Now.AddYears(-1))));
    }


    private ISeries[] LoadLast7Days()
    {
        ISeries[] series =
        [
            new ColumnSeries<decimal>
            {
                Name = "Ventas",
                Values = _salesCollection.Where(x => x.Date.Date > DateTime.Now.AddDays(-7).Date)
                    .GroupBy(x => x.Date).OrderBy(x => x.Key)
                    .Select(x => x.Sum(x => x.TotalAmount)).ToList(),
                Fill = new SolidColorPaint(new SKColor(255, 0, 190), 1)
            },
            new ColumnSeries<decimal>
            {
                Name = "Gastos",
                Values = _expensesCollection.Where(x => x.Date.Date > DateTime.Now.AddDays(-7).Date)
                    .GroupBy(x => x.Date.Date).OrderBy(x => x.Key)
                    .Select(x => x.Sum(ticket => ticket.TotalAmount)
                    )
                    .ToList(),
                Fill = new SolidColorPaint(new SKColor(0, 0, 0),
                    1)
            }
        ];
        return series;
    }

    public Axis[] LoadLast7DayXAxis(List<DtoSalesList> list)
    {
        Last7daysLabels = new List<String>();
        var Xlabels = new List<String>();

        for (var i = 0; i < 7; i++)
        {
            Xlabels.Add(list[i].Date.ToString("dddd", new CultureInfo("es-ES")));
        }

        return
            new Axis[]
            {
                new Axis
                {
                    Labels = Xlabels,
                    UnitWidth = 1,
                    TextSize = 12,
                    LabelsRotation = 0,
                    SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                    SeparatorsAtCenter = false,
                    TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                    TicksAtCenter = true,
                    ForceStepToMin = true,
                    Position = AxisPosition.Start,
                }
            };
    }


    public Axis[] LoadLastMontHXAxis()
    {
        return new Axis[]
        {
            new Axis
            {
                Labeler = value => DateTime.FromOADate(value).ToString("dd-MM"),
                UnitWidth = TimeSpan.FromDays(1).TotalDays,
                MinStep = TimeSpan.FromDays(7).TotalDays,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                LabelsRotation = 90,
                TextSize = 12, ShowSeparatorLines = true,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200), strokeWidth: 0.5f),
                // Opcional: Controla el estilo de las líneas de cuadrícula
                SeparatorsAtCenter = true,
                SubseparatorsCount = 2
            }
        };
    }

    public Axis[] LoadLastMontHYAxis()
    {
        return new Axis[]
        {
            new Axis
            {
                Name = "Total",
                Position = AxisPosition.Start,
                ShowSeparatorLines = true,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200), strokeWidth: 0.5f),
                SeparatorsAtCenter = true,
                SubseparatorsCount = 2
            }
        };
    }

    public Axis[] LoadLastThreeMonth()
    {
        return null;
    }


    public async Task GenerateTopArticleServices()
    {
        _bestArticlesSeller.Clear();
        _bestServicesSeller.Clear();
        var items = _ticketDetailsCollection;

        var groupedResult = items
            .GroupBy(x => x.ArticleName)
            .Select(g => new
            {
                Name = g.Key,
                TotalAmount = g.Sum(x => x.TotalPrice),
                ArticleTypeId = g.First().Article.ArticleTypeId,
                Date = g.First().Date
            });

        List<DtoBestSellerTicketDetails> services = new List<DtoBestSellerTicketDetails>();
        List<DtoBestSellerTicketDetails> articles = new List<DtoBestSellerTicketDetails>();

        DateTime startDate;

        switch (_selectedFilter.Value.Key)
        {
            case RangeFilter.LastSevenDays:
                startDate = DateTime.Now.AddDays(-7);
                break;
            case RangeFilter.LastMonth:
                startDate = DateTime.Now.AddMonths(-1).Date;
                break;
            case RangeFilter.LastThreeMonths:
                startDate = DateTime.Now.AddMonths(-3).Date;
                break;
            default:
                startDate = DateTime.MinValue; // O define un comportamiento por defecto
                break;
        }

        var filteredItems = groupedResult.Where(x => x.Date >= startDate).OrderByDescending(x => x.TotalAmount)
            .ToList();

        services = filteredItems
            .Where(x => x.ArticleTypeId == 4)
            .Select(x => new DtoBestSellerTicketDetails(x.Name, x.TotalAmount))
            .ToList();

        articles = filteredItems
            .Where(x => x.ArticleTypeId != 4)
            .Select(x => new DtoBestSellerTicketDetails(x.Name, x.TotalAmount))
            .ToList();

        foreach (var service in services)
        {
            _bestServicesSeller.Add(new DtoBestSellerTicketDetails(service.Name, service.TotalAmount));
        }

        foreach (var article in articles)
        {
            _bestArticlesSeller.Add(new DtoBestSellerTicketDetails(article.Name, article.TotalAmount));
        }
    }

    private void ApplyFilter()
    {
        _ = GenerateTopArticleServices();
        LoadAllSales();

        switch (_selectedFilter.Value.Key)
        {
            case RangeFilter.LastSevenDays:
                XLabels = LoadLast7DayXAxis(_salesCollection.OrderBy(x => x.Date)
                    .Where(x => x.Date.Date > DateTime.Now.Date.AddDays(-7))
                    .ToList());
                Series = LoadLast7Days();
                break;

            case RangeFilter.LastMonth:
                XLabels = LoadLastMontHXAxis();
                YLabels = LoadLastMontHYAxis();

                Series = LoadLastMonth();
                break;
            case RangeFilter.LastThreeMonths:
                Series = LoadLastMonth();
                XLabels = LoadLastThreeMonth();
                break;
        }
    }

    private void InitializeRangeOptions()
    {
        _rangeOptions = new ObservableCollection<KeyValuePair<RangeFilter, string>>
        {
            new KeyValuePair<RangeFilter, string>(RangeFilter.LastSevenDays, "Ultimos 7 dias"),
            new KeyValuePair<RangeFilter, string>(RangeFilter.LastMonth, "Ultimo mes"),
            new KeyValuePair<RangeFilter, string>(RangeFilter.LastThreeMonths, "Ultimos 3 meses")
        };

        _selectedFilter = _rangeOptions[0];
    }

    public ObservableCollection<DtoSalesList> LoadEmptyDates(ObservableCollection<DtoSalesList> collection)
    {
        var list = collection;
        var minDate = list.Min(x => x.Date);
        var maxDate = DateTime.Now.Date;

        var dates = new HashSet<DateTime>(list.Select(x => x.Date.Date)
            .OrderBy(x => x.Date.Date)
            .ToList());
        var allDates = new List<DateTime>();

        for (var i = minDate; i <= maxDate; i = i.AddDays(1))
        {
            allDates.Add(i.Date);
        }

        foreach (var i in allDates)
        {
            if (!dates.Contains(i.Date))
            {
                list.Add(
                    new DtoSalesList(i.ToString("dddd", new CultureInfo("es-ES")),
                        i.Date, 0));
            }
        }

        return list;
    }


    public void Exit()
    {
        _window.Close();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}