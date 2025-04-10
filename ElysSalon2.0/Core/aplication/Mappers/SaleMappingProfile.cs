using AutoMapper;
using ElysSalon2._0.Core.aplication.DTOs.DTOSales;
using ElysSalon2._0.Core.domain.Entities;

namespace ElysSalon2._0.Core.aplication.Mappers;

public class SaleMappingProfile : Profile
{
    public SaleMappingProfile()
    {
        CreateMap<Sales, DtoSalesList>();
        CreateMap<DtoSalesList, Sales>();
    }

}