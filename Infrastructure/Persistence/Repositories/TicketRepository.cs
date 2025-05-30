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


    public async Task<Ticket> GetLastId()
    {
        try
        {
            return await _context.Tickets.OrderByDescending(t => t.TicketId)
                .FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error al obtener el último ID de Ticket: {e.Message}");
        }
    }
}