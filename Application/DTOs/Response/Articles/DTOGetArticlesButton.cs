using Core.Domain.Entities;

namespace Application.DTOs.Response.Articles;

public record DTOGetArticlesButton(
    Article Article,
    string Name,
    decimal Price)
{
    public DTOGetArticlesButton()
        : this(null, string.Empty, 0)
    {
    }

    public DTOGetArticlesButton(Article article)
        : this(article, article.Name, article.PriceBuy)
    {
    }
};