using System.Windows;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews
{
    /// <summary>
    /// Lógica de interacción para AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {


            if (MainWindow.mainW != null)
            {
                MainWindow.mainW.Show();
                this.Close();
            }


        }

        private void salesBtn_Click(object sender, RoutedEventArgs e)
        {
            SalesWindow  salesWindow = new SalesWindow();
            salesWindow.Show();
            this.Close();
        }

        private void itemsBtn_Click(object sender, RoutedEventArgs e)
        {
            ItemManager item = new ItemManager();
            item.Show();
            this.Close();
        }
    }
}
