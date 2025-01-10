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
    /// Lógica de interacción para confirmWindow.xaml
    /// </summary>
    public partial class confirmWindow : Window
    {
        public confirmWindow()
        {
            InitializeComponent();
        }

        private void siBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void noBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
