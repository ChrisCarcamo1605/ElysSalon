using System.Windows;
using ElysSalon2._0.adapters.InBound.UI.views;
using Microsoft.Extensions.DependencyInjection;

namespace ElysSalon2._0.aplication.Management;

public class WindowsManager
{
    public delegate void GridUpdateRequestedHandler();

    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<Type, Window> _windows = new();

    public WindowsManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public event GridUpdateRequestedHandler GridUpdateRequested;

    public void NavigateToWindow<TWindow>(Action onGridUpdateRequested = null) where TWindow : Window
    {
        if (!_windows.TryGetValue(typeof(TWindow), out var newWindow))
        {
            newWindow = _serviceProvider.GetRequiredService<TWindow>();
            _windows[typeof(TWindow)] = newWindow;
            newWindow.Closed += (s, e) => _windows.Remove(typeof(TWindow));


            if (newWindow is IChildWindow childWindow)
                childWindow.UpdateParentGrid += () =>
                {
                    onGridUpdateRequested?.Invoke();
                    GridUpdateRequested?.Invoke();
                };
        }

        if (!newWindow.IsVisible) newWindow.Show();
        newWindow.Activate();
    }

    public void CloseCurrentWindowandShowWindow<TWindow>(Window currentWindow, Action onGridUpdateRequested = null)
        where TWindow : Window
    {
        NavigateToWindow<TWindow>(onGridUpdateRequested);
        currentWindow.Close();
    }
}