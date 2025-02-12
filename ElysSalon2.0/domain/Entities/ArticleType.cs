using ElysSalon2._0.aplication.DTOs.ArticleType;

namespace ElysSalon2._0.domain.Entities;

public class ArticleType {
    public int articleTypeId { get; set; }
    public string name { get; set; }


    public ArticleType(DTOGetTypeArticles dto){
        articleTypeId = dto.typeId;
        name = dto.typeName;
    }
    public ArticleType(DTOGetArticleType dto)
    {
        articleTypeId = dto.type_id;
        name = dto.type_name;
    }
}