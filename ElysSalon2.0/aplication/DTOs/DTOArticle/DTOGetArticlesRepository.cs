using System.Windows;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.DTOs.DTOArticle;

public record DTOGetArticlesRepository(
    int articleId,
    string articleName,
    ArticleType articleType,
    decimal priceCost,
    decimal priceBuy,
    int stock,
    string description)
{
}