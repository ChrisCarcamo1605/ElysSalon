using System.Collections.ObjectModel;
using System.Windows.Controls;
using ElysSalon2._0.aplication.DTOs.ArticleType;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.adapters.OutBound;

public class ArticleTypeRepository : IArticleTypeRepository {
    public void deleteType(int id){
        var db = DbUtil.getInstance();
        db.Delete("article_type", "article_type_id", id);
    }

    public void addType(string type_name){
        throw new NotImplementedException();
    }


    public void updateType(DTOGetArticleType articleType){
        var db = DbUtil.getInstance();
        var values = new Dictionary<string, Object>(
        )
        {
            { "article_type_name", articleType.type_name }
        };

        db.Update<DTOGetArticleType>("article_type","article_type_id", values, articleType.type_id);
    }

    public ObservableCollection<DTOGetTypeArticles> getTypes(){
        var db = DbUtil.getInstance();
        ObservableCollection<DTOGetTypeArticles> articleTypes = db.GetFromDB("article_type", "*", (reader) =>
        {
            return new DTOGetTypeArticles(
                reader.GetInt32(0),
                reader.GetString(1));
        });
        return articleTypes;
    }

    public int getTypeId(string type_name){
        var db = DbUtil.getInstance();

        int id = db.GetIdFrom("article_type", "article_type_id", "article_type_name", type_name);
        return id;
    }

    public ArticleType getArticleType(int id){
        var db = DbUtil.getInstance();

        var type_name = (ArticleType)db.GetFromDB(id, "article_type", "article_type_id", (reader) =>
        {
            return new ArticleType(new DTOGetArticleType(
                reader.GetInt32(0),
                reader.GetString(1)));
        });
        return type_name;
    }
}