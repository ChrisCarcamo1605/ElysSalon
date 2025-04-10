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
using AutoMapper;
using ElysSalon2._0.adapters.InBound.UI.ViewModels;
using ElysSalon2._0.adapters.OutBound.Repositories;
using ElysSalon2._0.Core.aplication.Management;
using ElysSalon2._0.Core.aplication.Ports.Repositories;
using ElysSalon2._0.Core.domain.Entities;
using ElysSalon2._0.Core.domain.Services;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews
{
    /// <summary>
    /// Interaction logic for SalesWindow.xaml
    /// </summary>
    public partial class SalesWindow : Window
    {
        public SalesWindow(ISalesRepository saleRepo, WindowsManager winManager, ITicketRepository ticketRepo,SalesService service,IMapper mapper)

        {
            InitializeComponent();
            DataContext = new SalesViewModel(saleRepo, this, winManager, ticketRepo, service,mapper);
        }
    }
}
