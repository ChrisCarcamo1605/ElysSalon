﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElySalon
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void adminBtn_Click(object sender, RoutedEventArgs e)
        {
           
            
        }

        private async void btnServices_Click(object sender, RoutedEventArgs e)
        {
            ArticlesWindow articlesWindow = new ArticlesWindow();
          
            await Task.Delay(450);
            this.Close();
            articlesWindow.Show();
        }
    }
}
