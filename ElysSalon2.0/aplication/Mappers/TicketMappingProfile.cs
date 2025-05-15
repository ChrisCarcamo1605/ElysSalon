using AutoMapper;
using ElysSalon2._0.aplication.DTOs.Request.SalesData;
using ElysSalon2._0.aplication.DTOs.Request.Tickets;
using ElysSalon2._0.aplication.DTOs.Response.Ticket;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Mappers;

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