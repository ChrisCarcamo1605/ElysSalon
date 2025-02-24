using System.Collections.ObjectModel;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Repositories;

public interface IArticleRepository {
    ObservableCollection<DTOGetArticlesButton> GetArticlesToButton();
    ObservableCollection<Article> GetArticles();
    Article GetArticle(int id);
    void AddArticle(Article article);
    void UpdateArticle(Article article);
    void DeleteArticle(int id);
}
