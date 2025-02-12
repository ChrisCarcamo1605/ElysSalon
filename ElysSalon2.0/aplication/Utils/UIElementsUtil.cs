using System.ComponentModel;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Identity.Client;
using ElysSalon2._0.aplication.DTOs.DTOArticle;

namespace ElysSalon2._0.aplication.Utils;

public static class UIElementsUtil
{


    public static void onlyDigits(dynamic textBox, TextCompositionEventArgs e)
    {
        char caracter = e.Text[0];

        if (!char.IsDigit(caracter) && !char.IsControl(caracter) && caracter != '.')
        {
            e.Handled = true;
        }
        else if (caracter == '.' && textBox.Text.Contains('.')) // Evitar múltiples puntos
        {
            e.Handled = true;
        }
    }

    public static void gotFocus(string text, dynamic textBox)
    {
        if (!string.IsNullOrWhiteSpace(textBox.Text))
        {
            textBox.Text = "";
            textBox.Foreground = new SolidColorBrush(Colors.Black);
        }
        else
        {
            textBox.Text = text;
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }

    public static void lostFocus(string text, dynamic textBox)
    {
        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            textBox.Text = text;
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }

    public static void searchInGrid(TextChangedEventArgs e, TextBox searchTxtBox, ICollectionView view)
    {
        string searchText = searchTxtBox.Text.ToLower();

        if (string.IsNullOrWhiteSpace(searchText) || searchText.Equals("nombre..."))
        {
            view.Filter = null;
        }
        else
        {
            view.Filter = (obj) =>
            {
                if (obj is DTOGetArticles article)
                {
                    return article.article_name.ToLower().Contains(searchText);
                }

                return false;
            };
        }

        view.Refresh();
    }

}