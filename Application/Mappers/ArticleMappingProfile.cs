using Application.DTOs.Request.Articles;
using Application.DTOs.Response.Articles;
using AutoMapper;
using Core.Domain.Entities;

namespace Application.Mappers;

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