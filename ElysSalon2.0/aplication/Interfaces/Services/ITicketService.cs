using System.Collections.ObjectModel;
using ElysSalon2._0.aplication.DTOs.DTOTicket;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Services;

public interface ITicketService
{
    Task<ServiceResult> SaveTicketAsync(DtoCreateTicket ticket);
    Task<ServiceResult> DeleteTicketAsync(int id);
    Task<ObservableCollection<Ticket>> GetTicketsAsync();
    Task<ServiceResult> GetTicketAsync(string id);
    Task<ServiceResult> UpdateTicket(Ticket ticket);
    Task<ServiceResult> SaveTicketsDetailsAsync(ObservableCollection<TicketDetails> ticketDetails);
}