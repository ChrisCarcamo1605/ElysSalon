using Core.Domain.Entities;

namespace Application.DTOs.Response.TicketDetails;

public record DTOGetTicketDetails(
    int ticketDetailsId,
    Core.Domain.Entities.Ticket ticket,
    Article article,
    int quantity,
    decimal price,
    decimal totalPrice);