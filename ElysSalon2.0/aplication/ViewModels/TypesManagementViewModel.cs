using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.adapters.InBound.UI.views.AdminViews;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Services;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.ViewModels;

public class TypesManagementViewModel : INotifyPropertyChanged
{
    private IArticleTypeRepository _typeRepository;
    private WindowsManager _windowManager;
    private IArticleService _service;
    private int _typeId;
    private TypeArticleWindow _window;

    public int TypeId
    {
        get { return _typeId; }
        set
        {
            _typeId = value;
            OnPropertyChanged(nameof(TypeId));
        }
    }

    private string _name;

    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    private ObservableCollection<ArticleType> _typesCollection;

    public ObservableCollection<ArticleType> TypesCollection
    {
        get { return _typesCollection; }
        set
        {
            _typesCollection = value;
            OnPropertyChanged(nameof(TypesCollection));
        }
    }

    public ICommand addTypeCommand { get; }
    public ICommand deleteTypeCommand { get; }
    public ICommand editTypeCommand { get; }
    public ICommand exitCommand { get; }

    public TypesManagementViewModel(IArticleTypeRepository typeRepository, WindowsManager windowsManager,
        IArticleService service, TypeArticleWindow window)
    {
        TypesCollection = new ObservableCollection<ArticleType>();
        _windowManager = windowsManager;
        _typeRepository = typeRepository;
        _service = service;
        _window = window;
        addTypeCommand = new AsyncRelayCommand(AddType);
        editTypeCommand = new AsyncRelayCommand<ArticleType>(EditType);
        deleteTypeCommand = new AsyncRelayCommand<ArticleType>(DeleteType);
        exitCommand = new RelayCommand(Exit);
        LoadTypes();
    }
    
    public async Task LoadTypes()
    {
        var types = await _typeRepository.getTypes();

        types.Remove(types.First(x => x.ArticleTypeId.Equals(1)));
        types.Remove(types.First(x => x.ArticleTypeId.Equals(2)));


        if (TypesCollection == null)
        {
            TypesCollection = types;
        }
        else
        {
            MessageBox.Show("Entramosss");
            TypesCollection.Clear();
            foreach (var type in types)
            {
                TypesCollection.Add(type);
            }
        }
    }

    public async Task AddType()
    {
        await _service.AddType(Name);
        MessageBox.Show("Se guardó exitosamente");
        await LoadTypes();
        _name = "";
    }

    public async Task EditType(ArticleType type)
    {
        await _service.EditType(type);
    }

    public async Task DeleteType(ArticleType type)
    {
        await _service.DeleteType(type.ArticleTypeId);
        MessageBox.Show("Se eliminó exitosamente");
        await LoadTypes();
    }

    public void Exit()
    {
        _window.Close();
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