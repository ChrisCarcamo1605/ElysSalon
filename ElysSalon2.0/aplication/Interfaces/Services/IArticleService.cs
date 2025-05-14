using System.Collections.ObjectModel;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.domain.Entities;
using ElysSalon2._0.domain.Services;

namespace ElysSalon2._0.aplication.Interfaces.Services;

public interface IArticleService
{
    public event Action clearForms;
    public event Action reloadItems;
    Task<ResultFromService> AddArticle(DTOAddArticle dto);
    Task<ResultFromService> UpdateArticle(DTOUpdateArticle dto);
    Task<ResultFromService> DeleteArticle(int id);
    Task<ResultFromService> AddType(string typeName);
    Task<ResultFromService> EditType(ArticleType type);
    Task<ResultFromService> DeleteType(int id);

    Task<ObservableCollection<DTOGetArticlesButton>> GetArticlesToButtons();
}