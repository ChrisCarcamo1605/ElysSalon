using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.adapters.InBound.UI.views.AdminViews;
using ElysSalon2._0.Core.aplication.Management;
using ElysSalon2._0.Core.aplication.Ports.Repositories;
using ElysSalon2._0.Core.domain.Entities;

namespace ElysSalon2._0.adapters.InBound.UI.ViewModels;

public class SalesViewModel : INotifyPropertyChanged
{
    private readonly ISalesRepository _saleRepo;
    private Window _window;
    private int _sortBy;

    public int sortBy
    {
        get => _sortBy;
        set
        {
            SetField(ref _sortBy, value);
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Sales> _salesCollection;

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

    private ICollectionView _saleView;
    private WindowsManager _winManager;

    public ICollectionView SalesView
    {
        get => _saleView;
        set
        {
            SetField(ref _saleView, value);
            OnPropertyChanged();
        }
    }

    public ICommand saveCommand { get; }
    public ICommand exitCommand { get; }
    public ICommand FilterLast7DaysCommand { get; }
    public ICommand FilterLastMonthCommand { get; }
    public ICommand FilterLast3MonthsCommand { get; }

    private bool _filterLast7Days;
    private bool _filterLastMonth;
    private bool _filterLast3Months;

    public bool FilterLast7Days
    {
        get => _filterLast7Days;
        set
        {
            SetField(ref _filterLast7Days, value);
            OnPropertyChanged();
            ApplyFilter();
        }
    }

    public bool FilterLastMonth
    {
        get => _filterLastMonth;
        set
        {
            SetField(ref _filterLastMonth, value);
            OnPropertyChanged();
            ApplyFilter();
        }
    }

    public bool FilterLast3Months
    {
        get => _filterLast3Months;
        set
        {
            SetField(ref _filterLast3Months, value);
            OnPropertyChanged();
            ApplyFilter();
        }
    }

    private string _selectedFilter;
    public string SelectedFilter
    {
        get => _selectedFilter;
        set
        {
            SetField(ref _selectedFilter, value);
            OnPropertyChanged();
            ApplyFilter();
        }
    }

    public SalesViewModel(ISalesRepository saleRepo, Window window, WindowsManager windowsManager)
    {
        _saleRepo = saleRepo;
        _winManager = windowsManager;
        _salesCollection = new ObservableCollection<Sales>();
        _saleView = CollectionViewSource.GetDefaultView(_salesCollection);
        _window = window;

        saveCommand = new AsyncRelayCommand(SaveVenta);
        exitCommand = new RelayCommand(Exit);
        FilterLast7DaysCommand = new RelayCommand(() => FilterLast7Days = true);
        FilterLastMonthCommand = new RelayCommand(() => FilterLastMonth = true);
        FilterLast3MonthsCommand = new RelayCommand(() => FilterLast3Months = true);

        _ = GetSales();
    }

    public async Task GetSales()
    {
        var sales = await _saleRepo.GetSales();

        _salesCollection.Clear();
        foreach (var item in sales)
        {
            _salesCollection.Add(item);
        }

        ApplyFilter();
    }

    private void ApplyFilter()
    {
        DateTime now = DateTime.Now;

        if (SelectedFilter == "Últimos 7 días")
        {
            _saleView.Filter = item =>
            {
                var sale = item as Sales;
                return sale != null && sale.SaleDate >= now.AddDays(-7);
            };
        }
        else if (SelectedFilter == "Último mes")
        {
            _saleView.Filter = item =>
            {
                var sale = item as Sales;
                return sale != null && sale.SaleDate >= now.AddMonths(-1);
            };
        }
        else if (SelectedFilter == "Últimos 3 meses")
        {
            _saleView.Filter = item =>
            {
                var sale = item as Sales;
                return sale != null && sale.SaleDate >= now.AddMonths(-3);
            };
        }
        else
        {
            _saleView.Filter = null;
        }

        _saleView.Refresh();
    }

    public async Task SaveVenta()
    {
        var sale = new ObservableCollection<Sales>();
        await _saleRepo.SavesSale(SalesCollection);
        MessageBox.Show("Venta creada correctamente");
        await GetSales();
    }

    public void Exit()
    {
        _winManager.CloseCurrentWindowandShowWindow<AdminWindow>(_window);
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