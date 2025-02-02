using System.Collections.ObjectModel;
using ElysSalon2._0.aplication.DTOs;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.domain.Entities;
using Microsoft.Data.SqlClient;

namespace ElysSalon2._0.adapters.OutBound;

public class ArticleRepository : IArticleRepository {
    private static DbUtil db;

    public ObservableCollection<Article> GetArticles(){
        db = DbUtil.getInstance();

        ObservableCollection<Article> articles = db.getFromDB("Article", "*", (reader) =>
        {
            return new Article(new DTOGetArticles(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetInt32(2),
                reader.GetDecimal(3),
                reader.GetDecimal(4),
                reader.GetInt32(5),
                reader.GetString(6)));
        });

        return articles;
    }

    public Article GetArticle(int id){
        db = DbUtil.getInstance();

        Article article = (Article)db.getFromDB(id, "Article", "Article_id", (reader) => new Article(new DTOGetArticle(
            reader.GetInt32(0),
            reader.GetString(1),
            reader.GetInt32(2),
            reader.GetDecimal(3),
            reader.GetDecimal(4),
            reader.GetInt32(5),
            reader.GetString(6))));

        return article;
    }

    public void AddArticle(Article article){
        throw new NotImplementedException();
    }

    public void UpdateArticle(Article article){
        throw new NotImplementedException();
    }
}