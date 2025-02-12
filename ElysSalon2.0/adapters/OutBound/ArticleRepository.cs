using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.domain.Entities;
using Microsoft.Data.SqlClient;

namespace ElysSalon2._0.adapters.OutBound;

public class ArticleRepository : IArticleRepository {
    private DbUtil db;
    private readonly IArticleTypeRepository _typeRepository;

    public ArticleRepository(IArticleTypeRepository typeRepository){
        _typeRepository = typeRepository;
    }

    public ObservableCollection<DTOGetArticlesButton> GetArticlesToButton(){
        db = DbUtil.getInstance();

        ObservableCollection<DTOGetArticlesButton> articles = db.GetFromDB<DTOGetArticlesButton>("Article", "*",
            (reader) =>
            {
                return new DTOGetArticlesButton(
                    reader.GetInt32(0),
                    reader.GetString(1));
            });

        return articles;
    }


    public ObservableCollection<DTOGetArticles> GetArticles(){
        db = DbUtil.getInstance();

        var articles = db.GetFromDB<DTOGetArticles>("Article", "*", (reader) =>
        {
            return new DTOGetArticles(
                reader.GetInt32(0),
                reader.GetString(1),
                _typeRepository.getArticleType((reader.GetInt32(2))).name,
                reader.GetDecimal(3),
                reader.GetDecimal(4),
                reader.GetInt32(5),
                reader.GetString(6)
            );
        });

        return articles;
    }

    public Article GetArticle(int id){
        db = DbUtil.getInstance();

        var article = (Article)db.GetFromDB(id, "Article", "Article_id", (reader) => new Article(new DTOGetArticle(
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
        var db = DbUtil.getInstance();

        var d = new Dictionary<string, object>
        {
            { "article_name", article.articleName },
            {
                "article_type_id",
                db.GetIdFrom("article_type", "article_type_id", "article_type_name", article.articleType)
            },
            { "price_Buy", article.priceBuy },
            { "price_cost", article.priceCost },
            { "stock", article.stock },
            { "description", article.description }
        };

        db.AddToDb<Article>("Article", d);
    }


    public void UpdateArticle(Article article){
        var db = DbUtil.getInstance();

        var d = new Dictionary<string, Object>
        {
            { "article_name", article.articleName },
            { "article_type_id", article.articleType },
            { "price_Buy", article.priceBuy },
            { "price_cost", article.priceCost },
            { "stock", article.stock },
            { "description", article.description }
        };
        db.Update<Article>("Article", "article_id",d, article.articleId);
    }

    public void DeleteArticle(int id){
        var db = DbUtil.getInstance();
        db.Delete("Article", "article_id", id);
    }
}