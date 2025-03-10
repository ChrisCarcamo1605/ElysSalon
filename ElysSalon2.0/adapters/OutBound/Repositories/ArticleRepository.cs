using System.Collections.ObjectModel;
using ElysSalon2._0.adapters.OutBound.DataBase;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElysSalon2._0.adapters.OutBound.Repository;

public class ArticleRepository : IArticleRepository
{
    private readonly ElyDbContext _context;
    private readonly IArticleTypeRepository _typeRepository;

    public ArticleRepository(IArticleTypeRepository typeRepository, ElyDbContext context)
    {
        _context = context;
        _typeRepository = typeRepository;
    }

    public async Task<ObservableCollection<DTOGetArticlesButton>> GetArticlesToButtonAsync()
    {
        var articles = await _context.Article.Select(x =>
            new DTOGetArticlesButton(x.ArticleId, x.Name, x.PriceBuy)).ToListAsync();

        return new ObservableCollection<DTOGetArticlesButton>(articles);
    }

    public async Task<ObservableCollection<Article>> GetArticlesAsync()
    {
        var articles = await _context.Article.ToListAsync();

        return new ObservableCollection<Article>(articles);
    }


    public async Task<Article> GetArticleAsync(int id)
    {
        return await _context.Article.FindAsync(id) ?? throw new NullReferenceException("No trajo na pa");
    }


    public async Task AddArticleAsync(Article article)
    {
        _context.Article.Add(article);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateArticleAsync(Article article)
    {
        _context.Entry(article).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteArticleAsync(int id)
    {
        _context.Article.Remove(_context.Article.Find(id) ?? throw new NullReferenceException());
        await _context.SaveChangesAsync();
    }
}