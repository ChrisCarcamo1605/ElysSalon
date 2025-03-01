using System.Collections.ObjectModel;
using System.Windows.Controls;
using ElysSalon2._0.aplication;
using ElysSalon2._0.aplication.DTOs;
using ElysSalon2._0.aplication.DTOs;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.domain.Entities;
using Microsoft.EntityFrameworkCore;

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
        _context.Remove(_context.ArticleType.Find(id));
        _context.SaveChanges();
    }

    public void addType(string type_name)
    {
        _context.ArticleType.Add(new ArticleType { Name = type_name });
        _context.SaveChanges();
    }


    public void updateType(ArticleType articleType)
    {
        
        _context.ArticleType.Update(articleType);
        _context.SaveChanges();
    }

    public async Task<ObservableCollection<ArticleType>> getTypes()
    {
            var types = await _context.ArticleType.ToListAsync();
            return new ObservableCollection<ArticleType>(types);
        
    }

    public  int getTypeId(string type_name)
    {
        var type =  _context.ArticleType.FirstOrDefault(x => x.Name == type_name);
        return type.ArticleTypeId;
    }

    public ArticleType getArticleType(int id)
    {
        return _context.ArticleType.Find(id);
    }
}