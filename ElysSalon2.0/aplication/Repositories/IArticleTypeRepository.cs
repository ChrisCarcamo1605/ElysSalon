using System.Collections.ObjectModel;
using ElysSalon2._0.aplication.DTOs;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Repositories;

public interface IArticleTypeRepository {

    void deleteType(int id);
    void addType(string type_name);
    void updateType(ArticleType articleType);
    ObservableCollection<ArticleType> getTypes();
    int getTypeId(string type_name);

    ArticleType getArticleType(int id);

}