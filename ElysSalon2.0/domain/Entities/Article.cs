using ElysSalon2._0.aplication.DTOs;

namespace ElysSalon2._0.domain.Entities;

public class Article {
    private int articleId;
    private string articleName;
    private int articleType;
    private decimal priceCost;
    private decimal priceBuy;
    private int stock;
    private string description;


    public Article(DTOGetArticles dto){
        articleId = dto.articleId;
        articleName = dto.articleName;
        articleType = dto.articleType;
        priceCost = dto.priceCost;
        priceBuy = dto.priceBuy;
        stock = dto.stock;
        description = dto.description;
    }

    public Article(DTOGetArticle dto){
        articleId = dto.articleId;
        articleName = dto.articleName;
        articleType = dto.articleType;
        priceCost = dto.priceCost;
        priceBuy = dto.priceBuy;
        stock = dto.stock;
        description = dto.description;
    }

    public void setArticleId(int id){
        articleId = id;
    }

    public void setArticleName(string name){
        articleName = name;
    }

    public void setArticleType(int type){
        articleType = type;
    }

    public void setPriceCost(decimal cost){
        priceCost = cost;
    }

    public void setPriceBuy(decimal buy){
        priceBuy = buy;
    }

    public void setStock(int stock){
        this.stock = stock;
    }

    public void setDescription(string description){
        this.description = description;
    }

    public int getArticleId(){
        return articleId;
    }

    public string getArticleName(){
        return articleName;
    }

    public int getArticleType(){
        return articleType;
    }

    public decimal getPriceCost(){
        return priceCost;
    }

    public decimal getPriceBuy(){
        return priceBuy;
    }

    public int getStock(){
        return stock;
    }

    public string getDescription(){
        return description;
    }
}