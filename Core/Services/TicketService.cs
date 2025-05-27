using Core.Common;
using Core.Domain.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Core.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository tiRepository)
    {
        _ticketRepository = (ITicketRepository)tiRepository;
    }

    public async Task<ResultFromService> AddAsync(Ticket ticket)
    {
        try
        {
            await _ticketRepository.SaveAsync(ticket);
            return ResultFromService.SuccessResult();
        }
        catch (Exception e)
        {
            return ResultFromService.Failed(e.Message);
        }
    }

    public async Task<ResultFromService> DeleteAsync(string id)
    {
        try
        {
            await _ticketRepository.DeleteAsync(await _ticketRepository.GetByIdAsync(id));
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
            return ResultFromService.SuccessResult(await _ticketRepository.GetAllAsync());
        }
        catch (Exception e)
        {
            return ResultFromService.Failed(e.Message);
        }
    }

    public async Task<ResultFromService> GetLastIdAsync()
    {
        try
        {
            return ResultFromService.SuccessResult(await _ticketRepository.GetLastId());
        }
        catch (Exception e)
        {
            return ResultFromService.Failed(e.Message);
        }
    }
}