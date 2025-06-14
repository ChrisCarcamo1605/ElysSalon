﻿using System.Collections.ObjectModel;
using Application.DTOs.Request.SalesData;
using Application.DTOs.Request.Tickets;
using Application.DTOs.Request.TicketsDetails;
using Application.DTOs.Response.Expenses;
using Application.DTOs.Response.SalesData;
using Application.DTOs.Response.TicketDetails;
using Application.DTOs.Response.Tickets;
using Application.Interfaces;
using AutoMapper;
using Core.Common;
using Core.Domain.Entities;
using Core.Interfaces.Services;

namespace Application.Services;

public class SaleDataAppService : ISaleDataAppService
{
    private readonly IExpensesService _expService;
    private readonly IMapper _mapper;
    private readonly ISalesService _salesService;
    private readonly ITicketDetailsService _tDetailsService;
    private readonly ITicketService _ticketService;

    public SaleDataAppService(ISalesService salesService, IExpensesService expService,
        ITicketService ticketService, ITicketDetailsService tDetailsService, IMapper mapper)
    {
        _salesService = salesService;
        _expService = expService;
        _ticketService = ticketService;
        _mapper = mapper;
        _tDetailsService = tDetailsService;
    }

    public async Task<ResultFromService> Add<T>(object obj)
    {
        switch (obj)
        {
            case Sales sales:
                return await _salesService.AddAsync(sales);
            case DtoCreateTicket ticket:
                var ticketSaved = await _ticketService.AddAsync(_mapper.Map<Ticket>(ticket));
                return ticketSaved.Success
                    ? ResultFromService.SuccessResult(new DTOSalesData((Ticket)ticketSaved.Data),
                        "Ticket guardado correctamente")
                    : ticketSaved;
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
        var tickResul = await _ticketService.GetLastIdAsync();
        var ticket = (Ticket)tickResul.Data;


        if (!tickResul.Success)
            return ResultFromService.Failed("No se pudo encontrar el ticket asociado." + "ticket de lista: " +
                                            ticketDetails[0].Ticket.TicketId);

        return await _tDetailsService.AddRange(ticketDetails.Select(x => new TicketDetails
        {
            ArticleId = x.Article.ArticleId,
            ArticleName = x.Article.Name,
            Date = x.Date,
            Quantity = x.Quantity,
            Ticket = ticket,
            TicketId = ticket.TicketId,
            Price = x.Price
        }).ToList());
    }

    public async Task<ResultFromService> Delete<T>(string id)
    {
        try
        {
            switch (typeof(T).Name)
            {
                case nameof(DTOGetSales):
                    await _salesService.DeleteAsync(id);
                    break;
                case nameof(DTOGetTicket):
                    await _ticketService.DeleteAsync(id);
                    break;
                case nameof(DTOGetExpense):
                    await _expService.DeleteAsync(int.Parse(id));
                    break;
                case nameof(TicketDetails):
                    await _tDetailsService.DeleteAsync(int.Parse(id));
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

        if (objType == typeof(DTOGetTicket))
        {
            var result = await _ticketService.GetAllOfAsync();
            var tickets = (ObservableCollection<Ticket>)result.Data;
            return result.Success
                ? ResultFromService.SuccessResult(
                    new ObservableCollection<DTOSalesData>(tickets.Select(x => new DTOSalesData(x))))
                : result;
        }

        if (objType == typeof(DTOGetExpense))
        {
            var result = await _expService.GetAllOfAsync();
            var expenses = (ObservableCollection<Expense>)result.Data;
            return result.Success
                ? ResultFromService.SuccessResult(
                    new ObservableCollection<DTOGetExpense>(expenses.Select(x => new DTOGetExpense(x))))
                : result;
        }

        if (objType == typeof(DTOGetTicketDetails))
        {
            var result = await _tDetailsService.GetAllOfAsync();
            var tDetails = (ObservableCollection<TicketDetails>)result.Data;
            return result.Success
                ? ResultFromService.SuccessResult(
                    new ObservableCollection<DTOGetTicketDetails>(tDetails.Select(x => new DTOGetTicketDetails(x))))
                : result;
        }

        return ResultFromService.Failed($"Tipo no soportado: {objType}");
    }

    public async Task<ResultFromService> GetLastIdTicket()
    {
        var ticketResult = await _ticketService.GetLastIdAsync();
        var ticket = (Ticket)ticketResult.Data;

        if (!ticketResult.Success) return ticketResult;

        return ticketResult.Success
            ? ResultFromService.SuccessResult(new DTOSalesData(ticket))
            : ticketResult;
    }
}