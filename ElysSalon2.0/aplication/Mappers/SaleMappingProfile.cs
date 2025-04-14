using AutoMapper;
using ElysSalon2._0.aplication.DTOs.DTOSales;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Mappers;

public class SaleMappingProfile : Profile
{
    public SaleMappingProfile()
    {
        CreateMap<Sales, DtoSalesList>();
        CreateMap<DtoSalesList, Sales>();
    }
}