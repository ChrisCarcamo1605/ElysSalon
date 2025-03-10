namespace ElysSalon2._0.aplication.DTOs.DTOArticle;

public record DTOAddArticle(
    string Name,
    int ArticleTypeId,
    decimal PriceCost,
    decimal PriceBuy,
    int Stock,
    string Description);