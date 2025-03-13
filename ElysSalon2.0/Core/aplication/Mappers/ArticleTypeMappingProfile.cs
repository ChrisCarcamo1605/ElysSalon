using AutoMapper;
using ElysSalon2._0.Core.aplication.DTOs.DTOArticle;
using ElysSalon2._0.Core.domain.Entities;

namespace ElysSalon2._0.Core.aplication.Mappers;

public class ArticleTypeMappingProfile : Profile
{
    public ArticleTypeMappingProfile()
    {
        CreateMap<ArticleType, DTOGetArticle>();
    }
}