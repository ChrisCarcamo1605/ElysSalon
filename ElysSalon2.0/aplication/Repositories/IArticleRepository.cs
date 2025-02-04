using System.Collections.ObjectModel;
using ElysSalon2._0.aplication.DTOs;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Repositories;

public interface IArticleRepository {
    ObservableCollection<Article> GetArticles();
    Article GetArticle(int id);
    void AddArticle(DTOAddArticle article);
    void UpdateArticle(Article article);

    List<ArticleType> getTypeArticle();
}