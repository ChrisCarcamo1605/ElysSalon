using System.Collections.ObjectModel;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Repositories;

public interface IArticleRepository {
    ObservableCollection<DTOGetArticlesButton> GetArticlesToButton();
    ObservableCollection<DTOGetArticles> GetArticles();
    Article GetArticle(int id);
    void AddArticle(DTOAddArticle article);
    void UpdateArticle(Article article);
    void DeleteArticle(int id);
}
