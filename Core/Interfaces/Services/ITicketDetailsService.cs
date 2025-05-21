using Core.Common;
using Core.Domain.Entities;

namespace Core.Interfaces.Services;

public interface ITicketDetailsService
{
    Task<ResultFromService> AddAsync(TicketDetails Tdetails);
    Task<ResultFromService> AddRange(List<TicketDetails> ticketDetails);
    Task<ResultFromService> DeleteAsync(int id);
    Task<ResultFromService> GetAllOfAsync();
}