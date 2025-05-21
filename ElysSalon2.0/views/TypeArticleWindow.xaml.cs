using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Application.Services;
using AutoMapper;
using Core.Domain.Entities;
using Core.Interfaces.Repositories;
using ElysSalon2._0.ViewModels;
using ElysSalon2._0.WinManagement;

namespace ElysSalon2._0.views;

public partial class TypeArticleWindow : Window
{
    private readonly ItemManagerWindow _itemsesManagerWindow;
    private readonly IArticleTypeRepository _typeRepository;
    private readonly ObservableCollection<ArticleType> _typesCollection;
    private readonly ICollectionView _view;
    private readonly WindowsManager _windowManagement;


    public TypeArticleWindow(WindowsManager windowManagement,
        ArticleAppService service, IMapper map)
    {
        InitializeComponent();


        DataContext = new TypesManagementViewModel(windowManagement, service, this, map);
    }
}