using Application.DTOs.Request.SalesData;
using Application.DTOs.Request.Tickets;
using Application.DTOs.Response.Tickets;
using AutoMapper;
using Core.Domain.Entities;

namespace Application.Mappers;

public class TicketMappingProfile : Profile
{
    public TicketMappingProfile()
    {
        //Ticket to DTO
        CreateMap<Ticket, DTOGetTicket>();
        CreateMap<DtoCreateTicket, Ticket>();
        CreateMap<DTOSalesData, Ticket>();
    }
}