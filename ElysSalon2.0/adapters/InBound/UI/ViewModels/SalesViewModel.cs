using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using AutoMapper;
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


    private ObservableCollection<DtoSalesList> _ticketsCollection;
    private IMapper _mapper;


    private ObservableCollection<DtoSalesList> _dtoInfoList;

    public ObservableCollection<DtoSalesList> dtoInfoList
    {
        get => _dtoInfoList;
        set
        {
            SetField(ref _dtoInfoList, value);
            OnPropertyChanged();
        }
    }


    public ObservableCollection<DtoSalesList> TicketsCollection
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


    public ObservableCollection<KeyValuePair<FilterSales, string>>? FilterOptions
    {
        get => _filterOptions;
        set
        {
            SetField(ref _filterOptions, value);
            OnPropertyChanged();
        }
    }

    private ObservableCollection<DtoSalesList> _salesCollection;

    public ObservableCollection<DtoSalesList> SalesCollection
    {
        get => _salesCollection;

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
        get
        {
            return _selectedFilter;
           
        }
        set
        {
            SetField(ref _selectedFilter, value);
            OnPropertyChanged();
            ApplyFilter();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public SalesViewModel(ISalesRepository saleRepo, Window window, WindowsManager windowsManager,
        ITicketRepository TicketRepo, SalesService service, IMapper mapper)
    {
        _saleRepo = saleRepo;
        _ticketRepo = TicketRepo;
        _service = service;
        _winManager = windowsManager;
        _salesCollection = [];
        _ticketsCollection = [];
        _mapper = mapper;
        _window = window;


        ExitCommand = new RelayCommand(Exit);
        GenerateReportCommand = new RelayCommand(GenerateReport);
        ApplyFilter();
        _collectionView = CollectionViewSource.GetDefaultView(SalesCollection);
        InitializeFilterOptions();
    }

    //Load all options in FilterOptions
    private void InitializeFilterOptions()
    {
        //It saves in ObservableCollection<KeyValuePair<...> to have a key to use and a value to show on front end
        _filterOptions = new ObservableCollection<KeyValuePair<FilterSales, string>>
        {
            new(FilterSales.Ticket, "Ticket"),
            new(FilterSales.Ventas, "Ventas"),
        };

        _selectedFilter = _filterOptions[0];
    }

    public async Task GetSales()
    {
        try
        {
            var sales = await _saleRepo.GetSales();
            var tickets = await _ticketRepo.GetTicketsAsync();
          
            _salesCollection.Clear();
            _ticketsCollection.Clear();

            foreach (var ticket in tickets)
            {
                _ticketsCollection.Add(new DtoSalesList(ticket));
            }

            foreach (var sale in sales)
            {
                _salesCollection.Add(new DtoSalesList(sale));
            }
            
          //  ApplyFilter();
            OnPropertyChanged(nameof(SalesView));
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar ventas: {ex.Message}");
        }
    }

    private async void GenerateMonthReport()
    {
        var prueba = _mapper.Map<ObservableCollection<Sales>>(_salesCollection);
        _service.GenerateMonthReport(prueba);
        MessageBox.Show("Reporte Generado");
    }

    private async void GenerateReport()
    {
        var result = await _service.GenerateReport(FromDate, UntilDate, _ticketsCollection, (x => x.Date),
            x => x.TotalAmount);
        if (result.Success) MessageBox.Show(result.Message);
    }

    private void ApplyFilter()
    {
        switch (_selectedFilter.Key)
        {
            case FilterSales.Ventas:
                _collectionView = CollectionViewSource.GetDefaultView(SalesCollection);

                _collectionView.Filter = items =>
                {
                   
                    var SaleList = items as DtoSalesList;
                    return SaleList != null && SaleList.Date >= FromDate && SaleList.Date <= UntilDate;
                };
               
                break;

            case FilterSales.Ticket:
                _collectionView = CollectionViewSource.GetDefaultView(TicketsCollection);

                _collectionView.Filter = items =>
                {
                    var ticket = items as DtoSalesList;
                    return ticket != null && ticket.Date >= FromDate &&
                           ticket.Date <= UntilDate;
                };
               
                break;
        }

        GetSales();
        _collectionView.Refresh();
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