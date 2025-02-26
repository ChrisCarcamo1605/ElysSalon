using Windows.UI.Composition.Scenes;
using AutoMapper;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Mappers;

public class ArticleMappingProfile : Profile
{
    public ArticleMappingProfile()
    {
        //Article to DTO
        CreateMap<Article, DTOGetArticle>();
        CreateMap<DTOGetArticlesButton, Article>();
        CreateMap<Article,DTOGetArticlesButton>();

        //DTO to Article
        CreateMap<DTOAddArticle, Article>();
        CreateMap<DTOUpdateArticle, Article>()
            .ForMember(dest => dest.ArticleType, opt => opt.Ignore()) // Ignora la navegación
            .AfterMap((dto, entity) => {
                entity.ArticleTypeId = dto.articleType;
            });
    }
}