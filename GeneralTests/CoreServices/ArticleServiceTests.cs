using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Core.Domain.Entities;
using Core.Interfaces.Repositories;
using Core.Services;
using Moq;

namespace GeneralTests.CoreServices;

[TestClass]
public sealed class ArticleServiceTests
{
    private Mock<IRepository<Article>> _articleRepository;
    private ArticleService _articleService;

    [TestInitialize]
    public void SetUp()
    {
        _articleRepository = new Mock<IRepository<Article>>();
        _articleService = new ArticleService(_articleRepository.Object);
    }


    [TestMethod]
    public async Task AddArticleAsync()
    {
        var article = new Article
        {
            ArticleId = 1,
            Name = "Test Article",
            PriceBuy = 10.0m,
            PriceCost = 5.0m,
            ArticleTypeId = 1,
            Description = "This is a test article.",
            Stock = 100
        };

        _articleRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(new ObservableCollection<Article>());
        _articleRepository.Setup(repo => repo.SaveAsync(It.IsAny<Article>()));

        var reloaded = false;
        var cleared = false;
        _articleService.reloadItems += () => reloaded = true;
        _articleService.clearForms += () => cleared = true;

        var result = await _articleService.AddArticleAsync(article);


        Assert.IsTrue(result.Success, "Expected the article to be added successfully.");
        Assert.IsTrue(reloaded, "Expected reloadItems to be invoked.");
        Assert.IsTrue(cleared, "Expected clearForms to be invoked.");

        _articleRepository.Verify(repo => repo.SaveAsync(It.IsAny<Article>()), Times.Once);
    }

    [TestMethod]
    public async Task UpdateArticleAsync()
    {
        var article = new Article
        {
            ArticleId = 1,
            Name = "Updated Article",
            PriceBuy = 15.0m,
            PriceCost = 7.0m,
            ArticleTypeId = 1,
            Description = "This is an updated test article.",
            Stock = 50
        };

        _articleRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(new ObservableCollection<Article>());
        _articleRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Article, bool>>>()))
            .ReturnsAsync(article);
        _articleRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Article>()));
        var reloaded = false;
        var cleared = false;
        _articleService.reloadItems += () => reloaded = true;
        _articleService.clearForms += () => cleared = true;


        var result = await _articleService.UpdateArticleAsync(article);
        Assert.IsTrue(result.Success, "Expected the article to be updated successfully.");
        Assert.IsTrue(reloaded, "Expected reloadItems to be invoked.");
        Assert.IsTrue(cleared, "Expected clearForms to be invoked.");

        _articleRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Article>()), Times.Once);
    }

    [TestMethod]
    public async Task DeleteArticleAsync()
    {
        var articleId = 1;
        _articleRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Article, bool>>>()))
            .ReturnsAsync(new Article { ArticleId = articleId });
        _articleRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Article>()));

        var reloaded = false;
        _articleService.reloadItems += () => reloaded = true;
        var result = await _articleService.DeleteArticleAsync(articleId);
        Assert.IsTrue(result.Success, "Expected the article to be deleted successfully.");
        Assert.IsTrue(reloaded, "Expected reloadItems to be invoked.");
        _articleRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Article>()), Times.Once);
    }

    [TestMethod]
    public async Task GetArticlesToButtonsAsync()
    {
        var articles = new ObservableCollection<Article>
        {
            new() { ArticleId = 1, Name = "Article 1", PriceBuy = 10.0m, Stock = 5 },
            new() { ArticleId = 2, Name = "Article 2", PriceBuy = 20.0m, Stock = 0 },
            new() { ArticleId = 3, Name = "Article 3", PriceBuy = 30.0m, Stock = 10 }
        };
        _articleRepository.Setup(repo => repo.FindAsync(
                It.IsAny<Expression<Func<Article, bool>>>(),
                It.IsAny<Expression<Func<Article, Article>>>()))
            .ReturnsAsync(new ObservableCollection<Article>(articles.Where(a => a.Stock >= 1).ToList()));

        var result = await _articleService.GetArticlesToButtonsAsync();

        Assert.AreEqual(2, result.Count, "Expected to retrieve only articles with stock greater than or equal to 1.");
        Assert.IsTrue(result.All(a => a.Stock >= 1),
            "All retrieved articles should have stock greater than or equal to 1.");

        _articleRepository.Verify(repo => repo.FindAsync(
            It.IsAny<Expression<Func<Article, bool>>>(),
            It.IsAny<Expression<Func<Article, Article>>>()), Times.Once);
    }


    [TestMethod]
    public async Task GerArticlesAsync()
    {
        _articleRepository.Setup(repo => repo.GetAllWithIncludesAsync(X => X.ArticleType))
            .ReturnsAsync(new ObservableCollection<Article>
            {
                new()
                {
                    ArticleId = 1, Name = "Article 1", PriceBuy = 10.0m, Stock = 5, ArticleType = new ArticleType()
                },
                new()
                {
                    ArticleId = 2, Name = "Article 2", PriceBuy = 20.0m, Stock = 0, ArticleType = new ArticleType()
                },
                new()
                {
                    ArticleId = 3, Name = "Article 3", PriceBuy = 30.0m, Stock = 10, ArticleType = new ArticleType()
                }
            });

        var result = await _articleService.GetArticlesAsync();

        Assert.IsNotNull(result, "Expected to retrieve articles.");

        _articleRepository.Verify(repo => repo.GetAllWithIncludesAsync(X => X.ArticleType), Times.Once);
    }

    [TestMethod]
    public async Task GetArticleById()
    {
        var Article = new Article
        {
            ArticleId = 1,
            Name = "Test Article",
            PriceBuy = 10.0m,
            PriceCost = 13.32m,
            ArticleTypeId = 1,
            Description = "This is a test article."
        };

        _articleRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Article, bool>>>()))
            .ReturnsAsync(Article);

        var result = await _articleService.GetArticleAsync(1);

        Assert.IsTrue(result.Success, "Expected to retrieve the article successfully.");

        _articleRepository.Verify(X => X.FindAsync(It.IsAny<Expression<Func<Article, bool>>>()));
    }
}