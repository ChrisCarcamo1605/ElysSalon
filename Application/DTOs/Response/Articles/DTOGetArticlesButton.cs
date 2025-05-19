namespace Application.DTOs.Response.Articles;

public record DTOGetArticlesButton(
    int ArticleId,
    string Name,
    decimal Price);