using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Repositories;

public interface ITicketRepository {
    void saveTicket(DTOsaveTicket dto);
}