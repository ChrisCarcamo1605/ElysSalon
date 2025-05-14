using ElysSalon2._0.aplication.Interfaces.Repositories;
using ElysSalon2._0.aplication.Interfaces.Services;
using ElysSalon2._0.domain.Entities;
using System.Collections.ObjectModel;

namespace ElysSalon2._0.domain.Services;

public class SalesDatasDataService : ISalesDataService
{
    private readonly IRepository<Expense> _expenseRepo;
    private readonly IRepository<Ticket> _ticketRepo;
    private readonly IRepository<Sales> _salesRepo;
    private readonly IRepository<TicketDetails> _tickDetailsRepo;

    private ReportsConfiguration _reportConfig;

    public SalesDatasDataService(IRepository<Sales> salesRepo, IRepository<Ticket> ticketRepo
        , IRepository<Expense> expenseRepo, IRepository<TicketDetails> ticketDetailsRepoRepo)
    {
        _salesRepo = salesRepo;
        _expenseRepo = expenseRepo;
        _ticketRepo = ticketRepo;
        _tickDetailsRepo = ticketDetailsRepoRepo;
    }


    public async Task Add<T>(T obj)
    {
        switch (obj)
        {
            case Sales sales:
                await _salesRepo.SaveAsync(sales);
                break;
            case Ticket ticket:
                await _ticketRepo.SaveAsync(ticket);
                break;
            case Expense expense:
                await _expenseRepo.SaveAsync(expense);
                break;
            case TicketDetails ticketDetails:
                await _tickDetailsRepo.SaveAsync(ticketDetails);
                break;
            default:
                throw new InvalidOperationException($"Tipo no soportado: {typeof(T).Name}");
        }
    }

    public async Task<ResultFromService> Delete<T>(string id)
    {
        try
        {
            switch (typeof(T).Name)
            {
                case nameof(Sales):
                    await _salesRepo.DeleteAsync(await _salesRepo.GetByIdAsync(int.Parse(id)));
                    break;
                case nameof(Ticket):
                    await _ticketRepo.DeleteAsync(await _ticketRepo.GetByIdAsync(id));
                    break;
                case nameof(Expense):
                    await _expenseRepo.DeleteAsync(await _expenseRepo.GetByIdAsync(int.Parse(id)));
                    break;
                case nameof(TicketDetails):
                    await _tickDetailsRepo.DeleteAsync(await _tickDetailsRepo.GetByIdAsync(int.Parse(id)));
                    break;
                default:
                    return ResultFromService.Failed($"Tipo no soportado: {typeof(T).Name}");
            }

            return ResultFromService.SuccessResult("Eliminado correctamente");
        }
        catch (System.Exception ex)
        {
            return ResultFromService.Failed($"Error al eliminar: {ex.Message}");
        }
    }

    public async Task<ObservableCollection<T>> GetAllOf<T>() where T : class
    {
        Type type = typeof(T);

        if (type == typeof(Sales))
        {
            var sales = await _salesRepo.GetAllAsync();
            return new ObservableCollection<T>(sales.Cast<T>());
        }
        else if (type == typeof(Ticket))
        {
            var tickets = await _ticketRepo.GetAllAsync();
            return new ObservableCollection<T>(tickets.Cast<T>());
        }
        else if (type == typeof(Expense))
        {
            var expenses = await _expenseRepo.GetAllAsync();
            return new ObservableCollection<T>(expenses.Cast<T>());
        }
        else if (type == typeof(TicketDetails))
        {
            var tickDetails = await _tickDetailsRepo.GetAllWithIncludesAsync(x=>x.Article);
            return new ObservableCollection<T>(tickDetails.Cast<T>());
        }

        return new ObservableCollection<T>();
    }
}