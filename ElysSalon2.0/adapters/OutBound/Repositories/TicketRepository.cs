using System.Collections.ObjectModel;
using System.Windows;
using ElysSalon2._0.adapters.OutBound.DataBase;
using ElysSalon2._0.Core.aplication.Ports.Repositories;
using ElysSalon2._0.Core.domain.Entities;
using ElysSalon2._0.Core.domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ElysSalon2._0.adapters.OutBound.Repositories;

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

    public async Task<ObservableCollection<TicketDetails>> GetTicketDetailsAsync()
    {
        var ticketDetails = await _context.TicketDetails.Include(td=> td.Article).ToListAsync();

        return new ObservableCollection<TicketDetails>(ticketDetails);
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