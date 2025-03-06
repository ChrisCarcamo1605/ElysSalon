using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.DTOs.DTOArticle;

public record DTOUpdateArticle(
    int articleId,
    string Name,
    int typeId,
    string priceCost,
    string priceBuy,
    int stock,
    string description
);