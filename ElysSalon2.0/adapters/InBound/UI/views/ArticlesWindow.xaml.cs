﻿using System.Windows;

namespace ElysSalon2._0.adapters.InBound.UI.views
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
            ConfirmWindow confirmWindow = new ConfirmWindow();
            confirmWindow.Show();
        }
    }
}