using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using AutoMapper;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.aplication.Interfaces.Repositories;
using ElysSalon2._0.aplication.Interfaces.Services;
using ElysSalon2._0.aplication.Validations;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.domain.Services;

public class ArticleService : IArticleService
{
    private readonly IRepository<Article> _articleRepository;
    private readonly IRepository<ArticleType> _typeRepository;
    private readonly IMapper _mapper;
    private ObservableCollection<Article> _articlesCollection;
    private ObservableCollection<ArticleType> _typesCollection;

    public ArticleService(IRepository<Article> articleRepository, IRepository<ArticleType> typeRepository,
        IMapper mapper)
    {
        _articleRepository = articleRepository;
        _typeRepository = typeRepository;
        _mapper = mapper;
    }

    public event Action reloadItems;
    public event Action clearForms;


    public async Task<ResultFromService> AddArticle(DTOAddArticle dto)
    {
        _articlesCollection = await _articleRepository.GetAllAsync();
        var validate = ArticleValidations.ValidateAddArticle(dto, _articlesCollection);
        if (validate.Success is false) return validate;

        await _articleRepository.SaveAsync(_mapper.Map<Article>(dto));
        reloadItems?.Invoke();
        clearForms?.Invoke();
        return validate;
    }


    public async Task<ResultFromService> UpdateArticle(DTOUpdateArticle dto)
    {
        _articlesCollection = await _articleRepository.GetAllAsync();

        var article = await _articleRepository.FindAsync(x => x.ArticleId == dto.ArticleId);

        var isValidted = ArticleValidations.ValidateUpdateArticle(article, dto, _articlesCollection);
        if (isValidted.Success is false) return isValidted;
        await _articleRepository.UpdateAsync((Article)isValidted.Data);

        reloadItems?.Invoke();
        clearForms?.Invoke();

        return isValidted;
    }

    public async Task<ResultFromService> DeleteArticle(int id)
    {
        if (id == 0) return ResultFromService.Failed("Seleccione un artículo para eliminar");

        await _articleRepository.DeleteAsync(await _articleRepository.FindAsync(x => x.ArticleId == id));
        reloadItems?.Invoke();
        return ResultFromService.SuccessResult("Artículo eliminado correctamente");
    }

    public async Task<ResultFromService> AddType(string name)
    {
        _typesCollection = await _typeRepository.GetAllAsync();
        var validate = ArticleValidations.ValidateAddType(name, _typesCollection);

        if (validate.Success is false) return validate;

        await _typeRepository.SaveAsync(new ArticleType { Name = name });
        reloadItems?.Invoke();
        return ResultFromService.SuccessResult("Tipo creado correctamente");
    }

    public async Task<ResultFromService> EditType(ArticleType type)
    {
        var articleType =
            await _typeRepository.FindAsync(x => x.Name != type.Name && x.ArticleTypeId == type.ArticleTypeId);

        if (articleType == null) return ResultFromService.Failed("Tipo ya existente");

        var validated = ArticleValidations.ValidateUpdateType(type.Name, articleType);
        await _typeRepository.UpdateAsync((ArticleType)validated.Data);

        return ResultFromService.SuccessResult(validated.Data, "Tipo actualizado correctamente");
    }

    public async Task<ResultFromService> DeleteType(int id)
    {
        if (id == 0) return ResultFromService.Failed("Seleccione un artículo para eliminar");

        await _typeRepository.DeleteAsync(await _typeRepository.FindAsync(x => x.ArticleTypeId == id));
        reloadItems?.Invoke();
        return ResultFromService.SuccessResult("Tipo eliminado correctamente");
    }

    public async Task<ObservableCollection<DTOGetArticlesButton>> GetArticlesToButtons()
    {
        return await _articleRepository.FindAsync(
            x => x.Stock >= 1,
            x => new DTOGetArticlesButton(x.ArticleId, x.Name, x.PriceBuy)
        );
    }
}