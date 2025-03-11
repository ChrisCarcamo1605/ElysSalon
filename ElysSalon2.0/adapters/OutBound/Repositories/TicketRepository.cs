using System.Collections.ObjectModel;
using ElysSalon2._0.adapters.OutBound.DataBase;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.aplication.DTOs.DTOTicket;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElysSalon2._0.adapters.OutBound.Repository;

public class TicketRepository : ITicketRepository
{
    private readonly ElyDbContext _context;

    public TicketRepository(ElyDbContext context)
    {
        _context = context;
    }


    public async Task SaveTicketAsync(Ticket ticket)
    {
        await _context.AddAsync(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTicketAsync(int id)
    {
        _context.Remove(id);
        await _context.SaveChangesAsync();
    }

    public async Task<ObservableCollection<Ticket>> GetTicketsAsync()
    {
        var tickets = await _context.Tickets.ToListAsync();
        return new ObservableCollection<Ticket>(tickets);
    }

    public async Task<Ticket> GetTicketAsync(string id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        return ticket ?? throw new NullReferenceException("Couldn't find the ticket");
    }

    public async Task SaveTicketDetailRangeAsync(ObservableCollection<TicketDetails> ticketDetails)
    {
        await _context.AddRangeAsync(ticketDetails.ToList());
        _context.SaveChanges();
    }

    public async Task UpdateTicket(Ticket ticket)
    {
        _context.Update(ticket);
        await _context.SaveChangesAsync();
    }
}