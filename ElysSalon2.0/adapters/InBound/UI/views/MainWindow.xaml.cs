using System.Windows;
using ElysSalon2._0.adapters.InBound.UI.views.AdminViews;

namespace ElysSalon2._0.adapters.InBound.UI.views
{
    
    public partial class MainWindow : Window
    {
        public static MainWindow mainW { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            mainW = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void adminBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            this.Hide();
            
        }

        private async void btnServices_Click(object sender, RoutedEventArgs e)
        {
            ArticlesWindow articlesWindow = new ArticlesWindow();
          
            await Task.Delay(450);
            this.Close();
            articlesWindow.Show();
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
