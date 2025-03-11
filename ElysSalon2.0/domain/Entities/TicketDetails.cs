using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ElysSalon2._0.domain.Entities;

public class TicketDetails : INotifyPropertyChanged
{
    private int _quantity;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TicketDetailsId { get; set; }

    public string TicketId { get; set; }

    public string ArticleName { get; set; }
    public int ArticleId { get; set; }

    public int Quantity
    {
        get => _quantity;
        set
        {
            if (SetField(ref _quantity, value)) OnPropertyChanged(nameof(TotalPrice));
        }
    }

    public decimal Price { get; set; }


    public decimal TotalPrice => Quantity * Price;

    [ForeignKey("TicketId")] public virtual Ticket Ticket { get; set; }

    [ForeignKey("ArticleId")] public virtual Article Article { get; set; }
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