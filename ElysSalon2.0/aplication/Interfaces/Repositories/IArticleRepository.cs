using System.Collections.ObjectModel;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Interfaces.Repositories;

public interface IArticleRepository
{
    Task<ObservableCollection<DTOGetArticlesButton>> GetArticlesToButtonAsync();
    Task<ObservableCollection<Article>> GetArticlesAsync();
    Task<Article> GetArticleAsync(int id);
    Task AddArticleAsync(Article article);
    Task UpdateArticleAsync(Article article);
    Task DeleteArticleAsync(int id);
}