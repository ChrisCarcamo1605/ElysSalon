using System.Collections.ObjectModel;
using Core.Common;
using Core.Domain.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Validations;

namespace Core.Services;

public class ArticleService : IArticleService
{
    private readonly IRepository<Article> _articleRepository;
    private readonly IRepository<ArticleType> _typeRepository;
    private ObservableCollection<Article> _articlesCollection;
    private ObservableCollection<ArticleType> _typesCollection;

    public ArticleService(IRepository<Article> articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public event Action reloadItems;
    public event Action clearForms;


    public async Task<ResultFromService> AddArticleAsync(Article article)
    {
        _articlesCollection = await _articleRepository.GetAllAsync();
        var validate = ArticleValidations.ValidateAddArticle(article, _articlesCollection);
        if (validate.Success is false) return validate;

        await _articleRepository.SaveAsync(article);
        reloadItems?.Invoke();
        clearForms?.Invoke();
        return validate;
    }


    public async Task<ResultFromService> UpdateArticleAsync(Article article)
    {
        _articlesCollection = await _articleRepository.GetAllAsync();

        var isArticle = await _articleRepository.FindAsync(x => x.ArticleId == article.ArticleId);

        var isValidted = ArticleValidations.ValidateUpdateArticle(article, isArticle, _articlesCollection);
        if (isValidted.Success is false) return isValidted;
        await _articleRepository.UpdateAsync((Article)isValidted.Data);

        reloadItems?.Invoke();
        clearForms?.Invoke();

        return isValidted;
    }

    public async Task<ResultFromService> DeleteArticleAsync(int id)
    {
        if (id == 0) return ResultFromService.Failed("Seleccione un artículo para eliminar");

        await _articleRepository.DeleteAsync(await _articleRepository.FindAsync(x => x.ArticleId == id));
        reloadItems?.Invoke();
        return ResultFromService.SuccessResult("Artículo eliminado correctamente");
    }


    public async Task<ObservableCollection<Article>> GetArticlesToButtonsAsync()
    {
        return await _articleRepository.FindAsync(
            x => x.Stock >= 1,
            x => new Article
            {
                ArticleId = x.ArticleId, Name = x.Name, PriceBuy = x.PriceBuy
            }
        );
    }

    public async Task<ResultFromService> GetArticleAsync(int id)
    {
        if (id <= 0) return ResultFromService.Failed("Seleccione un artículo para editar");
        try
        {
            var article = await _articleRepository.FindAsync(x => x.ArticleId == id);
            return ResultFromService.SuccessResult(article);
        }
        catch (Exception e)
        {
            return ResultFromService.Failed(e.Message);
        }
    }

    public async Task<ResultFromService> GetArticlesAsync()
    {
        try
        {
            var articles = await _articleRepository.GetAllAsync();
            return ResultFromService.SuccessResult(articles);
        }
        catch (Exception e)
        {
            return ResultFromService.Failed(e.Message);
        }
    }
}