using ElysSalon2._0.adapters.OutBound.DataBase;
using ElysSalon2._0.aplication.Interfaces.Repositories;
using ElysSalon2._0.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElysSalon2._0.adapters.OutBound.Repositories;

public class TicketRepository : Repository<Ticket>, ITicketRepository
{
    private readonly ElyDbContext _context;

    public TicketRepository(ElyDbContext context) : base(context)
    {
        _context = context;
    }


    public async Task<string> GetLastId()
    {
        return await _context.Tickets.OrderByDescending(t => t.TicketId)
            .Select(t => t.TicketId)
            .FirstOrDefaultAsync();
    }
}