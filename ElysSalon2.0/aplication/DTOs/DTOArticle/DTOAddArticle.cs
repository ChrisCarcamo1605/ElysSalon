namespace ElysSalon2._0.aplication.DTOs.DTOArticle;

public record DTOAddArticle(
    string articleName,
    int typeId,
    string priceCost,
    string priceBuy,
    int stock,
    string description);