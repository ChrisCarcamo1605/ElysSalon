namespace Application.DTOs.Request.Tickets;

public record DtoCreateTicket(string TicketId, DateTime EmissionDateTime, string Issuer, decimal TotalAmount);