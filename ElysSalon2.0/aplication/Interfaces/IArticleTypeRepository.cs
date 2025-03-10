using System.Collections.ObjectModel;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Repositories;

public interface IArticleTypeRepository
{
    Task DeleteTypeAsync(int id);
    Task AddTypeAsync(string type_name);
    Task UpdateTypeAsync(ArticleType articleType);
    Task<ObservableCollection<ArticleType>> GetTypesAsync();
    Task<int> GetTypeIdAsync(string type_name);

    Task<ArticleType> GetArticleTypeAsync(int id);
}