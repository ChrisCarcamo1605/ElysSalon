using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.aplication.Interfaces.Repositories;
using ElysSalon2._0.aplication.Interfaces.Services;
using ElysSalon2._0.domain.Entities;
using ElysSalon2._0.views;
using ElysSalon2._0.WinManagement;

namespace ElysSalon2._0.ViewModels;

public class TypesManagementViewModel : INotifyPropertyChanged
{
    private readonly IArticleService _service;
    private readonly IArticleTypeRepository _typeRepository;
    private readonly TypeArticleWindow _window;
    private readonly WindowsManager _windowManager;
    private string _name;
    private int _typeId;

    private ObservableCollection<ArticleType> _typesCollection;

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
        ExitCommand = new RelayCommand(Exit);
        LoadTypes();
    }

    public int TypeId
    {
        get => _typeId;
        set
        {
            _typeId = value;
            OnPropertyChanged();
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<ArticleType> TypesCollection
    {
        get => _typesCollection;
        set
        {
            _typesCollection = value;
            OnPropertyChanged();
        }
    }

    public ICommand addTypeCommand { get; }
    public ICommand deleteTypeCommand { get; }
    public ICommand editTypeCommand { get; }
    public ICommand ExitCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;


    public async Task LoadTypes()
    {
        var types = await _typeRepository.GetTypesAsync();

        types.Remove(types.First(x => x.ArticleTypeId.Equals(1)));
        types.Remove(types.First(x => x.ArticleTypeId.Equals(2)));


        if (TypesCollection == null)
        {
            foreach (var type in types)
            {
                var artType = new ArticleType
                {
                    ArticleTypeId = type.ArticleTypeId,
                    Name = type.Name
                };
                TypesCollection.Add(artType);
            }
        }
        else
        {
            TypesCollection.Clear();
            foreach (var type in types)
            {
                var artType = new ArticleType
                {
                    ArticleTypeId = type.ArticleTypeId,
                    Name = type.Name
                };
                TypesCollection.Add(artType);
            }
        }
    }

    public async Task AddType()
    {
        var result = await _service.AddType(Name);
        if (result.Success is true)
        {
            Name = "";
            MessageBox.Show(result.Message, "Operación exitosa", MessageBoxButton.OK);
        }
        else
        {
            MessageBox.Show(result.Message, "Error de formulario", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        await LoadTypes();
    }

    public async Task EditType(ArticleType type)
    {
        var result = await _service.EditType(type);
        if (result.Success is true) Name = "";

        MessageBox.Show(result.Message);
        await LoadTypes();
    }

    public async Task DeleteType(ArticleType type)
    {
        ;


        var option = MessageBox.Show("¿Seguro que quiere eliminar este tipo de articulo?", "Confirmar eliminación",
            MessageBoxButton.YesNo);
        if (option == MessageBoxResult.Yes)
        {
            var result = await _service.DeleteType(type.ArticleTypeId);
            if (result.Success is true) _name = "";
            MessageBox.Show(result.Message, "Operación exitosa", MessageBoxButton.OK);
        }

        await LoadTypes();
    }

    public void Exit()
    {
        _windowManager.CloseCurrentWindowandShowWindow<ItemManagerWindow>(_window);
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