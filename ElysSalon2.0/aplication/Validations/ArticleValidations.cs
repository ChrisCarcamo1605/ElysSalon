using System.Collections.ObjectModel;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.aplication.Interfaces.Repositories;
using ElysSalon2._0.domain.Entities;
using ElysSalon2._0.domain.Services;

namespace ElysSalon2._0.aplication.Validations;

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

    public static ResultFromService ValidateAddArticle(DTOAddArticle dto, ObservableCollection<Article> articles)
    {
        var existing = articles.FirstOrDefault(x => x.Name == dto.Name);


        if (string.IsNullOrWhiteSpace(dto.Name)) return ResultFromService.Failed("Ingrese un nombre válido");

        if (dto.ArticleTypeId == 2) return ResultFromService.Failed("Seleccione un tipo de artículo");

        if (!decimal.TryParse(dto.PriceBuy, out _) || Convert.ToDecimal(dto.PriceBuy) == 0)
            return ResultFromService.Failed("El precio de Venta debe ser un número válido.");

        if (!decimal.TryParse(dto.PriceCost, out _) || Convert.ToDecimal(dto.PriceCost) == 0)
            return ResultFromService.Failed("El precio de Costo debe ser un número válido.");

        if (!int.TryParse(dto.Stock, out _) || Convert.ToDecimal(dto.PriceCost) == 0)
            return ResultFromService.Failed("El stock debe ser un número entero válido.");

        if (existing != null) return ResultFromService.Failed("Articulo ya existente");

        return ResultFromService.SuccessResult("Articulo guardado exitosamente!");
    }

    public static ResultFromService ValidateUpdateArticle(Article art, DTOUpdateArticle dto,
        ObservableCollection<Article> articles)
    {
        var existing = articles.FirstOrDefault(x => x.Name == dto.Name);


        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            return ResultFromService.Failed("Ingrese un nombre válido");
        }

        if (dto.ArticleTypeId == 2) return ResultFromService.Failed("Seleccione un tipo de artículo");

        if (dto.PriceBuy == 0 || !decimal.TryParse(dto.PriceBuy.ToString(), out _))
            return ResultFromService.Failed("El precio de Venta debe ser un número válido.");

        if (dto.PriceCost == 0 || !decimal.TryParse(dto.PriceCost.ToString(), out _))
            return ResultFromService.Failed("El precio de Costo debe ser un número válido.");

        if (dto.Stock == 0 || !int.TryParse(dto.Stock.ToString(), out _))
            return ResultFromService.Failed("El stock debe ser un número entero válido.");

        if (existing != null)
            if (existing.Name != art.Name)
                return ResultFromService.Failed("Articulo ya existente");

        art.Name = dto.Name;
        art.PriceBuy = dto.PriceBuy;
        art.PriceCost = dto.PriceCost;
        art.Stock = dto.Stock;
        art.ArticleTypeId = dto.ArticleTypeId;
        art.Description = dto.Description;
        return ResultFromService.SuccessResult(art, "Articulo actualizado correctamente!");
    }


    public static ResultFromService ValidateAddType(string name, ObservableCollection<ArticleType> articleTypes)
    {
        var existing = articleTypes.FirstOrDefault(x => x.Name == name);


        if (string.IsNullOrWhiteSpace(name)) return ResultFromService.Failed("Ingrese un nombre");

        if (existing != null) return ResultFromService.Failed("Tipo ya existente");

        if (name.Any(char.IsDigit))
        {
            return ResultFromService.Failed("El nombre no debe contener números.");
        }

        return ResultFromService.SuccessResult("Tipo creado correctamente");
    }

    public static ResultFromService ValidateUpdateType(string name, ArticleType type)
    {
        if (string.IsNullOrWhiteSpace(type.Name)) return ResultFromService.Failed("Ingrese un nombre");

        if (name.Any(char.IsDigit))
        {
            return ResultFromService.Failed("El nombre no debe contener números.");
        }

        type.Name = name;

        return ResultFromService.SuccessResult(type, "Tipo actualizado correctamente");
    }
}