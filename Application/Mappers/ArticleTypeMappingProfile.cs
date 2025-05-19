using Application.DTOs.Response.Articles;
using AutoMapper;
using Core.Domain.Entities;

namespace Application.Mappers;

public class ArticleTypeMappingProfile : Profile
{
    public ArticleTypeMappingProfile()
    {
        CreateMap<ArticleType, DTOGetArticle>();
    }
}