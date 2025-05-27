using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Application.DTOs.Request.SalesData;
using Application.DTOs.Response.TicketDetails;
using Application.Enums;
using Application.Services;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.Factories;
using ElysSalon2._0.WinManagement;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.VisualElements;
using SkiaSharp;
using Window = System.Windows.Window;

namespace ElysSalon2._0.ViewModels;

public class ChartsViewModel : INotifyPropertyChanged
{
    private readonly ObservableCollection<DTOSalesData> _expensesCollection;

    private readonly Random _random = new();
    private readonly ObservableCollection<DTOSalesData> _salesCollection;
    private readonly ObservableCollection<DTOSalesData> _ticketDetailsCollection;
    private readonly Window _window;

    private ObservableCollection<DTOGetBestSellersTickDet> _bestArticlesSeller;

    private ObservableCollection<DTOGetBestSellersTickDet> _bestServicesSeller;

    private string _earnText;

    private ISeries[] _generalSeries;

    private Axis[] _generalXAxis;

    private Axis[] _generalYAxis;

    private ObservableCollection<KeyValuePair<RangeFilter, string>>? _rangeOptions;

    private KeyValuePair<RangeFilter, string>? _selectedFilter;

    public ISeries[] _series;
    private ObservableCollection<DTOSalesData> _ticketCollection;
    private WindowsManager _winManager;

    private Axis[] _xLabels;

    private Axis[] _yLabels;
    private List<string> Last7daysLabels;
    private ObservableCollection<KeyValuePair<RangeFilter, int>> OrderBy;


    public ChartsViewModel(Window window, WindowsManager winManager, ObservableCollection<DTOSalesData> salesCollection,
        ObservableCollection<DTOSalesData> ticketCollection, ObservableCollection<DTOSalesData> expensesCollection
        , ObservableCollection<DTOSalesData> ticketDetailsCollection)
    {
        InitializeRangeOptions();
        _winManager = winManager;
        _window = window;

        _salesCollection = salesCollection;
        _ticketCollection = ticketCollection;
        _expensesCollection = expensesCollection;
        _ticketDetailsCollection = ticketDetailsCollection;

        _bestArticlesSeller = new ObservableCollection<DTOGetBestSellersTickDet>();
        _bestServicesSeller = new ObservableCollection<DTOGetBestSellersTickDet>();
        _ = GenerateTopArticleServices();

        _expensesCollection = LoadEmptyDates(_expensesCollection);
        _salesCollection = LoadEmptyDates(_salesCollection);

        LoadAllSales();
        ApplyFilter();
        ExitCommand = new RelayCommand(Exit);
    }

    public ICommand ExitCommand { get; }

    public ObservableCollection<KeyValuePair<RangeFilter, string>>? RangeOptions
    {
        get => _rangeOptions;
        set
        {
            SetField(ref _rangeOptions, value);
            OnPropertyChanged();
        }
    }

    public KeyValuePair<RangeFilter, string>? SelectedFilter
    {
        get => _selectedFilter;
        set
        {
            SetField(ref _selectedFilter, value);
            OnPropertyChanged();
            ApplyFilter();
        }
    }

    public ObservableCollection<DTOGetBestSellersTickDet> BestArticlesSeller
    {
        get => _bestArticlesSeller;
        set
        {
            SetField(ref _bestArticlesSeller, value);
            OnPropertyChanged();
        }
    }

    public ObservableCollection<DTOGetBestSellersTickDet> BestServicesSeller
    {
        get => _bestServicesSeller;
        set
        {
            SetField(ref _bestServicesSeller, value);
            OnPropertyChanged();
        }
    }

    public ISeries[] Series
    {
        get => _series;
        set
        {
            SetField(ref _series, value);
            OnPropertyChanged();
        }
    }

    public Axis[] GeneralYAxis
    {
        get => _generalYAxis;
        set
        {
            SetField(ref _generalYAxis, value);
            OnPropertyChanged();
        }
    }

    public Axis[] GeneralXAxis
    {
        get => _generalXAxis;
        set
        {
            SetField(ref _generalXAxis, value);
            OnPropertyChanged();
        }
    }

    public ISeries[] GeneralSeries
    {
        get => _generalSeries;
        set
        {
            SetField(ref _generalSeries, value);
            OnPropertyChanged();
        }
    }

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

    public string EarnText
    {
        get => _earnText;
        set
        {
            SetField(ref _earnText, value);
            OnPropertyChanged();
        }
    }

    public IEnumerable<ISeries> PieSeries { get; set; }
    public IEnumerable<VisualElement> VisualElements { get; set; }
    public NeedleVisual Needle { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void LoadAllSales()
    {
        _generalYAxis = ChartAxisFactory.CreateYAxis();
        _generalXAxis = ChartAxisFactory.CreateGeneralXAxis();
        _generalSeries = ChartAxisFactory.CreateGeneralLineSeries(_salesCollection, _expensesCollection);
    }

    private void ApplyFilter()
    {
        _ = GenerateTopArticleServices();

        XLabels = ChartAxisFactory.CreateXAxis(_selectedFilter.Value.Key);
        YLabels = ChartAxisFactory.CreateYAxis();
        Series = ChartAxisFactory.CreateChartSeries(_salesCollection, _expensesCollection, _selectedFilter.Value.Key);


        InitializePieChart(
            FilterByRangeService.GetTotalFrom(_salesCollection, _selectedFilter.Value.Key),
            FilterByRangeService.GetTotalFrom(_expensesCollection, _selectedFilter.Value.Key));
    }


    public void InitializePieChart(decimal totalVentas, decimal totalGastos)
    {
        var sectionsOuter = 130;
        var sectionsWidth = 20;
        var ExpenseSaleDiference = totalVentas - totalGastos;

        // Calcular la rentabilidad real (porcentaje)
        double rentabilidad = 0;
        if (totalVentas > 0)
        {
            // Calculamos la rentabilidad como porcentaje
            var rawRentabilidad = ExpenseSaleDiference / totalVentas * 100;
            // Usamos el valor real directamente, limitado al rango [0-100]
            rentabilidad = (double)Math.Clamp(rawRentabilidad, 0, 100);
        }

        // Actualizamos la aguja con el valor real
        Needle = new NeedleVisual
        {
            Value = rentabilidad
        };

        // Mantenemos las secciones del gauge
        PieSeries = GaugeGenerator.BuildAngularGaugeSections(
            new GaugeItem(30, s => SetStyle(sectionsOuter, sectionsWidth, s, new SKColor(255, 92, 92))),
            new GaugeItem(30, s => SetStyle(sectionsOuter, sectionsWidth, s, new SKColor(92, 133, 255))),
            new GaugeItem(40, s => SetStyle(sectionsOuter, sectionsWidth, s, new SKColor(92, 255, 125)))
        );

        VisualElements =
        [
            new AngularTicksVisual
            {
                Labeler = value => value.ToString("N1") + "%",
                LabelsSize = 13,
                LabelsOuterOffset = 15,
                OuterOffset = 65,
                TicksLength = 10
            },
            Needle
        ];


        _earnText = ExpenseSaleDiference <= 0
            ? ExpenseSaleDiference.ToString()
            : "+" + ExpenseSaleDiference;

        // Notificar cambios para actualización de UI
        OnPropertyChanged(nameof(Needle));
        OnPropertyChanged(nameof(PieSeries));
        OnPropertyChanged(nameof(VisualElements));
        OnPropertyChanged(nameof(EarnText));
    }


    private static void SetStyle(
        double sectionsOuter, double sectionsWidth, PieSeries<ObservableValue> series, SKColor color)
    {
        series.OuterRadiusOffset = sectionsOuter;
        series.MaxRadialColumnWidth = sectionsWidth;
        series.CornerRadius = 0;
        series.Fill = new SolidColorPaint(color);
    }


    public async Task GenerateTopArticleServices()
    {
        _bestArticlesSeller.Clear();
        _bestServicesSeller.Clear();

        var startDate = _selectedFilter.Value.Key switch
        {
            RangeFilter.LastSevenDays => DateTime.Now.AddDays(-7),
            RangeFilter.LastMonth => DateTime.Now.AddMonths(-1).Date,
            RangeFilter.LastThreeMonths => DateTime.Now.AddMonths(-3).Date,
            _ => DateTime.MinValue
        };

        var filteredItems = _ticketDetailsCollection
            .Where(item => item.Date >= startDate);

        var groupedResults = filteredItems.GroupBy(x => new
        {
            x.article,
            TypeId = x.article.ArticleTypeId
        }).Select(x => new
        {
            x.Key.article.Name,
            x.Key.TypeId,
            TotalAmount = x.Sum(y => y.TotalAmount)
        }).OrderByDescending(x => x.TotalAmount);


        foreach (var item in groupedResults)
        {
            var dto = new DTOGetBestSellersTickDet(item.Name, item.TotalAmount);

            if (item.TypeId == 4)
                _bestServicesSeller.Add(dto);
            else
                _bestArticlesSeller.Add(dto);
        }
    }


    private void InitializeRangeOptions()
    {
        _rangeOptions = new ObservableCollection<KeyValuePair<RangeFilter, string>>
        {
            new(RangeFilter.LastSevenDays, "Ultimos 7 dias"),
            new(RangeFilter.LastMonth, "Ultimos 30 días"),
            new(RangeFilter.LastThreeMonths, "Ultimos 90 días")
        };

        _selectedFilter = _rangeOptions[0];
    }

    public ObservableCollection<DTOSalesData> LoadEmptyDates(ObservableCollection<DTOSalesData> collection)
    {
        var list = collection;
        var minDate = list.Min(x => x.Date);
        var maxDate = DateTime.Now.Date;


        var dates = new HashSet<DateTime>(list.Select(x => x.Date.Date)
            .OrderBy(x => x.Date.Date)
            .ToList());
        var allDates = new List<DateTime>();

        for (var i = minDate; i <= maxDate; i = i.AddDays(1)) allDates.Add(i.Date);

        foreach (var i in allDates)
            if (!dates.Contains(i.Date))
                list.Add(
                    new DTOSalesData(i.ToString("dddd", new CultureInfo("es-ES")),
                        i.Date, 0));

        return list;
    }


    public void DoRandomChange()
    {
        // modifying the Value property updates and animates the chart automatically
        Needle.Value = _random.Next(0, 100);
    }

    public void Exit()
    {
        _window.Close();
    }

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