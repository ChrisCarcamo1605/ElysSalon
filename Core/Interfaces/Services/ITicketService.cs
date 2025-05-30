using Core.Common;
using Core.Domain.Entities;

namespace Core.Interfaces.Services;

public interface ITicketService
{
    Task<ResultFromService> AddAsync(Ticket ticket);
    Task<ResultFromService> DeleteAsync(string id);
    Task<ResultFromService> GetAllOfAsync();
    Task<ResultFromService> GetLastIdAsync();
    Task<ResultFromService> GetByIdAsync(string id);
}