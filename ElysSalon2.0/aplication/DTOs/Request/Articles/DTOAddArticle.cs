namespace ElysSalon2._0.aplication.DTOs.Request.Articles;

public record DTOAddArticle(
    string Name,
    int ArticleTypeId,
    string PriceCost,
    string PriceBuy,
    string Stock,
    string Description);