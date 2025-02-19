using System.Security.RightsManagement;
using Windows.UI.Notifications;
using ElysSalon2._0.aplication.DTOs.DTOTicketDetails;

namespace ElysSalon2._0.domain.Entities;

public class TicketDetails {
    public int ticketDetailsId { get; set; }
    public Ticket ticket { get; set; }
    public Article article { get; set; }
    public int quantity { get; set; }
    public decimal price { get; set; }
    public decimal totalPrice { get; set; }

    public TicketDetails(DtoCreateTicketDetails dto)
    {
        this.article = dto.Article;
        this.ticket = dto.Ticket;
        this.quantity = dto.quantity;
        this.price = dto.price;

    }

    
}