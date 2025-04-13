using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.Core.aplication.Management;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SkiaSharp;
using System.Windows;
using ElysSalon2._0.adapters.InBound.UI.views.AdminViews;
using ElysSalon2._0.Core.domain.Entities;
using Wpf.Ui.Input;
using Window = System.Windows.Window;
using System.Collections.ObjectModel;
using System.Globalization;
using ElysSalon2._0.Core.aplication.DTOs.DTOSales;

namespace ElysSalon2._0.adapters.InBound.UI.ViewModels;

public class ChartsViewModel
{
    private Window _window;
    private WindowsManager _winManager;
    public ICommand ExitCommand { get; }

    List<String> Last7daysLabels;
    private ObservableCollection<DtoSalesList> _salesCollection;
    private ObservableCollection<DtoSalesList> _ticketCollection;


    public ISeries[] Last7DaysSeries { get; }

    public Axis[] Last7DaysXAxes { get; set; }

    public ISeries[] LastMonthSeries { get; set; }

    public Axis[] LastMonthXAxes { get; set; }

    public ISeries[] LastYearSeries { get; set; }
    public Axis[] LastYearXAxes { get; set; }

    public ChartsViewModel(Window window, WindowsManager winManager, ObservableCollection<DtoSalesList> salesCollection,
        ObservableCollection<DtoSalesList> ticketCollection)
    {
        _winManager = winManager;
        _window = window;

        ExitCommand = new RelayCommand(Exit);

        _salesCollection = salesCollection;
        _ticketCollection = ticketCollection;

        Last7DaysSeries = LoadLast7Days();
        // LoadLast7Days(salesCollection, ticketCollection);
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
            new ColumnSeries<decimal>
            {
                Name = "Ventas",
                Values = _salesCollection.Where(x => x.Date > DateTime.Now.AddMonths(-1)).Select(x => x.TotalAmount)
                    .ToList(),
            },
            new ColumnSeries<decimal>
            {
                Name = "Tickets",
                Values = _ticketCollection.Where(x => x.Date > DateTime.Now.AddMonths(-1)).Select(x => x.TotalAmount)
                    .ToList(),
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
        ISeries[] series =
        [
            new ColumnSeries<decimal>
            {
                Name = "Ventas",
                Values = _salesCollection.Where(x => x.Date > DateTime.Now.AddDays(-7).Date).Select(x => x.TotalAmount)
                    .ToList()
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
        MessageBox.Show(DateTime.Now.AddDays(-7).Date.ToString());
        LoadLast7DayLabels(_ticketCollection.Where(x => x.Date >= DateTime.Now.AddDays(-7)).ToList());
        return series;
    }

    public void LoadLast7DayLabels(List<DtoSalesList> list)
    {
        Last7daysLabels = new List<string>();
        var i = 0;
        Last7daysLabels.Add(sale.Day);
        if (list.Count >= 0)
        {
            foreach (var sale in list)
            {
                /*
                 *
                 *CHRISTIAN DEL FUTURO: TENES QUE TOMAR EL DIA DEL ULTIMO DIA DE LOS 7
                 *DESPUES Q TENGAS EL DIA CON UN BUCLE GENERA LOS RESTANTES 6 DIAS,
                 *
                 *
                 * AGREGAS UN FILTRADO DE GRAFICAS PARA VER DE UN ANIO, MES, TRIMESTRE CON UNA
                 * GRAFICA LINEAL,
                 *
                 * NO OLVIDES CAMBIAR TICKET POR GASTO.
                 *
                 * HACER UN TOP DE PRODUCTOS Y SERVICIOS MAS SOLICITADOS
                 *
                 *
                 */
            }
        }
        else
        {
            MessageBox.Show("No hay ventas en los ultimos 7 dias");
        }
    }


    public void Exit()
    {
        _winManager.CloseCurrentWindowandShowWindow<SalesWindow>(_window);
    }
}