using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.DTOs;

public record DTOUpdateArticle(
    int articleId,
    string articleName,
    int articleType,
    decimal priceCost,
    decimal priceBuy,
    int stock,
    string description);