using Core.Domain.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Persistence.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

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