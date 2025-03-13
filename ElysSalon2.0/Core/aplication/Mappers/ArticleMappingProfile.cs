using AutoMapper;
using ElysSalon2._0.Core.aplication.DTOs.DTOArticle;
using ElysSalon2._0.Core.domain.Entities;

namespace ElysSalon2._0.Core.aplication.Mappers;

public class ArticleMappingProfile : Profile
{
    public ArticleMappingProfile()
    {
        //Article to DTO
        CreateMap<Article, DTOGetArticle>();
        CreateMap<DTOGetArticlesButton, Article>();
        CreateMap<Article, DTOGetArticlesButton>();
        CreateMap<Article, DTOUpdateArticle>();

        //DTO to Article
        CreateMap<DTOAddArticle, Article>();
    }
}