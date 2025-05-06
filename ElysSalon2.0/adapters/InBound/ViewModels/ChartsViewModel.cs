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
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using ElysSalon2._0.adapters.InBound.views;
using ElysSalon2._0.adapters.OutBound.Repositories;
using ElysSalon2._0.aplication.DTOs.DTOSales;
using ElysSalon2._0.aplication.DTOs.DTOTicketDetails;
using ElysSalon2._0.aplication.Interfaces.Services;
using ElysSalon2._0.aplication.Management;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.Kernel.Sketches;
using ElysSalon2._0.domain.Entities;

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
        ObservableCollection<DtoSalesList> ticketCollection, ITicketService ticketService)
    {
        InitializeRangeOptions();
        _winManager = winManager;
        _window = window;
        _ticketService = ticketService;

        _salesCollection = salesCollection;
        _ticketCollection = ticketCollection;

        _bestArticlesSeller = new ObservableCollection<DtoBestSellerTicketDetails>();
        _bestServicesSeller = new ObservableCollection<DtoBestSellerTicketDetails>();
        _ = GenerateTopArticleServices();

        LoadAllSales();
        ApplyFilter();
        ExitCommand = new RelayCommand(Exit);
    }


    private ISeries[] LoadLastMonth(ObservableCollection<DtoSalesList> sales,
        ObservableCollection<DtoSalesList> tickets)
    {
        ISeries[] series =
        [
            new ColumnSeries<decimal>()
            {
                Name = "Ventas",
                Values = _salesCollection.Where(x => x.Date > DateTime.Now.AddMonths(-1).Date)
                    .Select(x => x.TotalAmount)
                    .ToList(),
                Fill = new SolidColorPaint(new SKColor(255, 0, 190), 1)
            },
            new ColumnSeries<decimal>()
            {
                Name = "Tickets",
                Values = _ticketCollection.Where(x => x.Date > DateTime.Now.AddMonths(-1)).GroupBy(x => x.Date.Date)
                    .Select(x => x.Sum(x => x.TotalAmount)).ToList()
                    .ToList(),
                Fill = new SolidColorPaint(new SKColor(0, 0, 0), 1)
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
                // Opcional: Controla el estilo de las líneas de cuadrícula
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
                LabelsRotation = 90,
                TextSize = 12,
                Name = "Fecha",
            }
        };

        _generalSeries = new ISeries[]
        {
            new LineSeries<ObservablePoint>
            {
                Values = puntos,
                GeometrySize = 10,
                Fill = null,
                Name = "Ventas",
                Stroke = new SolidColorPaint(new SKColor(255, 0, 190), 1),
                GeometryFill = new SolidColorPaint(new SKColor(255, 0, 190)),
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
                Values = _salesCollection.Where(x => x.Date > DateTime.Now.AddDays(-7).Date).Select(x => x.TotalAmount)
                    .ToList(),
                Fill = new SolidColorPaint(new SKColor(255, 0, 190), 1)
            },
            new ColumnSeries<decimal>
            {
                Name = "Tickets",
                Values = _ticketCollection.Where(x => x.Date > DateTime.Now.AddDays(-7).Date)
                    .GroupBy(x => x.Date.Date)
                    .Select(x => x.Sum(ticket => ticket.TotalAmount)
                    )
                    .ToList(),
                Fill = new SolidColorPaint(new SKColor(0, 0, 0),
                    1) //tickets.Where(x => x.Date >= DateTime.Now.AddDays(-7)).Select(X => X.TotalAmount).ToList()
            }
        ];

        return series;
    }

    public Axis[] LoadLast7DayLabels(List<DtoSalesList> list)
    {
        Last7daysLabels = new List<string>();
        var Xlabels = new List<String>();

        if (list.Count > 0)
        {
            Xlabels.Add(list[0].Date.ToString("dddd", new CultureInfo("es-ES")));
            for (var i = -6; i <= 0; i++)
                Xlabels.Add(list[0].Date.AddDays(i).Date.ToString("dddd", new CultureInfo("es-ES")));
        }
        else
            MessageBox.Show("No hay ventas en los ultimos 7 dias");

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
                }
            };
    }

    public Axis[] LoadLastMonthLabels()
    {
        var weeklyLabels = new List<string>();
        var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

        for (var weekStart = startOfMonth; weekStart <= endOfMonth; weekStart = weekStart.AddDays(7))
        {
            var weekEnd = weekStart.AddDays(6) > endOfMonth ? endOfMonth : weekStart.AddDays(6);
            weeklyLabels.Add($"{weekStart:dd/MM} - {weekEnd:dd/MM}");
        }

        return new Axis[]
        {
            new Axis
            {
                Labels = weeklyLabels,
                UnitWidth = 1,
                Name = "Semanas",
                TextSize = 12,
                LabelsRotation = 0,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                SeparatorsAtCenter = false,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                TicksAtCenter = true,
                ForceStepToMin = true,
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
        var items = await _ticketService.GetTicketDetailsAsync();

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

        var filteredItems = groupedResult.Where(x => x.Date >= startDate).OrderByDescending(x => x.TotalAmount).ToList();

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
        LoadAllSales();
        GenerateTopArticleServices();


        switch (_selectedFilter.Value.Key)
        {
            case RangeFilter.LastSevenDays:
                XLabels = LoadLast7DayLabels(_ticketCollection.Where(x => x.Date >= DateTime.Now.AddDays(-7).Date)
                    .ToList());
                Series = LoadLast7Days();

                break;

            case RangeFilter.LastMonth:

                XLabels = LoadLastMonthLabels();
                Series = LoadLastMonth(_salesCollection, _ticketCollection);
                break;
            case RangeFilter.LastThreeMonths:
                Series = LoadLastMonth(_salesCollection, _ticketCollection);
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


    public void Exit()
    {
        _winManager.CloseCurrentWindowandShowWindow<SalesWindow>(_window);
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