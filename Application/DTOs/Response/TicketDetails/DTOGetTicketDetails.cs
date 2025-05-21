using Core.Domain.Entities;

namespace Application.DTOs.Response.TicketDetails;

public record DTOGetTicketDetails(
    int TicketDetailsId,
    Ticket Ticket,
    Article Article,
    int Quantity,
    decimal PriceBuy,
    decimal TotalPrice);