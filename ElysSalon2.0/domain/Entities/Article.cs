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

}