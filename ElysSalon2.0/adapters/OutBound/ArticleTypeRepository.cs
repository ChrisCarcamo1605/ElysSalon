using System.Collections.ObjectModel;
using System.Windows.Controls;
using ElysSalon2._0.aplication;
using ElysSalon2._0.aplication.DTOs;
using ElysSalon2._0.aplication.DTOs;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.adapters.OutBound;

public class ArticleTypeRepository : IArticleTypeRepository
{
    private readonly ElyDbContext _context;

    public ArticleTypeRepository(ElyDbContext context)
    {
        _context = context;
    }


    public void deleteType(int id)
    {
        _context.Remove(_context.ArticleTypes.Find(id));
    }

    public void addType(string type_name)
    {
        _context.ArticleTypes.Add(new ArticleType { ArticleTypeName = type_name });
    }


    public void updateType(ArticleType articleType)
    {
        _context.ArticleTypes.Update(articleType);
    }

    public ObservableCollection<ArticleType> getTypes()
    {
        var types = _context.ArticleTypes.ToList();

        return new ObservableCollection<ArticleType>(types);
    }

    public int getTypeId(string type_name)
    {
        var type = _context.ArticleTypes.FirstOrDefault(x => x.ArticleTypeName == type_name);
        return type.articleTypeId;
    }

    public ArticleType getArticleType(int id)
    {
        return _context.ArticleTypes.Find(id);
    }
}