using System.Collections.ObjectModel;
using Core.Common;
using Core.Domain.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Core.Services;

public class SalesDataService : ISalesDataService
{
    private readonly IRepository<Expense> _expenseRepo;
    private readonly ITicketRepository _ticketRepo;
    private readonly IRepository<Sales> _salesRepo;
    private readonly IRepository<TicketDetails> _tickDetailsRepo;


    public SalesDataService(IRepository<Sales> salesRepo, ITicketRepository ticketRepo
        , IRepository<Expense> expenseRepo, IRepository<TicketDetails> ticketDetailsRepoRepo)
    {
        _salesRepo = salesRepo;
        _expenseRepo = expenseRepo;
        _ticketRepo = ticketRepo;
        _tickDetailsRepo = ticketDetailsRepoRepo;
    }


    public async Task<ResultFromService> Add<T>(T obj)
    {
        switch (obj)
        {
            case Sales sales:
                return ResultFromService.SuccessResult(await _salesRepo.SaveAsync(sales),
                    "Venta agregada exitosammente");
                break;
            case Ticket ticket:
                return ResultFromService.SuccessResult(await _ticketRepo.SaveAsync(ticket),
                    "Ticket agregado exitosamente");
                break;
            case Expense expense:
                return ResultFromService.SuccessResult(await _expenseRepo.SaveAsync(expense),
                    "Gasto agregado exitosamente");
                break;
            case TicketDetails ticketDetails:
                return ResultFromService.SuccessResult(await _tickDetailsRepo.SaveAsync(ticketDetails),
                    "Detalle de ticket agregado exitosamente");
                break;
            default:
                return ResultFromService.Failed($"Tipo no soportado: {typeof(T).Name}");
        }
    }

    public async Task<ResultFromService> AddRange<T>(List<T> objects)
    {
        if (objects == null || !objects.Any())
            return ResultFromService.Failed("La lista de objetos no puede ser nula o vacía.");

        switch (objects)
        {
            case List<Sales> salesList:
                await _salesRepo.SaveRangeAsync(salesList);
                return ResultFromService.SuccessResult("Ventas agregadas exitosamente");
            case List<Ticket> ticketList:
                await _ticketRepo.SaveRangeAsync(ticketList);
                return ResultFromService.SuccessResult("Tickets agregados exitosamente");
            case List<Expense> expenseList:
                await _expenseRepo.SaveRangeAsync(expenseList);
                return ResultFromService.SuccessResult("Gastos agregados exitosamente");
            case List<TicketDetails> ticketDetailsList:
                await _tickDetailsRepo.SaveRangeAsync(ticketDetailsList);
                return ResultFromService.SuccessResult("Detalles de ticket agregados exitosamente");
            default:
                return ResultFromService.Failed($"Tipo no soportado: {typeof(T).Name}");
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
        catch (Exception ex)
        {
            return ResultFromService.Failed($"Error al eliminar: {ex.Message}");
        }
    }

    public async Task<ResultFromService> GetAllOf<T>() where T : class
    {
        var type = typeof(T);
        ObservableCollection<T> data;

        if (type == typeof(Sales))
        {
            var sales = await _salesRepo.GetAllAsync();
           data = new ObservableCollection<T>(sales.Cast<T>());
        }
        else if (type == typeof(Ticket))
        {
            var tickets = await _ticketRepo.GetAllAsync();
            data = new ObservableCollection<T>(tickets.Cast<T>());
        }
        else if (type == typeof(Expense))
        {
            var expenses = await _expenseRepo.GetAllAsync();
            data = new ObservableCollection<T>(expenses.Cast<T>());
        }
        else if (type == typeof(TicketDetails))
        {
            var tickDetails = await _tickDetailsRepo.GetAllWithIncludesAsync(x => x.Article);
            data = new ObservableCollection<T>(tickDetails.Cast<T>());
        }
        else
        {
            return ResultFromService.Failed($"Tipo no soportado: {type.Name}");
        }

        return ResultFromService.SuccessResult(data);
    }

    public async Task<ResultFromService> GetLastId<T>()
    {
        try
        {
            var lastTicket = await _ticketRepo.GetLastId();
            return ResultFromService.SuccessResult(lastTicket, "ID generado exitosamente");
        }
        catch (Exception ex)
        {
            // Log del error (implementar según tu sistema de logging)
            // Logger.Error("Error generando nuevo ID", ex);

            return ResultFromService.Failed("Error al generar el nuevo ID");
        }
    }
}