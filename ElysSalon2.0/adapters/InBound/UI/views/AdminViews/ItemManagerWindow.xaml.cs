using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Services;
using ElysSalon2._0.aplication.ViewModels;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews
{
    /// <summary>
    /// Lógica de interacción para ItemManagerWindow.xaml
    /// </summary>
    public partial class ItemManagerWindow : Window
    {
        public ItemManagerWindow(IArticleRepository articleRepository, IArticleTypeRepository TypeRepository,
            IArticleService service, WindowsManager windowsManager)
        {
            InitializeComponent();
            DataContext = new ItemManagerViewModel(articleRepository, TypeRepository, this, service, windowsManager);
        }
    }
}
