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

    public void AddArticle(DTOAddArticle article){

        DbUtil db = DbUtil.getInstance();

        var d = new Dictionary<string, object>
        {
            {"article_name" , article.articleName},
            {"article_type_id" , db.getIdFrom("article_type","article_type_id","article_type_name",article.articleType)},
            {"price_Buy" , article.priceBuy},
            {"price_cost" , article.priceCost},
            {"stock" , article.stock},
            {"description" , article.description}

        };

        db.AddToDb<Article>("Article",d);
    }

    public void UpdateArticle(Article article){
        throw new NotImplementedException();
    }

    public List<ArticleType> getTypeArticle(){
        DbUtil db = DbUtil.getInstance();
        ObservableCollection<ArticleType> articleTypes = db.getFromDB("article_type", "*", (reader) =>
        {
            return new ArticleType(new DTOGetTypeArticles(
                reader.GetInt32(0),
                reader.GetString(1)));
        });
        return articleTypes.ToList();
    }

}