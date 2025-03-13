using AutoMapper;
using ElysSalon2._0.Core.aplication.DTOs.DTOTicket;
using ElysSalon2._0.Core.domain.Entities;

namespace ElysSalon2._0.Core.aplication.Mappers;

public class TicketMappingProfile : Profile
{
    public TicketMappingProfile()
    {
        //Ticket to DTO
        CreateMap<Ticket, DTOGetTicket>();
        CreateMap<DtoCreateTicket, Ticket>();
    }
}