using Application.DTOs.Request.SalesData;
using Application.DTOs.Request.TicketsDetails;
using AutoMapper;
using Core.Domain.Entities;

namespace Application.Mappers;

public class SaleMappingProfile : Profile
{
    public SaleMappingProfile()
    {
        CreateMap<Sales, DTOSalesData>();
        CreateMap<DTOSalesData, Sales>();

        CreateMap<Sales, DtoCreateTicketDetails>();
        CreateMap<DTOSalesData, Sales>();
    }
}