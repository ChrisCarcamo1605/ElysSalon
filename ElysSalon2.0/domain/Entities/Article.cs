using ElysSalon2._0.aplication.DTOs.DTOArticle;

namespace ElysSalon2._0.domain.Entities;

public class Article {
    public int articleId { get; set; }
    public string articleName { get; set; }
    public ArticleType articleType { get; set; }
    public decimal priceCost { get; set; }
    public decimal priceBuy { get; set; }
    public int stock { get; set; }
    public string description { get; set; }



    public Article(DTOAddArticle dto)
    {
        this.articleName = dto.articleName;
        this.articleType = dto.articleType;
        this.priceCost = dto.priceCost;
        this.priceBuy = dto.priceBuy;
        this.stock = dto.stock;
        this.description = dto.description;
    }


    public Article(DTOGetArticlesButton dto){
        this.articleId = dto.articleId;
        this.articleName = dto.articleName;
   
    }

    public Article(DTOGetArticle dto){
        this.articleId = dto.articleId;
        this.articleName = dto.articleName;
        this.articleType.articleTypeId = dto.articleType;
        this.priceCost = dto.priceCost;
        this.priceBuy = dto.priceBuy;
        this.stock = dto.stock;
        this.description = dto.description;
    }



    public Article(DTOGetArticlesRepository dto)
    {
        this.articleName = dto.articleName;
        this.articleType = dto.articleType;
        this.priceCost = dto.priceCost;
        this.priceBuy = dto.priceBuy;
        this.stock = dto.stock;
        this.description = dto.description;

    }


    public Article(DTOUpdateArticle dto){
        articleId = dto.articleId;

        if (dto.articleName != null) 
        {
            this.articleName = dto.articleName;
        }


        if (dto.articleType != null)
        {
            this.articleType.articleTypeId = dto.articleType;
        }


        if (dto.priceCost != null)
        {
            this.priceCost = dto.priceCost;
        }


        if (dto.priceBuy != null)
        {
            this.priceBuy = dto.priceBuy;
        }

        if (dto.stock != null)
        {
            this.stock = dto.stock;
        }


        if (dto.description != null)
        {
            this.description = dto.description;
        }
    }
}