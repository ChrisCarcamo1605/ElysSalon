﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.domain.Entities;
using Microsoft.IdentityModel.Abstractions;

namespace ElysSalon2._0.adapters.InBound.UI {
    internal class ButtonManagement {
        private IArticleRepository _articleRepository;
        public ObservableCollection<Button> ArticlesButtons { get; set; }

        public ButtonManagement(IArticleRepository articleRepository){
            ArticlesButtons = new ObservableCollection<Button>();
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            loadButtons();
        }

        public void loadButtons(){
            var articles = _articleRepository.GetArticles();

            if (articles != null && articles.Any())
            {
                foreach (Article article in articles)
                {
                    Button btn = new Button
                    {
                        Content = article.getArticleName(),
                        Tag = article.getArticleId(),
                        Style = (Style)Application.Current.FindResource("articlesBtn")
                    };

                    ArticlesButtons.Add(btn); // Agregar el nuevo botón a la colección
                }
            }
        }
    }
}