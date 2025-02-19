using System.ComponentModel;
using System.Runtime.CompilerServices;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.DTOs.DTOTicketDetails;

public class DtoCreateTicketDetails :INotifyPropertyChanged
{
    public int TicketDetailsId { get; set; }
    public Ticket Ticket { get; set; }
    public Article Article { get; set; }
    private int _quantity =1;

    public int quantity
    {
        get => _quantity;
        set
        {
            if (SetField(ref _quantity, value)) // Llama a SetField y verifica si cambió
            {
                totalPrice = price * quantity; // Recalcula TotalPrice
            }
        }
    }

    private decimal _price; 
    public decimal price 
    {
        get => _price;
        set
        {
            if (SetField(ref _price, value)) 
            {
                totalPrice = price * quantity; 
            }
        }
    }
    private decimal _totalPrice;

    public decimal totalPrice
    {
        get => _totalPrice;
        set => SetField(ref _totalPrice, value);
    }

    public DtoCreateTicketDetails() { }
    public DtoCreateTicketDetails(Ticket Ticket, Article Article, int quantity, decimal price)
    {
        this.TicketDetailsId = TicketDetailsId;
        this.Ticket = Ticket;
        this.Article = Article;
        this.quantity = quantity;
        this.price = price;
        totalPrice =  price*quantity;
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