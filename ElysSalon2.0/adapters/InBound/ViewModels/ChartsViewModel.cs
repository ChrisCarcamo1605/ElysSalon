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
using ElysSalon2._0.adapters.InBound.views;
using ElysSalon2._0.adapters.OutBound.Repositories;
using ElysSalon2._0.aplication.DTOs.DTOSales;
using ElysSalon2._0.aplication.DTOs.DTOTicketDetails;
using ElysSalon2._0.aplication.Interfaces.Services;
using ElysSalon2._0.aplication.Management;
using LiveChartsCore.SkiaSharpView.Extensions;

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

    public ISeries[] Last7DaysSeries { get; }

    public Axis[] Last7DaysXAxes { get; set; }

    public ISeries[] LastMonthSeries { get; set; }

    public Axis[] LastMonthXAxes { get; set; }

    public ISeries[] LastYearSeries { get; set; }
    public Axis[] LastYearXAxes { get; set; }

    private static int _index = 0;
    private static string[] _names = ["Maria", "Susan", "Charles", "Fiona", "George"];

    public IEnumerable<ISeries> Series { get; set; } =
        new[] { 8, 6, 5, 3, 3 }.AsPieSeries((value, series) =>
        {
            series.Name = _names[_index++ % _names.Length];
            series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer;
            series.DataLabelsSize = 15;
            series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
            series.ToolTipLabelFormatter = point => $"{point.StackedValue!.Share:P2}";
        });


    public ChartsViewModel(Window window, WindowsManager winManager, ObservableCollection<DtoSalesList> salesCollection,
        ObservableCollection<DtoSalesList> ticketCollection, ITicketService ticketService)
    {
        _winManager = winManager;
        _window = window;
        _ticketService = ticketService;

        ExitCommand = new RelayCommand(Exit);

        _salesCollection = salesCollection;
        _ticketCollection = ticketCollection;
        _bestArticlesSeller = new ObservableCollection<DtoBestSellerTicketDetails>();
        _bestServicesSeller = new ObservableCollection<DtoBestSellerTicketDetails>();

        _ = GenerateTopArticleServices();


        Last7DaysSeries = LoadLast7Days();
        LastMonthSeries = LoadLastMonth(salesCollection, ticketCollection);


        Last7DaysXAxes =

        [
            new Axis
            {
                Labels = Last7daysLabels, // last7daysLabels,
                LabelsRotation = 0,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                SeparatorsAtCenter = false,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                TicksAtCenter = true,
                // By default the axis tries to optimize the number of 
                // labels to fit the available space, 
                // when you need to force the axis to show all the labels then you must: 
                ForceStepToMin = true,
                MinStep = 1
            }
        ];

        LastMonthXAxes =

        [
            new Axis
            {
                LabelsRotation = 0,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                SeparatorsAtCenter = false,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                TicksAtCenter = true,
                // By default the axis tries to optimize the number of 
                // labels to fit the available space, 
                // when you need to force the axis to show all the labels then you must: 
                ForceStepToMin = true,
                MinStep = 1
            }
        ];
    }


    private ISeries[] LoadLastMonth(ObservableCollection<DtoSalesList> sales,
        ObservableCollection<DtoSalesList> tickets)
    {
        ISeries[] series =
        [
            new LineSeries<decimal>()
            {
                Name = "Ventas",
                Values = _salesCollection.Where(x => x.Date > DateTime.Now.AddMonths(-1)).Select(x => x.TotalAmount)
                    .ToList(),
                Fill = null, LineSmoothness = 0
            },
            new LineSeries<decimal>()
            {
                Name = "Tickets",
                Values = _ticketCollection.Where(x => x.Date > DateTime.Now.AddMonths(-1)).GroupBy(x => x.Date.Date)
                    .Select(x => x.Sum(x => x.TotalAmount)).ToList()
                    .ToList(),
                Fill = null, LineSmoothness = 0
            }
        ];
        return series;
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
        LoadLast7DayLabels(_ticketCollection.Where(x => x.Date >= DateTime.Now.AddDays(-7).Date).ToList());
        ISeries[] series =
        [
            new ColumnSeries<decimal>
            {
                Name = "Ventas",
                Values = _salesCollection.Where(x => x.Date > DateTime.Now.AddDays(-7).Date).Select(x => x.TotalAmount)
                    .ToList()
            },
            new ColumnSeries<decimal>
            {
                Name = "Tickets",
                Values = _ticketCollection.Where(x => x.Date > DateTime.Now.AddDays(-7).Date)
                    .GroupBy(x => x.Date.Date)
                    .Select(x => x.Sum(ticket => ticket.TotalAmount)
                    )
                    .ToList() //tickets.Where(x => x.Date >= DateTime.Now.AddDays(-7)).Select(X => X.TotalAmount).ToList()
            }
        ];

        return series;
    }

    public void LoadLast7DayLabels(List<DtoSalesList> list)
    {
        Last7daysLabels = new List<string>();


        if (list.Count >= 0)
            for (var i = -6; i <= 0; i++)
                Last7daysLabels.Add(list[0].Date.AddDays(i).Date.ToString("dddd", new CultureInfo("es-ES")));
        /*
         * AGREGAS UN FILTRADO DE GRAFICAS PARA VER DE UN ANIO, MES, TRIMESTRE CON UNA
         * GRAFICA LINEAL,
         *
         * NO OLVIDES CAMBIAR TICKET POR GASTO.
         */
        else
            MessageBox.Show("No hay ventas en los ultimos 7 dias");
    }

    public async Task GetBestArticles()
    {
        var result = await _ticketService.GetTicketDetailsAsync();

        var bestArticles = result
            .GroupBy(x => x.ArticleName)
            .Select(g => new
            {
                article = g.Key,
                TotalAmount = g.Sum(x => x.TotalPrice)
            }).OrderByDescending(x => x.TotalAmount);

        foreach (var article in bestArticles)
            _bestArticlesSeller.Add(new DtoBestSellerTicketDetails(article.article, article.TotalAmount));
    }


    public async Task GetBestServices()
    {
        var result = await _ticketService.GetTicketDetailsAsync();

        var bestServices = result
            .GroupBy(x => x.ArticleName)
            .Select(g => new
            {
                service = g.Key,
                TotalAmount = g.Sum(x => x.TotalPrice)
            })
            .OrderByDescending(x => x.TotalAmount);


        foreach (var service in bestServices)
            _bestServicesSeller.Add(new DtoBestSellerTicketDetails(service.service, service.TotalAmount));
    }

    public async Task GenerateTopArticleServices()
    {
        var items = await _ticketService.GetTicketDetailsAsync();

        var services = items.Where(x => x.Article.ArticleTypeId == 4).GroupBy(x => x.ArticleName)
            .Select(g => new
            {
                service = g.Key,
                TotalAmount = g.Sum(x => x.TotalPrice)
            })
            .OrderByDescending(x => x.TotalAmount);

        var articles = items.Where(x => x.Article.ArticleTypeId != 4).GroupBy(x => x.ArticleName)
            .Select(g => new
            {
                article = g.Key,
                TotalAmount = g.Sum(x => x.TotalPrice)
            })
            .OrderByDescending(x => x.TotalAmount).ToList();


        foreach (var article in articles)
            _bestArticlesSeller.Add(new DtoBestSellerTicketDetails(article.article, article.TotalAmount));


        foreach (var service in services)
            _bestServicesSeller.Add(new DtoBestSellerTicketDetails(service.service, service.TotalAmount));
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