using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Application.DTOs.Request.SalesData;
using Application.DTOs.Response.Expense;
using Application.DTOs.Response.SalesData;
using Application.DTOs.Response.TicketDetails;
using Application.DTOs.Response.Tickets;
using Application.Enums;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using Core.Common;
using ElysSalon2._0.views;
using ElysSalon2._0.WinManagement;

namespace ElysSalon2._0.ViewModels;

public class SalesViewModel : INotifyPropertyChanged
{
    private readonly ISalesReportsService _reportsService;
    private readonly SaleDataAppService _salesDataService;

    private readonly ObservableCollection<DTOSalesData> _ticketDetailsCollection;
    private readonly Window _window;
    private readonly WindowsManager _winManager;

    private ICollectionView _collectionView;


    private ObservableCollection<DTOSalesData> _dtoInfoList;
    private ObservableCollection<DTOSalesData> _expensesCollection;

    //Where saves our filters options
    private ObservableCollection<KeyValuePair<FilterSales, string>>? _filterOptions;

    private DateTime _fromDate = DateTime.Now.AddMonths(-1);


    private IMapper _mapper;

    private ObservableCollection<DTOSalesData> _salesCollection;

    //Binding to ComboBox
    private KeyValuePair<FilterSales, string> _selectedFilter;

    public KeyValuePair<SortOptionsBy, string> _selectedSort;
    private ObservableCollection<KeyValuePair<SortOptionsBy, string>>? _sortOptions;
    private ObservableCollection<DTOSalesData> _ticketsCollection;

    private DateTime _untilDate = DateTime.Now;

    public SalesViewModel(Window window, WindowsManager windowsManager,
        SaleDataAppService salesDataService, ISalesReportsService reportsService, IMapper mapper)
    {
        _reportsService = reportsService;
        _salesDataService = salesDataService;
        _winManager = windowsManager;
        _salesCollection = [];
        _ticketsCollection = [];
        _expensesCollection = [];
        _ticketDetailsCollection = [];
        _mapper = mapper;
        _window = window;

        ExitCommand = new RelayCommand(Exit);
        GenerateReportCommand = new RelayCommand(GenerateReport);
        OpenChartWindowCommand = new RelayCommand(OpenChartWindow);
        DeleteCommand = new AsyncRelayCommand<DTOSalesData>(Delete);


        _collectionView = CollectionViewSource.GetDefaultView(SalesCollection);
        InitializeFilterSortOptions();
        _ = LoadData();
    }

    public ObservableCollection<DTOSalesData> ExpensesCollection
    {
        get => _expensesCollection;
        set
        {
            SetField(ref _expensesCollection, value);
            OnPropertyChanged();
        }
    }

    public ObservableCollection<DTOSalesData> dtoInfoList
    {
        get => _dtoInfoList;
        set
        {
            SetField(ref _dtoInfoList, value);
            OnPropertyChanged();
        }
    }


    public ObservableCollection<DTOSalesData> TicketsCollection
    {
        get => _ticketsCollection;
        set
        {
            SetField(ref _ticketsCollection, value);
            OnPropertyChanged();
        }
    }

    public DateTime FromDate
    {
        get => _fromDate;

        set
        {
            SetField(ref _fromDate, value);
            OnPropertyChanged();
            LoadData();
        }
    }

    public DateTime UntilDate
    {
        get => _untilDate;
        set
        {
            SetField(ref _untilDate, value);
            OnPropertyChanged(nameof(FromDate));

            LoadData();
        }
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

    public ObservableCollection<KeyValuePair<SortOptionsBy, string>>? SortOptions
    {
        get => _sortOptions;
        set
        {
            SetField(ref _sortOptions, value);
            OnPropertyChanged();
        }
    }

    public ObservableCollection<DTOSalesData> SalesCollection
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
            ApplyFilter();
            OnPropertyChanged();
        }
    }

    public KeyValuePair<SortOptionsBy, string> SelectedSort
    {
        get => _selectedSort;

        set
        {
            SetField(ref _selectedSort, value);
            OnPropertyChanged();
            LoadData();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void InitializeFilterSortOptions()
    {
        _filterOptions = new ObservableCollection<KeyValuePair<FilterSales, string>>
        {
            new(FilterSales.Tickets, "Ticket"),
            new(FilterSales.Sales, "Ventas"),
            new(FilterSales.Expenses, "Gastos")
        };

        _sortOptions = new ObservableCollection<KeyValuePair<SortOptionsBy, string>>
        {
            new(SortOptionsBy.DateAscending, "Fecha Ascendente"),
            new(SortOptionsBy.DateDesending, "Fecha Desendente"),
            new(SortOptionsBy.AmountAscending, "Monto Ascendente"),
            new(SortOptionsBy.AmountDesending, "Monto Desendente")
        };

        _selectedFilter = _filterOptions[0];
        _selectedSort = _sortOptions[1];
    }

    public async Task LoadData()
    {
        try
        {
            var salesResult = await _salesDataService.GetAllOf<DTOGetSales>();
            var ticketsResult = await _salesDataService.GetAllOf<DTOGetTicket>();
            var expensesResult = await _salesDataService.GetAllOf<DTOGetExpense>();
            var ticketDetailsResult = await _salesDataService.GetAllOf<DTOGetTicketDetails>();

            LoadAndSortCollection(_salesCollection, salesResult);
            LoadAndSortCollection(_ticketsCollection, ticketsResult);
            LoadAndSortCollection(_expensesCollection, expensesResult);
            LoadAndSortCollection(_ticketDetailsCollection, ticketDetailsResult);

            ApplyFilter();
            OnPropertyChanged(nameof(SalesView));
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar ventas: {ex.Message}");
        }
    }

    //private async void GenerateMonthReport()
    //{
    //    var prueba = _mapper.Map<ObservableCollection<Sales>>(_salesCollection);
    //    _reportsService.GenerateMonthReport(prueba);
    //    MessageBox.Show("Reporte Generado");
    //}

    private async void GenerateReport()
    {
        var result = await _reportsService.GenerateReport(FromDate, UntilDate, _salesCollection,_expensesCollection, x => x.Date,
            x => x.TotalAmount);

        if (result.Success) MessageBox.Show(result.Message);
        else MessageBox.Show(result.Message);
    }

    private void ApplyFilter()
    {
        switch (_selectedFilter.Key)
        {
            case FilterSales.Sales:
                _collectionView = CollectionViewSource.GetDefaultView(SalesCollection);
                break;

            case FilterSales.Tickets:
                _collectionView = CollectionViewSource.GetDefaultView(TicketsCollection);
                break;

            case FilterSales.Expenses:
                _collectionView = CollectionViewSource.GetDefaultView(ExpensesCollection);
                break;
        }

        _collectionView.Filter = items =>
        {
            var salesData = items as DTOSalesData;
            return salesData != null &&
                   salesData.Date >= FromDate &&
                   salesData.Date <= UntilDate;
        };

        ApplySort();
        _collectionView.Refresh();

        OnPropertyChanged(nameof(SalesView));
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
            new ObservableCollection<DTOSalesData>(_salesCollection),
            new ObservableCollection<DTOSalesData>(_ticketsCollection),
            new ObservableCollection<DTOSalesData>(_expensesCollection),
            new ObservableCollection<DTOSalesData>(_ticketDetailsCollection));

        chartWindow.ShowDialog();
    }

    private async Task Delete(DTOSalesData sale)
    {
        var option = MessageBox.Show("¿Seguro que quiere eliminar este registro?", "Confirmar eliminación",
            MessageBoxButton.YesNo);

        if (option == MessageBoxResult.Yes)
        {
            ResultFromService? result = null;
            switch (_selectedFilter.Key)
            {
                case FilterSales.Sales:
                    result = await _salesDataService.Delete<DTOGetSales>(sale.Id);
                    break;
                case FilterSales.Tickets:
                    result = await _salesDataService.Delete<DTOGetTicket>(sale.Id);
                    break;
                case FilterSales.Expenses:
                    result = await _salesDataService.Delete<DTOGetExpense>(sale.Id);
                    break;
            }

            if (result.Success)
                MessageBox.Show(result.Message);
            else
                MessageBox.Show(result.Message);

            _ = LoadData();
            ApplySort();
        }
    }

    public void LoadAndSortCollection(ObservableCollection<DTOSalesData> collection,
        ResultFromService serviceResult)
    {
        if (serviceResult.Data is ObservableCollection<DTOSalesData> newCollection)
        {
            collection.Clear();
            var sortedItems = newCollection.OrderByDescending(x => x.Date.Date).ToList();
            foreach (var item in sortedItems) collection.Add(item);
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