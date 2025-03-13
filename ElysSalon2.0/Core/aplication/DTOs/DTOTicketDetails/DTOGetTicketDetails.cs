using ElysSalon2._0.Core.domain.Entities;

namespace ElysSalon2._0.Core.aplication.DTOs.DTOTicketDetails;

public record DTOGetTicketDetails(
    int ticketDetailsId,
    Ticket ticket,
    Article article,
    int quantity,
    decimal price,
    decimal totalPrice);