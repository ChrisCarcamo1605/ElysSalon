using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace ElysSalon2._0.WinManagement;

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

    public void NavigateToWindow<TWindow>(object parameter = null, Action onGridUpdateRequested = null)
        where TWindow : Window
    {
        if (!_windows.TryGetValue(typeof(TWindow), out var newWindow))
        {
            if (parameter != null)
                newWindow = (TWindow)Activator.CreateInstance(typeof(TWindow), parameter);
            else
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