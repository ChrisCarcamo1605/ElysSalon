﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.aplication.DTOs.DTOTicketDetails;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElysSalon2._0.aplication.Management;

public class ButtonManager : INotifyPropertyChanged
{
    public ICommand RemoveFromCartCommand { get; }
    public ICommand IncreaseQuantityCommand { get; }
    public ICommand DecreaseQuantityCommand { get; }

    private IMapper _mapper;
    private IArticleRepository _articleRepository;
    public ICollectionView cartItemsView;
    private DataGrid _grid;
    private ObservableCollection<TicketDetails> _cartItems;
    public decimal _totalPrice;

    public decimal totalPrice
    {
        get => _totalPrice;
        set
        {
            _totalPrice = value;
            OnPropertyChanged(nameof(totalPrice));
        }
    }

    public ObservableCollection<TicketDetails> cartItems
    {
        get => _cartItems;
        set
        {
            _cartItems = value;
            OnPropertyChanged(nameof(cartItems));
        }
    }

    public ObservableCollection<Button> ArticlesButtons { get; set; }

    public ButtonManager(IArticleRepository articleRepository, IMapper mapper)
    {
        _mapper = mapper;
        _cartItems = new ObservableCollection<TicketDetails>();
        cartItemsView = CollectionViewSource.GetDefaultView(_cartItems);
        ArticlesButtons = new ObservableCollection<Button>();
        _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        loadButtons();

        RemoveFromCartCommand = new RelayCommand<TicketDetails>(RemoveFromCart);
        DecreaseQuantityCommand = new RelayCommand<TicketDetails>(DecreaseFromCart);
        IncreaseQuantityCommand = new RelayCommand<TicketDetails>(IncreaseQuantity);
    }

    private void loadButtons()
    {
        var articles = _articleRepository.GetArticlesToButton();

        if (articles != null && articles.Any())
            foreach (var article in articles)
            {
                var btn = new Button
                {
                    Tag = article.articleId,
                    Style = (Style)Application.Current.FindResource("articlesBtn"),
                    Padding = new Thickness(10),
                };
                btn.Click += async (e, s) => { addToCart(_mapper.Map<Article>(article)); };
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


    private void addToCart(Article article)
    {
        var existingItem = cartItems.FirstOrDefault(x => x.article.articleId == article.articleId);

        if (existingItem != null)
        {
            existingItem.quantity++;
        }
        else
        {
            existingItem = new TicketDetails{ticketId = "1", article = article, quantity = 1, price = article.priceBuy};
            cartItems.Add(existingItem);
        }

        totalPrice += existingItem.price;
        cartItemsView?.Refresh();
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    public void RemoveFromCart(TicketDetails ticket)
    {
        if (ticket == null) ;
        cartItems.Remove(ticket);
        UpdateTotalPrice();
    }

    private void DecreaseFromCart(TicketDetails ticket)
    {
        if (ticket.quantity > 1)
        {
            ticket.quantity -= 1;
        }
        else
        {
            RemoveFromCart(ticket);
        }

        UpdateTotalPrice();
    }

    private void IncreaseQuantity(TicketDetails ticket)
    {
        if (ticket == null) return;
        ticket.quantity++;
        UpdateTotalPrice();
    }

    private void UpdateTotalPrice()
    {
        decimal total = 0;
       
        foreach (var item in cartItems)
        {
            total += item.totalPrice;
        }

        totalPrice = total;
        cartItemsView.Refresh();


    }


    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}