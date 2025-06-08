using Application.DTOs.Request.SalesData;
using Application.Enums;
using Application.Services;
using System.Collections.ObjectModel;

namespace GeneralTests.AppServices;

[TestClass]
public class FilterByRangeServiceTests
{
    private ObservableCollection<DTOSalesData> _testSalesData;

    [TestInitialize]
    public void Initialize()
    {
        _testSalesData = new ObservableCollection<DTOSalesData>
        {
            new DTOSalesData { Date = DateTime.Now.Date.AddDays(-1), TotalAmount = 100m },
            new DTOSalesData { Date = DateTime.Now.Date.AddDays(-5), TotalAmount = 200m },
            new DTOSalesData { Date = DateTime.Now.Date.AddDays(-10), TotalAmount = 300m },
            new DTOSalesData { Date = DateTime.Now.Date.AddDays(-60), TotalAmount = 400m },
            new DTOSalesData { Date = DateTime.Now.Date.AddDays(-100), TotalAmount = 500m }
        };
    }
    #region FilterByDateRange Tests

    [TestMethod]
    public void FilterByDateRange_LastSevenDays_ShouldReturnCorrectData()
    {
        // Act
        var result = FilterByRangeService.FilterByDateRange(_testSalesData, RangeFilter.LastSevenDays);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count); // Debería incluir días -1 y -5
        Assert.AreEqual(300m, result.Sum(x => x.TotalAmount)); // 100 + 200
        Assert.IsTrue(result.All(x => x.Date >= DateTime.Now.Date.AddDays(-6)));
    }

    [TestMethod]
    public void FilterByDateRange_LastMonth_ShouldReturnCorrectData()
    {
        // Act
        var result = FilterByRangeService.FilterByDateRange(_testSalesData, RangeFilter.LastMonth);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result.Count);
        Assert.AreEqual(600m, result.Sum(x => x.TotalAmount));
        Assert.IsTrue(result.All(x => x.Date >= DateTime.Now.Date.AddDays(-30)));
    }

    [TestMethod]
    public void FilterByDateRange_LastThreeMonths_ShouldReturnCorrectData()
    {
        // Act
        var result = FilterByRangeService.FilterByDateRange(_testSalesData, RangeFilter.LastThreeMonths);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(4, result.Count);
        Assert.AreEqual(1000m, result.Sum(x => x.TotalAmount));
        Assert.IsTrue(result.All(x => x.Date >= DateTime.Now.Date.AddDays(-90)));
    }

    [TestMethod]
    public void FilterByDateRange_WithEmptyCollection_ShouldReturnEmptyList()
    {
        // Arrange
        var emptyData = new ObservableCollection<DTOSalesData>();

        // Act
        var result = FilterByRangeService.FilterByDateRange(emptyData, RangeFilter.LastSevenDays);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
    }

    #endregion

    #region GetTotalFrom Tests

    [TestMethod]
    public void GetTotalFrom_LastSevenDays_ShouldReturnCorrectSum()
    {
        // Act
        var result = FilterByRangeService.GetTotalFrom(_testSalesData, RangeFilter.LastSevenDays);

        // Assert
        Assert.AreEqual(300m, result); // 100 + 200
    }

    [TestMethod]
    public void GetTotalFrom_LastMonth_ShouldReturnCorrectSum()
    {
        // Act
        var result = FilterByRangeService.GetTotalFrom(_testSalesData, RangeFilter.LastMonth);

        // Assert
        Assert.AreEqual(600m, result); // 100 + 200 + 300
    }

    [TestMethod]
    public void GetTotalFrom_LastThreeMonths_ShouldReturnCorrectSum()
    {
        // Act
        var result = FilterByRangeService.GetTotalFrom(_testSalesData, RangeFilter.LastThreeMonths);

        // Assert
        Assert.AreEqual(1000m, result); // 100 + 200 + 300 + 400
    }

    [TestMethod]
    public void GetTotalFrom_DefaultCase_ShouldReturnTotalSum()
    {
        // Act
        var result = FilterByRangeService.GetTotalFrom(_testSalesData, (RangeFilter)99); // Valor no definido en el enum

        // Assert
        Assert.AreEqual(1500m, result); // Suma de todos los valores
    }

    [TestMethod]
    public void GetTotalFrom_WithEmptyCollection_ShouldReturnZero()
    {
        // Arrange
        var emptyData = new ObservableCollection<DTOSalesData>();

        // Act
        var result = FilterByRangeService.GetTotalFrom(emptyData, RangeFilter.LastSevenDays);

        // Assert
        Assert.AreEqual(0m, result);
    }

    #endregion
}