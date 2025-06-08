using Application.DTOs.Request.Articles;
using Application.DTOs.Response.Articles;
using Application.Services;
using AutoMapper;
using Core.Common;
using Core.Domain.Entities;
using Core.Interfaces.Services;
using Moq;
using System.Collections.ObjectModel;

namespace GeneralTests.AppServices;

[TestClass]
public class ArticleAppServiceTests
{
    private Mock<IArticleService> _articleServiceMock;
    private Mock<IArtTypeService> _typeServiceMock;
    private Mock<IMapper> _mapperMock;
    private ArticleAppService _articleAppService;
    private bool _reloadItemsInvoked;
    private bool _clearFormsInvoked;

    [TestInitialize]
    public void Initialize()
    {
        _articleServiceMock = new Mock<IArticleService>();
        _typeServiceMock = new Mock<IArtTypeService>();
        _mapperMock = new Mock<IMapper>();

        _articleAppService = new ArticleAppService(
            _articleServiceMock.Object,
            _typeServiceMock.Object,
            _mapperMock.Object);

        _reloadItemsInvoked = false;
        _clearFormsInvoked = false;

        // Suscribirse a los eventos para verificar su invocación
        _articleAppService.ReloadItems += () => _reloadItemsInvoked = true;
        _articleAppService.ClearForms += () => _clearFormsInvoked = true;
    }

    [TestCleanup]
    public void Cleanup()
    {
        _articleAppService.Dispose();
    }

    #region GetArticlesAsync Tests

    [TestMethod]
    public async Task GetArticlesAsync_ShouldReturnMappedArticles()
    {
        // Arrange
        var articles = new ObservableCollection<Article>
        {
            new Article { ArticleId = 1, Name = "Article 1" },
            new Article { ArticleId = 2, Name = "Article 2" }
        };

        var serviceResult = ResultFromService.SuccessResult(articles);
        _articleServiceMock.Setup(x => x.GetArticlesAsync()).ReturnsAsync(serviceResult);

        // Act
        var result = await _articleAppService.GetArticlesAsync();

        // Assert
        Assert.IsTrue(result.Success);
        var returnedArticles = (ObservableCollection<DTOGetArticle>)result.Data;
        Assert.AreEqual(2, returnedArticles.Count);
        Assert.AreEqual("Article 1", returnedArticles[0].Name);
    }

    [TestMethod]
    public async Task GetArticlesAsync_WhenServiceFails_ShouldReturnFailure()
    {
        // Arrange
        var serviceResult = ResultFromService.Failed("Error getting articles");
        _articleServiceMock.Setup(x => x.GetArticlesAsync()).ReturnsAsync(serviceResult);

        // Act
        var result = await _articleAppService.GetArticlesAsync();

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Error getting articles", result.Message);
    }

    #endregion

    #region AddArticleAsync Tests

    [TestMethod]
    public async Task AddArticleAsync_ShouldCallServiceWithMappedArticle()
    {
        // Arrange
        var dto = new DTOAddArticle("Test Article", 1, "10.50", "15.99", "100", "Test Descripcion");

        var expectedResult = ResultFromService.SuccessResult();
        _articleServiceMock.Setup(x => x.AddArticleAsync(It.IsAny<Article>()))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _articleAppService.AddArticleAsync(dto);

        // Assert
        Assert.IsTrue(result.Success);
        _articleServiceMock.Verify(x => x.AddArticleAsync(It.Is<Article>(a =>
            a.Name == "Test Article" &&
            a.PriceCost == 10.50m &&
            a.PriceBuy == 15.99m &&
            a.Stock == 100
        )), Times.Once());
    }

    #endregion

    #region UpdateArticleAsync Tests

    [TestMethod]
    public async Task UpdateArticleAsync_ShouldCallServiceAndInvokeEvents()
    {
        // Arrange
        var dto = new DTOUpdateArticle(1,"Updated" ,1,12.25m,15.62m,4,"Test updated");
        var article = new Article { ArticleId = 1, Name = "Updated" };
        var expectedResult = ResultFromService.SuccessResult();

        _mapperMock.Setup(x => x.Map<Article>(dto)).Returns(article);
        _articleServiceMock.Setup(x => x.UpdateArticleAsync(article))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _articleAppService.UpdateArticleAsync(dto);

        // Assert
        Assert.IsTrue(result.Success);
        _mapperMock.Verify(x => x.Map<Article>(dto), Times.Once());
        _articleServiceMock.Verify(x => x.UpdateArticleAsync(article), Times.Once());
        Assert.IsTrue(_reloadItemsInvoked);
        Assert.IsTrue(_clearFormsInvoked);
    }

    #endregion

    #region DeleteArticleAsync Tests

    [TestMethod]
    public async Task DeleteArticleAsync_ShouldCallService()
    {
        // Arrange
        var id = 1;
        var expectedResult = ResultFromService.SuccessResult();
        _articleServiceMock.Setup(x => x.DeleteArticleAsync(id))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _articleAppService.DeleteArticleAsync(id);

        // Assert
        Assert.IsTrue(result.Success);
        _articleServiceMock.Verify(x => x.DeleteArticleAsync(id), Times.Once());
    }

    #endregion

    #region Type Management Tests

    [TestMethod]
    public async Task AddType_ShouldCallServiceAndInvokeReload()
    {
        // Arrange
        var typeName = "New Type";
        var expectedResult = ResultFromService.SuccessResult();
        _typeServiceMock.Setup(x => x.AddTypeAsync(typeName))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _articleAppService.AddType(typeName);

        // Assert
        Assert.IsTrue(result.Success);
        _typeServiceMock.Verify(x => x.AddTypeAsync(typeName), Times.Once());
        Assert.IsTrue(_reloadItemsInvoked);
    }

    [TestMethod]
    public async Task EditTypeAsync_ShouldUpdateTypeAndInvokeReload()
    {
        // Arrange
        var id = 1;
        var newName = "Updated Type";
        var existingType = new ArticleType { ArticleTypeId = id, Name = "Old Type" };

        var findResult = ResultFromService.SuccessResult(existingType);
        var updateResult = ResultFromService.SuccessResult();

        _typeServiceMock.Setup(x => x.GetTypeAsync(id)).ReturnsAsync(findResult);
        _typeServiceMock.Setup(x => x.EditTypeAsync(It.IsAny<ArticleType>()))
            .ReturnsAsync(updateResult);

        // Act
        var result = await _articleAppService.EditTypeAsync(id, newName);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(newName, existingType.Name);
        _typeServiceMock.Verify(x => x.EditTypeAsync(existingType), Times.Once());
        Assert.IsTrue(_reloadItemsInvoked);
    }

    [TestMethod]
    public async Task GetTypesAsync_ShouldReturnMappedTypes()
    {
        // Arrange
        var types = new ObservableCollection<ArticleType>
        {
            new ArticleType { ArticleTypeId = 1, Name = "Type 1" },
            new ArticleType { ArticleTypeId = 2, Name = "Type 2" }
        };

        var serviceResult = ResultFromService.SuccessResult(types);
        _typeServiceMock.Setup(x => x.GetTypesAsync()).ReturnsAsync(serviceResult);

        // Act
        var result = await _articleAppService.GetTypesAsync();

        // Assert
        Assert.IsTrue(result.Success);
        var returnedTypes = (ObservableCollection<DTOGetArtType>)result.Data;
        Assert.AreEqual(2, returnedTypes.Count);
        Assert.AreEqual("Type 1", returnedTypes[0].Name);
    }

    [TestMethod]
    public async Task DeleteTypeAsync_ShouldCallServiceAndInvokeReload()
    {
        // Arrange
        var id = 1;
        var expectedResult = ResultFromService.SuccessResult();
        _typeServiceMock.Setup(x => x.DeleteTypeAsync(id))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _articleAppService.DeleteTypeAsync(id);

        // Assert
        Assert.IsTrue(result.Success);
        _typeServiceMock.Verify(x => x.DeleteTypeAsync(id), Times.Once());
        Assert.IsTrue(_reloadItemsInvoked);
    }

    #endregion

    #region Other Methods Tests

    [TestMethod]
    public async Task GetArticlesToButtons_ShouldReturnMappedArticles()
    {
        // Arrange
        var articles = new ObservableCollection<Article>
        {
            new Article { ArticleId = 1, Name = "Button Article 1" },
            new Article { ArticleId = 2, Name = "Button Article 2" }
        };

        _articleServiceMock.Setup(x => x.GetArticlesToButtonsAsync())
            .ReturnsAsync(articles);

        // Act
        var result = await _articleAppService.GetArticlesToButtons();

        // Assert
        Assert.IsTrue(result.Success);
        var returnedArticles = (ObservableCollection<DTOGetArticlesButton>)result.Data;
        Assert.AreEqual(2, returnedArticles.Count);
        Assert.AreEqual("Button Article 1", returnedArticles[0].Name);
    }

    [TestMethod]
    public async Task GetArticleAsync_ShouldReturnMappedArticle()
    {
        // Arrange
        var article = new Article { ArticleId = 1, Name = "Single Article" };
        var serviceResult = ResultFromService.SuccessResult(article);
        _articleServiceMock.Setup(x => x.GetArticleAsync(1))
            .ReturnsAsync(serviceResult);

        // Act
        var result = await _articleAppService.GetArticleAsync(1);

        // Assert
        Assert.IsTrue(result.Success);
        var returnedArticle = (DTOGetArticle)result.Data;
        Assert.AreEqual("Single Article", returnedArticle.Name);
    }

    [TestMethod]
    public void Dispose_ShouldClearEventHandlers()
    {
        // Arrange
        bool handler1Invoked = false;
        bool handler2Invoked = false;

        _articleAppService.ClearForms += () => handler1Invoked = true;
        _articleAppService.ReloadItems += () => handler2Invoked = true;

        // Act
        _articleAppService.Dispose();
        _articleAppService.OnClearForms();
        _articleAppService.OnReloadItems();

        // Assert
        Assert.IsFalse(handler1Invoked);
        Assert.IsFalse(handler2Invoked);
    }

    #endregion
}

