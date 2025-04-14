using System.Collections.ObjectModel;
using ElysSalon2._0.adapters.OutBound.DataBase;
using ElysSalon2._0.aplication.Interfaces.Repositories;
using ElysSalon2._0.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElysSalon2._0.adapters.OutBound.Repositories;

public class ArticleTypeRepository : IArticleTypeRepository
{
    private readonly ElyDbContext _context;

    public ArticleTypeRepository(ElyDbContext context)
    {
        _context = context;
    }


    public async Task DeleteTypeAsync(int id)
    {
        _context.Remove(await _context.ArticleType.FindAsync(id));
        await _context.SaveChangesAsync();
    }

    public async Task AddTypeAsync(string type_name)
    {
        _context.ArticleType.Add(new ArticleType { Name = type_name });
        await _context.SaveChangesAsync();
    }


    public async Task UpdateTypeAsync(ArticleType articleType)
    {
        _context.ArticleType.Update(articleType);
        await _context.SaveChangesAsync();
    }

    public async Task<ObservableCollection<ArticleType>> GetTypesAsync()
    {
        var types = await _context.ArticleType.ToListAsync();
        return new ObservableCollection<ArticleType>(types);
    }

    public async Task<int> GetTypeIdAsync(string type_name)
    {
        var type = await _context.ArticleType.FirstOrDefaultAsync(x => x.Name == type_name);
        return type.ArticleTypeId;
    }

    public async Task<ArticleType> GetArticleTypeAsync(int id)
    {
        return await _context.ArticleType.FindAsync(id);
    }
}