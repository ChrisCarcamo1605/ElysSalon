using System.Collections.ObjectModel;
using Core.Common;
using Core.Domain.Entities;


namespace Core.Interfaces.Services;

public interface IArticleService
{
    public event Action clearForms;
    public event Action reloadItems;
    Task<ResultFromService> AddArticleAsync(Article article);
    Task<ResultFromService> UpdateArticleAsync(Article article);
    Task<ResultFromService> DeleteArticleAsync(int id);
    Task<ResultFromService> GetArticleAsync(int id);
    Task<ResultFromService> GetArticlesAsync();
    Task<ObservableCollection<Article>> GetArticlesToButtonsAsync();
}