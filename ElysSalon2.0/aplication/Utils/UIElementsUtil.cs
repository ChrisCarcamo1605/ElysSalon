﻿using System.ComponentModel;
using System.Net.Mime;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Identity.Client;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using System.Text.RegularExpressions;

namespace ElysSalon2._0.aplication.Utils;

public static class UIElementsUtil
{



    public static void NumericOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !Regex.IsMatch(e.Text, @"^\d*$"); 
    }

    public static void TextBoxGotFocus(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox)
        {
            textBox.Clear(); 
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

    public static void searchInGrid(string search, ICollectionView view)
    {
        string _searchText = search.ToLower();

        if (string.IsNullOrWhiteSpace(_searchText) || _searchText.Equals("nombre..."))
        {
            view.Filter = null;
        }
        else
        {
            view.Filter = (obj) =>
            {
                if (obj is ArticleGrid article)
                {
                    return article.articleName.ToLower().Contains(_searchText);
                }

                return false;
            };
        }

        view.Refresh();
    }

}