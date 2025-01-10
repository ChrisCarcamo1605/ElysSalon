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

namespace ElySalon
{
    /// <summary>
    /// Lógica de interacción para ArticlesWindow.xaml
    /// </summary>
    public partial class ArticlesWindow : Window
    {
        public ArticlesWindow()
        {
            InitializeComponent();
        }

        private void listoBtn(object sender, RoutedEventArgs e)
        {

        }

        private void atrasBtn_Click(object sender, RoutedEventArgs e)
        {


            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
            
        }

        private void listoBtn_Click(object sender, RoutedEventArgs e)
        {
            confirmWindow confirmWindow = new confirmWindow();
            confirmWindow.Show();
        }
    }
}
