using System.Windows;

namespace ElysSalon2._0.aplication.DTOs.DTOArticle;

public record DTOGetArticles
{
    public int ArticleId { get; init; }
    public string ArticleName { get; init; }
    public string ArticleType { get; init; }
    public decimal PriceCost { get; init; }
    public decimal PriceBuy { get; init; }
    public int Stock { get; init; }
    public string Description { get; init; }

    public DTOGetArticles(int articleId, string articleName, string articleType, decimal priceCost, decimal priceBuy, int stock, string description)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(articleName))
                throw new ArgumentException("El nombre del artículo no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(articleType))
                throw new ArgumentException("El tipo de artículo no puede estar vacío.");
            if (priceCost < 0)
                throw new ArgumentException("El precio de costo no puede ser negativo.");
            if (priceBuy < 0)
                throw new ArgumentException("El precio de venta no puede ser negativo.");
            if (stock < 0)
                throw new ArgumentException("El stock no puede ser negativo.");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("La descripción no puede estar vacía.");

            ArticleId = articleId;
            ArticleName = articleName;
            ArticleType = articleType;
            PriceCost = priceCost;
            PriceBuy = priceBuy;
            Stock = stock;
            Description = description;
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, "Error de validación", MessageBoxButton.OK, MessageBoxImage.Error);
            throw;
        }
    }
}
