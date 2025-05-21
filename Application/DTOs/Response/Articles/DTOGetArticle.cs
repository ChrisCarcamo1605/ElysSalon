using Core.Domain.Entities;

namespace Application.DTOs.Response.Articles;

public record DTOGetArticle(
    int ArticleId,
    string Name,
    ArticleType Type,
    decimal PriceCost,
    decimal PriceBuy,
    int Stock,
    string Description);