using ElysSalon2._0.Core.domain.Entities;

namespace ElysSalon2._0.Core.aplication.DTOs.DTOArticle;

public record DTOGetArticlesRepository(
    int articleId,
    string articleName,
    ArticleType articleType,
    decimal priceCost,
    decimal priceBuy,
    int stock,
    string description)
{
}