using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Web.UI;
using Microsoft.Extensions.DependencyInjection;

namespace ElysSalon2._0.aplication.Management
{
    public class WindowsManager
    {

        IServiceProvider _serviceProvider;
        private Dictionary<Type,Window> _windows = new Dictionary<Type, Window>();


        public WindowsManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void navigateToWindow<TWindow>() where TWindow : Window
        {
            // Obtener o crear la nueva ventana
            if (!_windows.TryGetValue(typeof(TWindow), out var newWindow))
            {
                newWindow = _serviceProvider.GetRequiredService<TWindow>();
                _windows[typeof(TWindow)] = newWindow;
                newWindow.Closed += (s, e) => _windows.Remove(typeof(TWindow));
            }

            // Mostrar la nueva ventana
            if (!newWindow.IsVisible)
            {
                newWindow.Show();
            }
            newWindow.Activate();
        }

        public void closeCurrentWindowandShowWindow<TWindow>(Window currenWindow) where TWindow : Window{
            navigateToWindow<TWindow>();
            currenWindow.Close();
        }
    }
}
