using System.Collections.ObjectModel;
using System.Windows;
using AutoMapper;
using ElysSalon2._0.Core.aplication.DTOs.DTOArticle;
using ElysSalon2._0.Core.aplication.Ports.Repositories;
using ElysSalon2._0.Core.aplication.Ports.Services;
using ElysSalon2._0.Core.aplication.Validations;
using ElysSalon2._0.Core.domain.Entities;

namespace ElysSalon2._0.Core.domain.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;
    private readonly IArticleTypeRepository _typeRepository;
    private ObservableCollection<Article> _articlesCollection;
    private ObservableCollection<ArticleType> _typesCollection;

    public ArticleService(IArticleRepository articleRepository, IArticleTypeRepository typeRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _typeRepository = typeRepository;
        _mapper = mapper;
    }

    public event Action reloadItems;
    public event Action clearForms;


    public async Task<ServiceResult> AddArticle(DTOAddArticle dto)
    {
        _articlesCollection = await _articleRepository.GetArticlesAsync();
        var validate = ArticleValidations.ValidateAddArticle(dto, _articlesCollection);
        if (validate.Success is false) return validate;

        await _articleRepository.AddArticleAsync(_mapper.Map<Article>(dto));
        reloadItems?.Invoke();
        clearForms?.Invoke();
        return validate;
    }


    public async Task<ServiceResult> UpdateArticle(DTOUpdateArticle dto)
    {
        _articlesCollection = await _articleRepository.GetArticlesAsync();
        var art = await _articleRepository.GetArticleAsync(dto.ArticleId);
        var validate = ArticleValidations.ValidateUpdateArticle(art, dto, _articlesCollection);

        if (validate.Success is false) return validate;

        art.Name = dto.Name;
        art.ArticleTypeId = dto.ArticleTypeId;
        art.PriceCost = decimal.Round(dto.PriceCost, 2);
        art.PriceBuy = decimal.Round(dto.PriceBuy, 2);
        art.Stock = dto.Stock;
        art.Description = dto.Description;
        await _articleRepository.UpdateArticleAsync(art);
        reloadItems?.Invoke();
        clearForms?.Invoke();

        return validate;
    }

    public async Task<ServiceResult> DeleteArticle(int id)
    {
        if (id == 0) return ServiceResult.Failed("Seleccione un artículo para eliminar");

        await _articleRepository.DeleteArticleAsync(id);
        return ServiceResult.SuccessResult("Artículo eliminado correctamente");
    }

    public async Task<ServiceResult> AddType(string name)
    {
        _typesCollection = await _typeRepository.GetTypesAsync();
        var validate = ArticleValidations.ValidateAddType(name, _typesCollection);

        if (validate.Success is false) return validate;

        await _typeRepository.AddTypeAsync(name);
        reloadItems?.Invoke();
        return ServiceResult.SuccessResult("Tipo creado correctamente");
    }

    public async Task<ServiceResult> EditType(ArticleType type)
    {
        var typeUpdated = await _typeRepository.GetArticleTypeAsync(type.ArticleTypeId);
        _typesCollection = await _typeRepository.GetTypesAsync();
        var validate = ArticleValidations.ValidateUpdateType(type, _typesCollection);

        if (validate.Success is false) return validate;

        typeUpdated.Name = type.Name;
        await _typeRepository.UpdateTypeAsync(typeUpdated);

        reloadItems?.Invoke();
        return ServiceResult.SuccessResult("Tipo actualizado correctamente");
    }

    public async Task<ServiceResult> DeleteType(int id)
    {
        if (id == 0) return ServiceResult.Failed("Seleccione un artículo para eliminar");

        await _typeRepository.DeleteTypeAsync(id);
        reloadItems?.Invoke();
        return ServiceResult.SuccessResult("Tipo eliminado correctamente");
    }

    public async Task<ObservableCollection<DTOGetArticlesButton>> GetArticlesToButtons()
    {
        return await _articleRepository.GetArticlesToButtonAsync(); ;
    }
}