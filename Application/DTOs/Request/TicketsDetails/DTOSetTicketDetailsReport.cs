using Core.Domain.Entities;

namespace Application.DTOs.Request.TicketsDetails;

public record DTOSetTicketDetailsReport(
    int ticketDetId,
    string Article,
    int Quantity,
    DateTime Date,
    decimal PriceBuy,
    decimal TotalPrice)
{
   
}