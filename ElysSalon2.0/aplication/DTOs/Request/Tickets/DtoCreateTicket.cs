namespace ElysSalon2._0.aplication.DTOs.Request.Tickets;

public record DtoCreateTicket(string TicketId,DateTime EmissionDateTime, string Issuer, decimal TotalAmount);