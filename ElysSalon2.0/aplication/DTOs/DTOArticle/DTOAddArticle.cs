namespace ElysSalon2._0.aplication.DTOs.DTOArticle;

public record DTOAddArticle(
    string articleName,
    string articleType,
    decimal priceCost,
    decimal priceBuy,
    int stock,
    string description);