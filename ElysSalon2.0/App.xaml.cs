using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using ElySalon2_0;
using ElysSalon2._0.adapters.InBound.UI.views;
using ElysSalon2._0.adapters.OutBound;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ElysSalon2._0 {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private readonly IServiceProvider _serviceProvider;
        public static IConfigurationRoot config;
        private static App app;

        public App(){
            _serviceProvider = new ServiceCollection()
                .AddScoped<ITicketRepository, TicketRepository>()
                .BuildServiceProvider();
        }

        protected void OnStarTuo(StartupEventArgs e){
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}