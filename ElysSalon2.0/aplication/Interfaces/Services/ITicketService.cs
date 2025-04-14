using System.Collections.ObjectModel;
using ElysSalon2._0.aplication.DTOs.DTOTicket;
using ElysSalon2._0.domain.Entities;
using ElysSalon2._0.domain.Services;

namespace ElysSalon2._0.aplication.Interfaces.Services;

public interface ITicketService
{
    Task<ServiceResult> SaveTicketAsync(DtoCreateTicket ticket);
    Task<ObservableCollection<TicketDetails>> GetTicketDetailsAsync();
    Task<ServiceResult> DeleteTicketAsync(int id);
    Task<ObservableCollection<Ticket>> GetTicketsAsync();
    Task<ServiceResult> GetTicketAsync(string id);
    Task<ServiceResult> UpdateTicket(Ticket ticket);
    Task<ServiceResult> SaveTicketsDetailsAsync(ObservableCollection<TicketDetails> ticketDetails);
}