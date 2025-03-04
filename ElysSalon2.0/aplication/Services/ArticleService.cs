using System.Windows;
using ElysSalon2._0.adapters.InBound.UI.views.AdminViews;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.ViewModels;
using ElysSalon2._0.domain.Entities;

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

        if (dto.typeId == 2)
        {
            MessageBox.Show("Seleccione un tipo de artículo");
            return;
        }
        if (!int.TryParse(dto.priceBuy.ToString(), out _))
        {
            MessageBox.Show("El precio de Venta debe ser un número válido.");
            return;
        }
        if (!int.TryParse(dto.priceCost.ToString(), out _))
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
    }


    public async Task EditArticle(DTOUpdateArticle dto)
    {
       
    }


    public async Task DeleteArticle(int id)
    {
        await _articleRepository.DeleteArticle(id);
    }
}