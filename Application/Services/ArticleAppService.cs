using System.Collections.ObjectModel;
using Application.DTOs.Request.Articles;
using Application.DTOs.Response.Articles;
using AutoMapper;
using Core.Common;
using Core.Domain.Entities;
using Core.Interfaces.Services;

namespace Application.Services;

public class ArticleAppService
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

    public event Action? clearForms;
    public event Action? reloadItems;

    public async Task<ResultFromService> GetArticlesAsync()
    {
        var artResult = await _articleService.GetArticlesAsync();

        var articles = (ObservableCollection<Article>)artResult.Data;
        var artsGetted = new ObservableCollection<DTOGetArticle>();
        foreach (var a in articles)
            artsGetted.Add(new DTOGetArticle(a));

        return artResult.Success
            ? ResultFromService.SuccessResult(artsGetted)
            : artResult;
    }

    public Task<ResultFromService> AddArticleAsync(DTOAddArticle article)
    {
        return _articleService.AddArticleAsync(new Article()
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
        return _articleService.UpdateArticleAsync(_map.Map<Article>(article));
    }

    public Task<ResultFromService> DeleteArticleAsync(int id)
    {
        return _articleService.DeleteArticleAsync(id);
    }

    public Task<ResultFromService> AddType(string typeName)
    {
        return _typeService.AddTypeAsync(typeName);
    }

    public async Task<ResultFromService> EditTypeAsync(string type)
    {
        var isExist = await _typeService.getTypeAsync(type);
        return await _typeService.EditTypeAsync((ArticleType)isExist.Data);
    }

    public async Task<ResultFromService> GetTypesAsync()
    {
        var result = await _typeService.GetTypesAsync();
        var types = (ObservableCollection<ArticleType>)result.Data;
        var typesGetted = new ObservableCollection<DTOGetArtType>();

        foreach (var a in types)
        {
            typesGetted.Add(new DTOGetArtType(a.ArticleTypeId, a.Name));
        }

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
        foreach (var a in articles) dtos.Add(_map.Map<DTOGetArticlesButton>(a));
        return ResultFromService.SuccessResult(dtos);
    }

    public async Task<ResultFromService> GetArticleAsync(int id)
    {
        var article = await _articleService.GetArticleAsync(id);
        return article.Success ? ResultFromService.SuccessResult(_map.Map<DTOGetArticle>(article.Data)) : article;
    }
}