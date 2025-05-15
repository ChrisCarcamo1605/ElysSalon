namespace ElysSalon2._0.aplication.DTOs.Request.Tickets;

public record DtoCreateTicket(DateTime EmissionDateTime, string Issuer, decimal TotalAmount);