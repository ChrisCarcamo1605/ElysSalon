using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.adapters.InBound.UI.views.AdminViews;
using ElysSalon2._0.Core.aplication.DTOs.DTOSales;
using ElysSalon2._0.Core.aplication.Management;
using ElysSalon2._0.Core.aplication.Ports.Repositories;
using ElysSalon2._0.Core.domain.Entities;
using ElysSalon2._0.Core.domain.Services;

namespace ElysSalon2._0.adapters.InBound.UI.ViewModels;

public class SalesViewModel : INotifyPropertyChanged
{
    private readonly ISalesRepository _saleRepo;
    private readonly ITicketRepository _ticketRepo;
    private readonly Window _window;
    private readonly WindowsManager _winManager;
    private readonly SalesService _service;
    //Where saves our filters options
    private ObservableCollection<KeyValuePair<FilterSales, string>>? _filterOptions;

    public ObservableCollection<Sales> _salesCollection;
    private ObservableCollection<Ticket> _ticketsCollection;

    public ObservableCollection<Ticket> TicketsCollection
    {
        get => _ticketsCollection;
        set
        {
            SetField(ref _ticketsCollection, value);
            OnPropertyChanged();
        }
    }

    private ICollectionView _collectionView;

    private DateTime _fromDate = DateTime.Now.AddMonths(-1);

    public DateTime FromDate
    {
        get => _fromDate;

        set
        {
            SetField(ref _fromDate, value);
            OnPropertyChanged(nameof(FromDate));
            ApplyFilter();
        }
    }

    private DateTime _untilDate = DateTime.Now;

    public DateTime UntilDate
    {
        get => _untilDate;
        set
        {
            SetField(ref _untilDate, value);
            OnPropertyChanged(nameof(FromDate));

            ApplyFilter();
        }
    }

    //Binding to ComboBox
    private KeyValuePair<FilterSales, string> _selectedFilter;

    public SalesViewModel(ISalesRepository saleRepo, Window window, WindowsManager windowsManager,
        ITicketRepository TicketRepo, SalesService service)
    {
        _saleRepo = saleRepo;
        _ticketRepo = TicketRepo;
        _service = service;
        _winManager = windowsManager;
        _salesCollection = [];
        _ticketsCollection = [];

        _collectionView = CollectionViewSource.GetDefaultView(_ticketsCollection);
        _window = window;

        InitializeFilterOptions();

        SaveCommand = new AsyncRelayCommand(SaveVenta);
        ExitCommand = new RelayCommand(Exit);
        GenerateReportCommand = new RelayCommand(GenerateReport);
        _ = GetSales();
    }

    public ObservableCollection<KeyValuePair<FilterSales, string>>? FilterOptions
    {
        get => _filterOptions;
        set
        {
            SetField(ref _filterOptions, value);
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Sales> SalesCollection
    {
        get
        {
            MessageBox.Show("get sales");

            return _salesCollection;
        }
        set
        {
            SetField(ref _salesCollection, value);
            OnPropertyChanged();
        }
    }

    public ICollectionView SalesView
    {
        get => _collectionView;
        set
        {
            SetField(ref _collectionView, value);
            OnPropertyChanged();
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand ExitCommand { get; }
    public ICommand GenerateReportCommand { get; }

    public KeyValuePair<FilterSales, string> SelectedFilter
    {
        get => _selectedFilter;
        set
        {
            SetField(ref _selectedFilter, value);
            ApplyFilter();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;


    //Load all options in FilterOptions
    private void InitializeFilterOptions()
    {
        //It saves in ObservableCollection<KeyValuePair<...> to have a key to use and a value to show on front end
        _filterOptions = new ObservableCollection<KeyValuePair<FilterSales, string>>
        {
            new(FilterSales.Todo, "Todo"),
            new(FilterSales.Ultimos7Dias, "Últimos 7 días"),
            new(FilterSales.UltimoMes, "Último mes"),
            new(FilterSales.Ultimos3Meses, "Últimos 3 meses")
        };

        _selectedFilter = _filterOptions[0];
    }

    public async Task GetSales()
    {
        try
        {
            var tickets = await _ticketRepo.GetTicketsAsync();
            var sales = await _saleRepo.GetSales();

            _salesCollection.Clear();
            _ticketsCollection.Clear();

            foreach (var item in sales) _salesCollection.Add(item);
            foreach (var item in tickets) _ticketsCollection.Add(item);

            ApplyFilter();
            OnPropertyChanged(nameof(SalesView));
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar ventas: {ex.Message}");
        }
    }

    private async void GenerateMonthReport()
    {
        _service.GenerateMonthReport(_salesCollection);
        MessageBox.Show("Reporte Generado");
    }

    private async void GenerateReport()
    {
        var result = await _service.GenerateReport(FromDate,UntilDate,_ticketsCollection,(x=>x.EmissionDateTime),x=> x.TotalAmount);
        MessageBox.Show(result.Message);
    }



    private void ApplyFilter()
    {
        _collectionView.Filter = items =>
        {
            var ticket = items as Ticket;
            return ticket != null && ticket.EmissionDateTime >= FromDate && ticket.EmissionDateTime <= UntilDate;
        };


        //_saleView.Filter = items =>
        //{
        //    var sale = items as Sales;
        //    return sale != null && sale.SaleDate >= FromDate && sale.SaleDate <= UntilDate;
        //};


        //var now = DateTime.Now;

        //switch (_selectedFilter.Key)
        //{
        //    case FilterSales.Ultimos7Dias:

        //        var sevenDaysAgo = now.AddDays(-7);
        //        _saleView.Filter = item =>
        //        {
        //            var sale = item as Sales;
        //            return sale != null && sale.SaleDate >= sevenDaysAgo;
        //        };

        //        break;

        //    case FilterSales.UltimoMes:
        //        var oneMonthAgo = now.AddMonths(-1);
        //        _saleView.Filter = item =>
        //        {
        //            var sale = item as Sales;
        //            return sale != null && sale.SaleDate >= oneMonthAgo;
        //        };
        //        break;

        //    case FilterSales.Ultimos3Meses:
        //        var threeMonthsAgo = now.AddMonths(-3);
        //        _saleView.Filter = item =>
        //        {
        //            var sale = item as Sales;
        //            return sale != null && sale.SaleDate >= threeMonthsAgo;
        //        };
        //        break;

        //    case FilterSales.Todo:
        //    default:
        //        _saleView.Filter = null;
        //        break;
        //}

        _collectionView.Refresh();
    }


    public async Task SaveVenta()
    {
        await _saleRepo.SavesSale(SalesCollection);
        MessageBox.Show("Venta creada correctamente");
        await GetSales();
    }

    public void Exit()
    {
        _winManager.CloseCurrentWindowandShowWindow<AdminWindow>(_window);
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