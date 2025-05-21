using Core.Domain.Entities;

namespace Application.DTOs.Response.Articles;

public record DTOGetArticlesButton(
    Article Article,
    string Name,
    decimal Price);