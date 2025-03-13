using ElysSalon2._0.Core.domain.Entities;

namespace ElysSalon2._0.Core.aplication.DTOs.DTOArticle;

public record DTOsaveTicket(
    TicketDetails ticketDetails,
    DateTime emissionDate,
    decimal withOutTax,
    decimal withTax,
    decimal totalAmount)
{
}