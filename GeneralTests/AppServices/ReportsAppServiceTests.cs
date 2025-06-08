using System.Collections.ObjectModel;
using Application.DTOs.Request.SalesData;
using Application.DTOs.Response.Expenses;
using Application.Interfaces;
using Application.Services;
using Application.Utils;
using Core.Common;
using Core.Domain.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Moq;

namespace GeneralTests.AppServices;

[TestClass]
public class ReportsAppServiceTests
{
    private Mock<IFilePathProvider> _fileFileDialog;
    private ReportsAppService _reportsAppService;
    private Mock<ISaleDataAppService> _saleDataAppService;


    [TestInitialize]
    public void SetUp()
    {
       _saleDataAppService = new Mock<ISaleDataAppService>();
       _fileFileDialog = new Mock<IFilePathProvider>();
        _reportsAppService = new ReportsAppService(_fileFileDialog.Object, _saleDataAppService.Object);
    }


    [TestMethod]
    public async Task GenerateReportAsync_Success()
    {
        var fromDate = new DateTime(2025, 1, 1);
        var untilDate = new DateTime(2025, 7, 31);
        var sales = new ObservableCollection<DTOSalesData>
        {
            new(new Sales { SaleDate = new DateTime(2025, 06, 15), Total = 75.25m, SaleId = 1 }),
            new(new Sales { SaleDate = new DateTime(2025, 06, 16), Total = 100.50m, SaleId = 2 }),
            new(new Sales { SaleDate = new DateTime(2025, 06, 17), Total = 50.75m, SaleId = 3 })
        };
        var expenses = new ObservableCollection<DTOSalesData>
        {
            new(new DTOGetExpense
                { Date = new DateTime(2025, 06, 15), Amount = 20.00m, ExpenseId = 1, Reason = "Office Supplies" }),
            new(new DTOGetExpense
                { Date = new DateTime(2025, 06, 16), Amount = 30.00m, ExpenseId = 2, Reason = "Utilities" }),
            new(new DTOGetExpense
                { Date = new DateTime(2025, 06, 17), Amount = 15.00m, ExpenseId = 3, Reason = "Travel" })
        };

        _fileFileDialog.Reset();
        _fileFileDialog.Setup(x => x.ShowSaveFileDialogAsync(fromDate, untilDate)).ReturnsAsync(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        
        var result = await _reportsAppService
            .GenerateReport<DTOSalesData>(fromDate, untilDate, sales, expenses, x => x.Date, x => x.TotalAmount);

        Assert.IsTrue(result.Success);

        _fileFileDialog.Verify(x => x.ShowSaveFileDialogAsync(fromDate, untilDate), Times.Once);
    }



    [TestMethod]
    public async Task GenerateReportAsync_DataOutsideDateRange_ShouldFilterOut()
    {
        // Arrange
        var fromDate = new DateTime(2025, 6, 1);
        var untilDate = new DateTime(2025, 6, 30);
        var sales = new ObservableCollection<DTOSalesData>
        {
            new(new Sales { SaleDate = new DateTime(2025, 5, 31), Total = 100m }),
            new(new Sales { SaleDate = new DateTime(2025, 6, 15), Total = 75.25m }) 
        };
        _fileFileDialog.Reset();
        _fileFileDialog.Setup(x => x.ShowSaveFileDialogAsync(fromDate, untilDate))
            .ReturnsAsync(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

        // Act
        var result = await _reportsAppService
            .GenerateReport(fromDate, untilDate, sales, new ObservableCollection<DTOSalesData>(),
                x => x.Date, x => x.TotalAmount);

        // Assert
        Assert.IsTrue(result.Success);
    }

    [TestMethod]
    public async Task GenerateReportAsync_ThrowsException_ShouldReturnFailedResult()
    {
        // Arrange
        var fromDate = new DateTime(2025, 1, 1);
        var untilDate = new DateTime(2025, 7, 31);
        var sales = new ObservableCollection<DTOSalesData>
        {
            new(new Sales { SaleDate = new DateTime(2025, 06, 15), Total = 75.25m })
        };

        _fileFileDialog.Reset();
        _fileFileDialog.Setup(x => x.ShowSaveFileDialogAsync(fromDate, untilDate))
            .ThrowsAsync(new IOException("Error de disco"));

        // Act
        var result = await _reportsAppService
            .GenerateReport(fromDate, untilDate, sales, new ObservableCollection<DTOSalesData>(),
                x => x.Date, x => x.TotalAmount);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.IsTrue(result.Message.Contains("Error al generar el reporte"));
    }

    [TestMethod]
    public async Task GenerateReportAsync_InvalidDateRange_ShouldFail()
    {
        // Arrange
        var fromDate = new DateTime(2025, 7, 31);
        var untilDate = new DateTime(2025, 1, 1);
        var sales = new ObservableCollection<DTOSalesData>();

        // Act
        var result = await _reportsAppService
            .GenerateReport(fromDate, untilDate, sales, new ObservableCollection<DTOSalesData>(),
                x => x.Date, x => x.TotalAmount);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual("La fecha 'desde' no puede ser posterior a la fecha 'hasta'.", result.Message);
    }
}