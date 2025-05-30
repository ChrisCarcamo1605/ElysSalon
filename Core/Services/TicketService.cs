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
            var ticketSaved = await _ticketRepository.SaveAsync(ticket);
            return ResultFromService.SuccessResult(ticketSaved);
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

    public async Task<ResultFromService> GetByIdAsync(string id)
    {
        try
        {
            var result = await _ticketRepository.FindAsync(x => x.TicketId == id);

            return result != null ? ResultFromService.SuccessResult() :
                ResultFromService.Failed("No se encontró el ticket con el ID proporcionado.");
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
            return ResultFromService.SuccessResult(await _ticketRepository.GetLastId(), "operacion exitosa");
        }
        catch (Exception e)
        {
            return ResultFromService.Failed(e.Message);
        }
    }
}