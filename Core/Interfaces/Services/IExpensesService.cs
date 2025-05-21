using Core.Common;
using Core.Domain.Entities;

namespace Core.Interfaces.Services;

public interface IExpensesService
{
    Task<ResultFromService> AddAsync(Expense expense);
    Task<ResultFromService> DeleteAsync(int id);
    Task<ResultFromService> GetAllOfAsync();
}