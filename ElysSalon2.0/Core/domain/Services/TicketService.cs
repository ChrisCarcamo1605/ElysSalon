using System.Collections.ObjectModel;
using AutoMapper;
using ElysSalon2._0.Core.aplication.DTOs.DTOTicket;
using ElysSalon2._0.Core.aplication.Ports.Repositories;
using ElysSalon2._0.Core.aplication.Ports.Services;
using ElysSalon2._0.Core.domain.Entities;

namespace ElysSalon2._0.Core.domain.Services;

public class TicketService : ITicketService
{
    private readonly IMapper _mapper;
    private readonly ITicketRepository _ticketRepo;

    public TicketService(ITicketRepository ticketRepo, IMapper mapper)
    {
        _mapper = mapper;
        _ticketRepo = ticketRepo;
    }

    public async Task<ServiceResult> SaveTicketAsync(DtoCreateTicket dto)
    {
        var ticket = _mapper.Map<Ticket>(dto);
        await _ticketRepo.SaveTicketAsync(ticket);
        return ServiceResult.SuccessResult(ticket, "Ticket generado exitosamente!");
    }


    public Task<ServiceResult> DeleteTicketAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ObservableCollection<Ticket>> GetTicketsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> GetTicketAsync(string id)
    {
        var ticket = await _ticketRepo.GetTicketAsync(id);
        return ServiceResult.SuccessResult(ticket, "Ticket Encontrado!");
    }

    public async Task<ServiceResult> SaveTicketsDetailsAsync(ObservableCollection<TicketDetails> ticketDetails)
    {
        await _ticketRepo.SaveTicketDetailRangeAsync(ticketDetails);
        return ServiceResult.SuccessResult("Detalle de Ticket Creado Correctamente!");
    }

    public async Task<ServiceResult> UpdateTicket(Ticket ticket)
    {
        await _ticketRepo.UpdateTicket(ticket);
        return ServiceResult.SuccessResult("Ticket Actualizado Correctamente!");
    }
}