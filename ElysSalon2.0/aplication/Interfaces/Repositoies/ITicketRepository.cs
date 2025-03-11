using System.Collections.ObjectModel;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.aplication.DTOs.DTOTicket;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Repositories;

public interface ITicketRepository
{
    Task SaveTicketAsync(Ticket dto);
    Task DeleteTicketAsync(int id);
    Task<ObservableCollection<Ticket>> GetTicketsAsync();
    Task<Ticket> GetTicketAsync(string id);
    Task UpdateTicket(Ticket ticket);
    Task SaveTicketDetailRangeAsync(ObservableCollection<TicketDetails> ticketDetails);
}