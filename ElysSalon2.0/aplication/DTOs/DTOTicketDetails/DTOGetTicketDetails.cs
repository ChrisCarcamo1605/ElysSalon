using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.DTOs.DTOTicketDetails;

public record DTOGetTicketDetails(int ticketDetailsId, Ticket ticket, Article article, int quantity, decimal amount);