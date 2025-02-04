using ElysSalon2._0.aplication.DTOs;

namespace ElysSalon2._0.domain.Entities {
    public class ArticleType {
        public int articleTypeId { get; set; }
        public string name { get; set; }


        public ArticleType(DTOGetTypeArticles dto){
            this.articleTypeId = dto.typeId;
            this.name = dto.typeName;
        }
    }
}