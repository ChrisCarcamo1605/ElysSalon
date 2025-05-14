using System.Collections.ObjectModel;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Interfaces.Repositories;

public interface ITicketRepository
{
    Task SaveTicketAsync(Ticket dto);
    Task DeleteTicketAsync(string id);
    Task<ObservableCollection<Ticket>> GetTicketsAsync();
    Task<ObservableCollection<TicketDetails>> GetTicketDetailsAsync();

    Task<Ticket> GetTicketAsync(string id);
    Task UpdateTicket(Ticket ticket);
    Task SaveTicketDetailRangeAsync(ObservableCollection<TicketDetails> ticketDetails);
}