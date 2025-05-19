using Core.Domain.Entities;

namespace Application.DTOs.Response.Articles;

public record DTOGetArticle(
    int articleId,
    string articleName,
    ArticleType articleType,
    decimal priceCost,
    decimal priceBuy,
    int stock,
    string description);