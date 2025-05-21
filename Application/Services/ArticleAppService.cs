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
    public event Action? clearForms;
    public event Action? reloadItems;
    private readonly IArticleService _articleService;
    private readonly IArtTypeService _typeService;
    private readonly IMapper _map;

    public ArticleAppService(IArticleService articleService, IArtTypeService typeService, IMapper map)
    {
        _articleService = articleService;
        _typeService = typeService;
        _map = map;
    }

    public async Task<ResultFromService> GetArticlesAsync()
    {
        var articles = await _articleService.GetArticlesAsync();
        return articles.Success
            ? ResultFromService.SuccessResult(_map.Map<ObservableCollection<DTOGetArticle>>(articles.Data))
            : articles;
    }

    public Task<ResultFromService> AddArticleAsync(DTOAddArticle article)
    {
        return _articleService.AddArticleAsync(_map.Map<Article>(article));
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
        return await _typeService.GetTypesAsync();
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