using System.ComponentModel;
using System.Windows;

namespace ElysSalon2._0.aplication.DTOs.DTOArticle;

public record DTOGetArticleGrid()
{
    public int articleId { get; set; }
    public domain.Entities.ArticleType articleType { get; set; }
    public string articleName { get; set; }

    public decimal priceCost { get; set; }
    public decimal priceBuy { get; set; }
    public decimal stocl { get; set; }
    public int stock { get; set; }
    public string description { get; set; }
};