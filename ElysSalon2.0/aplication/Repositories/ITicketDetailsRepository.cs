using ElysSalon2._0.aplication.DTOs.DTOTicketDetails;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Repositories;

public interface ITicketDetailsRepository
{
    void createTicketDetails(TicketDetails ticket);
    List<TicketDetails> GetTicketDetails(string ticketId);
}