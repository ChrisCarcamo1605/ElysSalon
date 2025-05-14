using AutoMapper;
using ElysSalon2._0.aplication.DTOs.DTOSales;
using ElysSalon2._0.aplication.DTOs.DTOTicket;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Mappers;

public class TicketMappingProfile : Profile
{
    public TicketMappingProfile()
    {
        //Ticket to DTO
        CreateMap<Ticket, DTOGetTicket>();
        CreateMap<DtoCreateTicket, Ticket>();

        CreateMap<DtoSalesList, Ticket>();
    }
}