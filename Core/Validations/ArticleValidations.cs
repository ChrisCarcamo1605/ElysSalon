using System.Collections.ObjectModel;
using Core.Common;
using Core.Domain.Entities;
using Core.Interfaces.Repositories;

namespace Core.Validations;

public class ArticleValidations
{
    private readonly IArticleRepository _artRepo;
    private readonly IArticleTypeRepository _typeRepo;
    private bool _isValid;
    private string _message;

    public ArticleValidations(IArticleRepository artRepo, IArticleTypeRepository typeRepo)
    {
        _artRepo = artRepo;
        _typeRepo = typeRepo;
    }

    public static ResultFromService ValidateAddArticle(Article article, ObservableCollection<Article> articles)
    {
        var existing = articles.FirstOrDefault(x => x.Name == article.Name);


        if (string.IsNullOrWhiteSpace(article.Name)) return ResultFromService.Failed("Ingrese un nombre válido");

        if (article.ArticleTypeId == 2) return ResultFromService.Failed("Seleccione un tipo de artículo");

        if (!decimal.TryParse(article.PriceBuy.ToString(), out _) || Convert.ToDecimal(article.PriceBuy) == 0)
            return ResultFromService.Failed("El precio de Venta debe ser un número válido.");

        if (!decimal.TryParse(article.PriceCost.ToString(), out _) || Convert.ToDecimal(article.PriceCost) == 0)
            return ResultFromService.Failed("El precio de Costo debe ser un número válido.");

        if (!int.TryParse(article.Stock.ToString(), out _) || Convert.ToDecimal(article.PriceCost) == 0)
            return ResultFromService.Failed("El stock debe ser un número entero válido.");

        if (existing != null) return ResultFromService.Failed("Articulo ya existente");

        return ResultFromService.SuccessResult("Articulo guardado exitosamente!");
    }

    public static ResultFromService ValidateUpdateArticle(Article art, Article articleToUp,
        ObservableCollection<Article> articles)
    {
        var existing = articles.FirstOrDefault(x => x.Name == articleToUp.Name);


        if (string.IsNullOrWhiteSpace(articleToUp.Name)) return ResultFromService.Failed("Ingrese un nombre válido");

        if (articleToUp.ArticleTypeId == 2) return ResultFromService.Failed("Seleccione un tipo de artículo");

        if (articleToUp.PriceBuy == 0 || !decimal.TryParse(articleToUp.PriceBuy.ToString(), out _))
            return ResultFromService.Failed("El precio de Venta debe ser un número válido.");

        if (articleToUp.PriceCost == 0 || !decimal.TryParse(articleToUp.PriceCost.ToString(), out _))
            return ResultFromService.Failed("El precio de Costo debe ser un número válido.");

        if (articleToUp.Stock == 0 || !int.TryParse(articleToUp.Stock.ToString(), out _))
            return ResultFromService.Failed("El stock debe ser un número entero válido.");

        if (existing != null)
            if (existing.Name != art.Name)
                return ResultFromService.Failed("Articulo ya existente");

        art.Name = articleToUp.Name;
        art.PriceBuy = articleToUp.PriceBuy;
        art.PriceCost = articleToUp.PriceCost;
        art.Stock = articleToUp.Stock;
        art.ArticleTypeId = articleToUp.ArticleTypeId;
        art.Description = articleToUp.Description;
        return ResultFromService.SuccessResult(art, "Articulo actualizado correctamente!");
    }


    public static ResultFromService ValidateAddType(string name, ObservableCollection<ArticleType> articleTypes)
    {
        var existing = articleTypes.FirstOrDefault(x => x.Name == name);


        if (string.IsNullOrWhiteSpace(name)) return ResultFromService.Failed("Ingrese un nombre");

        if (existing != null) return ResultFromService.Failed("Tipo ya existente");

        if (name.Any(char.IsDigit)) return ResultFromService.Failed("El nombre no debe contener números.");

        return ResultFromService.SuccessResult("Tipo creado correctamente");
    }

    public static ResultFromService ValidateUpdateType(string name, ArticleType type)
    {
        if (string.IsNullOrWhiteSpace(type.Name)) return ResultFromService.Failed("Ingrese un nombre");

        if (name.Any(char.IsDigit)) return ResultFromService.Failed("El nombre no debe contener números.");

        type.Name = name;

        return ResultFromService.SuccessResult(type, "Tipo actualizado correctamente");
    }
}