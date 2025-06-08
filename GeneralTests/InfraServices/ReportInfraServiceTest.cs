using Core.Common;
using Core.Interfaces.Services;
using Infrastructure.Service;
using Moq;

namespace GeneralTests.InfraServices;

[TestClass]
public class ReportInfraServiceTest
{
    private IReportInfraService _reportInfraService;
    private Mock<IReportsService> _reportService;

    [TestInitialize]
    public void SetUp()
    {
        _reportService = new Mock<IReportsService>();
        _reportInfraService = new ReportInfraService(_reportService.Object);
    }

    [TestMethod]
    public async Task GenerateDailyReportAsync_ShouldReturnSuccessResult_WhenServiceReturnsSuccess()
    {
        // Arrange
        var path = "testPath";
        _reportService.Setup(rs => rs.GenerateDailyReport(path))
            .ReturnsAsync(ResultFromService.SuccessResult(null));

        var result = await _reportInfraService.GenerateDailyReportAsync(path);

        Assert.IsTrue(result.Success);
        _reportService.Verify(rs => rs.GenerateDailyReport(path), Times.Once);
    }


    [TestMethod]
    public async Task GenerateDailyReportAsync_ShouldReturnFailedResult_WhenServiceThrowsException()
    {
        // Arrange
        var path = "testPath";
        var exceptionMessage = "An error occurred";
        _reportService.Setup(rs => rs.GenerateDailyReport(path))
            .ThrowsAsync(new Exception(exceptionMessage));
        var result = await _reportInfraService.GenerateDailyReportAsync(path);
        Assert.IsFalse(result.Success);
        Assert.AreEqual(exceptionMessage, result.Message);
        _reportService.Verify(rs => rs.GenerateDailyReport(path), Times.Once);
    }

    [TestMethod]
    public async Task GenerateMonthlyReportAsync_ShouldReturnSuccessResult_WhenServiceReturnsSuccess()
    {
        // Arrange
        var path = "testPath";
        _reportService.Setup(rs => rs.GenerateMonthReport(path))
            .ReturnsAsync(ResultFromService.SuccessResult(null));


        var result = await _reportInfraService.GenerateMonthlyReportAsync(path);

        Assert.IsTrue(result.Success);
        _reportService.Verify(rs => rs.GenerateMonthReport(path), Times.Once);
    }

    [TestMethod]
    public async Task GenerateMonthlyReportAsync_ShouldReturnFailedResult_WhenServiceThrowsException()
    {
        // Arrange
        var path = "testPath";
        var exceptionMessage = "An error occurred";
        _reportService.Setup(rs => rs.GenerateMonthReport(path))
            .ThrowsAsync(new Exception(exceptionMessage));
        var result = await _reportInfraService.GenerateMonthlyReportAsync(path);
        Assert.IsFalse(result.Success);
        Assert.AreEqual(exceptionMessage, result.Message);
        _reportService.Verify(rs => rs.GenerateMonthReport(path), Times.Once);
    }
}