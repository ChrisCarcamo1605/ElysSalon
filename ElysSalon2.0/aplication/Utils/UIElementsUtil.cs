using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ElysSalon2._0.aplication.DTOs.DTOArticle;

namespace ElysSalon2._0.aplication.Utils;

public static class UIElementsUtil
{
    public static void NumericOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !Regex.IsMatch(e.Text, @"^[0-9]*\.?[0-9]*$") ||
                    ((sender as TextBox)?.Text.Contains('.') == true && e.Text == ".");
    }


    public static void TextBoxGotFocus(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox) textBox.Clear();
    }

    public static void lostFocus(string text, dynamic textBox)
    {
        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            textBox.Text = text;
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }

}