using System.Collections.ObjectModel;
using Core.Domain.Entities;
using Core.Interfaces.Repositories;
using Core.Services;
using Moq;

namespace GeneralTests.CoreServices;

[TestClass]
public class SalesServiceTest
{
    private Mock<IRepository<Sales>> _salesRepository;
    private SalesService _salesService;

    [TestInitialize]
    public void Setup()
    {
        _salesRepository = new Mock<IRepository<Sales>>();
        _salesService = new SalesService(_salesRepository.Object);
    }

    [TestMethod]
    public async Task AddSaleAsync_ShouldAdd_WhenSaleIsValid()
    {
        var sale = new Sales
        {
            SaleId = 1,
            Total = 100.0m,
            SaleDate = DateTime.Now
        };
        _salesRepository.Setup(repo => repo.SaveAsync(It.IsAny<Sales>()));
        var result = await _salesService.AddAsync(sale);

        Assert.IsTrue(result.Success, "Expected the sale to be added successfully.");
        _salesRepository.Verify(repo => repo.SaveAsync(It.IsAny<Sales>()), Times.Once);
    }

    [TestMethod]
    public async Task GetSalesAsync_ShouldGet()
    {

        var sales = new ObservableCollection<Sales>();
        _salesRepository.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(new ObservableCollection<Sales>(sales));

        var result = await _salesService.GetAllOfAsync();

        Assert.IsTrue(result.Success, "Expected to retrieve sales successfully.");
        Assert.IsInstanceOfType(result.Data, typeof(ObservableCollection<Sales>), "Expected data to be of type ObservableCollection<Sales>.");
        _salesRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
    }


    [TestMethod]
    public async Task DeleteSaleAsync_ShouldDelete_WhenIdIsValid()
    {
        var saleId = "1";

        _salesRepository.Setup(repo => repo.GetByIdAsync(saleId))
            .ReturnsAsync(It.IsAny<Sales>());

        _salesRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Sales>()));
        var result = await _salesService.DeleteAsync(saleId);

        Assert.IsTrue(result.Success, "Expected the sale to be deleted successfully.");
        _salesRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Sales>()), Times.Once);
    }
}