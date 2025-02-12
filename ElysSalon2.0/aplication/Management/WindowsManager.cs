using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Web.UI;
using ElysSalon2._0.adapters.InBound.UI.views;
using Microsoft.Extensions.DependencyInjection;

namespace ElysSalon2._0.aplication.Management
{
    public class WindowsManager
    {
        IServiceProvider _serviceProvider;
        private Dictionary<Type, Window> _windows = new Dictionary<Type, Window>();

        public delegate void GridUpdateRequestedHandler();
        public event GridUpdateRequestedHandler GridUpdateRequested;

        public WindowsManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateToWindow<TWindow>(Action onGridUpdateRequested = null) where TWindow : Window
        {
            if (!_windows.TryGetValue(typeof(TWindow), out var newWindow))
            {
                newWindow = _serviceProvider.GetRequiredService<TWindow>();
                _windows[typeof(TWindow)] = newWindow;
                newWindow.Closed += (s, e) => _windows.Remove(typeof(TWindow));

                // Si la ventana implementa IChildWindow, suscribirse al evento
                if (newWindow is IChildWindow childWindow)
                {
                    childWindow.UpdateParentGrid += () =>
                    {
                        onGridUpdateRequested?.Invoke();
                        GridUpdateRequested?.Invoke();
                    };
                }
            }

            if (!newWindow.IsVisible)
            {
                newWindow.Show();
            }
            newWindow.Activate();
        }

        public void CloseCurrentWindowandShowWindow<TWindow>(Window currentWindow, Action onGridUpdateRequested = null) where TWindow : Window
        {
            NavigateToWindow<TWindow>(onGridUpdateRequested);
            currentWindow.Close();
        }
    }
}
