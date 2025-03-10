using ElysSalon2._0.aplication.DTOs.DTOArticle;

namespace ElysSalon2._0.aplication.Repositories;

public interface ITicketRepository
{
    void saveTicket(DTOsaveTicket dto);
}