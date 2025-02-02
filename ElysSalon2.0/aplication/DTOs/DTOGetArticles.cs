namespace ElysSalon2._0.aplication.DTOs;

public record DTOGetArticles(
    int articleId,
    string articleName,
    int articleType,
    decimal priceCost,
    decimal priceBuy,
    int stock,
    string description);