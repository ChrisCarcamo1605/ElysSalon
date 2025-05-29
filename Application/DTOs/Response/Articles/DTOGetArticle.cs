using Core.Domain.Entities;

namespace Application.DTOs.Response.Articles;

public record DTOGetArticle(
    int ArticleId,
    string Name,
    ArticleType Type,
    decimal PriceCost,
    decimal PriceBuy,
    int Stock,
    string Description)
{
    public DTOGetArticle(Article article) : this(article.ArticleId, 
        article.Name, article.ArticleType, article.PriceCost, article.PriceBuy,
        article.Stock, article.Description)
    {
    }

    public DTOGetArticle() : this(0, string.Empty, new ArticleType(), 0, 0, 0, string.Empty)
    {
    }
};