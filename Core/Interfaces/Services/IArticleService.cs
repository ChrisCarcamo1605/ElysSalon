using System.Collections.ObjectModel;
using Core.Common;
using Core.Domain.Entities;


namespace Core.Interfaces.Services;

public interface IArticleService
{
    public event Action clearForms;
    public event Action reloadItems;
    Task<ResultFromService> AddArticle(Article article);
    Task<ResultFromService> UpdateArticle(Article article);
    Task<ResultFromService> DeleteArticle(int id);
    Task<ResultFromService> AddType(string typeName);
    Task<ResultFromService> EditType(ArticleType type);
    Task<ResultFromService> DeleteType(int id);

    Task<ObservableCollection<Article>> GetArticlesToButtons();
}