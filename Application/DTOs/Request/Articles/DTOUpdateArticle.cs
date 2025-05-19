namespace Application.DTOs.Request.Articles;

public record DTOUpdateArticle(
    int ArticleId,
    string Name,
    int ArticleTypeId,
    decimal PriceCost,
    decimal PriceBuy,
    int Stock,
    string Description
);