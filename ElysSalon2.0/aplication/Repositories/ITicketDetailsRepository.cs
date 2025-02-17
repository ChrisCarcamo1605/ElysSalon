using ElysSalon2._0.aplication.DTOs.DTOTicketDetails;

namespace ElysSalon2._0.aplication.Repositories;

public interface ITicketDetailsRepository
{
    void createTicketDetails(DtoCreateTicketDetails dto);
    List<DTOGetTicketDetails> GetTicketDetails(string ticketId);
}