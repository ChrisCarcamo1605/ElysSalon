using ElysSalon2._0.aplication.DTOs.DTOArticle;

namespace ElysSalon2._0.aplication.Services;

public interface IArticleService
{
    public event Action clearForms;
    public event Action reloadItems;
    Task AddArticle(DTOAddArticle dto);
    Task EditArticle(DTOUpdateArticle article);
    Task DeleteArticle(int id);
}