using System.Windows;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews
{
    /// <summary>
    /// Lógica de interacción para SalesWindow.xaml
    /// </summary>
    public partial class SalesWindow : Window
    {
        public SalesWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            this.Close();
            adminWindow.Show();
        }
    }
}
