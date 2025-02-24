using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;
using ElysSalon2._0.aplication;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.domain.Entities;
using Microsoft.Data.SqlClient;

namespace ElysSalon2._0.adapters.OutBound;

public class ArticleRepository : IArticleRepository
{
    private DbUtil db;
    private ElyDbContext _context;
    private readonly IArticleTypeRepository _typeRepository;

    public ArticleRepository(IArticleTypeRepository typeRepository, ElyDbContext context)
    {
        _context   = context;
        _typeRepository = typeRepository;
    }

    public ObservableCollection<DTOGetArticlesButton> GetArticlesToButton()
    {
       var articles = _context.Articles.Select(x=>
            new DTOGetArticlesButton(x.articleId,x.articleName,x.priceBuy)).ToList();

        return new ObservableCollection<DTOGetArticlesButton>(articles);
    }

    public ObservableCollection<Article> GetArticles()
    {
        ICollection<Article> articles = _context.Articles.ToList();
        return new ObservableCollection<Article>(articles);
    }


    public Article GetArticle(int id)
    {

        return _context.Articles.Find(id) ?? throw new NullReferenceException("No trajo na pa"); ;
    }


    public void AddArticle(Article article)
    {
        _context.Articles.Add(article);
    }


    public void UpdateArticle(Article article)
    {
        _context.Articles.Update(article);
    }

    public void DeleteArticle(int id)
    {
        _context.Articles.Remove(_context.Articles.Find(id)?? throw new NullReferenceException());
    }
}