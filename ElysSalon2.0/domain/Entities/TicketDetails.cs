using Windows.UI.Notifications;
using ElysSalon2._0.aplication.DTOs.DTOTicketDetails;

namespace ElysSalon2._0.domain.Entities;

public class TicketDetails {
    private int ticketDetailsId { get; set; }
    private Ticket ticket { get; set; }
    private Article article { get; set; }
    private int quantity { get; set; }
    private decimal price { get; set; }

    public TicketDetails(DtoCreateTicketDetails dto)
    {
        this.ticketDetailsId = dto.TicketDetailsId;
        this.ticket = dto.Ticket;
        this.article = dto.Article;
        this.quantity = dto.quantity;
        this.price = dto.price;
    }
}