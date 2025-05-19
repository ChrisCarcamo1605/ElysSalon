using System.Collections.ObjectModel;
using Core.Domain.Entities;

namespace Core.Interfaces.Repositories;

public interface IArticleRepository
{
    Task<ObservableCollection<Article>> GetArticlesToButtonAsync();
    Task<ObservableCollection<Article>> GetArticlesAsync();
    Task<Article> GetArticleAsync(int id);
    Task AddArticleAsync(Article article);
    Task UpdateArticleAsync(Article article);
    Task DeleteArticleAsync(int id);
}