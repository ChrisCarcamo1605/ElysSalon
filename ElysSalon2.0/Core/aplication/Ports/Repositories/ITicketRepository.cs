using System.Collections.ObjectModel;
using ElysSalon2._0.Core.domain.Entities;
using ElysSalon2._0.Core.domain.Services;

namespace ElysSalon2._0.Core.aplication.Ports.Repositories;

public interface ITicketRepository
{
    Task SaveTicketAsync(Ticket dto);
    Task DeleteTicketAsync(int id);
    Task<ObservableCollection<Ticket>> GetTicketsAsync();
    Task<ObservableCollection<TicketDetails>> GetTicketDetailsAsync();

    Task<Ticket> GetTicketAsync(string id);
    Task UpdateTicket(Ticket ticket);
    Task SaveTicketDetailRangeAsync(ObservableCollection<TicketDetails> ticketDetails);
}