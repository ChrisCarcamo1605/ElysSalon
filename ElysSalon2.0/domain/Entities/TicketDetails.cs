using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.RightsManagement;
using Windows.UI.Notifications;
using ElysSalon2._0.aplication.DTOs.DTOTicketDetails;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using AutoMapper.Configuration.Annotations;

namespace ElysSalon2._0.domain.Entities;

public class TicketDetails : INotifyPropertyChanged
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TicketDetailsId { get; set; }

    public string TicketId { get; set; } 
    public int ArticleId { get; set; } 

    private int _quantity;

    public int Quantity
    {
        get { return _quantity; }
        set
        {
            if (SetField(ref _quantity, value))
            {
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
    }

    public decimal Price { get; set; }

    
    public decimal TotalPrice
    {
        get { return Quantity * Price; }
        
    }

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