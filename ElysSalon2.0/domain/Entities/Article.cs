using ElysSalon2._0.aplication.DTOs;

namespace ElysSalon2._0.domain.Entities;

public class Article {
    public int articleId { get; set; }
    public string articleName { get; set; }
    public int articleType { get; set; }
    public decimal priceCost { get; set; }
    public decimal priceBuy { get; set; }
    public int stock { get; set; }
    public string description { get; set; }


    public Article(DTOGetArticlesButton dto){
        articleId = dto.articleId;
        articleName = dto.articleName;
   
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

    public Article(DTOUpdateArticle dto){
        articleId = dto.articleId;

        if (dto.articleName != null) 
        {
            articleName = dto.articleName;
        }


        if (dto.articleType != null)
        {
            articleType = dto.articleType;
        }


        if (dto.priceCost != null)
        {
            priceCost = dto.priceCost;
        }


        if (dto.priceBuy != null)
        {
            priceBuy = dto.priceBuy;
        }

        if (dto.stock != null)
        {
            stock = dto.stock;
        }


        if (dto.description != null)
        {
            description = dto.description;
        }
    }
}