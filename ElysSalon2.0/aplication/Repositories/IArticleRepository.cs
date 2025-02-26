using System.Collections.ObjectModel;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Repositories;

public interface IArticleRepository
{
    Task<ObservableCollection<DTOGetArticlesButton>> GetArticlesToButton();
     Task<ObservableCollection<Article>>  GetArticles();
     Task<Article> GetArticle(int id);
    Task AddArticle(Article article);
    Task UpdateArticle(Article article);
    Task DeleteArticle(int id);
}