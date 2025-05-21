using Core.Common;
using Core.Domain.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Core.Services;

public class TicketDetailsService : ITicketDetailsService
{
    private readonly IRepository<TicketDetails> _tDetailsService;

    public TicketDetailsService(IRepository<TicketDetails> tDetailsService)
    {
        _tDetailsService = tDetailsService;
    }


    public async Task<ResultFromService> AddAsync(TicketDetails Tdetails)
    {
        try
        {
            await _tDetailsService.SaveAsync(Tdetails);
            return ResultFromService.SuccessResult();
        }
        catch (Exception e)
        {
            return ResultFromService.Failed(e.Message);
        }
    }

    public async Task<ResultFromService> AddRange(List<TicketDetails> ticketDetails)
    {
        try
        {
            await _tDetailsService.SaveRangeAsync(ticketDetails);
            return ResultFromService.SuccessResult();
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
            await _tDetailsService.DeleteAsync(await _tDetailsService.GetByIdAsync(id));
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
            var ticketDetails = await _tDetailsService.GetAllAsync();
            return ResultFromService.SuccessResult(ticketDetails);
        }
        catch (Exception e)
        {
            return ResultFromService.Failed(e.Message);
        }
    }
}