using System.ComponentModel;
using System.Runtime.CompilerServices;
using Core.Domain.Entities;

namespace Application.DTOs.Request.TicketsDetails;

public class DtoCreateTicketDetails : INotifyPropertyChanged
{
    private decimal _price;
    private int _quantity = 1;
    private decimal _totalPrice;
    private string ArticleName;
    public DateTime Date;
    public DtoCreateTicketDetails()
    {
    }

    public DtoCreateTicketDetails(Ticket Ticket, Article Article, int quantity, decimal price)
    {
        TicketDetailsId = TicketDetailsId;
        this.Ticket = Ticket;
        this.Article = Article;
        ArticleName = Article.Name;
        Date= DateTime.Now;
        Quantity = quantity;
        Price = price;
        TotalPrice = price * quantity;
    }

    public int TicketDetailsId { get; set; }
    public Ticket Ticket { get; set; }
    public Article Article { get; set; }

    public int Quantity
    {
        get => _quantity;
        set
        {
            if (SetField(ref _quantity, value))
                TotalPrice = Price * Quantity;
        }
    }

    public decimal Price
    {
        get => _price;
        set
        {
            if (SetField(ref _price, value)) TotalPrice = Price * Quantity;
        }
    }

    public decimal TotalPrice
    {
        get => _totalPrice;
        set => SetField(ref _totalPrice, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}