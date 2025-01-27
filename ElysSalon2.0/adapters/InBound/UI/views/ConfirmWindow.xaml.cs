using System.Windows;

namespace ElysSalon2._0.adapters.InBound.UI.views
{
    /// <summary>
    /// Lógica de interacción para confirmWindow.xaml
    /// </summary>
    public partial class ConfirmWindow : Window
    {
        public ConfirmWindow()
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
