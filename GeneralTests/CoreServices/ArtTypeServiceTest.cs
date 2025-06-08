using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Core.Domain.Entities;
using Core.Interfaces.Repositories;
using Core.Services;
using Moq;

namespace GeneralTests.CoreServices;

[TestClass]
public class ArtTypeServiceTest
{
    private Mock<IRepository<ArticleType>> _typeRepository;
    private ArtTypeService _typeService;

    [TestInitialize]
    public void Setup()
    {
        _typeRepository = new Mock<IRepository<ArticleType>>();
        _typeService = new ArtTypeService(_typeRepository.Object);
    }

    [TestMethod]
    public async Task AddTypeAsync_ShouldAdd_WhenNameIsValid()
    {
        var name = "NuevoTipo";

        _typeRepository.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new ObservableCollection<ArticleType>());

        _typeRepository.Setup(r => r.SaveAsync(It.IsAny<ArticleType>()));

        var result = await _typeService.AddTypeAsync(name);

        Assert.IsTrue(result.Success);

        _typeRepository.Verify(r => r.SaveAsync(It.IsAny<ArticleType>()), Times.Once);
    }

    [TestMethod]
    public async Task EditTypeAsync_ShouldUpdate_WhenNameIsDifferentAndNotDuplicate()
    {
        var originalType = new ArticleType { ArticleTypeId = 1, Name = "Bebida" };
        var updatedType = new ArticleType { ArticleTypeId = 1, Name = "Comida" };

        _typeRepository.Setup(r => r.FindAsync(It.IsAny<Expression<Func<ArticleType, bool>>>()))
            .ReturnsAsync((Expression<Func<ArticleType, bool>> expr) =>
            {
                if (expr.Compile().Invoke(originalType)) return originalType;
                return null;
            });

        _typeRepository.Setup(r => r.UpdateAsync(It.IsAny<ArticleType>()));

        var result = await _typeService.EditTypeAsync(updatedType);

        Assert.IsTrue(result.Success);
        _typeRepository.Verify(r => r.UpdateAsync(It.IsAny<ArticleType>()), Times.Once);
    }

    [TestMethod]
    public async Task EditTypeAsync_ShouldFail_WhenDuplicateNameExists()
    {
        var current = new ArticleType { ArticleTypeId = 1, Name = "Bebida" };
        var duplicate = new ArticleType { ArticleTypeId = 2, Name = "Comida" };
        var updated = new ArticleType { ArticleTypeId = 1, Name = "Comida" };

        _typeRepository.SetupSequence(r => r.FindAsync(It.IsAny<Expression<Func<ArticleType, bool>>>()))
            .ReturnsAsync(current)
            .ReturnsAsync(duplicate);

        var result = await _typeService.EditTypeAsync(updated);

        Assert.IsFalse(result.Success);
        Assert.AreEqual("Ya existe un tipo con ese nombre", result.Message);
        _typeRepository.Verify(r => r.UpdateAsync(It.IsAny<ArticleType>()), Times.Never);
    }

    [TestMethod]
    public async Task DeleteTypeAsync_ShouldDelete_WhenIdIsValid()
    {
        var type = new ArticleType { ArticleTypeId = 1, Name = "Tipo" };

        _typeRepository.Setup(r => r.FindAsync(It.IsAny<Expression<Func<ArticleType, bool>>>()))
            .ReturnsAsync(type);
        _typeRepository.Setup(r => r.DeleteAsync(It.IsAny<ArticleType>()));

        var result = await _typeService.DeleteTypeAsync(1);

        Assert.IsTrue(result.Success);
        _typeRepository.Verify(r => r.DeleteAsync(type), Times.Once);
    }

    [TestMethod]
    public async Task DeleteTypeAsync_ShouldFail_WhenIdIsZero()
    {
        var result = await _typeService.DeleteTypeAsync(0);
        Assert.IsFalse(result.Success);
        _typeRepository.Verify(r => r.DeleteAsync(It.IsAny<ArticleType>()), Times.Never);
    }

    [TestMethod]
    public async Task GetTypeAsync_ShouldReturnSuccess_WhenFound()
    {
        var type = new ArticleType { ArticleTypeId = 1, Name = "Tipo" };

        _typeRepository.Setup(r => r.FindAsync(It.IsAny<Expression<Func<ArticleType, bool>>>()))
            .ReturnsAsync(type);

        var result = await _typeService.GetTypeAsync(1);

        Assert.IsTrue(result.Success);
        Assert.AreEqual("Tipo encontrado", result.Message);
        Assert.AreEqual(type, result.Data);

        _typeRepository.Verify(r => r.FindAsync(It.IsAny<Expression<Func<ArticleType, bool>>>()), Times.Once);
    }

    [TestMethod]
    public async Task GetTypeByIdAsync_ShouldReturnError_WhenExceptionThrown()
    {
        _typeRepository.Setup(r => r.FindAsync(It.IsAny<Expression<Func<ArticleType, bool>>>()))
            .ThrowsAsync(new Exception("DB Error"));

        var result = await _typeService.GetTypeAsync(1);

        Assert.IsFalse(result.Success);
        Assert.IsTrue(result.Message.StartsWith("Tipo no encontrado, error:"));
    }

    [TestMethod]
    public async Task GetTypesAsync()
    {
        var types = new ObservableCollection<ArticleType>
        {
            new() { ArticleTypeId = 1, Name = "Bebida" },
            new() { ArticleTypeId = 2, Name = "Comida" }
        };

        _typeRepository.Setup(r => r.GetAllAsync())
            .ReturnsAsync(types);

        var result = await _typeService.GetTypesAsync();

        Assert.IsTrue(result.Success);
        Assert.IsInstanceOfType(result.Data, typeof(ObservableCollection<ArticleType>));

        _typeRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }
}