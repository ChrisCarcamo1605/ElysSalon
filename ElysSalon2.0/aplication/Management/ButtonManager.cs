using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Management;

public class ButtonManager {
    private IArticleRepository _articleRepository;
    public ObservableCollection<Button> ArticlesButtons { get; set; }

    public ButtonManager(IArticleRepository articleRepository){
        ArticlesButtons = new ObservableCollection<Button>();


        _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        loadButtons();
    }

    public void loadButtons()
    {
        var articles = _articleRepository.GetArticlesToButton();

        if (articles != null && articles.Any())
        {
            foreach (var article in articles)
            {
                var btn = new Button
                {
                    Tag = article.articleId,
                    Style = (Style)Application.Current.FindResource("articlesBtn"),
                    Padding = new Thickness(10), 
                };

                var textBlock = new TextBlock
                {
                    Text = article.articleName,
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                btn.Content = textBlock;
                ArticlesButtons.Add(btn);
            }
        }
    }

}