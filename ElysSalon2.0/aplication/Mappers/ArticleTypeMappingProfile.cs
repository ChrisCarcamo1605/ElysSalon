using AutoMapper;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Mappers;

public class ArticleTypeMappingProfile : Profile
{
    public ArticleTypeMappingProfile()
    {
        CreateMap<ArticleType, DTOGetArticle>();
    }
}