using System.Windows;
using ElysSalon2._0.adapters.InBound.UI.views.AdminViews;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.ViewModels;
using ElysSalon2._0.domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace ElysSalon2._0.aplication.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IArticleTypeRepository _typeRepository;

    public ArticleService(IArticleRepository articleRepository, IArticleTypeRepository typeRepository)
    {
        _articleRepository = articleRepository;
        _typeRepository = typeRepository;
    }

    public event Action reloadItems;
    public event Action clearForms;


    public async Task AddArticle(DTOAddArticle dto)
    {
        if (string.IsNullOrEmpty(dto.articleName))
        {
            MessageBox.Show("Ingrese un nombre");
            return;
        }

        if (dto.priceCost.IsNullOrEmpty())
        {
            MessageBox.Show("Ingrese un precio ");
            return;
        }

        if (dto.priceBuy.IsNullOrEmpty())
        {
            MessageBox.Show("Ingrese un precio venta");
            return;
        }

        if (dto.typeId == 2)
        {
            MessageBox.Show("Seleccione un tipo de artículo");
            return;
        }

        if (!decimal.TryParse(dto.priceBuy.ToString(), out _))
        {
            MessageBox.Show("El precio de Venta debe ser un número válido.");
            return;
        }

        if (!decimal.TryParse(dto.priceCost.ToString(), out _))
        {
            MessageBox.Show("El precio de Costo debe ser un número válido.");
            return;
        }


        if (!int.TryParse(dto.stock.ToString(), out _))
        {
            MessageBox.Show("El stock debe ser un número entero válido.");
            return;
        }

        if (dto.stock <= 0)
        {
            MessageBox.Show("Ingrese la cantidad en stock.");
            return;
        }

        var newArticle = new Article
        {
            Name = dto.articleName,
            ArticleTypeId = dto.typeId,
            PriceBuy = Math.Round(Convert.ToDecimal(dto.priceBuy), 2),
            PriceCost = Math.Round(Convert.ToDecimal(dto.priceCost), 2),
            Stock = dto.stock,
            Description = dto.description
        };

        await _articleRepository.AddArticle(newArticle);

        MessageBox.Show("Artículo guardado con éxito!");
        reloadItems?.Invoke();
        clearForms?.Invoke();
    }


    public async Task UpdateArticle(Article _article)
    {
        if (string.IsNullOrEmpty(_article.Name))
        {
            MessageBox.Show("Ingrese un nombre");
            return;
        }

        if (_article.ArticleTypeId == 2)
        {
            MessageBox.Show("Seleccione un tipo de artículo");
            return;
        }

        if (!decimal.TryParse(_article.PriceBuy.ToString(), out _))
        {
            MessageBox.Show("El precio de Venta debe ser un número válido.");
            return;
        }

        if (!decimal.TryParse(_article.PriceCost.ToString(), out _))
        {
            MessageBox.Show("El precio de Costo debe ser un número válido.");
            return;
        }


        if (!int.TryParse(_article.Stock.ToString(), out _))
        {
            MessageBox.Show("El stock debe ser un número entero válido.");
            return;
        }

        if (_article.Stock <= 0)
        {
            MessageBox.Show("Ingrese la cantidad en stock.");
            return;
        }

        _article.ArticleTypeId = _article.ArticleTypeId;
        _article.Name = _article.Name;
        _article.PriceBuy = decimal.Round(Convert.ToDecimal(_article.PriceBuy), 2);
        _article.Stock = _article.Stock;
        _article.PriceCost = decimal.Round(Convert.ToDecimal(_article.PriceCost), 2);
        _article.Description = _article.Description;
        await _articleRepository.UpdateArticle(_article);
        MessageBox.Show("Artículo actualizado correctamente");
        reloadItems?.Invoke();
    }

    public async Task DeleteArticle(int id)
    {
        await _articleRepository.DeleteArticle(id);
        +
    }

    public async Task AddType(String Name)
    {
        _typeRepository.addType(Name);
        reloadItems?.Invoke();
    }

    public async Task EditType(ArticleType type)
    {
        _typeRepository.updateType(type);
    }

    public async Task DeleteType(int id)
    {
        _typeRepository.deleteType(id);
        reloadItems?.Invoke();
    }
}