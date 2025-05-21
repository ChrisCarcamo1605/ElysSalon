using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Application.DTOs.Request.Articles;
using Application.DTOs.Response.Articles;
using Application.Services;
using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.views;
using ElysSalon2._0.WinManagement;

namespace ElysSalon2._0.ViewModels;

public class TypesManagementViewModel : INotifyPropertyChanged
{
    private readonly ArticleAppService _service;
    private readonly TypeArticleWindow _window;
    private readonly IMapper _map;
    private readonly WindowsManager _windowManager;
    private string _name;
    private int _typeId;

    private ObservableCollection<DTOGetArtType> _typesCollection;

    public TypesManagementViewModel(WindowsManager windowsManager,
        ArticleAppService service, TypeArticleWindow window, IMapper map)
    {
        TypesCollection = new ObservableCollection<DTOGetArtType>();
        _windowManager = windowsManager;
        _service = service;
        _window = window;
        _map = map;
        addTypeCommand = new AsyncRelayCommand(AddType);
        editTypeCommand = new AsyncRelayCommand<DTOGetArtType>(EditType);
        deleteTypeCommand = new AsyncRelayCommand<DTOGetArtType>(DeleteType);
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

    public ObservableCollection<DTOGetArtType> TypesCollection
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
        var types = (ObservableCollection<DTOGetArtType>)(await _service.GetTypesAsync()).Data;

        types.Remove(types.First(x => x.ArtTypeId.Equals(1)));
        types.Remove(types.First(x => x.ArtTypeId.Equals(2)));


        if (TypesCollection == null)
        {
            foreach (var type in types)
            {
                var artType = new DTOGetArtType(type.ArtTypeId, type.Name);
                TypesCollection.Add(artType);
            }
        }
        else
        {
            TypesCollection.Clear();
            foreach (var type in types)
            {
                var artType = new DTOGetArtType(type.ArtTypeId, type.Name);
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

    public async Task EditType(DTOGetArtType type)
    {
        var result = await _service.EditTypeAsync(type.Name);
        if (result.Success is true) Name = "";

        MessageBox.Show(result.Message);
        await LoadTypes();
    }

    public async Task DeleteType(DTOGetArtType type)
    {
        var option = MessageBox.Show("¿Seguro que quiere eliminar este tipo de articulo?", "Confirmar eliminación",
            MessageBoxButton.YesNo);
        if (option == MessageBoxResult.Yes)
        {
            var result = await _service.DeleteTypeAsync(type.ArtTypeId);
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