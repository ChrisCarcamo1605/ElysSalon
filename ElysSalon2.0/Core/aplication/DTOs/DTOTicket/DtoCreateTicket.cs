namespace ElysSalon2._0.Core.aplication.DTOs.DTOTicket;

public record DtoCreateTicket(DateTime EmissionDateTime, string Issuer, decimal TotalAmount);