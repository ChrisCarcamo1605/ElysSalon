using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.adapters.OutBound.Repository;

public class TicketDetailsRepository : ITicketDetailsRepository
{
    public void createTicketDetails(TicketDetails ticket)
    {
    }

    public List<TicketDetails> GetTicketDetails(string ticketId)
    {
        throw new NotImplementedException();
    }
}