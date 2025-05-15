using AutoMapper;
using ElysSalon2._0.aplication.DTOs.Request.SalesData;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Mappers;

public class SaleMappingProfile : Profile
{
    public SaleMappingProfile()
    {
        CreateMap<Sales, DTOSalesData>();
        CreateMap<DTOSalesData, Sales>();
    }
}