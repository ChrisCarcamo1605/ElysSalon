using Core.Domain.Entities;

namespace Core.Interfaces.Repositories;

public interface ITicketRepository : IRepository<Ticket>
{
    Task<Ticket> GetLastId();
}