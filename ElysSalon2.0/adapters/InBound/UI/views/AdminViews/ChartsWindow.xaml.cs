
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
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
using ElysSalon2._0.adapters.InBound.UI.ViewModels;
using ElysSalon2._0.Core.aplication.DTOs.DTOSales;
using ElysSalon2._0.Core.aplication.Management;
using ElysSalon2._0.Core.aplication.Ports.Services;
using ElysSalon2._0.Core.domain.Entities;
using ElysSalon2._0.Core.domain.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews
{
    /// <summary>
    /// Lógica de interacción para Charts.xaml
    /// </summary>
    public partial class ChartsWindow : Window
    {

     

        public ChartsWindow(WindowsManager windowsManager,ObservableCollection<DtoSalesList> salesCollection,
            ObservableCollection<DtoSalesList> ticketCollection,ITicketService ticketService)
        {
          InitializeComponent();

          DataContext = new ChartsViewModel(this, windowsManager, salesCollection, ticketCollection,ticketService);
        }

     
    }
}
