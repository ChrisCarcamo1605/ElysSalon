using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.DTOs.Response.Article;

public record DTOGetArticle(
    int articleId,
    string articleName,
    ArticleType articleType,
    decimal priceCost,
    decimal priceBuy,
    int stock,
    string description);