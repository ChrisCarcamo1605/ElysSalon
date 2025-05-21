using Core.Common;
using Core.Domain.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Core.Services;

public class ExpenseService : IExpensesService
{
    private readonly IRepository<Expense> _expenseRepo;

    public ExpenseService(IRepository<Expense> expenseRepo)
    {
        _expenseRepo = expenseRepo;
    }

    public async Task<ResultFromService> AddAsync(Expense expense)
    {
        try
        {
            var sales = await _expenseRepo.SaveAsync(expense);
            return ResultFromService.SuccessResult(sales);
        }
        catch (Exception e)
        {
            return ResultFromService.Failed(e.Message);
        }
    }

    public async Task<ResultFromService> DeleteAsync(int id)
    {
        try
        {
            await _expenseRepo.DeleteAsync(await _expenseRepo.GetByIdAsync(id));
            return ResultFromService.SuccessResult();
        }
        catch (Exception e)
        {
            return ResultFromService.Failed(e.Message);
        }
    }

    public async Task<ResultFromService> GetAllOfAsync()
    {
        try
        {
            var expenses = await _expenseRepo.GetAllAsync();
            return ResultFromService.SuccessResult(expenses);
        }
        catch (Exception e)
        {
            return ResultFromService.Failed(e.Message);
        }
    }
}