using System.Collections.ObjectModel;
using ElysSalon2._0.aplication.DTOs.ArticleType;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Repositories;

public interface IArticleTypeRepository {

    void deleteType(int id);
    void addType(string type_name);
    void updateType(DTOGetArticleType articleType);
    ObservableCollection<DTOGetTypeArticles> getTypes();
    int getTypeId(string type_name);

    ArticleType getArticleType(int id);

}