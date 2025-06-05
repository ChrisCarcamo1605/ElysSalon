using Core.Domain.Entities;

namespace Application.DTOs.Response.TicketDetails;

public record DTOGetTicketDetails(
    int TicketDetailsId,
    Ticket Ticket,
    Article Article,
    int Quantity,
    DateTime date,
    decimal PriceBuy,
    decimal TotalPrice)
{
    public DTOGetTicketDetails() : this(0, null, null, 0, DateTime.Now, 0, 0)
    {
    }

    public DTOGetTicketDetails(Core.Domain.Entities.TicketDetails ticketDetails) : this(
        ticketDetails.TicketDetailsId,
        ticketDetails.Ticket,
        ticketDetails.Article,
        ticketDetails.Quantity,
        ticketDetails.Date,
        ticketDetails.Price,
        ticketDetails.TotalPrice)
    {
    }
}