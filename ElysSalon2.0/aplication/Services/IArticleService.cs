using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Services;

public interface IArticleService
{
    public event Action clearForms;
    public event Action reloadItems;
    Task AddArticle(DTOAddArticle dto);
    Task UpdateArticle(Article _article);
    Task DeleteArticle(int id);
}