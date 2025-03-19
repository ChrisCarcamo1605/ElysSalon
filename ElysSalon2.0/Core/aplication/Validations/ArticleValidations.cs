using System.Collections.ObjectModel;
using ElysSalon2._0.Core.aplication.DTOs.DTOArticle;
using ElysSalon2._0.Core.aplication.Ports.Repositories;
using ElysSalon2._0.Core.domain.Entities;
using ElysSalon2._0.Core.domain.Services;

namespace ElysSalon2._0.Core.aplication.Validations;

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

    public static ServiceResult ValidateAddArticle(DTOAddArticle dto, ObservableCollection<Article> articles)
    {
        var existing = articles.FirstOrDefault(x => x.Name == dto.Name);


        if (string.IsNullOrWhiteSpace(dto.Name)) return ServiceResult.Failed("Ingrese un nombre válido");

        if (dto.ArticleTypeId == 2) return ServiceResult.Failed("Seleccione un tipo de artículo");

        if (!decimal.TryParse(dto.PriceBuy, out _) || Convert.ToDecimal(dto.PriceBuy) == 0)
            return ServiceResult.Failed("El precio de Venta debe ser un número válido.");

        if (!decimal.TryParse(dto.PriceCost, out _) || Convert.ToDecimal(dto.PriceCost) == 0)
            return ServiceResult.Failed("El precio de Costo debe ser un número válido.");

        if (!int.TryParse(dto.Stock, out _) || Convert.ToDecimal(dto.PriceCost) == 0)
            return ServiceResult.Failed("El stock debe ser un número entero válido.");

        if (existing != null) return ServiceResult.Failed("Articulo ya existente");

        return ServiceResult.SuccessResult("Articulo guardado exitosamente!");
    }

    public static ServiceResult ValidateUpdateArticle(Article art, DTOUpdateArticle dto,
        ObservableCollection<Article> articles)
    {
        var existing = articles.FirstOrDefault(x => x.Name == dto.Name);


        if (string.IsNullOrWhiteSpace(dto.Name)) return ServiceResult.Failed("Ingrese un nombre válido");

        if (dto.ArticleTypeId == 2) return ServiceResult.Failed("Seleccione un tipo de artículo");

        if (dto.PriceBuy == 0 || !decimal.TryParse(dto.PriceBuy.ToString(), out _))
            return ServiceResult.Failed("El precio de Venta debe ser un número válido.");

        if (dto.PriceCost == 0 || !decimal.TryParse(dto.PriceCost.ToString(), out _))
            return ServiceResult.Failed("El precio de Costo debe ser un número válido.");

        if (dto.Stock == 0 || !int.TryParse(dto.Stock.ToString(), out _))
            return ServiceResult.Failed("El stock debe ser un número entero válido.");

        if (existing != null)
            if (existing.Name != art.Name)
                return ServiceResult.Failed("Articulo ya existente");

        return ServiceResult.SuccessResult("Articulo actualizado correctamente!");
    }


    public static ServiceResult ValidateAddType(string name, ObservableCollection<ArticleType> articleTypes)
    {
        var existing = articleTypes.FirstOrDefault(x => x.Name == name);


        if (string.IsNullOrWhiteSpace(name)) return ServiceResult.Failed("Ingrese un nombre");

        if (existing != null) return ServiceResult.Failed("Tipo ya existente");

        return ServiceResult.SuccessResult("Tipo creado correctamente");
    }

    public static ServiceResult ValidateUpdateType(ArticleType type, ObservableCollection<ArticleType> articleTypes)
    {
        var existing = articleTypes.FirstOrDefault(x => x.Name == type.Name);


        if (string.IsNullOrWhiteSpace(type.Name)) return ServiceResult.Failed("Ingrese un nombre");

        if (existing != null) return ServiceResult.Failed("Tipo ya existente");

        return ServiceResult.SuccessResult("Tipo actualizado correctamente");
    }
}