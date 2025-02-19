using ElysSalon2._0.aplication.DTOs.ArticleType;

namespace ElysSalon2._0.domain.Entities;

public class ArticleType {
    public int articleTypeId { get; set; }
    public string ArticleTypeName { get; set; }


    public ArticleType(DTOGetTypeArticles dto){
        articleTypeId = dto.typeId;
        ArticleTypeName = dto.article_type;
    }
    public ArticleType(DTOGetArticleType dto)
    {
        articleTypeId = dto.type_id;
        ArticleTypeName = dto.type_name;
    }
}