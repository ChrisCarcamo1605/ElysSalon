namespace Application.DTOs.Request.Articles;

public record DTOAddArticle(
    string Name,
    int ArticleTypeId,
    string PriceCost,
    string PriceBuy,
    string Stock,
    string Description);