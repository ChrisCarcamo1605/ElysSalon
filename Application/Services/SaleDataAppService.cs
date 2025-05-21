using Core.Common;
using Core.Domain.Entities;
using Core.Interfaces.Services;
using System.Collections.ObjectModel;
using Application.DTOs.Request.SalesData;
using Application.DTOs.Request.TicketsDetails;
using Application.DTOs.Response.Expense;
using Application.DTOs.Response.SalesData;
using Application.DTOs.Response.TicketDetails;
using Application.DTOs.Response.Tickets;

namespace Application.Services;

public class SaleDataAppService
{
    private readonly ISalesService _salesService;
    private readonly IExpensesService _expService;
    private readonly ITicketService _ticketService;
    private readonly ITicketDetailsService _tDetailsService;


    public SaleDataAppService(ISalesService salesService, IExpensesService expService,
        ITicketService ticketService, ITicketDetailsService tDetailsService)
    {
        _salesService = salesService;
        _expService = expService;
        _ticketService = ticketService;
        _tDetailsService = tDetailsService;
    }

    public async Task<ResultFromService> Add<T>(object obj)
    {
        switch (obj)
        {
            case Sales sales:
                return await _salesService.AddAsync(sales);
            case Ticket ticket:
                return await _ticketService.AddAsync(ticket);
            case Expense expense:
                return await _expService.AddAsync(expense);
            case TicketDetails ticketDetails:
                return await _tDetailsService.AddAsync(ticketDetails);
            default:
                return ResultFromService.Failed($"Tipo no soportado: {typeof(T)}");
        }
    }

    public async Task<ResultFromService> AddTicketDetailsRange(List<DtoCreateTicketDetails> ticketDetails)
    {
        return await _tDetailsService.AddRange(ticketDetails.Select(x => new TicketDetails()
        {
            Article = x.Article,
            ArticleId = x.Article.ArticleId,
            Quantity = x.Quantity,
            TicketId = x.Ticket.TicketId,
            Price = x.Price
        }).ToList());
    }

    public async Task<ResultFromService> Delete<T>(object objType) where T : class
    {
        switch (objType)
        {
            case DTOGetSales sales:
                return await _salesService.DeleteAsync(sales.SaleId.ToString());
            case DTOGetTicket ticket:
                return await _ticketService.DeleteAsync(ticket.TicketId);
            case DTOGetExpense expense:
                return await _expService.DeleteAsync(expense.ExpenseId);
            case DTOGetTicketDetails ticketDetails:
                return await _tDetailsService.DeleteAsync(ticketDetails.TicketDetailsId);
            default:
                return ResultFromService.Failed($"Tipo no soportado: {typeof(T)}");
        }
    }

    public async Task<ResultFromService> GetAllOf<T>() where T : class
    {
        var objType = typeof(T);

        if (objType == typeof(DTOGetSales))
        {
            var result = await _salesService.GetAllOfAsync();
            var sales = (ObservableCollection<Sales>)result.Data;
            return result.Success
                ? ResultFromService.SuccessResult(
                    new ObservableCollection<DTOSalesData>(sales.Select(x => new DTOSalesData(x))))
                : result;
        }
        else if (objType == typeof(DTOGetTicket))
        {
            var result = await _ticketService.GetAllOfAsync();
            var tickets = (ObservableCollection<Ticket>)result.Data;
            return result.Success
                ? ResultFromService.SuccessResult(
                    new ObservableCollection<DTOSalesData>(tickets.Select(x => new DTOSalesData(x))))
                : result;
        }
        else if (objType == typeof(DTOGetExpense))
        {
            var result = await _expService.GetAllOfAsync();
            var expenses = (ObservableCollection<Expense>)result.Data;
            return result.Success
                ? ResultFromService.SuccessResult(
                    new ObservableCollection<DTOSalesData>(expenses.Select(x => new DTOSalesData(x))))
                : result;
        }
        else if (objType == typeof(DTOGetTicketDetails))
        {
            var result = await _tDetailsService.GetAllOfAsync();
            var tDetails = (ObservableCollection<TicketDetails>)result.Data;
            return result.Success
                ? ResultFromService.SuccessResult(
                    new ObservableCollection<DTOSalesData>(tDetails.Select(x => new DTOSalesData(x))))
                : result;
        }
        else
        {
            return ResultFromService.Failed($"Tipo no soportado: {objType}");
        }
    }

    public async Task<ResultFromService> GetLastIdTicket()
    {
        return await _ticketService.GetLastIdAsync();
    }
}