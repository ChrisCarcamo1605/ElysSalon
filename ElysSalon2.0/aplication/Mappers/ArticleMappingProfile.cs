using AutoMapper;
using ElysSalon2._0.aplication.DTOs.Request.Articles;
using ElysSalon2._0.aplication.DTOs.Response.Article;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Mappers;

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
        CreateMap<DTOUpdateArticle, Article>();
    }
}