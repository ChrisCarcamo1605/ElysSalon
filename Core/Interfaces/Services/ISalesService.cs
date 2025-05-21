using Core.Common;
using Core.Domain.Entities;

namespace Core.Interfaces.Services;

public interface ISalesService
{
    Task<ResultFromService> AddAsync(Sales sales);
    Task<ResultFromService> DeleteAsync(string id);
    Task<ResultFromService> GetAllOfAsync();
}