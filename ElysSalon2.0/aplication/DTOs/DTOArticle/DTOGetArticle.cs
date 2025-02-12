namespace ElysSalon2._0.aplication.DTOs.DTOArticle;

public record DTOGetArticle(
    int articleId,
    string articleName,
    int articleType,
    decimal priceCost,
    decimal priceBuy,
    int stock,
    string description);