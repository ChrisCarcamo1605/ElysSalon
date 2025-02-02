using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews
{
    /// <summary>
    /// Lógica de interacción para ItemManager.xaml
    /// </summary>
    public partial class ItemManager : Window {
        public ItemManager(){
            InitializeComponent();
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e){
            AdminWindow adminWindow = new AdminWindow();
            this.Close();
            adminWindow.Show();
        }

      

        private void TextBox_GotFocus(object sender, RoutedEventArgs e){
            TextBox textBox = nameTxtBox;
            if (textBox.Text == " Nombre Item")
            {
                textBox.Text = "";
                textBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void nameTxtBox_LostFocus(object sender, RoutedEventArgs e){
            TextBox textBox = nameTxtBox;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = " Nombre Item";
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void nameTxtBox_Loaded_1(object sender, RoutedEventArgs e){
            TextBox textBox = nameTxtBox;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = " Nombre Item";
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void priceCostBox_Loaded(object sender, RoutedEventArgs e){
            TextBox textBox = priceCostBox;
            if (string.IsNullOrWhiteSpace(priceCostBox.Text))
            {
                textBox.Text = " Precio Costo";
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void priceCostBox_LostFocus(object sender, RoutedEventArgs e){
            TextBox textBox = priceCostBox;
            if (string.IsNullOrWhiteSpace(priceCostBox.Text))
            {
                textBox.Text = " Precio Costo";
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void priceCostBox_GotFocus(object sender, RoutedEventArgs e){
            TextBox textBox = priceCostBox;

            if (textBox.Text == " Precio Costo")
            {
                textBox.Text = "";
                textBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void priceBuyBox_GotFocus(object sender, RoutedEventArgs e){
            TextBox textBox = priceBuyBox;

            if (textBox.Text == " Precio Venta")
            {
                textBox.Text = "";
                textBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void priceBuyBox_LostFocus(object sender, RoutedEventArgs e){
            TextBox textBox = priceBuyBox;

            if (string.IsNullOrWhiteSpace(priceBuyBox.Text))
            {
                textBox.Text = " Precio Venta";
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void priceBuyBox_Loaded(object sender, RoutedEventArgs e){
            TextBox textBox = priceBuyBox;

            if (string.IsNullOrWhiteSpace(priceBuyBox.Text))
            {
                textBox.Text = " Precio Venta";
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void descriptionBox_GotFocus(object sender, RoutedEventArgs e){
            TextBox textBox = descriptionBox;

            if (textBox.Text == " Descripcion")
            {
                textBox.Text = "";
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void descriptionBox_LostFocus(object sender, RoutedEventArgs e){
            TextBox textBox = descriptionBox;


            if (string.IsNullOrWhiteSpace(descriptionBox.Text))
            {
                textBox.Text = " Descripcion";
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void descriptionBox_Loaded(object sender, RoutedEventArgs e){
            TextBox textBox = descriptionBox;

            if (string.IsNullOrWhiteSpace(descriptionBox.Text))
            {
                textBox.Text = " Descripcion";
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}