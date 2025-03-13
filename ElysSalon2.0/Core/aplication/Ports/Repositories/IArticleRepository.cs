using System.Collections.ObjectModel;
using ElysSalon2._0.Core.aplication.DTOs.DTOArticle;
using ElysSalon2._0.Core.domain.Entities;

namespace ElysSalon2._0.Core.aplication.Ports.Repositories;

public interface IArticleRepository
{
    Task<ObservableCollection<DTOGetArticlesButton>> GetArticlesToButtonAsync();
    Task<ObservableCollection<Article>> GetArticlesAsync();
    Task<Article> GetArticleAsync(int id);
    Task AddArticleAsync(Article article);
    Task UpdateArticleAsync(Article article);
    Task DeleteArticleAsync(int id);
}