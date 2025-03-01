using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using AutoMapper;
using ElysSalon2._0.aplication.DTOs;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.aplication.Mappers;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.aplication.ViewModels;
using ElysSalon2._0.domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews;

public partial class ItemManager : Window
{

    private WindowsManager _windowsManager;
    public ItemManager(WindowsManager windowsManager,
        IArticleRepository articleRepository, IArticleTypeRepository TypeRepository)
    {
        InitializeComponent();

        _windowsManager = windowsManager;
        this.DataContext = new ItemManagerViewModel(articleRepository, TypeRepository);


    }





    private void exitBtn_Click(object sender, RoutedEventArgs e)
    {
        _windowsManager.CloseCurrentWindowandShowWindow<AdminWindow>(this);
    }

   

   
}