using System.Collections.ObjectModel;
using Application.DTOs.Request.Articles;
using Application.DTOs.Response.Articles;
using AutoMapper;
using Core.Common;
using Core.Domain.Entities;
using Core.Interfaces.Services;

namespace Application.Services;

public class ArticleAppService : IDisposable
{
    private readonly IArticleService _articleService;
    private readonly IMapper _map;
    private readonly IArtTypeService _typeService;

    public ArticleAppService(IArticleService articleService, IArtTypeService typeService, IMapper map)
    {
        _articleService = articleService;
        _typeService = typeService;
        _map = map;
    }

    public void Dispose()
    {
        ClearForms = null;
        ReloadItems = null;
    }

    public event Action? ClearForms;
    public event Action? ReloadItems;

    public async Task<ResultFromService> GetArticlesAsync()
    {
        var artResult = await _articleService.GetArticlesAsync();
        var artsGetted = new ObservableCollection<DTOGetArticle>();

        var articles = (ObservableCollection<Article>)artResult.Data;
        if (articles != null)
            foreach (var a in articles)
                artsGetted.Add(new DTOGetArticle(a));

        return artResult.Success
            ? ResultFromService.SuccessResult(artsGetted)
            : artResult;
    }

    public Task<ResultFromService> AddArticleAsync(DTOAddArticle article)
    {
        return _articleService.AddArticleAsync(new Article
        {
            ArticleTypeId = article.ArticleTypeId,
            Name = article.Name,
            PriceCost = decimal.Parse(article.PriceCost),
            PriceBuy = decimal.Parse(article.PriceBuy),
            Stock = int.Parse(article.Stock),
            Description = article.Description
        });
    }

    public Task<ResultFromService> UpdateArticleAsync(DTOUpdateArticle article)
    {
        var result = _articleService.UpdateArticleAsync(_map.Map<Article>(article));
        OnReloadItems();
        OnClearForms();
        return result;
    }

    public Task<ResultFromService> DeleteArticleAsync(int id)
    {
        return _articleService.DeleteArticleAsync(id);
    }

    public Task<ResultFromService> AddType(string typeName)
    {
        return _typeService.AddTypeAsync(typeName);
    }

    public async Task<ResultFromService> EditTypeAsync(int id, string typeName)
    {
        var findResult = await _typeService.GetTypeAsync(id);
        var type = (ArticleType)findResult.Data;
        type.Name = typeName;
        return await _typeService.EditTypeAsync(type);
    }

    public async Task<ResultFromService> GetTypesAsync()
    {
        var result = await _typeService.GetTypesAsync();
        var types = (ObservableCollection<ArticleType>)result.Data;
        var typesGetted = new ObservableCollection<DTOGetArtType>();

        if (types != null)
            foreach (var a in types)
                typesGetted.Add(new DTOGetArtType(a.ArticleTypeId, a.Name));


        return ResultFromService.SuccessResult(typesGetted);
    }


    public Task<ResultFromService> DeleteTypeAsync(int id)
    {
        return _typeService.DeleteTypeAsync(id);
    }

    public async Task<ResultFromService> GetArticlesToButtons()
    {
        var articles = await _articleService.GetArticlesToButtonsAsync();
        var dtos = new ObservableCollection<DTOGetArticlesButton>();
        foreach (var a in articles) dtos.Add(new DTOGetArticlesButton(a));
        return ResultFromService.SuccessResult(dtos);
    }

    public async Task<ResultFromService> GetArticleAsync(int id)
    {
        var getResult = await _articleService.GetArticleAsync(id);
        var article = (Article)getResult.Data;
        return getResult.Success
            ? ResultFromService.SuccessResult(new DTOGetArticle(article))
            : getResult;
    }

    // Método para invocar ClearForms de forma segura
    protected virtual void OnClearForms()
    {
        ClearForms?.Invoke();
    }

    // Método para invocar ReloadItems de forma segura
    protected virtual void OnReloadItems()
    {
        ReloadItems?.Invoke();
    }
}