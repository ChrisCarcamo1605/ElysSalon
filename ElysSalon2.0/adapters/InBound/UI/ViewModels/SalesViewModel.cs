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
    private readonly Window _window;

    //Where saves our filters options
    private ObservableCollection<KeyValuePair<FilterSales, string>>? _filterOptions;

    public ObservableCollection<KeyValuePair<FilterSales, string>>? FilterOptions
    {
        get => _filterOptions;
        set
        {
            SetField(ref _filterOptions, value);
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
    private readonly WindowsManager _winManager;

    public ICollectionView SalesView
    {
        get => _saleView;
        set
        {
            SetField(ref _saleView, value);
            OnPropertyChanged();
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand ExitCommand { get; }

    //Binding to ComboBox
    private KeyValuePair<FilterSales, string> _selectedFilter;
    public KeyValuePair<FilterSales, string> SelectedFilter
    {
        get => _selectedFilter;
        set
        {
            SetField(ref _selectedFilter, value);
            ApplyFilter();
        }
    }

    public SalesViewModel(ISalesRepository saleRepo, Window window, WindowsManager windowsManager)
    {
        _saleRepo = saleRepo;
        _winManager = windowsManager;
        _salesCollection = [];

        _saleView = CollectionViewSource.GetDefaultView(_salesCollection);
        _window = window;

        InitializeFilterOptions();

        SaveCommand = new AsyncRelayCommand(SaveVenta);
        ExitCommand = new RelayCommand(Exit);
        _ = GetSales();
    }


    //Load all options in FilterOptions
    private void InitializeFilterOptions()
    {
        //It saves in ObservableCollection<KeyValuePair<...> to have a key to use and a value to show on front end
        _filterOptions = new ObservableCollection<KeyValuePair<FilterSales, string>>
        {
            new KeyValuePair<FilterSales, string>(FilterSales.Todo, "Todo"),
            new KeyValuePair<FilterSales, string>(FilterSales.Ultimos7Dias, "Últimos 7 días"),
            new KeyValuePair<FilterSales, string>(FilterSales.UltimoMes, "Último mes"),
            new KeyValuePair<FilterSales, string>(FilterSales.Ultimos3Meses, "Últimos 3 meses")
        };

        _selectedFilter = _filterOptions[0];
    }

    public async Task GetSales()
    {
        try
        {
            var sales = await _saleRepo.GetSales();
            _salesCollection.Clear();

            foreach (var item in sales)
            {
                _salesCollection.Add(item);
            }

            ApplyFilter();
            OnPropertyChanged(nameof(SalesView));
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar ventas: {ex.Message}");
        }
    }

    private void ApplyFilter()
    {
        DateTime now = DateTime.Now;

        switch (_selectedFilter.Key)
        {
            case FilterSales.Ultimos7Dias:

                DateTime sevenDaysAgo = now.AddDays(-7);
                _saleView.Filter = item =>
                {
                    var sale = item as Sales;
                    return sale != null && sale.SaleDate >= sevenDaysAgo;
                };

                break;

            case FilterSales.UltimoMes:
                DateTime oneMonthAgo = now.AddMonths(-1);
                _saleView.Filter = item =>
                {
                    var sale = item as Sales;
                    return sale != null && sale.SaleDate >= oneMonthAgo;
                };
                break;

            case FilterSales.Ultimos3Meses:
                DateTime threeMonthsAgo = now.AddMonths(-3);
                _saleView.Filter = item =>
                {
                    var sale = item as Sales;
                    return sale != null && sale.SaleDate >= threeMonthsAgo;
                };
                break;

            case FilterSales.Todo:
            default:
                _saleView.Filter = null;
                break;
        }

        _saleView.Refresh();
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