using System.Collections.ObjectModel;
using ElysSalon2._0.domain.Entities;
using ElysSalon2._0.domain.Services;

namespace ElysSalon2._0.aplication.Interfaces.Repositories;

public interface ITicketRepository : IRepository<Ticket>
{
    Task<string> GetLastId();

}