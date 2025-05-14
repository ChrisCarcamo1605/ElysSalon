using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Windows.UI.Popups;
using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.adapters.InBound.views;
using ElysSalon2._0.aplication.DTOs.DTOSales;
using ElysSalon2._0.aplication.DTOs.Enums;
using ElysSalon2._0.aplication.Interfaces.Services;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.domain.Entities;
using ElysSalon2._0.domain.Services;
using LiveChartsCore;

namespace ElysSalon2._0.adapters.InBound.ViewModels;

public class SalesViewModel : INotifyPropertyChanged
{
    private readonly Window _window;
    private readonly WindowsManager _winManager;
    private readonly ISalesReportsService _reportsService;
    private readonly ISalesDataService _salesDataService;

    //Where saves our filters options
    private ObservableCollection<KeyValuePair<FilterSales, string>>? _filterOptions;
    private ObservableCollection<KeyValuePair<SortOptionsBy, string>>? _sortOptions;
    private ObservableCollection<DtoSalesList> _ticketsCollection;
    private ObservableCollection<DtoSalesList> _expensesCollection;

    public ObservableCollection<DtoSalesList> ExpensesCollection
    {
        get => _expensesCollection;
        set
        {
            SetField(ref _expensesCollection, value);
            OnPropertyChanged();
        }
    }

    private ObservableCollection<TicketDetails> _ticketDetailsCollection;


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

    public ObservableCollection<KeyValuePair<SortOptionsBy, string>>? SortOptions
    {
        get => _sortOptions;
        set
        {
            SetField(ref _sortOptions, value);
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
    public ICommand DeleteCommand { get; }
    public ICommand GenerateReportCommand { get; }

    public ICommand OpenChartWindowCommand { get; }

    public KeyValuePair<FilterSales, string> SelectedFilter
    {
        get => _selectedFilter;
        set
        {
            SetField(ref _selectedFilter, value);
            OnPropertyChanged();
            ApplyFilter();
        }
    }

    public KeyValuePair<SortOptionsBy, string> _selectedSort;

    public KeyValuePair<SortOptionsBy, string> SelectedSort
    {
        get => _selectedSort;

        set
        {
            SetField(ref _selectedSort, value);
            OnPropertyChanged();
            ApplyFilter();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public SalesViewModel(Window window, WindowsManager windowsManager,
        ISalesDataService salesDataService, ISalesReportsService reportsService, IMapper mapper)
    {
        _reportsService = reportsService;
        _salesDataService = salesDataService;
        _winManager = windowsManager;
        _salesCollection = [];
        _ticketsCollection = [];
        _expensesCollection = [];
        _mapper = mapper;
        _window = window;

        ExitCommand = new RelayCommand(Exit);
        GenerateReportCommand = new RelayCommand(GenerateReport);
        OpenChartWindowCommand = new RelayCommand(OpenChartWindow);
        DeleteCommand = new AsyncRelayCommand<DtoSalesList>(Delete);


        _collectionView = CollectionViewSource.GetDefaultView(SalesCollection);
        InitializeFilterSortOptions();
        ApplyFilter();
    }

    private void InitializeFilterSortOptions()
    {
        _filterOptions = new ObservableCollection<KeyValuePair<FilterSales, string>>
        {
            new(FilterSales.Tickets, "Ticket"),
            new(FilterSales.Sales, "Ventas"),
            new(FilterSales.Expenses, "Gastos"),
        };

        _sortOptions = new ObservableCollection<KeyValuePair<SortOptionsBy, string>>
        {
            new(SortOptionsBy.DateAscending, "Fecha Ascendente"),
            new(SortOptionsBy.DateDesending, "Fecha Desendente"),
            new(SortOptionsBy.AmountAscending, "Monto Ascendente"),
            new(SortOptionsBy.AmountDesending, "Monto Desendente"),
        };

        _selectedFilter = _filterOptions[0];
        _selectedSort = _sortOptions[1];
    }

    public async Task LoadData()
    {
        try
        {
            var sales = await _salesDataService.GetAllOf<Sales>();
            var tickets = await _salesDataService.GetAllOf<Ticket>();
            var expenses = await _salesDataService.GetAllOf<Expense>();
            _ticketDetailsCollection = await _salesDataService.GetAllOf<TicketDetails>();

            _salesCollection.Clear();
            _ticketsCollection.Clear();
            _expensesCollection.Clear();

            foreach (var ticket in tickets) _ticketsCollection.Add(new DtoSalesList(ticket));

            foreach (var sale in sales) _salesCollection.Add(new DtoSalesList(sale));

            foreach (var expense in expenses) _expensesCollection.Add(new DtoSalesList(expense));


            //  ApplyFilter();
            OnPropertyChanged(nameof(SalesView));

            _salesCollection =
                new ObservableCollection<DtoSalesList>(_salesCollection.OrderByDescending(x => x.Date.Date).ToList());

            _ticketsCollection =
                new ObservableCollection<DtoSalesList>(_ticketsCollection.OrderByDescending(x => x.Date.Date).ToList());
            _expensesCollection =
                new ObservableCollection<DtoSalesList>(_expensesCollection.OrderByDescending(x => x.Date.Date)
                    .ToList());
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar ventas: {ex.Message}");
        }
    }

    private async void GenerateMonthReport()
    {
        var prueba = _mapper.Map<ObservableCollection<Sales>>(_salesCollection);
        _reportsService.GenerateMonthReport(prueba);
        MessageBox.Show("Reporte Generado");
    }

    private async void GenerateReport()
    {
        var result = await _reportsService.GenerateReport(FromDate, UntilDate, _ticketsCollection, x => x.Date,
            x => x.TotalAmount);
        if (result.Success) MessageBox.Show(result.Message);
    }

    private void ApplyFilter()
    {
        switch (_selectedFilter.Key)
        {
            case FilterSales.Sales:
                _collectionView = CollectionViewSource.GetDefaultView(SalesCollection);

                _collectionView.Filter = items =>
                {
                    var SaleList = items as DtoSalesList;
                    return SaleList != null && SaleList.Date >= FromDate && SaleList.Date <= UntilDate;
                };

                break;

            case FilterSales.Tickets:
                _collectionView = CollectionViewSource.GetDefaultView(TicketsCollection);

                _collectionView.Filter = items =>
                {
                    var ticket = items as DtoSalesList;
                    return ticket != null && ticket.Date >= FromDate &&
                           ticket.Date <= UntilDate;
                };

                break;
            case FilterSales.Expenses:
                _collectionView = CollectionViewSource.GetDefaultView(ExpensesCollection);

                _collectionView.Filter = items =>
                {
                    var expense = items as DtoSalesList;

                    return expense != null && expense.Date >= FromDate &&
                           expense.Date <= UntilDate;
                };

                break;
        }

        _ = LoadData();
        ApplySort();
        _collectionView.Refresh();
    }

    public void ApplySort()
    {
        switch (_selectedSort.Key)
        {
            case SortOptionsBy.DateAscending:
                _collectionView.SortDescriptions.Clear();
                _collectionView.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Ascending));
                break;
            case SortOptionsBy.DateDesending:
                _collectionView.SortDescriptions.Clear();
                _collectionView.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Descending));
                break;
            case SortOptionsBy.AmountAscending:
                _collectionView.SortDescriptions.Clear();
                _collectionView.SortDescriptions.Add(new SortDescription("TotalAmount", ListSortDirection.Ascending));
                break;
            case SortOptionsBy.AmountDesending:
                _collectionView.SortDescriptions.Clear();
                _collectionView.SortDescriptions.Add(new SortDescription("TotalAmount", ListSortDirection.Descending));
                break;
        }
    }

    public void OpenChartWindow()
    {
        var chartWindow = new ChartsWindow(_winManager,
            _salesCollection,
            _ticketsCollection, _expensesCollection, _ticketDetailsCollection);

        chartWindow.ShowDialog();
    }

    private async Task Delete(DtoSalesList sale)
    {
        var option = MessageBox.Show("¿Seguro que quiere eliminar este registro?", "Confirmar eliminación",
            MessageBoxButton.YesNo);
        ResultFromService resultFromService = ResultFromService.Failed("Hubo un error");

        if (option == MessageBoxResult.Yes)
        {
            switch (_selectedFilter.Key)
            {
                case FilterSales.Sales:

                    resultFromService = await _salesDataService.Delete<Sales>(sale.Id);
                    break;
                case FilterSales.Tickets:
                    resultFromService = await _salesDataService.Delete<Ticket>(sale.Id);
                    break;
                case FilterSales.Expenses:
                    resultFromService = await _salesDataService.Delete<Expense>(sale.Id);
                    break;
            }

            if (resultFromService.Success)
            {
                MessageBox.Show(resultFromService.Message);
            }
            else
            {
                MessageBox.Show(resultFromService.Message);
            }

            ApplyFilter();
        }
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